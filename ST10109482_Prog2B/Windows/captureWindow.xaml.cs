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

        //Creation of data structures
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

            // Initialize class variables with provided parameters
            ModuleInfo = PModuleInf;
            records = Precords;
            recordInfo = PrecordInfo;
            moduleList = PmoduleList;
            weekTrack = PweekTrack;
            weekInfo = PweekInfo;
        }

        // Event handler for the "Close" button
        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Close the current window
        }

        // Event handler for the "Add" button
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (testError()) // Check if input data is valid
            {
                // Extract input data
                string code = moduleCode.Text.ToUpper();
                string courseName = moduleName.Text;
                int courseCredit = Convert.ToInt32(credits.Text);
                double hoursPerWeek = Convert.ToDouble(classHour.Text);
                int numWeeks = Convert.ToInt32(semesterWeek.Text);
                string startingDate = startDate.Text;
                double StudyHours = functions.calcStudyHours(courseCredit, numWeeks, hoursPerWeek);
                double recordedHours = 0;

                // Create a new Module object and add it to moduleList
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
                });

                MessageBox.Show("You have added a module");
            }
            else
            {
                // Data input is not valid; display an error message (commented out)
                // MessageBox.Show("Please double-check all data entered");
            }
        }

        // Event handler for the "Submit" button
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
            MessageBox.Show("You have added a module");
        }

        // Event handler for the "Clear" button
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            // Clear input fields
            moduleCode.Text = string.Empty;
            moduleName.Text = string.Empty;
            credits.Text = string.Empty;
            classHour.Text = string.Empty;
            semesterWeek.Text = string.Empty;
            startDate.Text = string.Empty;
        }

        // Event handler for a button click event (replace "Button_Click" with a more descriptive name)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create a new MainWindow instance with the provided data and show it
            MainWindow mw = new MainWindow(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            mw.Show();
            this.Close(); // Close the current window
        }

        // Event handler for a button click event (replace "Button_Click_1" with a more descriptive name)
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Create a new updateStudyTime instance with the provided data and show it
            updateStudyTime udt = new updateStudyTime(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close(); // Close the current window
            udt.Show();
        }

        // Event handler for the "Display" button
        private void displayBtn_Click(object sender, RoutedEventArgs e)
        {
            // Create a new display instance with the provided data and show it
            display ds = new display(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close(); // Close the current window
            ds.Show();
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        //Validation method calls
        public bool testError()
        {
            // Create a Validation instance
            studyPlanner_dll.Validation vd = new studyPlanner_dll.Validation();

            // Validate the "moduleCode" input
            if (vd.ValidateOnEmpty(moduleCode.Text) == false)
            {
                // If validation fails, set the error message and return false
                moduleCodeError.Content = "Please enter a value";
                return false;
            }
            else
            {
                // If validation succeeds, clear the error message
                moduleCodeError.Content = "";
            }

            // Validate the "moduleName" input (similar pattern as above)
            if (vd.ValidateOnEmpty(moduleName.Text) == false)
            {
                moduleNameError.Content = "Please enter a value";
                return false;
            }
            else
            {
                moduleNameError.Content = "";
            }

            // Validate the "credits" input (similar pattern as above)
            if (vd.ValidateOnEmpty(credits.Text) == false)
            {
                creditError.Content = "Please enter a value";
                return false;
            }
            else
            {
                creditError.Content = "";
            }

            // Validate the "classHour" input (similar pattern as above)
            if (vd.ValidateOnEmpty(classHour.Text) == false)
            {
                hoursError.Content = "Please enter a value";
                return false;
            }
            else
            {
                hoursError.Content = "";
            }

            // Validate the "semesterWeek" input (similar pattern as above)
            if (vd.ValidateOnEmpty(semesterWeek.Text) == false)
            {
                weeksError.Content = "Please enter a value";
                return false;
            }
            else
            {
                weeksError.Content = "";
            }

            if (startDate.SelectedDate == null)
            {
                // The selected date is not null, you can proceed with your code here
                //DateTime selectedDate = startDate.SelectedDate.Value;
                MessageBox.Show("Please enter a date");
            }

            // Validate that "credits" is a valid integer (similar pattern as above)
            if (vd.validateInteger(credits.Text) == false)
            {
                creditError.Content = "Please enter a valid number";
                return false;
            }
            else
            {
                creditError.Content = "";
            }

            // Validate that "classHour" is a valid integer (similar pattern as above)
            if (vd.validateInteger(classHour.Text) == false)
            {
                hoursError.Content = "Please enter a valid number";
                return false;
            }
            else
            {
                hoursError.Content = "";
            }

            // Validate that "semesterWeek" is a valid integer (similar pattern as above)
            if (vd.validateInteger(semesterWeek.Text) == false)
            {
                weeksError.Content = "Please enter a valid number";
                return false;
            }
            else
            {
                weeksError.Content = "";
            }

            // Validate that "credits" is greater than zero (similar pattern as above)
            if (vd.ValidateCredit(Convert.ToInt32(credits.Text)) == false)
            {
                creditError.Content = "credit has to be greater than zero";
                return false;
            }
            else
            {
                creditError.Content = "";
            }

            // Validate that "classHour" is greater than zero (similar pattern as above)
            if (vd.ValidateCredit(Convert.ToInt32(classHour.Text)) == false)
            {
                hoursError.Content = "hours has to be greater than zero";
                return false;
            }
            else
            {
                hoursError.Content = "";
            }

            // Validate that "semesterWeek" is greater than zero (similar pattern as above)
            if (vd.ValidateNumWeeks(Convert.ToInt32(semesterWeek.Text)) == false)
            {
                weeksError.Content = "weeks has to be higher than zero";
                return false;
            }
            else
            {
                weeksError.Content = "";
            }

            // All validations passed, return true
            return true;
        }
    }
}
