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

namespace ST10109482_Prog2B.Windows
{
    /// <summary>
    /// Interaction logic for captureWindow.xaml
    /// </summary>
    public partial class captureWindow : Window
    {
        public captureWindow()
        {
            InitializeComponent();
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
