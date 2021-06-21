using BusinessLayer.Concrate;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        ContactManager cm = new ContactManager(new EFContactDal());
        MessageManager mm = new MessageManager(new EFMessageDal());
        ContactValidatior cv = new ContactValidatior();
        [Authorize]
        public ActionResult Index()
        {
            var contactvalues = cm.GetList();
            return View(contactvalues);
        }
        public ActionResult GetContactDetails(int id)
        {
            var contactvalues = cm.GetByID(id);
            return View(contactvalues);
        }

        public PartialViewResult ContactPartial()
        {
            //var messageinboxcount = c.Messages.Count(x => x.ReceiverMail == "admin@gmail.com" && x.MarkAsRead == false && x.isDraft == false).ToString();
            //var messagesendboxcount = c.Messages.Count(x => x.SenderMail == "admin@gmail.com" && x.MarkAsRead == false && x.isDraft == false).ToString();
            //var readmessage = c.Messages.Count(x => x.ReceiverMail == "admin@gmail.com" && x.MarkAsRead == true && x.isDraft == false).ToString();
            //var draftmessage = 
            //var contactmessagecount = c.Contacts.Count().ToString();
            //ViewBag.rm = readmessage;
            //ViewBag.mic = messageinboxcount;
            //ViewBag.msc = messagesendboxcount;
            //ViewBag.cmc = contactmessagecount;
            //return PartialView();

            var contactList = cm.GetList();
            ViewBag.contactCount = contactList.Count();

            var listResult = mm.GetListSendbox();
            var sendList = listResult.FindAll(x => x.isDraft == false);
            ViewBag.sendCount = sendList.Count();

            var listResult2 = mm.GetListInbox();
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