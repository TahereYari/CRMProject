using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Data.SqlClient;
using System.Data;


namespace BLL
{
    public class UserBLL
    {
        UserDAL udal = new UserDAL();
        private string Encode(string Pass)
        {
            byte[] endata = new byte[Pass.Length];
            endata = System.Text.Encoding.UTF8.GetBytes(Pass);
            string encodedata = Convert.ToBase64String(endata);
            return encodedata;
        }

        private string Decode(string EncodedPass)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8decoder = encoder.GetDecoder();
            byte[] todecoder_byte = Convert.FromBase64String(EncodedPass);
            int charcount = utf8decoder.GetCharCount(todecoder_byte, 0, todecoder_byte.Length);
            char[] decode_char = new char[charcount];
            utf8decoder.GetChars(todecoder_byte, 0, todecoder_byte.Length, decode_char, 0);
            string result = new string(decode_char);
            return result;

        }
        public string Create(User u, UserGroup ug)
        {
            u.Password = Encode(u.Password);
            return udal.Create(u, ug);
        }

        public bool IsRegisterd()
        {
            return udal.IsRegisterd();
        }
        public User Read(int id)
        {
            return udal.Read(id);
        }
        public List<string> ReadUserName()
        {
            return udal.ReadUserName();
        }
        public User ReadU(string s)
        {
            return udal.ReadU(s);
        }

        public DataTable Read()
        {
            return udal.Read();
        }

        public string Update(User u, int id)
        {
            u.Password = Encode(u.Password);
            return udal.Update(u, id);
        }

        public string Delete(int id)
        {
            return udal.Delete(id);
        }

        public User Login(string s, string p)
        {
            return udal.Login(s, Encode(p));
        }

        public bool Access(User u, string s, int a)
        {
            return udal.Access(u, s, a);
        }
         public User Readid(int id)
         {
            return udal.Readid(id);
         }

        public List<User> ReadInvoices()
        {
            return udal.ReadInvoices();
        }
        public List<User> RegCustomer()
        {
            return udal.RegCustomer();
        }

        public List<User> RegActivity()
        {
            return udal.RegActivity();
        }

        }
    
}
