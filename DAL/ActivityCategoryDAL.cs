using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public  class ActivityCategoryDAL
    {
        DB db = new DB();
        public string Create(ActivityCategory ac, User u)
        {
            try
            {
                ac.users = db.users.Find(u.id);
                    db.ActivityCategories.Add(ac);
                    db.SaveChanges();
                    return "ثبت  با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "خطایی درافزودن دسته بندی وجود دارد:\n" + e.Message;
            }
        }

        public DataTable Read()
        {
            string cmd = "SELECT  id AS شناسه, CategoryName AS [دسته بندی ها] FROM dbo.ActivityCategories";
             SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMfinaltosegar2; Integrated Security=True");
            var da = new SqlDataAdapter(cmd, con);
            var builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];


        }
        public ActivityCategory Read(int id)
        {
            return db.ActivityCategories.Where(i => i.id == id).FirstOrDefault();
        }
        public List<string>ReadName()
        {
            return db.ActivityCategories.Select(i => i.CategoryName).ToList();
        }
        public ActivityCategory ReadCat(string catname)
        {
            return db.ActivityCategories.Where(i => i.CategoryName == catname).SingleOrDefault();


           
        }

        public string Update(ActivityCategory ac, int id)
        {
            var q = db.ActivityCategories.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.CategoryName = ac.CategoryName;
                    db.SaveChanges();
                    return "ویرایش با موفقیت انجام شد.";
                }
                else
                {
                    return "دسته بندی مورد نظر یافت نشد";
                }
            }

            catch (Exception e)
            {
                return "ویرایش با مشکل مواجه شده است:\n" + e.Message;
            }
        }

        public string Delete(int id)
        {
            var q = db.ActivityCategories.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    db.ActivityCategories.Remove(q);

                    db.SaveChanges();
                    return "حذف با موفقیت انجام شد.";
                }
                else
                {
                    return "دسته بندی مورد نظر یافت نشد";
                }
            }

            catch (Exception e)
            {
                return "حذف با مشکل مواجه شده است:\n" + e.Message;
            }
        }
    }
}
