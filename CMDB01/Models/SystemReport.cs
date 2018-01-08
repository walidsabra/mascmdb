using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMDB01.Models
{
    public class SystemReport
    {
        public int Id { get; set; }
        public string ReportName { get; set; }
        public string ReportPath {get; set;}
        public string ReportCategory { get; set; }
        public string DS1Name { get; set; }
        public string DS2Name { get; set; }
        public string DS3Name { get; set; }
        public string DS4Name { get; set; }
        public string DS5Name { get; set; }
    }
}