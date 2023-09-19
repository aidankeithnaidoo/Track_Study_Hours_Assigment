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
using studyPlanner_dll;

namespace ST10109482_Prog2B.Windows
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        List<RecordData> records = new List<RecordData>();
        public List<Module> moduleList = new List<Module>();

        private Dictionary<int, List<RecordData>> recordInfo = new Dictionary<int, List<RecordData>>();
        private Dictionary<int, List<Module>> ModuleInfo = new Dictionary<int, List<Module>>();

        public LoginPage()
        {
            InitializeComponent();
        }

        public LoginPage(Dictionary<int, List<studyPlanner_dll.Module>> PModuleInf, List<studyPlanner_dll.RecordData> Precords, Dictionary<int, List<studyPlanner_dll.RecordData>> PrecordInfo)
        {
            InitializeComponent();
            recordInfo = PrecordInfo;
            //this.moduleList = moduleList;
            records = Precords; 
            ModuleInfo = PModuleInf;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }
    }
}
