using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Models
{
    public class Message
    {

        public List<MailboxAddress> To { set; get; }

        public string Subject { get; set; }
        public string Content { get; set; }



        public Message(IEnumerable<string> to,string Content,string Subject)
        {

            this.To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("email",x)));
            this.Content=Content;
            this.Subject = Subject;
            
        }

    }
}
