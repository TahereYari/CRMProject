using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Data;

namespace BLL
{
    public class ProductBLL
    {
        ProductDAL pdal = new ProductDAL();
        public string Create(Product p,User u)
        {
            return pdal.Create(p,u);
        }

        public Product Read(int id)
        {
           return pdal.Read(id);
        }
        public DataTable Read()
        {
           return pdal.Read();
        }
        public DataTable Search(string s)
        {
            return pdal.Search(s);
        }

        public List<string> ReadName()
        {
            return pdal.ReadName();
        }
        public Product ReadN(string name)
        {
            return pdal.ReadN(name);
        }
        public string Upadate(Product p , int id)
        {
            return pdal.Update(p,id);
        }

        public string Delete(int id)
        {
            return pdal.Delete(id);
        }
    }
}
