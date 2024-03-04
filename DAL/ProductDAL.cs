using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public class ProductDAL
    {
        DB db = new DB();
        public string Create(Product p,User u)
        {
            try
            {
                p.users = db.users.Find(u.id);
                db.products.Add(p);
                db.SaveChanges();
                return "ثبت با موفقیت انجام شد.";
            }
            catch(Exception e)
            {
                return "ثبت اطلاعات با مشکلی مواجه شده است:\n" + e.Message;

            }
        }

        public Product Read(int id)
        {
            return db.products.Where(i => i.id == id).FirstOrDefault();
        }
        public DataTable Read()
        {
            string cmd = "select id AS [شناسه] ,Name AS [نام محصول],Price AS [قیمت],Stock AS [موجودی]  from dbo.Products where (DeleteStatus=0)";
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMfinaltosegar2; Integrated Security=True");
            var da = new SqlDataAdapter(cmd, con);
            var builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }

        public DataTable Search(string s)
        {
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMfinaltosegar2; Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            cmd = new SqlCommand("dbo.SearchProduct");
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
        public List<string>ReadName()
        {
            return db.products.Where(i=>i.DeleteStatus==false && i.Stock>0).Select(i => i.Name).ToList();
        }
        public Product ReadN(string name)
        {
            return db.products.Where(i => i.Name == name).SingleOrDefault();
        }
        public string Update(Product p, int id)
        {
            var q = db.products.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.Name = p.Name;
                    q.Price = p.Price;
                    q.Stock = p.Stock;
                    db.SaveChanges();
                    return "ویرایش با مو فقیت اتجام شد.";
                }
                else
                {
                    return "کاربر یافت نشد";
                }
            }
            catch(Exception e)
            {
                return "ویرایش اطلاعات با مشکل مواجه شده است:\n" + e.Message;
            }
            
        }
        public string Delete(int id)
        {
            var q = db.products.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return"حذف با موفقیت انجام شد.";

                }
                else
                {
                    return "محصول پیدا نشد";
                }
            }

            catch (Exception e)
            {
                return "حذف با مشکل مواجه شده است:\n" + e.Message;
            }

        }
        
    }
}
