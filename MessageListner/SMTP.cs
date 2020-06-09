using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace MessageListner
{
    public class SMTP
    {
        //SMTP method to send mail
        public void SendMail(string name, string mail, string data)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(mail));

                message.To.Add(new MailboxAddress("Parking Lot", "chittevikey5@gmail.com"));

                message.Subject = "Registration";

                message.Body = new TextPart("plain")
                {
                    Text = data
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("chittevikey55@gmail.com", "vik98CH!");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}