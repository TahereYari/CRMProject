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
using BLL;
using System.Windows;

namespace CRMfinalProject
{
   
    public partial class ProductForm : Form
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

        ProductBLL pbll = new ProductBLL();
        Product p = new Product();
        int id;
        void DataGridFill()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = pbll.Read();
            dataGridView1.Columns["شناسه"].Visible = false;
        }
        void emptytext()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        public ProductForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            DataGridFill();
        }

        private void label11_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        msgBox m = new msgBox();
       
        UserBLL ubll = new UserBLL();
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            User u1 = new User();
            u1 = mf.loggedinuser;
            p.Name = textBox1.Text;
            p.Price = Convert.ToInt32(textBox2.Text);
            p.Stock = Convert.ToInt32(textBox3.Text);
            if (label3.Text== "ثبت اطلاعات")
            {

                if (ubll.Access(u1, "بخش کالاها", 2))
                {
                    m.myshowdialog("",pbll.Create(p,u1),"",false,false);
                }
                else
                {
                    m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید","", false, true);
                }
            }
            else
            {
               

                    m.myshowdialog("",pbll.Upadate(p,id),"",false,false);
               

            }
            DataGridFill();
            emptytext();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X,Cursor.Position.Y);
            id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["شناسه"].Value);

        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
    {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            User u1 = new User();
            u1 = mf.loggedinuser;
            if (ubll.Access(u1, "بخش کالاها", 3))
              {
            Product p = pbll.Read(id);
            textBox1.Text = p.Name;
            textBox2.Text = Convert.ToString(p.Price);
            textBox3.Text = Convert.ToString(p.Stock);
            label3.Text = "ویرایش اطلاعات";
            DataGridFill();
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            User u1 = new User();
            u1 = mf.loggedinuser;
            if (ubll.Access(u1, "بخش کالاها", 3))
            {
                m.myshowdialog("",pbll.Delete(id),"",false,false);
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            DataGridFill();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = pbll.Search(textBox4.Text);
        }
    }
}
