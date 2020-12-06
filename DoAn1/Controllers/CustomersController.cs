using DoAn1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn1.Controllers
{
    public class CustomersController : Controller
    {
        DAdoanEntities3 _db = new DAdoanEntities3();
        // GET: Customers
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Customer cus)
        {
            _db.Customers.Add(cus);
            _db.SaveChanges();
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
    }
}