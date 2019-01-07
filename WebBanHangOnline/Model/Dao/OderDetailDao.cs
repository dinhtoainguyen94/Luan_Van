using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;


namespace Model.Dao
{
    public class OderDetailDao
    {
        OnlineShopDBContext db = null;

        public OderDetailDao()
        {
            db = new OnlineShopDBContext();
        }
        public bool Insert(ChiTietDDH oderDetail)
        {
            try
            {
                db.ChiTietDDHs.Add(oderDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int productID, int oderID)
        {
            try
            {
                ChiTietDDH oderDetail = db.ChiTietDDHs.Find(productID, oderID);
                db.ChiTietDDHs.Remove(oderDetail);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ChiTietDDH> ListAll(string searchString, int page, int pageSize)
        {
            IEnumerable<ChiTietDDH> model = db.ChiTietDDHs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID_SP.ToString().Contains(searchString));
            }
            return model.OrderByDescending(x => x.Quantity).ToPagedList(page, pageSize);
        }
    }
}
