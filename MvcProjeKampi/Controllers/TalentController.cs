using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class TalentController : Controller
    {
        // GET: Talent
        TalentManager tm = new TalentManager(new EFTalentDal());
        public ActionResult Index()
        {
            var result = tm.GetTalents();
            return View(result);
        }
    }
     
}