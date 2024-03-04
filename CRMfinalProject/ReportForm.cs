using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Stimulsoft.Report;
using BE;
using DAL;
using BLL;

namespace CRMfinalProject
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        msgBox m = new msgBox();
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            try
            {
            StiReport sti = new StiReport();
            if (radioButton4.Checked)
            {
                sti.Load(@"G:\peractis of modhej\C#\CRMfinalProject\factorReport.mrt");
                sti.Render();
                sti.Show();
            }
            if(radioButton3.Checked)
            {
                sti.Load(@"G:\peractis of modhej\C#\CRMfinalProject\Reportweek.mrt");
                sti.Render();
                sti.Show();
            }

            if(radioButton5.Checked)
             {
                sti.Load(@"G:\peractis of modhej\C#\CRMfinalProject\Reportyear.mrt");
                sti.Render();
                sti.Show();
            }
            if(radioButton1.Checked)
                {
                    sti.Load(@"G:\peractis of modhej\C#\CRMfinalProject\ReportCustN.mrt");
                    sti.Render();
                    sti.Show();
                }

            if(radioButton2.Checked)
                {
                    sti.Load(@"G:\peractis of modhej\C#\CRMfinalProject\ReportActUserReg.mrt");
                    sti.Render();
                    sti.Show();
                }
            }
            catch (Exception)
            {
                m.myshowdialog("اطلاعیه", "لطفا یکی از موارد را انتخاب کنید.", "", false, false);
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        UserDAL udal = new UserDAL();
        UserBLL ubll = new UserBLL();
        CustomerBll cbll = new CustomerBll();
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.Clear();
            if (radioButton8.Checked)
            {
                foreach (var item in ubll.ReadInvoices())
                {
                    int x = 0;
                    foreach (var q in item.Invoices)
                    {
                        if (q.RegDate > dateTimePicker1.Value && q.RegDate.Date < dateTimePicker2.Value)
                        {
                            x++;
                        }

                    }
                    chart1.Series["Series1"].Points.AddXY(item.Name, x);
                }
            }

            //**********************************************
            if (radioButton10.Checked)
            {

                foreach (var item in ubll.RegCustomer())
                {
                    int x = 0;
                    foreach (var q in item.Customers)
                    {
                        if (q.RegDate.Date > dateTimePicker1.Value && q.RegDate.Date < dateTimePicker2.Value)
                        {
                            x++;
                        }

                    }
                    chart1.Series["Series1"].Points.AddXY(item.Name, x);
                }
            }
            //********************************
            if (radioButton9.Checked)
            {

                foreach (var item in ubll.RegActivity())
                {
                    int x = 0;
                    foreach (var q in item.Activities)
                    {
                        if (q.RegDate.Date > dateTimePicker1.Value && q.RegDate.Date < dateTimePicker2.Value)
                        {
                            x++;
                        }

                    }
                    chart1.Series["Series1"].Points.AddXY(item.Name, x);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
