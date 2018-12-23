using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductCategoryDao
    {
         OnlineShopDBContext db = null;
         public ProductCategoryDao()
        {
            db = new OnlineShopDBContext();
        }

         public List<DanhMucSP> ListAll()
         {
             return db.DanhMucSPs.Where(x => x.Status == true).OrderBy(x => x.DisplayOder).ToList();
         }
         public DanhMucSP ViewDetail(long id)
         {
             return db.DanhMucSPs.Find(id);
         }
    }
}
