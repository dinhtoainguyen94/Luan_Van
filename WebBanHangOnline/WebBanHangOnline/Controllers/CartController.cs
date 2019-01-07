using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Common;
using WebBanHangOnline.Models;
using System.Web.Script.Serialization;
using Model.EF;
using System.Configuration;
using Common;

namespace WebBanHangOnline.Controllers
{
    public class CartController : Controller
    {
        
        //
        // GET: /Cart/
        [HttpGet]
        public ActionResult Index()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Index(DonDatHang oder)
        {
            var user = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (user != null)
            {
                var user1 = new UserDao().ViewDetail(user.UserID);
                oder.CreateDate = DateTime.Now;
                oder.ShipName = user1.Name;
                oder.ShipMobie = user1.Phone;
                oder.ShipAddress = user1.Address;
                oder.ShipEmail = user1.Email;

                try
                {
                    var id = new OderDao().Insert(oder);
                    var cart = (List<CartItem>)Session[CommonConstants.CartSession];
                    var detailDao = new OderDetailDao();
                    decimal total = 0;
                    foreach (var item in cart)
                    {
                        var oderDetail = new ChiTietDDH();
                        oderDetail.ID_SP = item.Product.ID_SP;
                        oderDetail.ID_DDH = id;
                        oderDetail.Price = item.Product.Price;
                        oderDetail.Quantity = item.Quantity;
                        detailDao.Insert(oderDetail);

                        total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                    }
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/newoder.html"));

                    content = content.Replace("{{CustomerName}}", user1.Name);
                    content = content.Replace("{{Phone}}", user1.Phone);
                    content = content.Replace("{{Email}}", user1.Email);
                    content = content.Replace("{{Address}}", user1.Address);
                    content = content.Replace("{{Total}}", total.ToString("N0"));
                    var toEmail = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();

                    new MailHelper().SendMail(user1.Email, "Đơn hàng mới từ OnlineShop", content);
                    new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
                }
                catch (Exception ex)
                {
                    //ghi log
                    return Redirect("/loi-thanh-toan");
                }
                return Redirect("/hoan-thanh");
            }
            else
            {
                return Redirect("/thanh-toan");
            }

        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];
            sessionCart.RemoveAll(x => x.Product.ID_SP == id);
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x=>x.Product.ID_SP == item.Product.ID_SP);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new{
                status = true
            });
        }

        public ActionResult AddItem(long productId, int quantity)
        {

                var product = new ProductClientDao().ViewDetail(productId);
                var cart = Session[CommonConstants.CartSession];
                if (cart != null)
                {
                    var list = (List<CartItem>)cart;
                    if (list.Exists(x => x.Product.ID_SP == productId))
                    {
                        foreach (var item in list)
                        {
                            if (item.Product.ID_SP == productId)
                            {
                                item.Quantity += quantity;
                            }
                        }
                    }
                    else
                    {
                        var item = new CartItem();
                        item.Product = product;
                        item.Quantity = quantity;
                        list.Add(item);
                    }
                    // gán vào session
                    Session[CommonConstants.CartSession] = list;
                }
                else
                {
                    // tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    var list = new List<CartItem>();
                    list.Add(item);

                    // gán vào session
                    Session[CommonConstants.CartSession] = list;

                }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Payment()
        {

            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment( string shipName, string mobile, string address, string email)
        {

                var oder = new DonDatHang();
                oder.CreateDate = DateTime.Now;
                oder.ShipName = shipName;
                oder.ShipMobie = mobile;
                oder.ShipAddress = address;
                oder.ShipEmail = email;
                
                try
                {
                    var id = new OderDao().Insert(oder);
                    var cart = (List<CartItem>)Session[CommonConstants.CartSession];
                    var detailDao = new OderDetailDao();
                    decimal total = 0;
                    foreach (var item in cart)
                    {
                        var oderDetail = new ChiTietDDH();
                        oderDetail.ID_SP = item.Product.ID_SP;
                        oderDetail.ID_DDH = id;
                        oderDetail.Price = item.Product.Price;
                        oderDetail.Quantity = item.Quantity;
                        detailDao.Insert(oderDetail);

                        total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                    }
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/newoder.html"));

                    content = content.Replace("{{CustomerName}}", shipName);
                    content = content.Replace("{{Phone}}", mobile);
                    content = content.Replace("{{Email}}", email);
                    content = content.Replace("{{Address}}", address);
                    content = content.Replace("{{Total}}", total.ToString("N0"));
                    var toEmail = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();

                    new MailHelper().SendMail(email, "Đơn hàng mới từ OnlineShop", content);
                    new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
                }
                catch (Exception ex)
                {
                    //ghi log
                    return Redirect("/loi-thanh-toan");
                }

            return Redirect("/hoan-thanh");

        }

        public ActionResult Success()
        {
            return View();
        }
	}
}