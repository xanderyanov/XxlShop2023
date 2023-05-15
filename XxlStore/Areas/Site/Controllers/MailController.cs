using Microsoft.AspNetCore.Mvc;
using XxlStore.Models;

namespace XxlStore.Areas.Site.Controllers
{
    [Area("Site")]
    public class MailController : XxlController
    {
        public IActionResult Index()
        {
           return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(Message message)
        {

            if (ModelState.IsValid) {
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(message.Email, message.Subject, message.Text);
                return RedirectToAction("Success");
            } else {
                return View("Index");
            }
        }
    }
}
