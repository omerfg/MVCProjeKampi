using BusinessLayer.Concrate;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    
    public class IstatistikController : Controller
    {
        // GET: Istatistik

        Context c = new Context();
        public ActionResult Index()
        {
            /*Sorgu 1 */
            var result = c.Categories.Count().ToString();
            ViewBag.sorgu1 = result;

            /*Sorgu 2*/
            var result2 = c.Headings.Count(x => x.HeadingName == "Yazılım");
            ViewBag.sorgu2 = result2;

            /*Sorgu 3*/
            var result3 = (from x in c.Writers select x.WriterName.IndexOf("a")).Distinct().Count().ToString();
            ViewBag.sorgu3 = result3;
            /*Hocam bu kısmı yardım alarak buldum ama mantığını anladım.Şu şekilde;
             from x in c.Writers : Writers tablosunun içinde
             select x.WriterName.IndexOf("a") :WriterName Alanında içinde "a" harfi geçen verinin index numarasını geriye döndür
             Distinct : Bunun kullanım amacıda tekrara düşen verileri bidaha saymamak.
            */

            /*Sorgu 4*/
            var result4 = c.Headings.Max(x => x.Category.CategoryName);
            ViewBag.sorgu4 = result4;

            /*Sorgu 5*/
            var result5 = c.Categories.Count(x => x.CategoryStatus == true) - c.Categories.Count(x => x.CategoryStatus == false);
            ViewBag.sorgu5 = result5;

            return View();
        }
    }
}