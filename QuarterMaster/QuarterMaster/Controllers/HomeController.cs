using Microsoft.AspNet.Identity;
using QuarterMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace QuarterMaster.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {
            var HomeInfo = context.homePages.Select(h => h).FirstOrDefault();
            ViewBag.Picture1 = HomeInfo.SliderPic1;
            ViewBag.Picture2 = HomeInfo.SliderPic2;
            ViewBag.Picture3 = HomeInfo.SliderPic3;

            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(string message)
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Contact([Bind(Include = "Id,Subject,Message")] Email emailEntered)
        {
            ApplicationUser user = context.Users.Find(User.Identity.GetUserId());
            Email email = new Email { Id = emailEntered.Id, Subject = emailEntered.Subject, Message = emailEntered.Message };
            
            email.RecipientEmail = "QuarterMasterManager@gmail.com";
            email.SenderEmail = "QuarterMasterUser@gmail.com";
            email.SenderPassword = "poiuyt1!";

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            try
            {
                mail.From = new MailAddress(email.SenderEmail);
                mail.To.Add(email.RecipientEmail);
                mail.Subject = email.Subject;
                mail.Body = email.Message + "\n\nReply to: " + user.Email;
            }
            catch
            {
                RedirectToAction("Index");
            }
            //mail.From = new MailAddress(email.SenderEmail);
            //mail.To.Add(email.RecipientEmail);
            //mail.Subject = email.Subject;
            //mail.Body = email.Message + "\n\nReply to: " + user.Email;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(email.SenderEmail, email.SenderPassword);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

            return View("ThankYou");
        }
        public ActionResult ThankYou()
        {
            return View();
        }
    }
}