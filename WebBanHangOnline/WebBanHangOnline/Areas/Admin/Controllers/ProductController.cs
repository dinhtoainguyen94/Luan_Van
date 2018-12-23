using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Admin/Product/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new ProductDao();
            var model = dao.ListAll(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(SanPham product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();

                long id = dao.Insert(product);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm User không thành công");
                }
            }
            return View("Index");
        }
        

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var product = new ProductDao().ViewDetail(id);

            return View(product);
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(SanPham sanpham)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();

                var result = dao.Update(sanpham);
                if (result)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật sản phẩm không thành công");
                }
            }
            return View("Index");
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);
            return RedirectToAction("Index");
        }
	}
}