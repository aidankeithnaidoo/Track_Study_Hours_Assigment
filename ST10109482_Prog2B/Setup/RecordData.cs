using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10109482_Prog2B.Setup
{
    public class RecordData
    {
        private string mCode;
        private double hoursRecorded = 0;
        private string studyDate;
        private double tempVar;

        public RecordData()
        {
           
        }

        public RecordData(string mCode, double hoursRecorded, string studyDate, double tempVar)
        {
            this.mCode = mCode;
            this.hoursRecorded = hoursRecorded;
            this.studyDate = studyDate;
            this.TempVar = tempVar;
        }

        public double HoursRecorded { get => hoursRecorded; set => hoursRecorded = value; }
        public string StudyDate { get => studyDate; set => studyDate = value; }
        public string MCode { get => mCode; set => mCode = value; }
        public double TempVar { get => tempVar; set => tempVar = value; }
    }
}
