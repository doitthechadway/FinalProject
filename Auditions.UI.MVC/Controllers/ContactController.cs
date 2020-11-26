using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using Auditions.UI.MVC.Models;

namespace Auditions.UI.MVC.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }

        //POST: Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }


            string emailBody = $"You have received an email from {cvm.Name} with a subject {cvm.Subject}. Please respond to {cvm.Email} with your response to the following message: <br /> <br />{cvm.Message}";

            MailMessage msg = new MailMessage("no-reply@chadway.net", "wayct@outlook.com", "Email from www.chadway.net", emailBody);

            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient("mail.chadway.net");
            client.Credentials = new NetworkCredential("no-reply@chadway.net", "0nmyb3half@");

            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = $"Sorry, something went wrong. Error message: {ex.Message}<br />{ex.StackTrace}";
                return View(cvm);
            }

            return View("EmailConfirmation", cvm);
        }
    }
}