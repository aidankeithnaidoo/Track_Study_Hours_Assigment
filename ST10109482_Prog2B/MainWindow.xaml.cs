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
using System.Media;


namespace ST10109482_Prog2B
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Creation of datat strcutres
        private List<Module> moduleList = new List<Module>();
        private List<RecordData> records = new List<RecordData>();
        private Dictionary<int, List<Module>> ModuleInfo = new Dictionary<int, List<Module>>();
        private Dictionary<int, List<RecordData>> recordInfo = new Dictionary<int, List<RecordData>>();
        private Dictionary<string, weekTracker> weekTrack = new Dictionary<string, weekTracker>();
        private Dictionary<int, double> weekInfo = new Dictionary<int, double>();
        private Dictionary<int, double> tempInfo = new Dictionary<int, double>();

        public MainWindow()
        {
            InitializeComponent();
            // Default constructor for MainWindow
        }

        public MainWindow(List<Module> PmoduleList, Dictionary<int, List<Module>> PModuleInf, List<RecordData> Precords, Dictionary<int, List<RecordData>> PrecordInfo, Dictionary<string, weekTracker> PweekTrack, Dictionary<int, double> PweekInfo)
        {
            InitializeComponent();
            // Constructor that accepts data to populate the window
            ModuleInfo = PModuleInf;
            records = Precords;
            recordInfo = PrecordInfo;
            moduleList = PmoduleList;
            weekTrack = PweekTrack;
            weekInfo = PweekInfo;
        }

        // Event handler for closing the MainWindow
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Event handler for adding a course (opens a new window)
        private void addCourseSwitch_click(object sender, RoutedEventArgs e)
        {
            captureWindow cw = new captureWindow(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close();
            cw.Show();
        }

        // Event handler for displaying course data (opens a new window)
        private void display_click(object sender, RoutedEventArgs e)
        {
            display ds = new display(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close();
            ds.Show();
        }

        // Event handler for updating study hours (opens a new window)
        private void addHours_click(object sender, RoutedEventArgs e)
        {
            updateStudyTime updateStudyTime = new updateStudyTime(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close();
            updateStudyTime.Show();
        }

        // Event handlers for buttons that may trigger sounds (placeholders)
        private void firstBtn_Click(object sender, RoutedEventArgs e)
        {
            // First sound logic goes here
        }

        private void secondBtn_Click(object sender, RoutedEventArgs e)
        {
            // Second sound logic goes here
        }

        private void thirdBtn_Click(object sender, RoutedEventArgs e)
        {
            // Third sound logic goes here
        }

        private void forthBtn_Click(object sender, RoutedEventArgs e)
        {
            // Fourth sound logic goes here
        }

        private void home_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
