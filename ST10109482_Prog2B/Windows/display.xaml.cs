using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Interaction logic for display.xaml
    /// </summary>
    public partial class display : Window
    {
        private List<RecordData> records = new List<RecordData>();
        private List<Module> moduleList = new List<Module>();
        private Dictionary<int, List<Module>> ModuleInfo = new Dictionary<int, List<Module>>();
        private Dictionary<int, List<RecordData>> recordInfo = new Dictionary<int, List<RecordData>>();
        private ObservableCollection<string> moduleCode = new ObservableCollection<string>();
        private Dictionary<string, weekTracker> weekTrack = new Dictionary<string, weekTracker>();
        private Dictionary<int, double> weekInfo = new Dictionary<int, double>();
        private Dictionary<int, double> tempInfo = new Dictionary<int, double>();

        public display()
        {
            InitializeComponent();
            addToCombo();
            moduleCodeCombo.ItemsSource = moduleCode;
            // Default constructor for the display window
        }

        public display(List<Module> PmoduleList, Dictionary<int, List<Module>> PModuleInf, List<RecordData> Precords, Dictionary<int, List<RecordData>> PrecordInfo, Dictionary<string, weekTracker> PweekTrack, Dictionary<int, double> PweekInfo)
        {
            InitializeComponent();
            ModuleInfo = PModuleInf;
            records = Precords;
            recordInfo = PrecordInfo;
            moduleList = PmoduleList;
            weekTrack = PweekTrack;
            weekInfo = PweekInfo;
            addToCombo();
            moduleCodeCombo.ItemsSource = moduleCode;
            // Constructor that accepts data to populate the display window
        }

        private void displayBtn_Click(object sender, RoutedEventArgs e)
        {
           
                string enteredModule = moduleCodeCombo.SelectedValue.ToString();
                displaySelected(enteredModule);
           
        }

        private void addCourseSwitch_click(object sender, RoutedEventArgs e)
        {
            captureWindow cw = new captureWindow(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close();
            cw.Show();
            // Event handler for adding a course (opens a new window)
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close();
            mw.Show();
            // Event handler for returning to the main window
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
            // Event handler for closing the display window
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Placeholder event handler
        }

        private void addHours_click(object sender, RoutedEventArgs e)
        {
            updateStudyTime upt = new updateStudyTime(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close();
            upt.Show();
            // Event handler for updating study hours (opens a new window)
        }

        private void displayAllBtn_Click(object sender, RoutedEventArgs e)
        {
            displayAll();
            // Event handler for displaying all module information
        }

        public void addToCombo()
        {
            foreach (var item in moduleList)
            {
                moduleCode.Add(item.ModuleCode.ToUpper());
            }
            // This method populates the 'moduleCode' list with uppercase module codes from 'moduleList'.
        }

        public void displaySelected(string moduleCode)
        {
            StringBuilder stringBuilder = new StringBuilder();
            updateStudyTime update = new updateStudyTime(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);

            var moduleInfoQuery = ModuleInfo
                                 .Where(kvp => kvp.Value.Any(moduleList => moduleList.ModuleCode == moduleCode))
                                 .SelectMany(kvp => kvp.Value);

            double selfHour = update.GetSelfHours(moduleCode);

            update.addWeeks(moduleCode, selfHour, getStartDate(moduleCode));

            foreach (var module in moduleInfoQuery)
            {
                stringBuilder.AppendLine($"Module Code: {module.ModuleCode}");
                stringBuilder.AppendLine($"Module Name: {module.ModuleName}");
                stringBuilder.AppendLine($"Credits: {module.Credits}");
                stringBuilder.AppendLine($"Self Study Hours: {module.StudyHours}");
                stringBuilder.AppendLine($"Semester Weeks: {module.SemsterWeeks}");
                stringBuilder.AppendLine($"Start Date: {module.StartDate}");

                // Append other module properties...
                stringBuilder.AppendLine(); // Add an empty line between modules
            }

            stringBuilder.AppendLine($"Update time: \n{update.getData(moduleCode)}");

            // Now 'result' contains all the matching records as a single string.

            displayTxt.Text = stringBuilder.ToString();
        }

        public void displayAll()
        {
            StringBuilder stringBuilder = new StringBuilder();

            int count = 1;

            var moduleInfoQuery = ModuleInfo
                                 .Where(kvp => kvp.Value.Any(moduleList => moduleList.ModuleCode != null))
                                 .SelectMany(kvp => kvp.Value);

            foreach (var module in moduleInfoQuery)
            {
                stringBuilder.AppendLine($"Module ID: {count}");
                stringBuilder.AppendLine($"Module Code: {module.ModuleCode}");
                stringBuilder.AppendLine($"Module Name: {module.ModuleName}");
                stringBuilder.AppendLine($"Credits: {module.Credits}");
                stringBuilder.AppendLine($"Self Study Hours: {module.StudyHours}");
                stringBuilder.AppendLine($"Semester Weeks: {module.SemsterWeeks}");
                stringBuilder.AppendLine($"Start Date: {module.StartDate}");

                // Append other module properties...
                stringBuilder.AppendLine(); // Add an empty line between modules

                count++;
            }

            //stringBuilder.AppendLine($"Update time: {update.calcWeekHour(enteredModule)}");

            displayTxt.Text = stringBuilder.ToString();
        }


        public string getStartDate(string search)
        {
            string startDate = "";

            string startDateString = (from h in moduleList
                                      where h.StartDate != null && h.ModuleCode == search
                                      select h.StartDate).FirstOrDefault()?.ToString();

            if (startDateString.Contains("/"))
            {
                startDate = startDateString.ToString();
            }
            return startDate;
            // This method retrieves the start date of a module based on the search module code.
        }

        

    }
}
