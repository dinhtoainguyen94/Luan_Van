using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

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
    }
}
