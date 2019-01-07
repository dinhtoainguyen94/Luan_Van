using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class OderController : BaseController
    {
        //
        // GET: /Admin/Oder/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new OderDao();
            var model = dao.ListAll(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new OderDao().Delete(id);
            return RedirectToAction("Index");
        }
	}
}