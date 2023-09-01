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
        List<RecordData> records = new List<RecordData>();
        private Dictionary<int, List<Module>> ModuleInfo = new Dictionary<int, List<Module>>();
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(Dictionary<int, List<Module>> ModuleInfo, List<RecordData> records)
        {
            InitializeComponent();
            this.ModuleInfo = ModuleInfo;
            this.records = records;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addCourseSwitch_click(object sender, RoutedEventArgs e)
        {
            captureWindow cw = new captureWindow(ModuleInfo, records);
            this.Close();
            cw.Show();
        }

        

        private void display_click(object sender, RoutedEventArgs e)
        {
            display ds = new display(ModuleInfo,records);
            this.Close();
            ds.Show();
        }

        private void addHours_click(object sender, RoutedEventArgs e)
        {
            updateStudyTime updateStudyTime = new updateStudyTime(ModuleInfo,records);
            this.Close();
            updateStudyTime.Show();
        }
    }
}
