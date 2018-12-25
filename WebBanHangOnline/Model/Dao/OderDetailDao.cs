using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
