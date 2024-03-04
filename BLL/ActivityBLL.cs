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
    public  class ActivityBLL
    {
        ActivityDAL adal = new ActivityDAL();

        public string Create(Activity A, User u, Customer c, ActivityCategory ac)
        {
            return adal.Create(A, u, c, ac);
        }
        public Activity Read(int id)
        {
            return adal.Read(id);
        }
        public DataTable Read()
        {
            return adal.Read();
        }
        public string ReadInfo(int id)
        {
            return adal.ReadInfo(id);
        }
        public string Delete(int id)
        {
            return adal.Delete(id);
        }

        public DataTable Search(string s)
        {
            return adal.Search(s);
        }
    }
}
