using DoAn1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn1.Controllers
{
    public class HomeController : Controller
    {
        DAdoanEntities3 _db = new DAdoanEntities3();
        // GET: Home
        public ActionResult Index()
        {

            return View(_db.Products.ToList());
        }
        public ActionResult Men()
        {
            ViewBag.UserName = User.Identity.Name;


            return View(_db.Products.ToList());
        }
        public ActionResult Sanpham(int id)
        {

            return View(_db.Products.Where(s => s.IDProduct == id).FirstOrDefault());
        }

        public ActionResult GetTop3()
        {
            return View(_db.Products.OrderBy(a => a.UnitPrice).Take(3).ToList());
        }
        //public ActionResult Profile()
        //{
        //    return View();
        //}


    }
}
