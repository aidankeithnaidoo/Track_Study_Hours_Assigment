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
    /// Interaction logic for captureWindow.xaml
    /// </summary>
    public partial class captureWindow : Window
    {
        
        Functions functions = new Functions(); 

        private List<RecordData> records = new List<RecordData>();
        private List<Module> moduleList = new List<Module>();
        private Dictionary<int, List<Module>> ModuleInfo = new Dictionary<int, List<Module>>();
        private Dictionary<int, List<RecordData>> recordInfo = new Dictionary<int, List<RecordData>>();
        private Dictionary<string, weekTracker> weekTrack = new Dictionary<string, weekTracker>();
        private Dictionary<int, double> weekInfo = new Dictionary<int, double>();
        private Dictionary<int, double> tempInfo = new Dictionary<int, double>();


        public captureWindow()
        {
            InitializeComponent();
        }

        public captureWindow(List<Module> PmoduleList, Dictionary<int, List<Module>> PModuleInf, List<RecordData> Precords, Dictionary<int, List<RecordData>> PrecordInfo, Dictionary<string, weekTracker> PweekTrack, Dictionary<int, double> PweekInfo)
        {
            InitializeComponent();
            ModuleInfo = PModuleInf;
            records = Precords;
            recordInfo = PrecordInfo;
            moduleList = PmoduleList;
            weekTrack = PweekTrack;
            weekInfo = PweekInfo;
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
           studyPlanner_dll.Validation valid = new studyPlanner_dll.Validation();

            string code = moduleCode.Text.ToUpper();
            string courseName = moduleName.Text;
            int courseCredit = Convert.ToInt32(credits.Text);
            double hoursPerWeek = Convert.ToDouble(classHour.Text);
            int numWeeks = Convert.ToInt32(semesterWeek.Text);
            string startingDate = startDate.Text;
            double StudyHours = functions.calcStudyHours(courseCredit, numWeeks, hoursPerWeek);
            double recordedHours = 0;


            moduleList.Add(new Module(code, courseName, courseCredit, hoursPerWeek, numWeeks, startingDate, StudyHours, recordedHours)
            {
                ModuleCode = code,
                ModuleName = courseName,
                Credits = courseCredit,
                ClassHours = hoursPerWeek,
                SemsterWeeks = numWeeks,
                StartDate = startingDate,
                StudyHours = StudyHours,
                RecordedHours = recordedHours
            }
            );

            MessageBox.Show("You have added a module");
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Module item in moduleList)
            {
                // Check if the module already exists in the dictionary
                bool moduleExists = ModuleInfo.Values.Any(moduleList => moduleList.Any(module => module.ModuleCode == item.ModuleCode));

                if (!moduleExists)
                {
                    int count = 1; // Start the key from 1 or any other appropriate value

                    // Find the next available key
                    while (ModuleInfo.ContainsKey(count))
                    {
                        count++;
                    }

                    // Create a new list and add the module
                    ModuleInfo[count] = new List<Module> { item };
                }
            }
            MessageBox.Show("List of modules created");
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            moduleCode.Text = string.Empty;
            moduleName.Text = string.Empty;
            credits.Text = string.Empty;
            classHour.Text = string.Empty;
            semesterWeek.Text = string.Empty;
            startDate.Text = string.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            mw.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            updateStudyTime udt = new updateStudyTime(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close();
            udt.Show();
        }

        private void displayBtn_Click(object sender, RoutedEventArgs e)
        {
            display ds = new display(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close();
            ds.Show();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;
            DateTime selectedDate = datePicker.SelectedDate ?? DateTime.MinValue; // Handle null selected date

            // Get the current date
            DateTime currentDate = DateTime.Now.Date;

            // Check if the selected date is before the current date
            if (selectedDate < currentDate)
            {
                MessageBox.Show("Please select a date on or after the current date.");
                datePicker.SelectedDate = currentDate; // Reset the date to the current date
            }
        }
    }
}
