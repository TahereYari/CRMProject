using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BLL
{
     public class UserGroupBLL
    {
        DB db = new DB();
        UserGroupDAL ugdal = new UserGroupDAL();
        public string Create(UserGroup ug)
        {
            return ugdal.Create(ug);
        }

        public UserGroup ReadByTitle(string Title)
        {
            return ugdal.ReadByTitle(Title);
        }

        public UserGroup ReadN(string s)
        {
            return ugdal.ReadN(s);
        }

        public List<string> ReadGroup()
        {
            return ugdal.ReadGroup();
        }
    }
}
