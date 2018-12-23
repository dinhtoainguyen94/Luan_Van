using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FooterDao
    {
         OnlineShopDBContext db = null;
         public FooterDao()
        {
            db = new OnlineShopDBContext();
        }

         public Foodter GetFooter()
         {
             return db.Foodters.SingleOrDefault(x => x.Status == true);
         }
    }
}
