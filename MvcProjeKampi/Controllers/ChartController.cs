using DataAccessLayer.Concrete;
using MvcProjeKampi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CategoryChart()
        {
            return Json(BlogList(),JsonRequestBehavior.AllowGet);
        }
        public List<CategoryClass> BlogList()
        {
            List<CategoryClass> ct = new List<CategoryClass>();
            ct.Add(new CategoryClass()
            {
                CategoryName = "Yazılım",
                CategoryCount = 8
            }); 
            ct.Add(new CategoryClass()
            {
                CategoryName = "Seyehat",
                CategoryCount = 4
            });
            ct.Add(new CategoryClass()
            {
                CategoryName = "Teknoloji",
                CategoryCount = 7
            });
            ct.Add(new CategoryClass()
            {
                CategoryName = "Spor",
                CategoryCount = 1
            });
            return ct;
        }
        public ActionResult CategoryPieChart()
        {
            return View();
        }
        public ActionResult CategoryColumnChart()
        {
            return View();
        }
        public ActionResult CategoryLineChart()
        {
            return View();
        }
        public List<CategoryClass> CategoryListChart()
        {
            List<CategoryClass> categoryClasses = new List<CategoryClass>();
            using (var _context = new Context())
            {
                categoryClasses = _context.Categories.Select(x => new CategoryClass
                {
                    CategoryName = x.CategoryName,
                    CategoryCount = x.CategoryName.Length
                }).ToList();
            }

            return categoryClasses;
        }

        public ActionResult VisualizeByCategory()
        {
            return Json(CategoryListChart(), JsonRequestBehavior.AllowGet);
        }
    }
}