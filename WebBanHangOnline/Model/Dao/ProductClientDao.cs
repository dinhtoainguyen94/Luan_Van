using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductClientDao
    {
        OnlineShopDBContext db = null;
        public ProductClientDao()
        {
            db = new OnlineShopDBContext();
        }

        public List<SanPham> ListProduct(int top)
        {
            return db.SanPhams.Where(x=>x.ID_DMSP == 1).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }

        public List<SanPham> ListAccessories(int top)
        {
            return db.SanPhams.Where(x => x.ID_DMSP == 2).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }

        //Lấy tất cả các sản phẩm của danh mục sản phẩm
        public List<SanPham> ListByCategoryById(long categoryID,ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.SanPhams.Where(x => x.ID_DMSP == categoryID).Count();
            var model = db.SanPhams.Where(x => x.ID_DMSP == categoryID).OrderByDescending(x=>x.CreateDate).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            return model;
        }

        // lấy ra sản phẩm liên quan
        public List<SanPham> ListRelatedProducts(long productId)
        {
            var product = db.SanPhams.Find(productId);
            return db.SanPhams.Where(x => x.ID_SP != productId && x.ID_DMSP == product.ID_DMSP).ToList();
        }
        public SanPham ViewDetail(long id)
        {
            return db.SanPhams.Find(id);
        }
    }
}
