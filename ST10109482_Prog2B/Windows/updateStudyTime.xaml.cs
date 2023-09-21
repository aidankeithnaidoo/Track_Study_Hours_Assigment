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
        }


        private void home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close();
            mw.Show();
        }

        private void addCourseSwitch_click(object sender, RoutedEventArgs e)
        {   
            captureWindow cw = new captureWindow(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close();
            cw.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            display ds = new display(moduleList, ModuleInfo, records, recordInfo, weekTrack, weekInfo);
            this.Close();
            ds.Show();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
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
            double selfHours = GetSelfHours(search);

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

            return (int)(selfHours - totalHours);
        }

        public void addCombo()
        {
            foreach(var item in moduleList)
            {
                moduleCode.Add(item.ModuleCode.ToUpper());
            }
        }

        public double GetSelfHours(string search)
        {
            double selfHours = (from h in moduleList
                                where h.StudyHours > 0 && h.ModuleCode == search
                                select h.StudyHours)
                                .FirstOrDefault();

            return selfHours;
        }

       public void addWeeks(string search,double selfHours, int hours, string startDateString, string selectedDateString)
        {
            DateTime start = DateTime.Parse(startDateString);
            DateTime selectedDate = DateTime.Parse(selectedDateString);
            

            int weeks = getNumWeeks(search);


            for(int i = 1; i <= weeks;i++)
            {                
                tempInfo.Add(i, selfHours);
            }

            weekInfo = tempInfo;

            TimeSpan newDiff = selectedDate - start;
            int val = (int)Math.Abs(newDiff.TotalDays/7);

            foreach(KeyValuePair<int,double> item in tempInfo)
            {
                if(item.Key == val)
                {
                    weekTrack[search] = new weekTracker(item.Key, item.Value);
                    weekInfo[item.Key] = item.Value - hours;
                    break;
                }
            }
        }

        public string getData(string search)
        {
            var query = from h in weekTrack
                        where h.Key == search && h.Value != null
                        select $"{h.Key}: {h.Value}";

            return string.Join(", ", query);
        }

        public int getNumWeeks(string search)
        {
            int weeks = (from h in moduleList
                         where h.SemsterWeeks > 0 && h.ModuleCode == search
                         select h.SemsterWeeks).FirstOrDefault();

            return weeks;

        }
    }

}
