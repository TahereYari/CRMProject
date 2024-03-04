using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoxLearn.License;
using BE;
using BLL;
using System.IO;
using System.Globalization;

namespace CRMfinalProject
{
    public partial class RegisterAdmin : UserControl
    {
        public RegisterAdmin()
        {
            InitializeComponent();
        }
        
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        Timer t1 = new Timer();
        void SwitchPanel()
        {
            t1.Enabled = true;
            t1.Interval = 15;
            t1.Tick += timer1_Tick;
            t1.Start();

        }
        private void RegisterAdmin_Load(object sender, EventArgs e)
        {
            textBox1.Text = ComputerInfo.GetComputerId();
        }
        msgBox m = new msgBox();
      
        
        UserBLL ubll = new UserBLL();
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(textBox1.Text);
            string productKey = textBox2.Text;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    LicenseInfo lic = new LicenseInfo();
                    lic.ProductKey = productKey;
                    lic.FullName = "Personal accounting";
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        lic.Day = kv.Expiration.Day;
                        lic.Month = kv.Expiration.Month;
                        lic.Year = kv.Expiration.Year;
                    }

                    km.SaveSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), lic);
                    m.myshowdialog("اطلاعیه", "نرم افزار با موفقیت فعالسازی شد.", "", false,false);
                  //  MessageBox.Show("You have been successfully registered.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SwitchPanel();
                }
            }
            else
                //  MessageBox.Show("Your product key is invalid.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m.myshowdialog("اخطار", "کد وارد شده نا معتبر است.", "", false, true);
        }

        int y = 331;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(panel1.Location.Y<525)
            {
              
                y = y +15;
                panel1.Location = new Point(73,y);
            }
            else
            {
                t1.Stop();
                panel2.Visible = true;
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            User u = new User();
            CreateAdminGroup();
            u.Name = textBox4.Text;
            u.UserName = textBox3.Text;
           
            if (textBox6.Text == textBox5.Text)
            {
                u.Password = textBox5.Text;
            }
            else
            {
                m.myshowdialog("اخطار", "تکرار کلمه عبور نادرست است.", "", false, false);
             
            }
            //if (u.Pic != null)
            //{
            //    m.myshowdialog("", "ubll.Create(u, ug)", "", false, false);
            //}

            //else
            //{
            //    m.myshowdialog("", "تصویر کاربر انتخاب نشده است.", "", false, false);
            //}

            u.Pic = SavePic(textBox3.Text);
            u.RegDate = DateTime.Now.Date;
            m.myshowdialog("اطلاعیه", ubll.Create(u, ugbll.ReadN("مدیریت")), "", false, false);
            this.Visible = false;
            ((LoginForm)Application.OpenForms["LoginForm"]).LoadLoginForm();

            


        }
        OpenFileDialog ofp = new OpenFileDialog();
       
        UserGroupBLL ugbll = new UserGroupBLL();
        string SavePic(string UserName)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\userpic\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string PicName = UserName + ".jpg";
            try
            {
                string picpath = ofp.FileName;
                File.Copy(picpath, path + PicName, true);
            }
            catch (Exception e)
            {
                m.myshowdialog("اخطار", "سیستم قادر به ذخیره عکس نمیباشد:\n" + e.Message, "", false, false);
              
            }
            return path + PicName;
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Image Pic;
            ofp.Filter = "JPG(*.JPG)|*.JPG";
            ofp.Title = "تصویر کاربر  را انتخاب کنید.";
            if (ofp.ShowDialog() == DialogResult.OK)
            {
                Pic = Image.FromFile(ofp.FileName);
               
            }
        }
        AccessRole FillUserAccessRole(string section, bool CanEnter, bool CanCreate, bool CanUpdate, bool CanDelete)
        {
            AccessRole ar = new AccessRole();
            ar.Section = section;
            ar.CanEnter = CanEnter;
            ar.CanCreate = CanCreate;
            ar.CanUpdate = CanUpdate;
            ar.CanDelete = CanDelete;
            return ar;
        }
        void CreateAdminGroup()
        {
            UserGroup ug = new UserGroup();
            ug.title = "مدیریت";
            ug.AccessUserGroups.Add(FillUserAccessRole("بخش مشتریان", true,true,true,true));
            ug.AccessUserGroups.Add(FillUserAccessRole("بخش کالاها", true, true, true, true));
            ug.AccessUserGroups.Add(FillUserAccessRole("بخش فاکتورها", true, true, true, true));
            ug.AccessUserGroups.Add(FillUserAccessRole("بخش فعالیت ها", true, true, true, true));
            ug.AccessUserGroups.Add(FillUserAccessRole("بخش یادآور", true, true, true, true));
            ug.AccessUserGroups.Add(FillUserAccessRole("بخش کاربران", true, true, true, true));
            ug.AccessUserGroups.Add(FillUserAccessRole("پنل پیامکی", true, true, true, true));
            ug.AccessUserGroups.Add(FillUserAccessRole("بخش گزارشات", true, true, true, true));
            ug.AccessUserGroups.Add(FillUserAccessRole("بخش تنظیمات", true, true, true, true));
            ugbll.Create(ug);

        }
    }
}
