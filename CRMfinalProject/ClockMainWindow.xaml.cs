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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Forms;

namespace CRMfinalProject
{
    /// <summary>
    /// Interaction logic for ClockMainWindow.xaml
    /// </summary>
    public partial class ClockMainWindow : System.Windows.Controls.UserControl
    {
        public ClockMainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            ClockText.Text = DateTime.Now.ToString();
        }
     
        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
