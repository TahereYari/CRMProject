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
    public class ActivityDAL
    {
        DB db = new DB();
        public string Create(Activity A,User u,Customer c,ActivityCategory ac)
        {

            try
            {
                A.user = db.users.Find(u.id);
                A.customer = db.customers.Find(c.id);
                A.ActivityCategory = db.ActivityCategories.Find(ac.id);
                db.activities.Add(A);
                db.SaveChanges();
                return "ثبت نام با موفقیت انجام شد";


            }
            catch (Exception e)
            {
                return "خطایی درافزودن فعالیت وجود دارد:\nدر ورود اطلاعات دقت فرمایید:\n" + e.Message;
            }
        }
        public Activity Read(int id)
        {
            //این فعل برای ویرایش اطلاعات استفاده میشود
            return db.activities.Where(i => i.id == id).FirstOrDefault();
        }
        public DataTable Read()
        {
            //این فعل برای نمایش دیتای دیتا گرید است
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMfinaltosegar2; Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("dbo.ReadActivity");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];


        }
        public string ReadInfo(int id)
        {
            var q = db.activities.Where(i => i.id == id).FirstOrDefault();
            return q.Info;


        }
        public string Delete(int id)
        {
            var q = db.activities.Where(i => i.id == id).FirstOrDefault();
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
                return "فعالیت مورد نظر یافت نشد";
            }
            }

            catch (Exception e)
            {
                return "حذف با مشکل مواجه شده است:\n" + e.Message;
            }
        }

        public DataTable Search(string s)
        {
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMfinaltosegar2; Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("dbo.SearchReminders");
            cmd.Parameters.AddWithValue("@Search", s);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }

    }
}
