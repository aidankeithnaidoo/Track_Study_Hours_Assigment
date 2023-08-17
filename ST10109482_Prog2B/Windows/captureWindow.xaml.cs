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
        public List<Module> moduleList = new List<Module>();
        private Dictionary<int, List<Module>> ModuleInfo = new Dictionary<int, List<Module>>();
        
        public captureWindow()
        {
            InitializeComponent();
        }

        public captureWindow(Dictionary<int, List<Module>> ModuleInfo)
        {
            InitializeComponent();
            this.ModuleInfo = ModuleInfo;
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            populateUser();
            MessageBox.Show("You have added a module");
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            
            ModuleInfo.Add(1, moduleList);
            MessageBox.Show("List of modules created");
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(ModuleInfo);
            mw.Show();
            this.Close();
        }

        public void populateUser()
        {
            string code = moduleCode.Text.ToUpper();
            string courseName = moduleName.Text;
            string courseCredit = credits.Text;
            double hoursPerWeek = Convert.ToDouble(classHour.Text);
            string numWeeks = semesterWeek.Text;
            string startingDate = startDate.Text;

            moduleList.Add(new Module(code, courseName, courseCredit, hoursPerWeek, numWeeks, startingDate) 
            {ModuleCode = code, ModuleName = courseName, Credits = courseCredit, ClassHours = hoursPerWeek, SemsterWeeks = numWeeks, StartDate = startingDate}
            );
        }
    }
}
