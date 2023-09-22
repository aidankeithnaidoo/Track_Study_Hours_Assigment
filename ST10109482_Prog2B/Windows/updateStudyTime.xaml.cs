using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Interaction logic for updateStudyTime.xaml
    /// </summary>
    public partial class updateStudyTime : Window
    {
        private List<RecordData> records = new List<RecordData>();
        private List<Module> moduleList = new List<Module>();

        private Dictionary<int, List<RecordData>> recordInfo = new Dictionary<int, List<RecordData>>();
        private Dictionary<int, List<Module>> ModuleInfo = new Dictionary<int, List<Module>>();
        private Dictionary<string, weekTracker> weekTrack = new Dictionary<string, weekTracker>();
        private Dictionary<int, double> weekInfo = new Dictionary<int, double>();
        private Dictionary<int, double> tempInfo = new Dictionary<int, double>();
        private ObservableCollection<string> moduleCode = new ObservableCollection<string>();


        public updateStudyTime()
        {
            InitializeComponent();
            addCombo();
            moduleCodeCombo.ItemsSource = moduleCode;
            moduleCodeCombo.SelectedIndex = 0;
            // This constructor initializes the updateStudyTime window without data.
        }

        public updateStudyTime(List<Module> PmoduleList, Dictionary<int, List<Module>> PModuleInf, List<RecordData> Precords, Dictionary<int, List<RecordData>> PrecordInfo, Dictionary<string, weekTracker> PweekTrack, Dictionary<int, double> PweekInfo)
        {
            InitializeComponent();
            ModuleInfo = PModuleInf;
            records = Precords;
            recordInfo = PrecordInfo;
            moduleList = PmoduleList;
            weekTrack = PweekTrack;
            weekInfo = PweekInfo;
            addCombo();
            moduleCodeCombo.ItemsSource = moduleCode;
            moduleCodeCombo.SelectedIndex = 0;
            // This constructor initializes the updateStudyTime window with data.
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close();
            mw.Show();
            // Handles the home button click event, which returns to the main window.
        }

        private void addCourseSwitch_click(object sender, RoutedEventArgs e)
        {
            captureWindow cw = new captureWindow(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close();
            cw.Show();
            // Handles the addCourseSwitch button click event, which opens the captureWindow.
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            display ds = new display(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close();
            ds.Show();
            // Handles the Button_Click_1 event, which opens the display window.
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
            // Handles the close button click event, which closes the updateStudyTime window.
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if(testError())
            {
                string code = moduleCodeCombo.SelectedValue.ToString();
                double hours = Convert.ToDouble(hourStudyTxt.Text);
                double tempVar = 0;
                string date = datePicker.Text;

                records.Add(new RecordData(code, hours, date, tempVar)
                {
                    MCode = code,
                    HoursRecorded = hours,
                    StudyDate = date,
                    TempVar = tempVar
                });
                MessageBox.Show("Information Captured");
            }
            else
            {

            }
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            
                int count = 0; // Start the key from 1 or any other appropriate value

                foreach (RecordData item in records)
                {
                    if (!recordInfo.ContainsKey(count))
                    {
                        recordInfo[count] = new List<RecordData>();
                    }

                    recordInfo[count].Add(item); // Add the IdentityInfo to the list under the key
                    count++;
                }

                MessageBox.Show("Record has been captured");
                // Handles the SubmitBtn button click event, which captures records and stores them in recordInfo.
            
            
        }

        //Populates combo box
        public void addCombo()
        {
            // Adds module codes from moduleList to the moduleCode list.
            foreach (var item in moduleList)
            {
                moduleCode.Add(item.ModuleCode.ToUpper());
            }
        }

        //Gets self astudy hgours
        public double GetSelfHours(string search)
        {
            // Retrieves self study hours for a given module code from moduleList.
            double selfHours = (from h in moduleList
                                where h.StudyHours > 0 && h.ModuleCode == search
                                select h.StudyHours)
                                .FirstOrDefault();

            return selfHours;
        }

        //This keeps track of weeks with remianing hours
        public void addWeeks(string search, double selfHours, string startDateString)
        {
            string test = (from h in records
                           where h.StudyDate != null && h.MCode == search
                           select h.StudyDate).FirstOrDefault()?.ToString();

            int weeks = getNumWeeks(search);


            for (int i = 1; i <= weeks; i++)
            {
                tempInfo.Add(i, selfHours);
            }

            weekInfo = tempInfo;

            foreach (var record in records.Where(r => r.StudyDate != null && r.MCode == search).ToList())
            {
                DateTime start = DateTime.Parse(startDateString);
                DateTime selectedDate = DateTime.Parse(record.StudyDate.ToString());

                TimeSpan newDiff = selectedDate - start;
                int val = (int)Math.Abs(newDiff.TotalDays / 7);

                foreach (KeyValuePair<int, double> item in tempInfo)
                {
                    if (item.Key == val)
                    {
                        weekInfo[item.Key] = item.Value - record.HoursRecorded;
                        break;
                    }
                }

                foreach (KeyValuePair<int, double> item in weekInfo)
                {
                    if (item.Key == val)
                    {
                        weekTrack[search] = new weekTracker(item.Key, item.Value);
                        break;
                    }
                }
            }
        }
        //gets the number of weeks in sesmeter
        public int getNumWeeks(string search)
        {
            // Retrieves the number of weeks for a given module code from moduleList.
            int weeks = (from h in moduleList
                         where h.SemsterWeeks > 0 && h.ModuleCode == search
                         select h.SemsterWeeks).FirstOrDefault();

            return weeks;
        }

        //method gets the start date of semseter
        public string getStartDate()
        {
            // Retrieves the start date from moduleList.
            string date = (from h in moduleList
                           where h.StartDate != null
                           select h.StartDate).FirstOrDefault();

            return date.ToString();
        }

        //Date validation
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handles the date picker's selected date change event to ensure dates are not in the past.
            DatePicker datePicker = (DatePicker)sender;
            DateTime selectedDate = datePicker.SelectedDate ?? DateTime.MinValue; // Handle null selected date

            // Get the current date
            string current = getStartDate();
            DateTime currentDate = DateTime.Parse(current);

            // Check if the selected date is before the current date
            if (selectedDate < currentDate)
            {
                MessageBox.Show("Please select a date on or after the current date.");
                datePicker.SelectedDate = currentDate; // Reset the date to the current date
            }
        }

        //Mehtod calls for validation
        public bool testError()
        {
            // Validates input fields for adding study hours.
            studyPlanner_dll.Validation vd = new studyPlanner_dll.Validation();

            if (vd.ValidateOnEmpty(moduleCodeCombo.SelectedValue.ToString()) == false)
            {
                mError.Content = "Please enter a value";
                return false;
            }
            else
            {
                mError.Content = "";
            }

            if (vd.ValidateOnEmpty(hourStudyTxt.Text) == false)
            {
                hError.Content = "Please enter a value";
                return false;
            }
            else
            {
                hError.Content = "";
            }

            if (vd.ValidateOnEmpty(datePicker.Text) == false)
            {
                dError.Content = "Please enter a value";
                return false;
            }
            else
            {
                dError.Content = "";
            }

            if (vd.validateInteger(hourStudyTxt.Text) == false)
            {
                hError.Content = "Please enter in a valid number";
                return false;
            }
            else
            {
                hError.Content = "";
            }

            if (vd.ValidateCredit(Convert.ToInt32(hourStudyTxt.Text)) == false)
            {
                hError.Content = "hours have to be greater than zero";
                return false;
            }
            else
            {
                hError.Content = "";
            }

            return true;
        }

        public string getData(string search)
        {
            string result = "";
            foreach (var kvp in weekInfo)
            {
                result += $"Module Code: {search} Week: {kvp.Key} Remaining Hours: {kvp.Value}\n";
            }
            return result;
        }
    }

}
