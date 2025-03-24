using MailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Service
{
    public  interface IEmailService
    {
        void sendEmail(Message message);
    }
}
