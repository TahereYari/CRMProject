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
    public class ReminderBLL
    {
        DB db = new DB();
        ReminderDAL rdal = new ReminderDAL();
        public string Create(Reminder r,User u)
        {
            return rdal.Create(r,u);
        }
        public DataTable Read()
        {
          return  rdal.Read();
        }
        public DataTable Search(string s)
        {
            return rdal.Search(s);
        }
        public Reminder Read(int id)
        {
            return rdal.Read(id);
        }
        public string Update(Reminder r, int id)
        {
            return rdal.Update(r,id);
        }
        public String Delete(int id)
        {
            return rdal.Delete(id);
        }
        public String IsDone(int id)
        {
            return rdal.IsDone(id);
        }
    }
}
