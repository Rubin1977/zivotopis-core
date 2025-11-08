using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using ZivotopisCore.Models.Home;

namespace ZivotopisCore.Util
{
    public class Mailer
    {
        public void OdoslanieEmalilu(OdoslanieSpravyModel model)
        {
            var mail = new MailMessage
            {
                From = new MailAddress("ruzbacky@yahoo.com", "Rastislav Ruzbacky"),
                Subject = "Vaša správa pre nás",
                IsBodyHtml = false,
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8,
                Body = $"Ďakujeme, že ste nás kontaktovali. Váš email: {model.Email}, Váš telefón: {model.Telefon}, Vaša správa: {model.Sprava}"
            };

            mail.Headers["X-Mailer"] = "developerboss.sk";
            mail.To.Add(model.Email);
            mail.Bcc.Add("rubin7y@gmail.com");

            var client = new SmtpClient("smtp.mail.yahoo.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("ruzbacky@yahoo.com", "canzahesxtgogros")
            };

            client.Send(mail);
        }

    }
}