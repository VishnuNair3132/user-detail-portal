using IdentityAspCore.DTOs;
using IdentityAspCore.Models;
using MailService.Models;
using MailService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace IdentityAspCore.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {

        public readonly UserManager<ApplicationUser> _userManager;


        public readonly RoleManager<IdentityRole> _roleManager;


        public readonly IEmailService _emailServie;


        public readonly IConfiguration _configuration;
        public UserController(UserManager<ApplicationUser> userManage, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManage;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailServie = emailService;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {


            var foundUser = await _userManager.FindByEmailAsync(registerUser.Email);

            if (foundUser != null)
            {

                return StatusCode(StatusCodes.Status403Forbidden, new ApiResponse<ApplicationUser>(false, "User ALready exists"));

            }

            //if not assigned it will be assigned
            registerUser.Role ??= "User";

            ApplicationUser user = new ApplicationUser
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.Username,
                Firstname = registerUser.Firstname,
                Lastname = registerUser.Lastname,
                Role = registerUser.Role
            };


            if (await _roleManager.RoleExistsAsync(registerUser.Role))
            {
                var result = await _userManager.CreateAsync(user, registerUser.Password);

                Console.WriteLine(result.Errors);

                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        new ApiResponse<ApplicationUser>(false, "Ensure That the Password has 1 uppercase 1 SpecialCharacter and minimum Length of 8 and make the username unique"));
                }

                await _userManager.AddToRoleAsync(user, registerUser.Role);



                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var content = Url.Action(nameof(ConfirmEmail), "User", new { token, email = user.Email }, Request.Scheme);



                Message message = new Message(new string[] { user.Email! }, content, "Email Confirmation");
                _emailServie.sendEmail(message);




                return StatusCode(StatusCodes.Status201Created,
                    new ApiResponse<ApplicationUser>(true, $"User successfully created & Email Send To {user.Email} Please Confirm the email"));
            }

            return StatusCode(StatusCodes.Status400BadRequest,
                new ApiResponse<ApplicationUser>(false, "Invalid role specified."));
        }



        //[HttpGet]

        //public IActionResult testMail()
        //{

        //    Message message = new Message(new String[] { "210303126228@paruluniversity.ac.in" }, "Test", "testing");

        //    _emailServie.sendEmail(message);


        //    return StatusCode(StatusCodes.Status200OK,
        //            new ApiResponse<User>(true, "Email Send Successfully"));
        //}



        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {

            var foundUser = await _userManager.FindByEmailAsync(email);

            if (foundUser != null)
            {
                IdentityResult Result = await _userManager.ConfirmEmailAsync(foundUser, token);

                if (Result.Succeeded)
                {

                    return StatusCode(StatusCodes.Status200OK,
                        new ApiResponse<ApplicationUser>(true, "Email Confirmed Succesfully"));
                }
            }


            return StatusCode(StatusCodes.Status500InternalServerError,
                new ApiResponse<ApplicationUser>(false, "Error While Sending the Confirmation Email"));
        }




        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginUser loginRequest)
        {
            ApplicationUser foundUser = await _userManager.FindByNameAsync(loginRequest.Username!);

            Console.WriteLine(foundUser);

            if (foundUser != null && await _userManager.CheckPasswordAsync(foundUser, loginRequest.Password))
            {

                string givenName = foundUser.Firstname + " " + foundUser.Lastname;
                var userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,foundUser.UserName !),
                    new Claim(ClaimTypes.Email,foundUser.Email !),
                    new Claim(ClaimTypes.GivenName,givenName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                var roles = await _userManager.GetRolesAsync(foundUser);

                Console.WriteLine(roles);

                foreach (var role in roles)
                {
                    userClaims.Add(new Claim(ClaimTypes.Role, role));
                }


                var token = tokenGenerator(userClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }



        [Authorize(Roles = "Admin")]
        [HttpGet("getUserByEmail/{email}")]
        public async Task<IActionResult> getUserByEmail([FromRoute] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<ApplicationUser>(true, "User Found", user));
            }
            return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<ApplicationUser>(false, "User not Found"));
        }





        [HttpGet("forgetPassword")]
        public async Task<IActionResult> forgetPassword([Required] string email)
        {
            var User = await _userManager.FindByEmailAsync(email);

            if (User != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(User);

                var content = Url.Action(nameof(ResetPassword), "User", new { token, email = User.Email }, Request.Scheme);
                Message message = new Message(new string[] { email! }, content!, "Reset-Password Email");
                _emailServie.sendEmail(message);
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<string>(true, $"Reset Password Email Successfully to {email}"));
            }
            return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<string>(false, "Email could not be found"));
        }




        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };

            return Ok(new
            {
                model
            });
        }


        [HttpPost("updatePassword")]
        public async Task<IActionResult> updatePassword([FromBody] UpdatePassword updatePassword)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var Result = await _userManager.ChangePasswordAsync(user, updatePassword.oldPassword, updatePassword.newPassword);

                    if (Result.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status202Accepted, new ApiResponse<ApplicationUser>(true, "Password Updated Successfully"));
                    }
                    return StatusCode(StatusCodes.Status406NotAcceptable, new ApiResponse<ApplicationUser>(false, "Incorrect Old Password"));
                }
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<ApplicationUser>(false, "Cant Find the User"));
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }




        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<string>(false, "User not found."));
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(StatusCodes.Status202Accepted, new ApiResponse<string>(true, "Password Reset Successfully."));
        }




        //[Authorize(Roles = "User,Admin")]
        [HttpDelete("deleteUser/{email}")]
        public async Task<IActionResult> deleteUser([FromRoute(Name = "email")] string email)
        {

            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (role != null)
            {
                if (role.Equals("User"))
                {
                    string? tokenUsername = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                    if (tokenUsername != null)
                    {

                        ApplicationUser? foundUser = await _userManager.FindByEmailAsync(email);
                        if (foundUser != null && foundUser.UserName == tokenUsername)
                        {
                            IdentityResult Result = await _userManager.DeleteAsync(foundUser);
                            if (Result.Succeeded)
                            {
                                return StatusCode(StatusCodes.Status200OK, new ApiResponse<ApplicationUser>(true, $"User With the Given Email {email} is Successfully Deleted"));
                            }
                            return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ApplicationUser>(false, $"Internal Server Error"));
                        }
                        return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<ApplicationUser>(false, $"Can Not Find the User With the Given email {email}"));
                    }
                }
                else if (role.Equals("Admin"))
                {
                    ApplicationUser? foundUser = await _userManager.FindByEmailAsync(email);
                    if (foundUser != null)
                    {
                        IdentityResult Result = await _userManager.DeleteAsync(foundUser);
                        if (Result.Succeeded)
                        {
                            return StatusCode(StatusCodes.Status200OK, new ApiResponse<ApplicationUser>(true, $"User With the Given Email {email} is Successfully Deleted"));
                        }
                        return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ApplicationUser>(false, $"Internal Server Error"));
                    }
                    return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<ApplicationUser>(false, $"Can Not Find the User With the Given email {email}"));
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ApplicationUser>(false, $"Invalid Request"));
            }
            return StatusCode(StatusCodes.Status403Forbidden, new ApiResponse<ApplicationUser>(false, $"Unauthorize"));
        }


        [Authorize(Roles = "User,Admin")]
        [HttpPost("updateUser/{email}")]
        public async Task<IActionResult> UpdateUser([FromRoute] String email, [FromBody] UpdateRequest updateRequest)
        {

            string? ClaimEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            string? ClaimUser = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;


            string? Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;


            if (Role.Equals("User"))
            {
                ApplicationUser? foundUser = await _userManager.FindByEmailAsync(email);

                if (foundUser != null)
                {
                    if (ClaimEmail == email && foundUser.UserName == ClaimUser)
                    {

                        var user = await _userManager.FindByEmailAsync(email);



                        user.Lastname = updateRequest.Lastname ?? user.Lastname;
                        user.Firstname = updateRequest.Firstname ?? user.Firstname;


                        IdentityResult Result = await _userManager.UpdateAsync(user);



                        if (Result.Succeeded)
                        {
                            return StatusCode(StatusCodes.Status200OK, new ApiResponse<ApplicationUser>(true, "User Updated Successfully"));
                        }

                        return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ApplicationUser>(false, "Cant Update the User Internal Server Issue"));
                    }

                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ApplicationUser>(false, "Invalid Jwt Token"));

                }

                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<ApplicationUser>(false, $"Cant Find the User with This Email {email}"));


            }
            else if (Role.Equals("Admin"))
            {
                ApplicationUser? foundUser = await _userManager.FindByEmailAsync(email);

                if (foundUser != null)
                {


                    var user = await _userManager.FindByEmailAsync(email);
                    user.Lastname = updateRequest.Lastname ?? user.Lastname;
                    user.Firstname = updateRequest.Firstname ?? user.Firstname;
                    IdentityResult Result = await _userManager.UpdateAsync(user);
                    if (Result.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status200OK, new ApiResponse<ApplicationUser>(true, "User Updated Successfully"));
                    }
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ApplicationUser>(false, "Cant Update the User Internal Server Issue"));
                }

                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<ApplicationUser>(false, "Invalid Jwt Token"));

            }

            return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<ApplicationUser>(false, $"Cant Find the User with This Email {email}"));

        }













        private  JwtSecurityToken tokenGenerator(List<Claim> claimsList)
        {

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Sceret"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:validIssuer"],
                audience: _configuration["Jwt:validAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: claimsList,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));


            return token;
        }



    }
}
