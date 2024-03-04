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
    public class InvoiceBLL
    {
        InvoiceDAL idal = new InvoiceDAL();
        public string create(Invoice i, Customer c, List<Product> p, User u)
        {
            return idal.create(i, c, p,u);
        }

        public DataTable Read()
        {
            return idal.Read();
        }

        public string Delete(int id)
        {
            return idal.Delete(id);
        }
        public bool Ischecked(int id)
        {
            return idal.Ischecked(id);
        }
        public bool ReadChecked(int id)
        {
            return idal.ReadChecked(id);
        }

        public string ReadInvoiceNum()
        {
            return idal.ReadInvoiceNum();
        }
    }
}
