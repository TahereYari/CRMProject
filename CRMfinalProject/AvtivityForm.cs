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
    public partial class AvtivityForm : Form
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
        public AvtivityForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

        }
        #region Definations
        Reminder r = new Reminder();
        Activity a = new Activity();
        User u = new User();
        ActivityCategory ac = new ActivityCategory();
        Customer c = new Customer();
        msgBox m = new msgBox();
        ActivityBLL abll = new ActivityBLL();
        ReminderBLL rbll = new ReminderBLL();
        CustomerBll cbll = new CustomerBll();
        UserBLL ubll = new UserBLL();
        ActivityCategoryBLL acbll = new ActivityCategoryBLL();
        int id;
        
        string de;
        #endregion

        #region Methods
        void datafill()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = abll.Read();
            dataGridView1.Columns["شناسه"].Visible = false;
            dataGridView1.Columns["جزییات"].Visible = false;
        }
        void emptytext()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            richTextBox1.Text = "";
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox5.Enabled = true;
        }
        #endregion

        

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region date
        public string GetPersianmonth(string s)
        {

            if (s.Contains("فروردین"))
                return s.Replace("فروردین", "/01/");

            else if (s.Contains("اردیبهشت"))
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

            else if (s.Contains("یک شنبه"))
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
        #endregion

        private void AvtivityForm_Load(object sender, EventArgs e)
        {
            datafill();
           
            #region AutoComplete

            AutoCompleteStringCollection phones = new AutoCompleteStringCollection();
            foreach (var item in cbll.ReadPhone())
            {
                phones.Add(item);
            }
            textBox2.AutoCompleteCustomSource = phones;

            //************************************************************************
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            foreach (var item in ubll.ReadUserName())
            {
                names.Add(item);
            }
            textBox1.AutoCompleteCustomSource = names;
            //************************************************************************
            AutoCompleteStringCollection CatName = new AutoCompleteStringCollection();
            foreach (var item in acbll.ReadName())
            {
                CatName.Add(item);
            }
            textBox5.AutoCompleteCustomSource = CatName;


            #endregion


        }
        User lu = new User();
        private void pictureBox1_Click(object sender, EventArgs e)
        {
          //  MainForm mf = new MainForm();
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            lu = mf.loggedinuser;
            a.Title = textBox3.Text;
            a.Info = richTextBox1.Text;
            a.RegDate = DateTime.Now.Date;
           
            if (ubll.Access(lu, "بخش فعالیت ها", 2))
            {
                m.myshowdialog("", abll.Create(a, u, c, ac), "", false, false);
                if (checkBox3.Checked)
                {
                    Reminder r = new Reminder();
                    r.Title = textBox3.Text;
                    r.ReminderInfo = richTextBox1.Text;


                    r.ReminderDate = dateTimePicker1.Value.Date;
                    r.RegDate = DateTime.Now.Date;

                    m.myshowdialog("", rbll.Create(r, u), "", false, false);
                }
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید","", false, true);
            }

            datafill();
            emptytext();


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["شناسه"].Value);
           de =  (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["جزییات"].Value).ToString();
        }

        #region ToolStripMenuItem
        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(lu, "بخش فعالیت ها", 3))
            {
                m.myshowdialog("", abll.Delete(id), "", false, false);
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید","", false, true);
            }
            datafill();
        }
        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Details d = new Details();
            Activity ac = abll.Read(id);
            d.ShowDialog();
            d.label1.Text = ac.ToString(); ;
        }
        #endregion

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            u = ubll.ReadU(textBox1.Text);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            c = cbll.ReadP(textBox2.Text);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            textBox5.Enabled = false;
           ac = acbll.ReadCat(textBox5.Text);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = abll.Search(textBox4.Text);
        }
    }
}
