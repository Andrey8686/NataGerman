using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebSite.Utils
{
    public static class MessageUtil
    {
        //public static async Task SendEmail(string email, string subject, string message)
        //{
        //    try
        //    {
        //        const string @from = "natagerman777@gmail.com";
        //        const string pass = "natagerman123";

        //        var client = new SmtpClient("smtp.gmail.com", 465)
        //        {
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            UseDefaultCredentials = false,
        //            Credentials = new System.Net.NetworkCredential(from, pass),
        //            EnableSsl = true
        //        };

        //        var mail = new MailMessage(from, email)
        //        {
        //            Subject = subject,
        //            Body = message,
        //            IsBodyHtml = true
        //        };

        //        await client.SendMailAsync(mail);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}

        public static async Task SendEmailAsync(string email, string name, string subject, string message)
        {
            try
            {
                const string fromEmail = "noreply@natagerman.ru";
                const string pass = "4fl2rY6_";

                var client = new SmtpClient("natagerman.ru", 25)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromEmail, pass),
                    EnableSsl = false
                };

                await client.SendMailAsync(new MailMessage(fromEmail, email)
                {
                    From = new MailAddress(fromEmail, name),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void SendEmail(string email, string subject, string message)
        {
            var t = new Task(() =>
            {
                try
                {
                    const string from = "noreply@natagerman.ru";
                    const string pass = "4fl2rY6_";

                    var client = new SmtpClient("natagerman.ru", 25)
                    {
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential(from, pass),
                        EnableSsl = false
                    };

                    client.Send(new MailMessage(from, email)
                    {
                        Subject = subject,
                        Body = message,
                        IsBodyHtml = true
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });

            t.Start();
        }
    }
}