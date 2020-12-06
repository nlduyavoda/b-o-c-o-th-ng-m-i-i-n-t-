using DoAn1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Commonnn;
using System.Net;

namespace DoAn1.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart

        DAdoanEntities3 _db = new DAdoanEntities3();
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddtoCart(int id)
        {
            var pro = _db.Products.SingleOrDefault(s => s.IDProduct == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("ShowToCart", "ShoppingCart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["ID_Product"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity_Shopping(id_pro, quantity);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int _t_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                _t_item = cart.Total_Quantity();
            }
            ViewBag.infoCart = _t_item;
            return PartialView("BagCart");
        }
        public ActionResult Shopping_success()
        {
            return View();
        }
        public ActionResult Checkout(FormCollection form)
        {
           
            

                Cart cart = Session["Cart"] as Cart;
                Order _order = new Order();
                _order.OrderDate = DateTime.Now;
               
                _order.CodeCus = int.Parse(form["CodeCustomer"]);
                _order.Descriptions = form["Address_Delivery"];
                _db.Orders.Add(_order);
                foreach (var item in cart.Items)
                {
                    OrderDetail _order_Detail = new OrderDetail();
                    _order_Detail.IDOrder = _order.IDOrder;
                    _order_Detail.IDProduct = item._shopping_product.IDProduct;
                    _order_Detail.UnitPriceSale = item._shopping_product.UnitPrice;
                    _order_Detail.QuantitySale = item._shopping_quantity; //
                    _db.OrderDetails.Add(_order_Detail);
                }
            try
            {
                if (ModelState.IsValid)
                {
                    
                    var senderEmail = new MailAddress("anduy1712@gmail.com", "Watches Shop");
                    var receiverEmail = new MailAddress(form["Address_Delivery"], "Receiver");
                    var password = "hipa1999";
                    var sub = "Đơn hàng đến từ Watches Shop - Thương Hiệu Đồng Hồ Việt Nam";
                    foreach (var item in cart.Items)
                    {
                        List<string> termsList = new List<string>();
                        termsList.Add("THÔNG TIN ĐƠN HÀNG - WATCHESHOP");
                        termsList.Add("Tên sản phẩm:" + item._shopping_product.NameProduct);
                        termsList.Add("Giá:" + item._shopping_product.UnitPrice + "$");
                        termsList.Add("Địa chỉ:" );
                        string combindedString = string.Join("\n", termsList);
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = "Đơn hàng đến từ Watches Shop - Thương Hiệu Đồng Hồ Việt Nam",
                            Body = combindedString
                        })
                        {
                            smtp.Send(mess);
                        }
                    }


                    return RedirectToAction("Shopping_success", "ShoppingCart");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            _db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("Shopping_success", "ShoppingCart");

            
        }

    }
}