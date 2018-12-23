using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;

namespace WebBanHangOnline.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            var productClientDao = new ProductClientDao();
            ViewBag.NewProduct = productClientDao.ListProduct(4);
            ViewBag.Accessories = productClientDao.ListAccessories(4);
            return View();
        }

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult MenuTop()
        {
            var model = new MenuDao().ListByGroupId(2);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new FooterDao().GetFooter();
            return PartialView(model);
        }
	}
}