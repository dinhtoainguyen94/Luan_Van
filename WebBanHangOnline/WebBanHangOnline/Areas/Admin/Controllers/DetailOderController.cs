using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class DetailOderController : BaseController
    {
        //
        // GET: /Admin/DetailOder/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new OderDetailDao();
            var model = dao.ListAll(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(int productID, int oderID)
        {
            new OderDetailDao().Delete(productID, oderID);
            return RedirectToAction("Index");
        }
	}
}