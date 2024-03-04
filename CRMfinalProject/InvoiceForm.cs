using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BE;
using BLL;
using System.Windows;
using System.Linq;
using Stimulsoft.Report;
using Stimulsoft.Report.Win;
using System.Globalization;


namespace CRMfinalProject
{

    public partial class InvoiceForm : Form
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
        public InvoiceForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

        }

        CustomerBll cbll = new CustomerBll();
        Customer c = new Customer();
        ProductBLL pbll = new ProductBLL();
        Product p = new Product();
        public List<Product> products = new List<Product>();
        int id;
        DataGridViewCheckBoxColumn ch = new DataGridViewCheckBoxColumn();
        void datafill1 ()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = products;
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["stock"].Visible = false;
            dataGridView1.Columns["DeleteStatus"].Visible = false;
            dataGridView1.Columns["Name"].HeaderText = "نام کالا";
            dataGridView1.Columns["Price"].HeaderText = "قیمت";
           
            //ch.ValueType = typeof(bool);
            //ch.Name = "checkdel";
            //ch.HeaderText = "حذف";
            //dataGridView1.Columns.Add(ch);


        }
        void gridFill()
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = ibll.Read();
            dataGridView2.Columns["شناسه"].Visible = false;
        }
        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            label6.Text = DateTime.Now.Date.ToString("yyyy/MM/dd");
            AutoCompleteStringCollection phones = new AutoCompleteStringCollection();
            foreach (var item in cbll.ReadPhone())
            {
                phones.Add(item);
            }
            textBox1.AutoCompleteCustomSource = phones;

            AutoCompleteStringCollection Names = new AutoCompleteStringCollection();
            foreach(var item in pbll.ReadName())
            {
                Names.Add(item);
            }
            textBox2.AutoCompleteCustomSource = Names;
            gridFill();
        }

        Invoice i = new Invoice();
        InvoiceBLL ibll = new InvoiceBLL();
        msgBox m = new msgBox();
        private void label17_Click(object sender, EventArgs e)
        {
           
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            mf.RefreshPage();
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            mf.RefreshPage();
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            c = cbll.ReadP(textBox1.Text);
            label10.Text = c.Name;
            label8.Text = c.Phone;

           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
          //  MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
          
            p = pbll.ReadN(textBox2.Text);
            products.Add(p);
           datafill1 ();
            string s = p.Name + "  به ارزش  " + p.Price.ToString("N0");
            listBox1.Items.Add(s);
            double sum = 0;
            //قیمت
            foreach (var item in products)
            {
                sum = sum + item.Price;
            }
            label2.Text = sum.ToString("N0");
            //مبلغ قابل پرداخت
           
            label13.Text = (sum - Convert.ToDouble(textBox3.Text)).ToString("N0");
            textBox3.Text = "0";
        }

       UserBLL ubll = new UserBLL();
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {

            
            User u1 = new User();
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            u1 = mf.loggedinuser;
            if (ubll.Access(u1, "بخش فاکتورها", 2))
            {
                i.RegDate = DateTime.Now; 

            if(checkBox1.Checked)
            {
                i.Ischeckout = true;
                i.CheckOutDate = DateTime.Now; 
            }
            else
            {
                i.Ischeckout = false;

            }
                StiReport sti = new StiReport();



            DialogResult res = m.myshowdialog("اطلاعیه",ibll.create(i,c,products,u1)+"آیا قصد چاپ فاکتور رادارید ؟","",true,false);
                if (res == DialogResult.Yes)
                {

                    sti.Load(@"G:\peractis of modhej\C#\CRMfinalProject\ReportInvoice.mrt");
                    sti.Dictionary.Variables["InvoiceNumber"].Value =ibll.ReadInvoiceNum();
                    sti.Dictionary.Variables["Date"].Value = label6.Text;
                    sti.Dictionary.Variables["Name"].Value =label10.Text;
                    sti.Dictionary.Variables["CustPhone"].Value =label8.Text;
                    sti.RegBusinessObject("Product",products);
                    sti.Render();
                    sti.Show();

                }

            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }

            gridFill();
            }
            catch (Exception)
            {

            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
  
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["شناسه"].Value);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User u1 = new User();
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            u1 = mf.loggedinuser;
            if (ubll.Access(u1, "بخش فاکتورها", 4))
            {
                DialogResult res = m.myshowdialog("حذف اطلاعات", "آیا از حذف اطلاعات اطمینان دارید؟", "", true, false);
            if(res==DialogResult.Yes)
            {
                m.myshowdialog("",ibll.Delete(id), "", false, false);
            }
            gridFill();

            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["شناسه"].Value);
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ibll.Ischecked(id))
            {
                //contextMenuStrip1.Items["tool1"].Enabled = true;
                m.myshowdialog("", "عملیات پرداخت با موفقیت انجام شد.", "", false, false);
            }
            else
            {
                //contextMenuStrip1.Items["tool1"].ForeColor = System.Drawing.Color.Red;

                //contextMenuStrip1.Items["tool1"].Enabled = false;

               m.myshowdialog("","فاکتور قبلا پرداخت شده است.", "", false, false);
            }
            gridFill();


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //List<DataGridViewRow> rows_with_checked_column = new List<DataGridViewRow>();
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    if (Convert.ToBoolean(row.Cells["checkdel"].Value) == true)
            //    {
            //        rows_with_checked_column.RemoveAt(row);
            //    }
            //}
            //DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            //ch1 = (DataGridViewCheckBoxCell)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["checkdel"];
            //if (Convert.ToBoolean(ch1.Value))
            //{
            //    dataGridView1.Rows.RemoveAt(ch1.RowIndex);
            //}
            //dataGridView1.DataSource = null;
            //dataGridView1.DataSource = products;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
