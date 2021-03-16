using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailHandle
{
    public class EmailSender
    {

        public void SendMail(string to, string subject, string body)
        {
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient(Settings.Default.SmtpServer);

                mail.From = new MailAddress(Settings.Default.From);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;

                smtpServer.Port = Settings.Default.Port;
                smtpServer.Credentials = new System.Net.NetworkCredential(Settings.Default.From, Settings.Default.Password);
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                Console.WriteLine("Se envio el correo");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }


    }
}
