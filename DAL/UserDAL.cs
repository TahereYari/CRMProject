using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;


namespace DAL
{
     public   class UserDAL
    {
        DB db = new DB();
        public string Create(User u, UserGroup ug)
        {

            try
            {
                if (Read(u))
                {
                    u.UserGroup = db.usergroups.Find(ug.id);
                    db.users.Add(u);
                    db.SaveChanges();
                    return "ثبت نام با موفقیت انجام شد";
                }
                else
                {
                    return "نام کاربری تکراری است.";
                }

            }
            catch (Exception e)
            {
                return "خطایی درافزودن کاربر وجود دارد:\n" + e.Message;
            }


        }

        public bool Read(User u)
        {
            //فعل تکراری بودن
            var q = db.users.Where(i => i.UserName == u.UserName);
            if(q.Count()==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User Read(int id)
        {
            //این فعل برای ویرایش اطلاعات استفاده میشود
            return db.users.Where(i => i.id == id).FirstOrDefault();
        }

        public DataTable Read()
        {
            //این فعل برای نمایش دیتای دیتا گرید است
            string cmd = "select id AS [شناسه] ,Name AS [نام و نام خانوادگی],UserName AS[نام کاربری] ,RegDate AS [تاریخ ثبت] from dbo.users where (DeleteStatus=0)";
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMfinaltosegar2; Integrated Security=True");
            var da = new SqlDataAdapter(cmd, con);
            var builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];


        }
        public User ReadU(string s)
        {
            return db.users.Where(i => i.UserName == s).SingleOrDefault();
        }

        public User Readid(int id)
        {
            return db.users.Where(i => i.id == id).SingleOrDefault();
        }
        public bool IsRegisterd()
        {
            return db.users.Count() > 0;
          
        }
        public List<string>ReadUserName()
        {
            return db.users.Where(i=>i.DeleteStatus==false).Select(i => i.UserName).ToList();
        }
      

        public string Update(User u, int id)
        {
            var q = db.users.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if(q!=null)
                {
                    q.Name = u.Name;
                    q.UserName = u.UserName;
                    q.Password = u.Password;
                    q.Pic = u.Pic;
                    db.SaveChanges();
                    return "ویرایش اطلاعات با موفقیت انجام شد";
                }

                else
                {
                    return"کاربر یافت نشد.";
                }
            }
            catch(Exception e)
            {
                return "ویرایش اطلاعات با خطایی مواجه شده است:\n" + e.Message;
            }
        }

        public string Delete(int id)
        {
            var q = db.users.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.DeleteStatus = true;

                    db.SaveChanges();
                    return "حذف با موفقیت انجام شد.";
                }
                else
                {
                    return "کاربر یافت نشد";
                }
            }

            catch (Exception e)
            {
                return "حذف با مشکل مواجه شده است:\n" + e.Message;
            }
        }

        public User Login(string s,string p)
        {
            return db.users.Include("UserGroup").Where(i => i.UserName==s && i.Password == p ).SingleOrDefault();
        }

        public bool Access(User u,string s,int a)
        {
            UserGroup ug = db.usergroups.Include("AccessUserGroups").Where(i => i.id == u.UserGroup.id).FirstOrDefault();
            AccessRole ac = ug.AccessUserGroups.Where(z => z.Section == s).FirstOrDefault();
            if(a==1)
            {
                return ac.CanEnter;
            }
           else if (a == 2)
            {
                return ac.CanCreate;
            }
            else if (a == 3)
            {
                return ac.CanUpdate;
            }
            else 
            {
                return ac.CanDelete;
            }
        }

        public List<User>ReadInvoices()
        {
            //فاکتورهای ثبت شده توسط هر کاربر
            return db.users.Include("invoices").Where(i => i.DeleteStatus == false).ToList();
        }
        public List<User> RegCustomer()
        {
            return db.users.Include("Customers").Where(i => i.DeleteStatus == false).ToList();
        }

        public List<User> RegActivity()
        {
            return db.users.Include("Activities").Where(i => i.DeleteStatus == false).ToList();
        }

    }
}
