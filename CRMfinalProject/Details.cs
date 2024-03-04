using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;


namespace CRMfinalProject
{
    public partial class Details : Form
    {
        public Details()
        {
            InitializeComponent();
        }
        Activity a = new Activity();
        ActivityBLL abll = new ActivityBLL();
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Details_Load(object sender, EventArgs e)
        {

        }
    }
}
