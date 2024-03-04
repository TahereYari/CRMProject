using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using System.Windows;


namespace CRMfinalProject
{
    public partial class UCLogin : UserControl
    {
        public UCLogin()
        {
            InitializeComponent();
        }
        msgBox m = new msgBox();
        UserBLL ubll = new UserBLL();
        User u = new User();
        DashbordBLL dbll = new DashbordBLL();
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
            u = ubll.Login(textBox1.Text, textBox2.Text);
            if(u!=null)
           {
                m.myshowdialog("خوش آمدید", "برای ورود  کلیک کنید.", "", false, false);
               

              //MainForm w = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
                 
               MainForm w = new MainForm();
                    w.Show();
                  w.loggedinuser = u;
                w.RefreshPage();

              
                  ((LoginForm)System.Windows.Forms.Application.OpenForms["LoginForm"]).Close();
            }
            else
            {
                m.myshowdialog("اخطار", "رمز عبور با نام کاربر اشتباه است.", "", false, true);
            }


            textBox1.Text = "";
            textBox2.Text = "";

        }

     

      

       
    }
}
