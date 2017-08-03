using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMDB01.Models
{
    public class account
    {
        public int Id { get; set; } //Account Id
        [Display(Name="Account Name")]
        public string Name { get; set; } //Account Name
        [Display(Name = "Ultimate ID")]
        public string UltimateId { get; set; }
        [Display(Name = "Contract Date")]
        public string ContractDate { get; set; }
        public string Status { get; set; } //

        public string Opportunity { get; set; }
        [Display(Name = "Projector Project")]
        public string ProjectorProject { get; set; } //Projector Code
        public bool RequestIMS { get; set; }
        public string SuccessAdminLevel { get; set; }
        public string LinktoC4S { get; set; }
        public bool Billable { get; set; }

        public virtual List<contact> contacts { get; set; }
        //public virtual List<contract> contracts { get; set; }

        public virtual List<server> servers { get; set; }
        //public virtual List<comment> comments { get; set; }
    }
}