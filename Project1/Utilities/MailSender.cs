/*using System.Net.Mail;
using System.Net;
using Project1.Utilities.Interfaces;

namespace Project1.Utilities
{
    public class MailSender: IMailSender
    {
        private string _mailTo;
        private string _mailSubject;
        private string _mailBody;

        public MailSender(IEmailStructureBuilder emailStructure)
        {
            _mailBody = emailStructure.mailBody;
            _mailSubject = emailStructure.mailSubject;
            _mailTo = emailStructure.mailToAddress;
        }

        public void sendMail()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                    .SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json")
                                                    .Build();

            string? mailFromAddress = configuration.GetSection("SmtpEmailConfig:mailFromAddress").Value;
            string? smtpGoogleHost = configuration.GetSection("SmtpEmailConfig:smtpGoogleHost").Value;
            string? smtpGooglePort = configuration.GetSection("SmtpEmailConfig:smtpGooglePort").Value;
            string? credentialPassword = configuration.GetSection("SmtpEmailConfig:credentialPassword").Value;

            if(mailFromAddress != null && smtpGoogleHost != null && credentialPassword != null && int.TryParse(smtpGooglePort, out int port))
            {
                MailMessage mailMessage = new MailMessage(mailFromAddress, _mailTo, _mailSubject, _mailBody);
                SmtpClient smtpClient = new SmtpClient(smtpGoogleHost, port);
                smtpClient.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential(mailFromAddress, credentialPassword);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Send(mailMessage);
            }
        }
    }
}*/
