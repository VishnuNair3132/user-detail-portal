using MailService.Models;
using MimeKit;
using MailKit.Net.Smtp;


namespace MailService.Service
{
    public class EmailService : IEmailService
    {



        public readonly EmailConfiguration _emailConfiguration;



        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }






        public void sendEmail(Message message)
        {
            var emailmessage = createEmailMessage(message);
            Send(emailmessage);

        }



        public MimeMessage createEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };


            return emailMessage;

        }



        public void Send(MimeMessage message)
        {
            using var mailService = new SmtpClient();
            try
            {

                mailService.Connect(_emailConfiguration.SmtpServer,_emailConfiguration.Port,true);
                mailService.AuthenticationMechanisms.Remove("XOAUTH2");
                mailService.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);

                mailService.Send(message);


            }catch(Exception e)
            {


                throw new Exception("Error While Sending the Email: "+e);
            }
            finally
            {
                mailService.Disconnect(true);
                mailService.Dispose();
            }



        }
    }
}
