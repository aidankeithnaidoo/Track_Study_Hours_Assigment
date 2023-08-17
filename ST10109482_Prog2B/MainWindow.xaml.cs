using ST10109482_Prog2B.Setup;
using ST10109482_Prog2B.Windows;
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

namespace ST10109482_Prog2B
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Module> moduleList = new List<Module>();
        private Dictionary<int, List<Module>> ModuleInfo = new Dictionary<int, List<Module>>();
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(Dictionary<int, List<Module>> ModuleInfo)
        {
            InitializeComponent();
            this.ModuleInfo = ModuleInfo;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addCourseSwitch_click(object sender, RoutedEventArgs e)
        {
            captureWindow cw = new captureWindow(ModuleInfo);
            this.Close();
            cw.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var kvp in ModuleInfo)
            {
                foreach (var module in kvp.Value)
                {
                    stringBuilder.AppendLine($"Credits: {kvp.Key}");
                    stringBuilder.AppendLine($"Module Code: {module.ModuleCode}");
                    stringBuilder.AppendLine($"Module Name: {module.ModuleName}");
                    // Append other module properties...
                    stringBuilder.AppendLine(); // Add an empty line between modules
                }
            }

            testDisplay.Text = stringBuilder.ToString();
        }
    }
}
