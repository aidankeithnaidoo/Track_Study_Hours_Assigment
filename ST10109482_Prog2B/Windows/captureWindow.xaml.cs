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
    /// Interaction logic for captureWindow.xaml
    /// </summary>
    public partial class captureWindow : Window
    {
        List<RecordData> records = new List<RecordData>();
        public List<Module> moduleList = new List<Module>();
        private Dictionary<int, List<Module>> ModuleInfo = new Dictionary<int, List<Module>>();
        
        public captureWindow()
        {
            InitializeComponent();
        }

        public captureWindow(Dictionary<int, List<Module>> ModuleInfo, List<RecordData> records)
        {
            InitializeComponent();
            this.ModuleInfo = ModuleInfo;
            this.records = records; 
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Module md = new Module();
            string code = moduleCode.Text.ToUpper();
            string courseName = moduleName.Text;
            int courseCredit = Convert.ToInt32(credits.Text);
            double hoursPerWeek = Convert.ToDouble(classHour.Text);
            int numWeeks = Convert.ToInt32(semesterWeek.Text);
            string startingDate = startDate.Text;
            double StudyHours = (courseCredit * 10 / numWeeks) - hoursPerWeek;


            moduleList.Add(new Module(code, courseName, courseCredit, hoursPerWeek, numWeeks, startingDate, StudyHours)
            {
                ModuleCode = code,
                ModuleName = courseName,
                Credits = courseCredit,
                ClassHours = hoursPerWeek,
                SemsterWeeks = numWeeks,
                StartDate = startingDate,
                StudyHours = StudyHours,
            }
            );

            MessageBox.Show("You have added a module");
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            int count = 0; // Start the key from 1 or any other appropriate value

            foreach (Module item in moduleList)
            {
                if (!ModuleInfo.ContainsKey(count))
                {
                    ModuleInfo[count] = new List<Module>();
                }

                ModuleInfo[count].Add(item); // Add the IdentityInfo to the list under the key
            }

            count = count++;

            MessageBox.Show("List of modules created");
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(ModuleInfo, records);
            mw.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            updateStudyTime udt = new updateStudyTime(ModuleInfo, records);
            this.Close();
            udt.Show();
        }
    }
}
