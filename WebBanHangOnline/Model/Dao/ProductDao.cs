using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Model.Content;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDBContext db = null;
        
        public ProductDao()
        {
            db = new OnlineShopDBContext();
        }

        public long Insert(SanPham entity)
        {
            var convertString = new ConvertString();
            entity.MetaTitle = convertString.ConvertToUnSign(entity.Name);
            db.SanPhams.Add(entity);
            db.SaveChanges();
            return entity.ID_SP;
        }

        public Boolean Update(SanPham entity)
        {
            var convertString = new ConvertString();
            
            try
            {
                
                var sanpham = ViewDetail(entity.ID_SP);
                sanpham.Name = entity.Name;
                sanpham.MetaTitle = convertString.ConvertToUnSign(entity.Name);
                sanpham.Code = entity.Code;
                sanpham.ID_DMSP = entity.ID_DMSP;
                sanpham.Image = entity.Image;
                sanpham.Price = entity.Price;
                sanpham.Quantity = entity.Quantity;
                sanpham.Detail = entity.Detail;
                sanpham.Description = entity.Description;
                sanpham.CreateDate = entity.CreateDate;
                sanpham.CreateBy = entity.CreateBy;
                sanpham.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var sanpham = db.SanPhams.Find(id);
                db.SanPhams.Remove(sanpham);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<SanPham> ListAll(string searchString, int page, int pageSize)
        {
            IEnumerable<SanPham> model = db.SanPhams;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Code.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public SanPham ViewDetail(long id)
        {
            return db.SanPhams.Find(id);
        }
    }
}
