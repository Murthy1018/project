/*using Project1.Utilities.Interfaces;

namespace Project1.Utilities.Implementation
{
    public class EmailStructureBuilder : IEmailStructureBuilder
    {
        private string _mailBody;
        private string _mailSubject;

        private IConfiguration _configuration { get; }


        public EmailStructureBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string mailToAddress { get; set; }
        public string mailSubject { get { return _mailSubject; } }
        public string mailBody { get { return _mailBody; } }

        public void SetMailBody(string userName, MailType mailPurpose)
        {
            _mailBody = getMailBody(userName, mailPurpose);
        }

        private string getMailBody(string userName, MailType mailPurpose)
        {
            string greeting = "Hi " + userName + ", \n\n";
            string regards = "Regards, \n" + "JobSearch Team.";

            string mailConfigContentConfigPath = "SmtpEmailConfig:mailContent:";

            switch (mailPurpose)
            {
                case MailType.Activation:
                    {
                        _mailSubject = _configuration.GetSection($"{mailConfigContentConfigPath}activationSubject").Value;
                        return greeting +
                                "You are successfully registered with us. Happy Job Hunt! \n\n" +
                                regards;
                    }
                case MailType.PasswordReset:
                    {
                        var resetPassword = Guid.NewGuid().ToString();
                        _mailSubject = _configuration.GetSection($"{mailConfigContentConfigPath}passwordResetSubject").Value;
                        return greeting +
                                "Your password is been successfully reset. Your new password is " + resetPassword + ". Please reset it with your password." + " \n\n" +
                                regards;
                    }
                case MailType.NewJobPosting:
                    {
                        _mailSubject = _configuration.GetSection($"{mailConfigContentConfigPath}newJobAlert").Value;
                        return greeting +
                                "New jobs are posted as posted. Please check." + "\n\n" +
                                regards;
                    }
                default:
                    {
                        _mailSubject = "";
                        return "";
                    }
            }
        }
    }
}*/
