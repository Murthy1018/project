using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Project1.Models;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost("sendemail")]
        public IActionResult SendEmail([FromBody] User emailRequest)
        {
            // Configure your SMTP settings (e.g., SMTP server, port, credentials)
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("onlinejobsearch5@gmail.com", "kqhu zhmg zcso nrwd"),
                EnableSsl = true,
            };

            // Create the email message
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("onlinejobsearch5@gmail.com"),
                Subject = "Registration Successful!",
                Body = " Hello!!\n\n You are successfully registered with us.\n\n We are thrilled to welcome you to JobFinder, your gateway to a world of career opportunities and professional growth.\n If you ever need assistance or have questions about our platform, our dedicated support team is here to help. Simply reach out to us at onlinejobsearch5@gmail.com.\n\n Thank you for choosing us as your partner in finding the perfect job.\n Happy Job Hunt! \n\n Regards, \n JobSearch Team.",
            };

            mailMessage.To.Add(emailRequest.Email);

            try
            {
                smtpClient.Send(mailMessage);
                return Ok(new { message = "Email sent successfully" });

            }
            catch (Exception ex)
            {
                return BadRequest($"Email sending failed: {ex.Message}");
            }
        }
    }
}


