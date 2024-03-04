using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Data;

namespace BLL
{
    public class CustomerBll
    {
        CustomerDAL cdal = new CustomerDAL();
        DB db = new DB();
        public string Create(Customer c,User u)
        {
            cdal.Create(c,u);
            return "ثبت انجام شد";
        }

        public DataTable Read()
        {
            return cdal.Read();
        }
        public Customer Read(int id)
        {
            return cdal.Read(id);
        }
        public List<string> ReadPhone()
        {
            return cdal.ReadPhone();
        }
        public Customer ReadP(string phone)
        {
            return cdal.ReadP(phone);
        }
        public DataTable Search(string s,int index)
        {
            return cdal.Search(s,index);
        }

        public string Update(Customer c, int id)
        {
         
                cdal.Update(c, id);
            return "ویرایش انجام شد";
        }
        public string Delete(int id)
        {
            return cdal.Delete(id);
        }

      
    }
}
