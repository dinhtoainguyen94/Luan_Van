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

namespace WebBanHangOnline.Controllers
{
    public class CartController : Controller
    {
        
        //
        // GET: /Cart/
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
        public ActionResult Payment(string shipName, string mobile, string address, string email)
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
                foreach (var item in cart)
                {
                    var oderDetail = new ChiTietDDH();
                    oderDetail.ID_SP = item.Product.ID_SP;
                    oderDetail.ID_DDH = id;
                    oderDetail.Price = item.Product.Price;
                    oderDetail.Quantity = item.Quantity;
                    detailDao.Insert(oderDetail);

                }
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