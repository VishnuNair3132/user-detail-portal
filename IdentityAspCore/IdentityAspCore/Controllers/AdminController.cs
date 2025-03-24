using IdentityAspCore.DTOs;
using IdentityAspCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityAspCore.Controllers
{



    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[Controller]")]
    public class AdminController:ControllerBase
    {



        public readonly UserManager<ApplicationUser> _userManager;


        public readonly RoleManager<IdentityRole> _roleManager;


        public AdminController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }




        [HttpGet("getAllUsers")]
        public async Task<IActionResult> getAllUser()
        {
            List<ApplicationUser> UserList = await _userManager.Users.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<List<ApplicationUser>>(true, "User List", UserList));
        }





    }
}
