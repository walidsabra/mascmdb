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
        [Required]
        public string Name { get; set; } //Account Name
        [Display(Name = "Ultimate ID")]
        public string UltimateId { get; set; }
        [Display(Name = "Contract Date")]
        public string ContractDate { get; set; }
        public string Status { get; set; } //

        public string Opportunity { get; set; }
        [Display(Name = "Projector Project")]
        public string ProjectorProject { get; set; } //Projector Code
        [Display(Name = "Request IMS?")]
        public string RequestIMS { get; set; }  //yes, No, completed

        [Display(Name = "Success Admin Level")]
        public string SuccessAdminLevel { get; set; }
        [Display(Name = "Link to C4S")]
        public string LinktoC4S { get; set; }
        public string Billable { get; set; }
        public virtual List<ContactLinks> AccountContacts {get; set;}
        
        public virtual List<serverFarms> servers { get; set; }

        //public virtual List<comment> comments { get; set; }
    }
}