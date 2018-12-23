using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.Dao
{
    public class UserDao
    {
        OnlineShopDBContext db = null;
        public UserDao()
        {
            db = new OnlineShopDBContext();
        }

        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.UserID;
        }

        public Boolean Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.UserID);
                user.Name = entity.Name;
                user.Email = entity.Email;
                user.Address = entity.Address;
                user.Phone = entity.Phone;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }  
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public int Login(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0; // Tài khoản không tồn tại
            }
            else
            {
                if (result.Status == false)
                {
                    return -1; // tài khoản bị khóa
                }
                else
                {
                    if (result.Password == passWord)
                    {
                        return 1; // đúng password
                    }
                    else
                    {
                        return -2; // sai password
                    }
                }
            }
        }
    }
}
