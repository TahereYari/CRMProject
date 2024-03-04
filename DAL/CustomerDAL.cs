using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DAL
{
    public class CustomerDAL
    {
        DB db = new DB();
        public string Create(Customer c,User u)
        {
            try
            {
                c.users = db.users.Find(u.id);
                db.customers.Add(c);
                db.SaveChanges();
                return "ثبت اطلاعات با موفقیت انجام شد.";

            }
            catch (Exception e)
            {
                return "ثبت اطلاعات با مشکلی مواجه شده است:\n" + e.Message;

            }
        }
        public DataTable Read()
        {
            string cmd= "select id AS [شناسه] ,Phone AS [شماره تلفن],Name AS [نام مشتری] ,RegDate AS [تاریخ ثبت] from dbo.customers where (DeleteStatus=0)";
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMfinaltosegar2; Integrated Security=True" );
            var da = new SqlDataAdapter(cmd, con);
            var builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];

           
        }
       public DataTable Search(string s, int index)
        {
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMfinaltosegar2; Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
          
            if (index==0)
            {
               
              cmd = new SqlCommand("dbo.SearchCustomer");
            
            }
            else if(index==1)
            {
              
                cmd = new SqlCommand("dbo.searchcustomername");
              

            }

            else if(index==2)
            {
                cmd = new SqlCommand("dbo.searchcustomerphone");
               
            }
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
        public Customer Read(int id)
        {
            return db.customers.Where(i => i.id == id).FirstOrDefault();
        }
        public List<string>ReadPhone()
        {
            return db.customers.Where(i=>i.DeleteStatus==false ).Select(i => i.Phone).ToList();
        }
        public Customer ReadP(string phone)
        {
            return db.customers.Where(i => i.Phone == phone).SingleOrDefault();
           
        }

        //public List<Customer> Read()
        //{
        //    return db.customers.ToList();
        //}
        public string Update(Customer c, int id)
        {
            var q = db.customers.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.Name = c.Name;
                    q.Phone = c.Phone;
                    db.SaveChanges();
                    return "ویرایش با موفقیت انجام شد.";
                }
                else
                {
                    return "کاربر یافت نشد";
                }
            }

            catch (Exception e)
            {
                return "ویرایش با مشکل مواجه شده است:\n" + e.Message;
            }
        }

        public string Delete(int id)
        {
            var q = db.customers.Where(i => i.id == id).FirstOrDefault();
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

      


      

    }
}
