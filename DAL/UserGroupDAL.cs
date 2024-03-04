using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace DAL
{
    public  class UserGroupDAL
    {
        DB db = new DB();
        //AccessRole ac = new AccessRole();
        public string Create(UserGroup ug)
        {
            try
            {
                if (Read(ug))
                {

                    db.usergroups.Add(ug);
                    db.SaveChanges();
                    return " ثبت با موفقیت انجام شد.";
                }
                else
                {
                    return "نام گروه کاربری تکراری است.";
                }

            }

            catch (Exception e)
            {
                return "خطایی در ثبت اطلاعات رخ داده است" + e.Message;
            }
        }
      
        public bool Read(UserGroup ug)
        {
            var q = db.usergroups.Where(i=>i.title==ug.title);
           if(q.Count()==0)
            {
                return true;
            }
           else
            {
                return false;
            }

        }

        public UserGroup ReadN(string s)
        {
            return db.usergroups.Where(i => i.title == s).SingleOrDefault();
        }
        public UserGroup ReadByTitle(string t)
        {
            return db.usergroups.Where(x => x.title == t).SingleOrDefault();
        }

        public List<string> ReadGroup()
        {
            return db.usergroups.Select(i => i.title).ToList();
        }
    }
}
