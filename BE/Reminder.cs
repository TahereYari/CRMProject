using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Reminder
    {
        public Reminder()
        {
            DeleteStatus = false;
            IsDone = false;
        }
        public int id { get; set; }
        public string Title { get; set; }
        public string ReminderInfo { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool DeleteStatus { get; set; }
        public bool IsDone { get; set; }
        public User Users { get; set; }
        
    }
}
