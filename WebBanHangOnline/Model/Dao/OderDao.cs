using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.Dao
{
    public class OderDao
    {
         OnlineShopDBContext db = null;
        
        public OderDao()
        {
            db = new OnlineShopDBContext();
        }
        public long Insert(DonDatHang oder)
        {
            db.DonDatHangs.Add(oder);
            db.SaveChanges();
            return oder.ID_DDH;
        }

        public bool Delete(int id)
        {
            try
            {
                var oder = db.DonDatHangs.Find(id);
                db.DonDatHangs.Remove(oder);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<DonDatHang> ListAll(string searchString, int page, int pageSize)
        {
            IEnumerable<DonDatHang> model = db.DonDatHangs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ShipName.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
    }
}
