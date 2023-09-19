using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection <string> moduleCode = new ObservableCollection<string>();


        public display()
        {
            InitializeComponent();
            addToCombo();
            moduleCodeCombo.ItemsSource = moduleCode;
        }

        public display(List<Module> PmoduleList,Dictionary<int, List<Module>> PModuleInf, List<RecordData> Precords, Dictionary<int, List<RecordData>> PrecordInfo)
        {
            InitializeComponent();
            ModuleInfo = PModuleInf;
            records = Precords;
            recordInfo = PrecordInfo;
            moduleList = PmoduleList;
            addToCombo();
            moduleCodeCombo.ItemsSource = moduleCode;
        }

        private void displayBtn_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder stringBuilder = new StringBuilder();
            updateStudyTime update = new updateStudyTime(moduleList,ModuleInfo, records, recordInfo);

            DateTime date = DateTime.Now;

            string enteredModule = moduleCodeCombo.SelectedValue.ToString();
            int weekCount = 1;

            var moduleInfoQuery = ModuleInfo
                                  .Where(kvp => kvp.Value.Any(moduleList => moduleList.ModuleCode == enteredModule))
                                  .SelectMany(kvp => kvp.Value);

            
            foreach (var module in moduleInfoQuery)
            {
                stringBuilder.AppendLine($"Week: {weekCounter(weekCount)}");
                stringBuilder.AppendLine($"Module ID: {module.ModuleCode}");
                stringBuilder.AppendLine($"Credits: {module.Credits}");
                stringBuilder.AppendLine($"Module Code: {module.ModuleCode}");
                stringBuilder.AppendLine($"Module Name: {module.ModuleName}");
                stringBuilder.AppendLine($"Self Study Hours: {module.StudyHours}");
                // Append other module properties...
                stringBuilder.AppendLine(); // Add an empty line between modules
            }

            //stringBuilder.AppendLine($"Update time: {update.calcWeekHour(enteredModule)}");

            displayTxt.Text = stringBuilder.ToString();
        }

        private void addCourseSwitch_click(object sender, RoutedEventArgs e)
        {
            captureWindow cw = new captureWindow(moduleList, ModuleInfo, records, recordInfo);
            this.Close();
            cw.Show();
        }


        private void home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(moduleList,ModuleInfo, records,recordInfo);
            this.Close();
            mw.Show();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void addHours_click(object sender, RoutedEventArgs e)
        {
            updateStudyTime upt = new updateStudyTime(moduleList, ModuleInfo, records, recordInfo);
            this.Close();
            upt.Show();
        }

        public int weekCounter(int weekCount)
        {
            DateTime dt = new DateTime();

            if (dt.Day.Equals(1))
            {
                weekCount++;
            }
            return weekCount;
        }

        public void addToCombo()
        {
            foreach(var item in moduleList)
            {
                moduleCode.Add(item.ModuleCode.ToUpper());
            }
        }

        private void displayAllBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
