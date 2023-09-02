using ST10109482_Prog2B.Setup;
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

namespace ST10109482_Prog2B.Windows
{
    /// <summary>
    /// Interaction logic for display.xaml
    /// </summary>
    public partial class display : Window
    {
        List<RecordData> records = new List<RecordData>();
        public List<Module> moduleList = new List<Module>();
        private Dictionary<int, List<Module>> ModuleInfo = new Dictionary<int, List<Module>>();
        private Dictionary<int, List<RecordData>> recordInfo = new Dictionary<int, List<RecordData>>();


        public display()
        {
            InitializeComponent();
        }

        public display(Dictionary<int, List<Module>> ModuleInfo, List<RecordData> records, Dictionary<int, List<RecordData>> recordInfo)
        {
            InitializeComponent();
            this.ModuleInfo = ModuleInfo;
            this.records = records;
            this.recordInfo = recordInfo;
        }

        private void displayBtn_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder stringBuilder = new StringBuilder();

            //foreach (var kvp in ModuleInfo)
            //{
            //    foreach (var module in kvp.Value)
            //    {
            //        stringBuilder.AppendLine($"Module ID: {kvp.Key}" );
            //        stringBuilder.AppendLine($"Credits: {module.Credits}");
            //        stringBuilder.AppendLine($"Module Code: {module.ModuleCode}");
            //        stringBuilder.AppendLine($"Module Name: {module.ModuleName}");
            //        stringBuilder.AppendLine($"Self Study Hours: {module.StudyHours}");
            //        // Append other module properties...
            //        stringBuilder.AppendLine(); // Add an empty line between modules
            //    }
            //}

            displayTxt.Text = stringBuilder.ToString();

            updateStudyTime update = new updateStudyTime(ModuleInfo, records, recordInfo);

          // foreach(var item in records)
          // {
          string enteredModule = searchTxt.Text;
              stringBuilder.AppendLine($"Update time: {update.calcWeekHour(enteredModule)}");
          // }

            displayTxt.Text = stringBuilder.ToString();

            //StringBuilder stringBuilder = new StringBuilder();

            //foreach (var module in records)
            //{
            //    stringBuilder.AppendLine($"Module Code: {module.MCode}");
            //    stringBuilder.AppendLine($"Hour Studied: {module.HoursRecorded}");
            //    stringBuilder.AppendLine($"Date captured: {module.StudyDate}");
            //    // Append other module properties...
            //    stringBuilder.AppendLine(); // Add an empty line between modules
            //}

            //displayTxt.Text = stringBuilder.ToString();

        }

        private void addCourseSwitch_click(object sender, RoutedEventArgs e)
        {
            captureWindow cw = new captureWindow(ModuleInfo, records, recordInfo);
            this.Close();
            cw.Show();
        }


        private void home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(ModuleInfo, records,recordInfo);
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
    }
}
