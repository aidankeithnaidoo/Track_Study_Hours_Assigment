using ST10109482_Prog2B.Setup;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for updateStudyTime.xaml
    /// </summary>
    public partial class updateStudyTime : Window
    {
        List<RecordData> records = new List<RecordData>();
        public List<Module> moduleList = new List<Module>();

        private Dictionary<int, List<RecordData>> recordInfo = new Dictionary<int, List<RecordData>>();
        private Dictionary<int, List<Module>> ModuleInfo = new Dictionary<int, List<Module>>();

        public updateStudyTime()
        {
            InitializeComponent();
        }

        public updateStudyTime(Dictionary<int, List<Module>> ModuleInfo, List<RecordData> records, Dictionary<int, List<RecordData>> recordInfo)
        {
            InitializeComponent();
            this.ModuleInfo = ModuleInfo;
            this.records = records;
            this.recordInfo = recordInfo;
        }


        private void home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(ModuleInfo, records, recordInfo);
            this.Close();
            mw.Show();
        }

        private void addCourseSwitch_click(object sender, RoutedEventArgs e)
        {   
            captureWindow cw = new captureWindow(ModuleInfo, records, recordInfo);
            this.Close();
            cw.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            display ds = new display(ModuleInfo, records, recordInfo);
            this.Close();
            ds.Show();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            string code = moduleCode.Text;
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
        }

        public int calcWeekHour(string search)
        {

            var totalHours = 0;

            foreach (var item in records)
            {
                totalHours = (int)(from h in records
                                   where h.MCode == search && h.HoursRecorded > 0
                                   select h.HoursRecorded).Sum();

                if (search.Equals(item.MCode))
                {
                    item.TempVar = totalHours;
                }
            }

            return totalHours;


            //SELECT ALL WHERE MODULECODE = MODULECODE
            //ADD EVERYTHING TO A DICOTNARY AND THAT WILL BE LIKE A DUMMY STORAGE AND WE CAN SORT FROM THERE 
            //DateTime date = DateTime.Now;
            //DatePicker datePicker = new DatePicker();

            ////string enteredModuleName = moduleCode.Text;
            //var totalHoursForModule = 0; // Default value if no modules are found
            //var totalHours = 0;

            //foreach (var module in moduleList)
            //{
            //    // Calculate the total hours from records for the current module code (MCode)
            //     totalHours = (int)records
            //        .Where(h => h.HoursRecorded > 0 && h.MCode == module.ModuleCode)
            //        .Sum(h => h.HoursRecorded);

            //    // Update the recordedHours property of the current module with the calculated total hours
            //    module.RecordedHours = totalHours;
            //}

            //return (int)(totalHoursForModule - totalHours);

            ////totalHoursForModule = (int)moduleList.Where(module => module.ModuleCode == search);


            //            // Now, totalHoursForModule will contain the sum of StudyHours for the specified module



            //            //var totalHoursForModule = ModuleInfo.Values
            //            //        .SelectMany(moduleList => moduleList)
            //            //        .Where(module => module.ModuleName == moduleCode.Text)
            //            //        .Sum(module => module.StudyHours);
            //    }

            ////if (!search.Equals(search))
            //        //{
            //        //    totalHours = 0;
            //        //}

            //// If the condition is not met, you may want to return a default value or handle it differently
            ////return 0; // For example, return 0 if the condition is not met
        }
    }
 
}
