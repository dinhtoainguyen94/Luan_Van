using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;

namespace WebBanHangOnline.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        public ActionResult Slide()
        {
            var model = new SlideDao().GetSlide();
            return PartialView(model);
        }

        public ActionResult Category(long cateID, int page = 1, int pageSize = 2)
        {
            var category = new ProductCategoryDao().ViewDetail(cateID);
            ViewBag.Category = category;
            int totalRecord = 0;
            var model = new ProductClientDao().ListByCategoryById(cateID, ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public ActionResult Detail(long id)
        {
            var productDetail = new ProductClientDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(productDetail.ID_DMSP.Value);
            ViewBag.RelatedProduct = new ProductClientDao().ListRelatedProducts(id);
            return View(productDetail);
        }
	}
}