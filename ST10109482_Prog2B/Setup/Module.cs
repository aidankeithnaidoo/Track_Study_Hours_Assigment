using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10109482_Prog2B.Setup
{
    public class Module
    {
        private string moduleCode;
        private string moduleName;
        private int credits;
        private double classHours;
        private int semsterWeeks;
        private string startDate;

        private List<Module> moduleList = new List<Module>();

        private Dictionary<int, List<Module>> ModuleInfo = new Dictionary<int, List<Module>>();

        public Module(string moduleCode, string moduleName, int credits, double classHours, int semsterWeeks, string startDate)
        {
            this.ModuleCode = moduleCode;
            this.ModuleName = moduleName;
            this.Credits = credits;
            this.ClassHours = classHours;
            this.SemsterWeeks = semsterWeeks;
            this.StartDate = startDate;
        }

        public string ModuleCode { get => moduleCode; set => moduleCode = value; }
        public string ModuleName { get => moduleName; set => moduleName = value; }
        public int Credits { get => credits; set => credits = value; }
        public double ClassHours { get => classHours; set => classHours = value; }
        public int SemsterWeeks { get => semsterWeeks; set => semsterWeeks = value; }
        public string StartDate { get => startDate; set => startDate = value; }
    }
}
