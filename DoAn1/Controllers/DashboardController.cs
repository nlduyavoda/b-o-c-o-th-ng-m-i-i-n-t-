using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAn1.Models;

namespace DoAn1.Controllers
{
  
    public class DashboardController : Controller
    {
        DAdoanEntities3 db = new DAdoanEntities3();

        // GET: Dashboard
        public ActionResult Index()
        {

            var orderDetails = db.OrderDetails.Include(o => o.Order).Include(o => o.Product);
            return View(orderDetails.ToList());
        }

        public ActionResult Count_ID()
        {

            var Model = db.OrderDetails.OrderByDescending(model => model.IDOrder);
            int count = Model.Count();

            return View(Model.ToList());
        }

        public ActionResult GetData()
        {
            //int ID_Product = db.OrderDetails.ToString().Count();

            //int iD_16 = db.OrderDetails.Where(x => x.IDOrder == "16").Count();
            //int iD_19 = db.OrderDetails.Where(x => x.IDOrder == "ID_19").Count();
            //int iD_22 = db.OrderDetails.Where(x => x.IDOrder == "ID_22").Count();
            //int iD_23 = db.OrderDetails.Where(x => x.IDOrder == "ID_23").Count();
            //int iD_1023 = db.OrderDetails.Where(x => x.IDOrder == "ID_1023").Count();
            //int iD_1024 = db.OrderDetails.Where(x => x.IDOrder == "ID_1024").Count();
            //int iD_1025 = db.OrderDetails.Where(x => x.IDOrder == "ID_1025").Count();
            //int iD_1026 = db.OrderDetails.Where(x => x.IDOrder == "ID_1026").Count();
            //Ratio ojb = new Ratio();
            //ojb.ID_16 = iD_16;
            //ojb.ID_19 = iD_19;
            //ojb.ID_22 = iD_22;
            //ojb.ID_23 = iD_23;
            //ojb.ID_1023 = iD_1023;
            //ojb.ID_1024 = iD_1024;
            //ojb.ID_1025 = iD_1025;
            //ojb.ID_1026 = iD_1026;

            int iD_16 = db.OrderDetails.Where(x => x.IDProduct == 16).Count();
            int iD_19 = db.OrderDetails.Where(x => x.IDProduct == 19).Count();
            int iD_22 = db.OrderDetails.Where(x => x.IDProduct == 22).Count();
            int iD_23 = db.OrderDetails.Where(x => x.IDProduct == 23).Count();
            int iD_1023 = db.OrderDetails.Where(x => x.IDProduct == 1023).Count();
            int iD_1024 = db.OrderDetails.Where(x => x.IDProduct == 1024).Count();
            int iD_1025 = db.OrderDetails.Where(x => x.IDProduct == 1025).Count();
            int iD_1026 = db.OrderDetails.Where(x => x.IDProduct == 1026).Count();
            Ratio ojb = new Ratio();
            ojb.ID_16 = iD_16;
            ojb.ID_19 = iD_19;
            ojb.ID_22 = iD_22;
            ojb.ID_23 = iD_23;
            ojb.ID_1023 = iD_1023;
            ojb.ID_1024 = iD_1024;
            ojb.ID_1025 = iD_1025;
            ojb.ID_1026 = iD_1026;


            return Json(ojb, JsonRequestBehavior.AllowGet);
        }
        public class Ratio
        {
            public int ID_16 { get; set; }
            public int ID_19 { get; set; }
            public int ID_22 { get; set; }
            public int ID_23 { get; set; }
            public int ID_1023 { get; set; }
            public int ID_1024 { get; set; }
            public int ID_1025 { get; set; }
            public int ID_1026 { get; set; }

        }
        //public ActionResult Sum_price()
        //{

        //    var Model = db.OrderDetails.OrderByDescending(model => model.Total);
        //    int Sum = Model.Sum();

        //    return View(Model.ToList());
        //}

    }
}