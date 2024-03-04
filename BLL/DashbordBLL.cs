using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;


namespace BLL
{

    public class DashbordBLL
    {
        DAshbordDAL ddal = new DAshbordDAL();
        public string UserREminderCount(User u)
        {
            return ddal.UserREminderCount(u);
        }

        public string CustomrCount()
        {
            return ddal.CustomrCount();
        }

        //public string SellCount()
        //{
        //    return ddal.SellCount();
        //}

        public List<Reminder> GetsersReminders(User u)
        {
            return ddal.GetsersReminders(u);
        }
    }
}
