using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using DAL;
using BLL;
using System.Windows;
using System.Globalization;

namespace CRMfinalProject
{
    public partial class CustomerForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        public CustomerForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

        }
        CustomerBll cbll = new CustomerBll();
        Customer c = new Customer();
        int id;
        int index;
        msgBox m = new msgBox();
        UserBLL ubll = new UserBLL();
      
       
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            datagrifill();

        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void datagrifill()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = cbll.Read();
            dataGridView1.Columns["شناسه"].Visible = false;
        }

        void emptytexts()
        {
            textBox1.Text = "";
            textBox2.Text = "";



        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
      
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            User u1 = new User();
            u1 = mf.loggedinuser;
            c.Name = textBox1.Text;
            c.Phone = textBox2.Text;
            c.RegDate = DateTime.Now;
            if (label3.Text == "ثبت اطلاعات")
            {
                if (ubll.Access(u1, "بخش مشتریان", 2))
                {


                    if (textBox1.Text != "" && textBox2.Text != "")
                    m.myshowdialog("ثبت اطلاعات", cbll.Create(c,u1), "", false, false);

                }
                else
                {
                    m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید","", false, true);
                }

            }
          
          
            else
            {
               

                    m.myshowdialog("ویرایش اطلاعات",cbll.Update(c,id),"",false,false);

                label3.Text = "ثبت اطلاعات";
          
            }
           
            datagrifill();
            emptytexts();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
             id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["شناسه"].Value);
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
    {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            User u1 = new User();
            u1 = mf.loggedinuser;
            if (ubll.Access(u1, "بخش مشتریان", 3))
            {
                Customer c;
                c = cbll.Read(id);
                textBox1.Text = c.Name;
                textBox2.Text = c.Phone;
                label3.Text = "ویرایش اطلاعات";
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید","", false, true);
            }
            datagrifill();
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            User u1 = new User();
            u1 = mf.loggedinuser;
            if (ubll.Access(u1, "بخش مشتریان", 3))
            {
                DialogResult dr = m.myshowdialog("اخطار", "درصورت حذف اطلاعات تمام اطلاعات مربوط به آن حذف خواهد شد.\n آیا از حذف اطمینان دارید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {

                   m.myshowdialog("حذف اطلاعات", cbll.Delete(id),"",false,false);
                }
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید","", false, true);
            }

                datagrifill();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if(((checkBox1.Checked) && (checkBox2.Checked)) ||((!checkBox1.Checked && !checkBox2.Checked)))
            {
                index = 0;
            }

           else if (checkBox1.Checked && !checkBox2.Checked)
            {
                index = 1;
            }

            else if (checkBox2.Checked && !checkBox1.Checked)
            {
                index = 2;
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = cbll.Search(textBox4.Text,index);
        }
    }
}
