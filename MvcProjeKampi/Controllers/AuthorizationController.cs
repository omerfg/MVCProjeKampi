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
    public class AuthorizationController : Controller
    {
        // GET: Authorization
        AdminManager am = new AdminManager(new EFAdminDal());
        RoleManager rm = new RoleManager(new EFRoleDal());
        public ActionResult Index()
        {
            var adminvalues = am.GetList();
            return View(adminvalues);
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(Admin p)
        {
            am.AdminAdd(p);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            List<SelectListItem> valueadminrole = (from c in rm.GetRoles()
                                                   select new SelectListItem
                                                   {
                                                       Text = c.RoleName,
                                                       Value = c.RoleId.ToString()

                                                   }).ToList();

            ViewBag.valueadmin = valueadminrole;
            var adminvalue = am.GetByID(id);
            return View(adminvalue);
            //var adminvalue = am.GetByID(id);
            //return View(adminvalue);
        }

        [HttpPost]
        public ActionResult EditAdmin(Admin p)
        {
            //p.AdminStatus = true;
            am.AdminUpdate(p);
            return RedirectToAction("Index");
            //am.AdminUpdate(p);
            //return RedirectToAction("Index");
        }
        public ActionResult DeleteAdmin(int id)
        {
            var result = am.GetByID(id);
            if (result.AdminStatus == true)
            {
                result.AdminStatus = false;
            }
            else
            {
                result.AdminStatus = true;
            }
            am.AdminUpdate(result);
            return RedirectToAction("Index");
        }
    }
}