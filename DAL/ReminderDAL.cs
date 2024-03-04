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
     public class ReminderDAL
    {
        DB db = new DB();
      public string Create(Reminder r, User u)
        {
            //try
            //{

                r.Users = db.users.Find(u.id);
                db.reminders.Add(r);
                db.SaveChanges();
                return "اطلاعات با موفقیت ثبت شد.";

            //}
            //catch (Exception e)
            //{
            //    return "ثبت اطلاعات با مشکلی مواجه شده است:\n" + e.Message;
            //}
        }

        public DataTable Read()
        {
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMfinaltosegar2; Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("dbo.ReadRemindres");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];

        }
        public Reminder Read(int id)
        {
            //این فعل برای ویرایش اطلاعات استفاده میشود
            return db.reminders.Where(i => i.id == id).FirstOrDefault();
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
       
        public string Update(Reminder r,int id)
        {
            var q = db.reminders.Where(i => i.id == id).SingleOrDefault();
            try
            {
                if(q!=null)
                {
                    q.ReminderDate = r.ReminderDate;
                    q.ReminderInfo = r.ReminderInfo;
                    q.Title = r.Title;
                    db.SaveChanges();
                    return "ویرایش با موفقیت انجام شد.";
                }
                else
                {
                    return "کاربر یافت نشد";
                }
            }
            catch(Exception e)
            {
                return "ویرایش با مشکل مواجه شده است:\n" + e.Message;
            }
        }

        public String Delete(int id)
        {
            var q = db.reminders.Where(i => i.id == id).FirstOrDefault();
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
                    return "یادآور یافت نشد";
                }
            }

            catch (Exception e)
            {
                return "حذف با مشکل مواجه شده است:\n" + e.Message;
            }
        }
        public String IsDone(int id)
        {
            var q = db.reminders.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.IsDone = true;

                    db.SaveChanges();
                    return "یادآور غیرفعال شد.";
                }
                else
                {
                    return "یادآور یافت نشد";
                }
            }

            catch (Exception e)
            {
                return "غیر فعال کردن یاد آور با مشکل مواجه شده است:\n" + e.Message;
            }
        }

       
    }
}
