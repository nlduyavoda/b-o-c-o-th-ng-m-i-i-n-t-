using DoAn1.Models;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DoAn1.Controllers
{
   
    public class LoginController : Controller
    {
        DAdoanEntities3 _db = new DAdoanEntities3();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        //public void AddUserRole(User userRole)
        //{
        //    var roleEntry = _db.Users.Where(r => r.Roles == userRole.Roles);
        //    if (roleEntry != null)
        //    {
        //        _db.Users.Remove(userRole);
        //        _db.SaveChanges();
        //    }
        //    _db.Users.Add(userRole);
        //    _db.SaveChanges();
        //}


        //private User SetupFormsAuthTicket(string email, bool persistanceFlag)
        //{

        //    User user;
        //    user = _db.Users.Where(r => r.Email == user.Email);

        //    var iduser = email.IDUser;
        //    var userData = iduser.ToString(CultureInfo.InvariantCulture);
        //    var authTicket = new FormsAuthenticationTicket(1, //version
        //                        email, // user name
        //                        DateTime.Now,             //creation
        //                        DateTime.Now.AddMinutes(30), //Expiration
        //                        persistanceFlag, //Persistent
        //                        userData);

        //    var encTicket = FormsAuthentication.Encrypt(authTicket);
        //    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        //    return user;
        //}


        //method Login
        [HttpPost]
        public ActionResult Authen(User _user)
        {
            var check = _db.Users.Where(s => s.Email.Equals(_user.Email) 
            && s.Password.Equals(_user.Password)).FirstOrDefault();
            if (check == null)
            {
                _user.LoginErrorMessage = "Error Email or Password! Try again please!";
                return View("Index", _user);

            }
            else // login eccessfull
            {
                var test = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (test.Email != "admin@gmail.com")//khong phai admin
                {
                    
                    System.Web.HttpContext.Current.Session["IDUser"] = _user.IDUser;
                    System.Web.HttpContext.Current.Session["Email"] = _user.Email;
                    ViewBag.Name = test.Email;
                    return RedirectToAction("Index", "Home");
                }
                else // neu la admin
                {
                    return RedirectToAction("Index", "Product");
                }

            }

        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        //method xử lý model
        [HttpPost]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);

                    _db.SaveChanges();
                    // nếu mà đk thành công thì chuyển hướng trang register sang login
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email alredy exists! Use another email please!";
                    return View();
                }
            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");//mehthod login là Index
        }
    }
}