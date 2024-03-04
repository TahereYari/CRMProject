using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data;
using DAL;

namespace BLL
{
   public   class ActivityCategoryBLL
    {
        ActivityCategoryDAL acadal = new ActivityCategoryDAL();
        public string Create(ActivityCategory ac,User u)
        {
            return acadal.Create(ac,u);
        }

        public DataTable Read()
        {
            return acadal.Read();
        }
        public ActivityCategory Read(int id)
        {
            return acadal.Read(id);
        }
        public List<string> ReadName()
        {
            return acadal.ReadName();
        }

        public ActivityCategory ReadCat(string catname)
        {
            return acadal.ReadCat(catname);
        }
        public string Update(ActivityCategory ac, int id)
        {
            return acadal.Update(ac, id);
        }

        public string Delete(int id)
        {
            return acadal.Delete(id);
        }
    }
}
