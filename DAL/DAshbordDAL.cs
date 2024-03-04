using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Globalization;

namespace DAL
{
    public class DAshbordDAL
    {
        DB db = new DB();

        public object PersianDateTime { get; private set; }

        public string UserREminderCount(User u)
        {
            User q = db.users.Include("Reminders").Where(i => i.id == u.id ).FirstOrDefault();
            return q.Reminders.Count().ToString();
        }

        public string CustomrCount()
        {
            return db.customers.Count().ToString();
        }
        //public string SellCount()
        //{
        //    //int sum=0;
        //    //foreach(var item in db.invoices)
        //    //{
        //    //    if(item.RegDate == PersianDateTime.Now.ToString())
        //    //    {
        //    //        sum += sum;
        //    //    }
        //    //}
        //    //return sum.ToString();
        //}

      public List<Reminder> GetsersReminders (User u)
        {
            return db.reminders.Include("Users").Where(i => i.Users.id == u.id && i.IsDone==false).ToList();
        }

    }
}
