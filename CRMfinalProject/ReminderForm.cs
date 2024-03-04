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
using DAL;
using BE;
using BLL;
using System.Windows;
using System.Globalization;



namespace CRMfinalProject
{
    public partial class ReminderForm : Form
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
        public ReminderForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

        }

        UserBLL ubll = new UserBLL();
        User u = new User();
        Reminder r = new Reminder();
        ReminderBLL rbll = new ReminderBLL();
        msgBox m = new msgBox();
        int id;
        private void ReminderForm_Load(object sender, EventArgs e)
        {
            DataFill();
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            foreach (var item in ubll.ReadUserName())
            {
                names.Add(item);
            }
            textBox1.AutoCompleteCustomSource = names;
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            mf.RefreshPage();
        }

        public string GetPersianmonth(string s)
        {

            if (s.Contains("فروردین"))
                return s.Replace ("فروردین", "/01/") ;

            else if(s.Contains("اردیبهشت"))
                return s.Replace("اردیبهشت", "/02/");

            else if (s.Contains("خرداد"))
                return s.Replace("خرداد", "/03/");

            else if (s.Contains("تیر"))
                return s.Replace("تیر", "/04/");

            else if (s.Contains("مرداد"))
                return s.Replace("مرداد", "/05/");

            else if (s.Contains("شهریور"))
                return s.Replace("شهریور", "/06/");

            else if (s.Contains("مهر"))
                return s.Replace("مهر", "/07/");

            else if (s.Contains("آبان"))
                return s.Replace("آبان", "/08/");

            else if (s.Contains("آذر"))
                return s.Replace("آذر", "/09/");

            else if (s.Contains("دی"))
                return s.Replace("دی", "/10/");

            else if (s.Contains("بهمن"))
                return s.Replace("بهمن", "/11/");

            else if (s.Contains("اسفند"))
                return s.Replace("اسفند", "/12/");
           GetPersiaDays(s);
            return s;
        }
 
        public string GetPersiaDays(string s)
        {


            if (s.Contains("شنبه"))
                s = s.Replace("شنبه", String.Empty);

          else  if (s.Contains("یک شنبه"))
                    s = s.Replace("یک شنبه", String.Empty);

            else if (s.Contains("دو شنبه"))
                    s = s.Replace("دو شنبه", String.Empty);

            else if (s.Contains("سه شنبه"))
                    s = s.Replace("سه شنبه", String.Empty);

            else if (s.Contains("چهار شنبه"))
                    s = s.Replace("چهار شنبه", String.Empty);

            else if (s.Contains("پنج شنبه"))
                    s = s.Replace("پنج شنبه", String.Empty);

            else if (s.Contains("جمعه"))
                    s = s.Replace("جمعه", String.Empty);
         //   GetPersianmonth(s);
            return s;
        }
        private void label11_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            mf.RefreshPage();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            mf.RefreshPage();
            this.Close();

        }
        void DataFill()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = rbll.Read();
            dataGridView1.Columns["شناسه"].Visible = false;
            dataGridView1.Columns["انجام شده"].Visible = false;
            

        }
        void emptytexts()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            richTextBox1.Text = "";
            textBox1.Visible = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            User u1 = new User();
            u1 = mf.loggedinuser;
            if (ubll.Access(u1, "بخش یادآور", 2))
            {
                r.Title = textBox2.Text;
                r.ReminderInfo = richTextBox1.Text;
                r.RegDate = DateTime.Now;

                //    r.ReminderDate = dateTimePicker1.Value.Date;
                r.ReminderDate =Convert.ToDateTime( faDatePicker1.Text);
                m.myshowdialog("", rbll.Create(r, u), "", false, false);
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "تصویر کاربر انتخاب نشده است.", "شما اجازه ورود به این قسمت را ندارید", false, true);
            }
            DataFill();
            emptytexts();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            u = ubll.ReadU(textBox1.Text);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            User u1 = new User();
            u1 = mf.loggedinuser;
            if (ubll.Access(u1, "بخش یادآور", 4))
            {
                DialogResult dr = m.myshowdialog("اخطار", "درصورت حذف اطلاعات تمام اطلاعات مربوط به آن حذف خواهد شد.\n آیا از حذف اطمینان دارید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {

                    m.myshowdialog("حذف اطلاعات", rbll.Delete(id), "", false, false);
                }
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید","", false, true);
            }
            DataFill();
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
            if (ubll.Access(u1, "بخش یادآور", 3))
            {
                Reminder r;
                r = rbll.Read(id);
                textBox1.Text = r.Title;
                richTextBox1.Text = r.ReminderInfo;
                label3.Text = "ویرایش اطلاعات";
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            DataFill();
        }

        private void انجامشدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            User u1 = new User();
            u1 = mf.loggedinuser;
            DialogResult dr = m.myshowdialog("اخطار", " آیا از غیر فعال کردن یادآور اطمینان دارید؟", "", true, false);
            if (dr == DialogResult.Yes)
            {
                m.myshowdialog("حذف اطلاعات", rbll.IsDone(id), "", false, false);
            }
            DataFill();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = rbll.Search(textBox4.Text);
        }
    }
}
