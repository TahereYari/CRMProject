using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;

namespace CRMfinalProject
{
    public class msgBox
    {
        public DialogResult myshowdialog(string title,string fainfo, string enginfo, bool buttons,bool type)
        {
            MyMsgBox m = new MyMsgBox();
            m.label3.Text = title;
            m.label1.Text = fainfo;
            m.label2.Text = enginfo;
            if(buttons)
            {
                m.button2.Text = "خیر";
            }
            else
            {
                m.button1.Visible = false;
            }

            if (type)
            {
                m.BackColor = Color.FromArgb(242, 69, 29);
                m.pictureBox1.Image = Properties.Resources.add;
            }
            m.ShowDialog();
            return m.DialogResult;
            


        }
    }
}
