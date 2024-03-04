using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Invoice
    {
        public Invoice()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime RegDate { get; set; }
        public Nullable<DateTime>  CheckOutDate { get; set; }
        public bool Ischeckout { get; set; }
        public bool DeleteStatus { get; set; }
        public Customer customer { get; set; }
        public User user { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
