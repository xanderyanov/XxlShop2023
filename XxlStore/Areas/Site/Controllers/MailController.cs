using Microsoft.AspNetCore.Mvc;

namespace XxlStore.Areas.Site.Controllers
{
    [Area("Site")]
    public class MailController : XxlController
    {
        public IActionResult Index()
        {
           
            return View();
        }

        public async Task<IActionResult> SendMessage()
        {
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync("zokrat@yandex.ru", "Тема письма", "Тест письма: тест!");
            return RedirectToAction("Index");
        }
    }
}
