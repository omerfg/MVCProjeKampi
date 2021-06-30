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
    public class WriterPanelMessageController : Controller
    {
        // GET: WriterPanelMessage
        ContactManager cm = new ContactManager(new EFContactDal());
        MessageValidatior messageValidator = new MessageValidatior();
        MessageManager mm = new MessageManager(new EFMessageDal());
        public ActionResult Inbox()
        {
            string p = (string)Session["WriterMail"];
            var messagelist = mm.GetListInbox(p);
            var inboxlist = messagelist.FindAll(x => x.MarkAsRead == false);
            return View(inboxlist);
        }
        public ActionResult Sendbox()
        {
            string p = (string)Session["WriterMail"];
            var messagelist = mm.GetListSendbox(p);
            var sendlist = messagelist.FindAll(x => x.isDraft == false);
            return View(sendlist);
        }
        public ActionResult ReadMessages()
        {
            var messagelist = mm.GetListReadInbox();
            var readlist = messagelist.FindAll(x => x.MarkAsRead == true);
            return View(readlist);
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
        public ActionResult Draft(string p )
        {
            var sendvalue = mm.GetListSendbox(p);
            var draftvalue = sendvalue.FindAll(x => x.isDraft == true);
            return View(draftvalue);
        }

        public ActionResult GetDraftMessageDetails(int id)
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
        public ActionResult NewMessage(Message model, string button)
        {
            ValidationResult results = new ValidationResult();
            string sender = (string)Session["WriterMail"];
            if (button == "draft")
            {

                results = messageValidator.Validate(model);
                if (results.IsValid)
                {
                    model.MessageDate = DateTime.Now;
                    model.SenderMail = sender;
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
                    model.SenderMail = sender;
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
        public PartialViewResult MessageListMenu(string p)
        {
            var contactList = cm.GetList();
            ViewBag.contactCount = contactList.Count();

            var listResult = mm.GetListSendbox(p);
            var sendList = listResult.FindAll(x => x.isDraft == false);
            ViewBag.sendCount = sendList.Count();

            var listResult2 = mm.GetListInbox(p);
            var inboxlist = listResult2.FindAll(x => x.MarkAsRead == false);
            ViewBag.inboxCount = inboxlist.Count();

            var readList = mm.GetListReadInbox();
            ViewBag.readCount = readList.Count();

            var drafList = listResult.FindAll(x => x.isDraft == true);
            ViewBag.draftCount = drafList.Count();
            return PartialView();
        }
    }
}