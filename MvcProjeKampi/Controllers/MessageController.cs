using BusinessLayer.Concrate;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        MessageValidatior messageValidator = new MessageValidatior();
        MessageManager mm = new MessageManager(new EFMessageDal());
        Context c = new Context();
        [Authorize]
        public ActionResult Inbox()
        {
            var messagelist = mm.GetListInbox();
            var inboxlist = messagelist.FindAll(x => x.MarkAsRead == false);
            return View(inboxlist);
        }

        public ActionResult ReadMessages()
        {
            var messagelist = mm.GetListReadInbox();
            var readlist = messagelist.FindAll(x => x.MarkAsRead == true);
            return View(readlist);
        }

        public ActionResult Sendbox()
        {
            var messagelist = mm.GetListSendbox();
            var sendlist = messagelist.FindAll(x => x.isDraft == false);
            return View(sendlist);
        }

        public ActionResult GetInBoxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message model,string button)
        {
            ValidationResult results = new ValidationResult();
            if (button == "draft")
            {

                results = messageValidator.Validate(model);
                if (results.IsValid)
                {
                    model.MessageDate = DateTime.Now;
                    model.SenderMail = "admin@gmail.com";
                    model.isDraft = true;
                    mm.MessageAdd(model);
                    return RedirectToAction("Draft");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            else if (button == "save")
            {
                results = messageValidator.Validate(model);
                if (results.IsValid)
                {
                    model.MessageDate = DateTime.Now;
                    model.SenderMail = "admin@gmail.com";
                    model.isDraft = false;
                    mm.MessageAdd(model);
                    return RedirectToAction("SendBox");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            return View();
            //if (result.IsValid)
            //{
            //    p.MessageDate =DateTime.Parse(DateTime.Now.ToShortDateString());
            //    mm.MessageAdd(p);
            //    return RedirectToAction("Sendbox");
            //}
            //else
            //{
            //    foreach (var item in result.Errors)
            //    {
            //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //    }
            //}

        }

        public ActionResult MarkAsRead(int id)
        {
            var values = mm.GetByID(id);
            values.MarkAsRead = true;
            mm.MessageMarkIsRead(values);
            return RedirectToAction("Inbox");
        }
        public ActionResult MarkAsUnRead(int id)
        {
            var values = mm.GetByID(id);
            values.MarkAsRead = false;
            mm.MessageMarkIsRead(values);
            return RedirectToAction("ReadMessages");
        }

        public ActionResult Draft()
        {
            var sendvalue = mm.GetListSendbox();
            var draftvalue = sendvalue.FindAll(x => x.isDraft == true);
            return View(draftvalue);
        }

        public ActionResult GetDraftMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }

    }
}