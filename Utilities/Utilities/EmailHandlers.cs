using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace utilities
{
    public class EmailHandlers : NLog.Logger
    {
        static string to = "clintonbartley@empathhealth.org";
        static string from = "clintonbartley@empathhealth.org";
        static string subject = "Testing email logger";
        static string body = "body";
        MailMessage message = new MailMessage(from, to, subject, body);
        SmtpClient smtpClient = new SmtpClient("postfix-smtp.thehospice.net");
        
        public void sendemail()
        {
            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex )
            {
                Console.WriteLine(ex.ToString());
            }

        }

        

    }


}
