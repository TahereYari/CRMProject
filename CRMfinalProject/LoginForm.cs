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
using DAL;
using BLL;



namespace CRMfinalProject
{
    public partial class LoginForm : Form
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
        public LoginForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            //panel1.Controls.Add(ra);
            //panel1.Controls["RegisterAdmin"].Location = new Point(110, 15);
            //panel1.Controls.Add(ucl);
            //panel1.Controls["UCLogin"].Location = new Point(110, 15);
        }

  

        #region Definations
        Timer t1 = new Timer();
        Timer t2 = new Timer();
        List<string> usernames = new List<string>();
        RegisterAdmin ra = new RegisterAdmin();
        UCLogin ucl = new UCLogin();
        UserBLL ubll = new UserBLL(); 
        bool IsRegister;

       #endregion

        private void LodinForm_Load(object sender, EventArgs e)
        {
            label2.Visible = false;
            t1.Enabled = true;
            t1.Interval = 15;
            t1.Tick += timer1_Tick;
            t1.Start();
            //if(panel1.Controls.Count>0)
            //{
            //    panel1.Controls[0].de;

            //}
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(progressBar1.Value>=100)
            {
                t1.Stop();
                progressBar1.Visible = false;
                label2.Visible = false;
              
                t2.Enabled = true;
                t2.Interval = 1;
                t2.Tick += timer2_Tick;
                t2.Start();
            }
            else if(progressBar1.Value==45)
            {
                label3.Visible = true;
                IsRegister = ubll.IsRegisterd();
                progressBar1.Value++;
            }
            else
            {
                progressBar1.Value++;
                label2.Visible = false;
            }

        }
        int y = 283;
        int y2 = 623;
        int y3 = 623;
        Timer t3 = new Timer();
        public void LoadLoginForm()
        {
            t3.Enabled = true;
            t3.Interval = 30;
            t3.Tick += timer3_Tick;
            t3.Start();
            
        }
        private void timer2_Tick(object sender, EventArgs e)
        {


            //if (label3.Location.Y >= 45)
            //{
            //    y = y - 15;
            //    y2 = y2 - 30;
            //    label3.Location = new Point(271, y);
            //    //    //if (__IsRegister)
            //    //    //{
            //    this.Controls["RegisterAdmin"].Location = new Point(110, y2);

            //}

            //else
            //{
            //    panel1.Visible = true;
            //    t2.Stop();
            //}
            //  label3.Location = new Point(271, 45);
            //label2.Visible = false;
            //panel1.Visible = false;
            

            if (IsRegister)
            {

              
               
                panel1.Controls.Add(ucl);
                panel1.Controls["UCLogin"].Location = new Point(115, 15);
                t2.Stop();



            }
            else
            {
                panel1.Controls.Add(ra);

                panel1.Controls["RegisterAdmin"].Location = new Point(115, 15);
                t2.Stop();

            }
           


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            //if(panel1.Controls["UCLogin"].Location.Y >= 15)
            //{
            //    y3 = y3 - 30;
                panel1.Controls["UCLogin"].Location = new Point(115, 15);
            //}
            //else
            //{
                t3.Stop();
          //  }
        }
    }

        

        
    
}
