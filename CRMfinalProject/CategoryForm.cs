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
using System.Windows;

namespace CRMfinalProject
{
    public partial class CategoryForm : Form
    {
        ActivityCategory ac = new ActivityCategory();
        ActivityCategoryBLL acbll = new ActivityCategoryBLL();
        msgBox m = new msgBox();
        int id;
        public CategoryForm()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        UserBLL ubll = new UserBLL();
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            User u1 = new User();
            u1 = mf.loggedinuser;
            ac.CategoryName = textBox1.Text;
            if(label3.Text== "ثبت اطلاعات")
               {
                    if (ubll.Access(u1, "بخش یادآور", 2))
                        {
                   
                        m.myshowdialog("", acbll.Create(ac,u1), "", false, false);
                        }
                        else
                        {
                            m.myshowdialog("محدودیت دسترسی", "تصویر کاربر انتخاب نشده است.", "شما اجازه ورود به این قسمت را ندارید", false, true);
                        }
               }
            else
            {
                m.myshowdialog("", acbll.Update(ac,id), "", false, false);
                label3.Text ="ثبت اطلاعات";
            }
          
            DataFill();
            textBox1.Text = "";

        }
        void DataFill()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = acbll.Read();
            dataGridView1.Columns["شناسه"].Visible = false;
        }
        private void CategoryForm_Load(object sender, EventArgs e)
        {
            DataFill();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["شناسه"].Value);

        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ac = acbll.Read(id);
            textBox1.Text = ac.CategoryName;
            label3.Text = "ویرایش اطلاعات";
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m.myshowdialog("", acbll.Delete(id), "", false, false);
            DataFill();
        }
    }
}
