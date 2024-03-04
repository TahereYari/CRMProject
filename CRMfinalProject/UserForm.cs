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
using System.IO;
using BE;
using BLL;
using System.Windows;
using System.Globalization;

namespace CRMfinalProject
{
    public partial class UserForm : Form
    {
        #region Round
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
        public UserForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

        }
        #endregion

        #region Definations

        User u = new User();
        UserBLL ubll = new UserBLL();
        int id;
        
        msgBox m = new msgBox();
        UserGroupBLL ugbll = new UserGroupBLL();
        OpenFileDialog ofp = new OpenFileDialog();
        UserGroup ug = new UserGroup();
        MainForm mf = new MainForm();

        #endregion

        #region Methods

        void DataFill()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ubll.Read();
            dataGridView1.Columns["شناسه"].Visible = false;
        }
        void emptytext()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        


        }
        string SavePic(string UserName)
        {
            string path = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\userpic\";
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
                System.Windows.Forms.MessageBox.Show("سیستم قادر به ذخیره عکس نمیباشد:\n" + e.Message);
            }
            return path + PicName;
        }
       
        AccessRole FillUserAccessRole(string section,bool CanEnter, bool CanCreate, bool CanUpdate, bool CanDelete)
        {
            AccessRole ar = new AccessRole();
            ar.Section = section;
            ar.CanEnter = CanEnter;
            ar.CanCreate = CanCreate;
            ar.CanUpdate = CanUpdate;
            ar.CanDelete = CanDelete;
            return ar;
        }
        #endregion

        #region pictureBox
        private void pictureBox3_Click(object sender, EventArgs e)
        {

            Image Pic;
            ofp.Filter = "JPG(*.JPG)|*.JPG";
            ofp.Title = "تصویر کاربر  را انتخاب کنید.";
            if (ofp.ShowDialog() == DialogResult.OK)
            {
                Pic = Image.FromFile(ofp.FileName);
                pictureBox3.Image = Pic;
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            User u1 = new User();
            u1 = mf.loggedinuser;
            u.Name = textBox1.Text;
            u.UserName = textBox2.Text;
           ug = ugbll.ReadByTitle(textBox5.Text);
            if (textBox3.Text == textBox4.Text)
            {
                u.Password = textBox4.Text;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("تکرار کلمه عبور نادرست است.");
            }
            u.RegDate = DateTime.Now;
            if (label3.Text == "ثبت اطلاعات")

             {
                if (ubll.Access(u1, "بخش کاربران", 2))
                {

                        u.Pic = SavePic(textBox2.Text);
                        if (u.Pic != null)
                        {
                            m.myshowdialog("", ubll.Create(u, ug), "", false, false);
                        }
                        else
                        {
                            m.myshowdialog("", "تصویر کاربر انتخاب نشده است.", "", false, false);
                        }
               }
                else
                    {
                    m.myshowdialog("محدودیت دسترسی", "تصویر کاربر انتخاب نشده است.", "شما اجازه ورود به این قسمت را ندارید", false, true);
                    }

                }
            else
            {
                
                    u.Pic = SavePic(textBox2.Text);
                m.myshowdialog("ویرایش اطلاعات",ubll.Update(u, id),"",false,false);
                label3.Text = "ثبت اطلاعات";
              


            }
            pictureBox3.Image = Properties.Resources.add;
            DataFill();
            emptytext();



        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void User_Load(object sender, EventArgs e)
        {
            DataFill();
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            foreach (var item in ugbll.ReadGroup())
            {
                names.Add(item);
            }
            textBox5.AutoCompleteCustomSource = names;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        #region ToolStripMenuItem

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            User u1 = new User();
            u1 = mf.loggedinuser;
            //if (ubll.Access(u1, "بخش کاربران", 3))
            //{

                u = ubll.Read(id);
            textBox1.Text = u.Name;
            textBox2.Text = u.UserName;
            // textBox3.Text = u.Password;
            pictureBox3.Image = Image.FromFile(u.Pic);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

            label3.Text = "ویرایش اطلاعات";
            //}
            //else
            //{
            //    m.myshowdialog("محدودیت دسترسی",  "شما اجازه ورود به این قسمت را ندارید", "",false, true);
            //}


        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm mf = (MainForm)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            User u1 = new User();
            u1 = mf.loggedinuser;
            if (ubll.Access(u1, "بخش کاربران", 3))
            {
                DialogResult dr = m.myshowdialog("", "آیا از حذف گاربر اطمینان دارید؟", "", true, false);
                if(dr==DialogResult.Yes)
                {
                    m.myshowdialog("حذف کاربر", ubll.Delete(id), "", false, false);
                }           
            }
            else
            {
                m.myshowdialog("محدودیت دسترسی",  "شما اجازه ورود به این قسمت را ندارید","", false, true);
            }
            DataFill();
        }
        #endregion


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id =Convert.ToInt32 (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["شناسه"].Value);
        }

      

        #region checkBox
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox6.Checked = true;
                checkBox10.Checked = true;
                checkBox14.Checked = true;
                checkBox18.Checked = true;
                checkBox22.Checked = true;
                checkBox26.Checked = true;
                checkBox30.Checked = true;
                checkBox34.Checked = true;
                checkBox38.Checked = true;


            }
            else
            {
                checkBox6.Checked = false;
                checkBox10.Checked = false;
                checkBox14.Checked = false;
                checkBox18.Checked = false;
                checkBox22.Checked = false;
                checkBox26.Checked = false;
                checkBox30.Checked = false;
                checkBox34.Checked = false;
                checkBox38.Checked = false;

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox8.Checked = true;
                checkBox12.Checked = true;
                checkBox16.Checked = true;
                checkBox20.Checked = true;
                checkBox24.Checked = true;
                checkBox28.Checked = true;
                checkBox32.Checked = true;
                checkBox36.Checked = true;
                checkBox40.Checked = true;


            }
            else
            {
                checkBox8.Checked = false;
                checkBox12.Checked = false;
                checkBox16.Checked = false;
                checkBox20.Checked = false;
                checkBox24.Checked = false;
                checkBox28.Checked = false;
                checkBox32.Checked = false;
                checkBox36.Checked = false;
                checkBox40.Checked = false;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked)
            {
                checkBox7.Checked = true;
                checkBox11.Checked = true;
                checkBox15.Checked = true;
                checkBox19.Checked = true;
                checkBox23.Checked = true;
                checkBox27.Checked = true;
                checkBox31.Checked = true;
                checkBox35.Checked = true;
                checkBox39.Checked = true;


            }
            else
            {
                checkBox7.Checked = false;
                checkBox11.Checked = false;
                checkBox15.Checked = false;
                checkBox19.Checked = false;
                checkBox23.Checked = false;
                checkBox27.Checked = false;
                checkBox31.Checked = false;
                checkBox35.Checked = false;
                checkBox39.Checked = false;

            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox4.Checked)
            {
                checkBox5.Checked = true;
                checkBox9.Checked = true;
                checkBox13.Checked = true;
                checkBox17.Checked = true;
                checkBox21.Checked = true;
                checkBox25.Checked = true;
                checkBox29.Checked = true;
                checkBox33.Checked = true;
                checkBox37.Checked = true;


            }
            else
            {
                checkBox5.Checked = false;
                checkBox9.Checked = false;
                checkBox13.Checked = false;
                checkBox17.Checked = false;
                checkBox21.Checked = false;
                checkBox25.Checked = false;
                checkBox29.Checked = false;
                checkBox33.Checked = false;
                checkBox37.Checked = false;

            }
        }
        #endregion

        
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            UserGroup ug = new UserGroup();
            ug.title = textBox6.Text;
            ug.AccessUserGroups.Add(FillUserAccessRole(label17.Text,checkBox6.Checked, checkBox8.Checked, checkBox7.Checked, checkBox5.Checked));
            ug.AccessUserGroups.Add(FillUserAccessRole(label18.Text, checkBox10.Checked, checkBox12.Checked, checkBox11.Checked, checkBox9.Checked));
            ug.AccessUserGroups.Add(FillUserAccessRole(label19.Text, checkBox14.Checked, checkBox16.Checked, checkBox15.Checked, checkBox13.Checked));
            ug.AccessUserGroups.Add(FillUserAccessRole(label20.Text, checkBox18.Checked, checkBox20.Checked, checkBox19.Checked, checkBox17.Checked));
            ug.AccessUserGroups.Add(FillUserAccessRole(label21.Text, checkBox22.Checked, checkBox24.Checked, checkBox23.Checked, checkBox21.Checked));
            ug.AccessUserGroups.Add(FillUserAccessRole(label22.Text, checkBox26.Checked, checkBox28.Checked, checkBox27.Checked, checkBox25.Checked));
            ug.AccessUserGroups.Add(FillUserAccessRole(label23.Text, checkBox30.Checked, checkBox32.Checked, checkBox31.Checked, checkBox29.Checked));
            ug.AccessUserGroups.Add(FillUserAccessRole(label24.Text, checkBox34.Checked, checkBox36.Checked, checkBox35.Checked, checkBox33.Checked));
            ug.AccessUserGroups.Add(FillUserAccessRole(label25.Text, checkBox38.Checked, checkBox40.Checked, checkBox39.Checked, checkBox37.Checked));
            m.myshowdialog("ثبت شد",ugbll.Create(ug),"",false,false);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
