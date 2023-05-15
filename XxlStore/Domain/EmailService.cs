using MimeKit;
using MailKit.Net.Smtp;

namespace XxlStore
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "zokrat@bk.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient()) {
                await client.ConnectAsync("smtp.mail.ru", 465, true);
                await client.AuthenticateAsync("zokrat@bk.ru", "aPufVxa5dfnxkpTGxG1k");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
