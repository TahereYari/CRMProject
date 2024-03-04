using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;

using System.Windows.Media.Effects;
using BE;
using BLL;


namespace CRMfinalProject
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
       
    public MainForm()
        {
           
            InitializeComponent();

        }
        public User loggedinuser = new User();

        msgBox m = new msgBox();
        
        void openform(Form f)
        {
            Window w = (Window)this.FindName("Main") as Window;
            BlurEffect blur = new BlurEffect();
            blur.Radius=20;
            this.Effect = blur;

            f.ShowDialog();

            blur.Radius = 0;
            this.Effect = blur;

        }
       
        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }
        DashbordBLL dbll = new DashbordBLL();
       public void RefreshPage()
        {
            usernametxt.Text = loggedinuser.UserName;
            personnametxt.Text = loggedinuser.Name;
            remindercounttxt.Text = dbll.UserREminderCount(loggedinuser);
            Cust1count.Text = dbll.CustomrCount();
         //   sellcount.Text = dbll.SellCount();

            int a = 0;
            foreach(var item in dbll.GetsersReminders(loggedinuser))
            {
                if(a<7)
                {
                    ReminderUC ruc = new ReminderUC();
                    ruc.remindertitle.Text = item.Title;
                    ruc.reminderinfo.Text = item.ReminderInfo;
                    Grid.SetRow(ruc, 5 + a);
                    Grid.SetColumnSpan(ruc, 6);
                    MainGrid.Children.Add(ruc);
                    a++;
                }
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(loggedinuser, "بخش یادآور", 1))
            {

                ReminderForm rf = new ReminderForm();
                openform(rf);
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید.", "", false, true);
            }
              //  RefreshPage();



            }
       
        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(loggedinuser, "پنل پیامکی", 1))
            {
                SmsPanel ap = new SmsPanel();
                openform(ap);
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید.", "", false, true);
            }
          //  RefreshPage();

        }



        private void TextBlock_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(loggedinuser, "بخش کالاها", 1))
            {
                ProductForm pf = new ProductForm();
                openform(pf);
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید.", "", false, true);
            }
        //    RefreshPage();

        }

        private void TextBlock_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(loggedinuser, "بخش فعالیت ها", 1))
            {
                AvtivityForm af = new AvtivityForm();
                openform(af);
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید.", "", false, true);
            }
          //  RefreshPage();



        }
        UserBLL ubll = new UserBLL();
        private void TextBlock_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            if(ubll.Access(loggedinuser, "بخش مشتریان", 1))
            {
                CustomerForm cf = new CustomerForm();
                openform(cf);
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید.","", false, true);
            }
           // RefreshPage();

        }

        private void TextBlock_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            
            if (ubll.Access(loggedinuser, "بخش فاکتورها", 1))
            {
                InvoiceForm i = new InvoiceForm();
                openform(i);
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید.", "", false, true);
            }
          //  RefreshPage();
           
        }

        private void TextBlock_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            //if (ubll.Access(loggedinuser, "بخش کاربران", 1))
            //{
             UserForm u = new UserForm();
                openform(u);
            //}
            //else
            //{
            //    m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید.", "", false, true);
            //}

           // RefreshPage();


        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            msgBox m = new msgBox();
            m.myshowdialog("خروج", "برای خروج دکمه خروج را بزنید", "", false, false);

        }

        private void TextBlock_MouseLeftButtonDown_7(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextBlock_MouseLeftButtonDown_8(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextBlock_MouseLeftButtonDown_9(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(loggedinuser, "بخش تنظیمات", 1))
            {
                CategoryForm f = new CategoryForm();
                openform(f);
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید.", "", false, true);
            }
           // RefreshPage();


        }

        private void TextBlock_MouseLeftButtonDown_10(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(loggedinuser, "بخش گزارشات", 1))
            {
                ReportForm rf = new ReportForm();
                openform(rf);
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید.", "", false, true);
            }
        }
    }
}
