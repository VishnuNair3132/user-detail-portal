using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Models
{
    public class EmailConfiguration
    {

        public string From { get; set; }

        public string SmtpServer { get; set; }

        public int Port { set; get; }

        public string Username { get; set; }

        public string Password { get; set; }

    }
}
