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

        public RecordData()
        {
           
        }

        public RecordData(string mCode, double hoursRecorded, string studyDate)
        {
            this.mCode = mCode;
            this.hoursRecorded = hoursRecorded;
            this.studyDate = studyDate;
        }

        public double HoursRecorded { get => hoursRecorded; set => hoursRecorded = value; }
        public string StudyDate { get => studyDate; set => studyDate = value; }
        public string MCode { get => mCode; set => mCode = value; }
    }
}
