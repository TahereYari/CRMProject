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
    public  class InvoiceDAL
    {
        DB db = new DB();
        public string create(Invoice i,Customer c,List<Product>p, User u)
        {
            //try
            //{
                i.customer = db.customers.Find(c.id);
                i.user = db.users.Find(u.id);
                
                foreach(var item in p)
                {
                    i.Products.Add(db.products.Find(item.id));

                }
                Random rnd = new Random();
                string s = rnd.Next(1000000).ToString();
                var q = db.invoices.Where(z => z.InvoiceNumber == s);
                while(q.Count()>0)
                {
                    s= rnd.Next(1000000).ToString();
                }
                i.InvoiceNumber = s; 
                db.invoices.Add(i);
                db.SaveChanges();
                return "ثبت فاکتور با موفقیت انجام شد:\n.";
            //}
            //catch(Exception e)
            //{
            //    return "مشکلی در ثبت فاکتور وجود دارد، :\nلطفا در ورود اطلاعات دقت فرمایید.:\n" + e.Message;
            //}
        }

        public DataTable Read()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source =.; Initial Catalog = CRMfinaltosegar2; Integrated Security = True";
            SqlCommand com = new SqlCommand();
            com = new SqlCommand("dbo.ReadInvoice");
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = com;
            var builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }

        public string Delete(int id)
        {
            var q = db.invoices.Where(i => i.id == id).FirstOrDefault();
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
                    return "فاکتور مورد نظر یافت نشد";
                }
            }

            catch (Exception e)
            {
                return "حذف با مشکل مواجه شده است:\n" + e.Message;
            }

        }

        public bool Ischecked(int id)
        {
            //try
            //{
                var q = db.invoices.Where(i => i.id == id && i.Ischeckout == false).SingleOrDefault();
            if(q!=null)
                {
                    q.CheckOutDate = DateTime.Now;
                    q.Ischeckout = true;
                db.SaveChanges();
                    return true;
                 }
            else
                {
                    return false;
                }
            //}

            //catch (Exception)
            //{
            //    return false;
            //}


        }
        public bool ReadChecked(int id)
        {
            var q = db.invoices.Where(i => i.id == id && i.Ischeckout == false).SingleOrDefault();
            if(q!=null) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string ReadInvoiceNum()
        {
            var q = db.invoices.OrderByDescending(i => i.id).FirstOrDefault();
            return q.InvoiceNumber;
        }
    }
}
