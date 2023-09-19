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
using studyPlanner_dll;

namespace ST10109482_Prog2B
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Module> moduleList = new List<Module>();
        private List<RecordData> records = new List<RecordData>();
        private Dictionary<int, List<Module>> ModuleInfo = new Dictionary<int, List<Module>>();
        private Dictionary<int, List<RecordData>> recordInfo = new Dictionary<int, List<RecordData>>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(List<Module> PmoduleList,Dictionary<int, List<Module>> PModuleInf, List<RecordData> Precords,Dictionary<int, List<RecordData>> PrecordInfo)
        {
            InitializeComponent();
            ModuleInfo = PModuleInf;
            records = Precords;
            recordInfo = PrecordInfo;
            moduleList = PmoduleList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addCourseSwitch_click(object sender, RoutedEventArgs e)
        {
            captureWindow cw = new captureWindow(moduleList, ModuleInfo, records, recordInfo);
            this.Close();
            cw.Show();
        }

        

        private void display_click(object sender, RoutedEventArgs e)
        {
            display ds = new display(moduleList, ModuleInfo,records, recordInfo);
            this.Close();
            ds.Show();
        }

        private void addHours_click(object sender, RoutedEventArgs e)
        {
            updateStudyTime updateStudyTime = new updateStudyTime(moduleList, ModuleInfo,records, recordInfo);
            this.Close();
            updateStudyTime.Show();
        }
    }
}
