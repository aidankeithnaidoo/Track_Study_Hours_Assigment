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
    /// Interaction logic for updateStudyTime.xaml
    /// </summary>
    public partial class updateStudyTime : Window
    {
        List<RecordData> records = new List<RecordData>();
        public List<Module> moduleList = new List<Module>();
        private Dictionary<int, List<Module>> ModuleInfo = new Dictionary<int, List<Module>>();

        public updateStudyTime()
        {
            InitializeComponent();
        }

        public updateStudyTime(Dictionary<int, List<Module>> ModuleInfo, List<RecordData> records)
        {
            InitializeComponent();
            this.ModuleInfo = ModuleInfo;
            this.records = records;
        }


        private void home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(ModuleInfo, records);
            this.Close();
            mw.Show();
        }

        private void addCourseSwitch_click(object sender, RoutedEventArgs e)
        {
            captureWindow cw = new captureWindow(ModuleInfo, records);
            this.Close();
            cw.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            display ds = new display(ModuleInfo,records);
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

            records.Add(new RecordData(code,tempVar,date) 
            {
                MCode = code, 
                HoursRecorded = hours + tempVar, 
                StudyDate = date
            });
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
