using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMDB01.Models
{
    public class serverFarms
    {
        public int Id { get; set; } //Server Id
        [Required]
        public string Name { get; set; } //Server Name
        [Display(Name = "Data Center")]
        public string DataCenter { get; set; } //Data Source Location
        [Display(Name = "Deployed Version")]
        public string DeployedVersion { get; set; } //PW Version
        public string FQDN { get; set; }
        public string NonTypicalArchitecture { get; set; }
        public string CustomSLA { get; set; }
        //GoLiveDateSubmitted
        //public string Role { get; set; } 
        public string Architecture { get; set; }
        public string Purpose { get; set; }  //role
        public string Status { get; set; }
        public string ServerFarmIPAddress { get; set; }
        public string DeployId { get; set; }


        //Stats -----------------------------------------------------------
        public int ActiveUsers7Days { get; set; }
        public int ActiveUsers30Days { get; set; }
        public int ActiveUsers90Days { get; set; }
        public int ActiveUsers120Days { get; set; }
        public int FileStorageSpace { get; set; }
        public int ProvisionedStorageSpace { get; set; }
        public int FreeStoragePercent { get; set; }
        public int FreeStorageSpaceMB { get; set; }
        
        //------------------------------------------------------------------

        //URLs --------------------------------------------------------------
        public string WebServerURL { get; set; }
        public string WebServiceGatewayURL { get; set; }
        public string MonitoringLink { get; set; }
        //-------------------------------------------------------------------






        public virtual List<datasource> datasources { get; set; }
        public virtual List<Server> servers { get; set; }
        public virtual List<ContactLinks> ServerContacts { get; set; }
        //public virtual List<contact> contacts { get; set; }
        //public virtual contract contract { get; set; }

        public virtual account account { get; set; }

        //public List<comment> comments
        //{
        //    get
        //    {
        //        CMDB db = new CMDB();
        //        return db.comments.Where(x => x.entity_Id == server.Id && x.entity =="Server");
        //    }
        //}

        //public virtual List<comment> comments { get; set; }

        //public string AccountName
        //{
        //    get
        //    {
        //        CMDB db = new CMDB();
        //        return db.accounts.Where(x => x.Id == account.Id).FirstOrDefault().Name;
        //    }
        //}

        //public string UtlimateId
        //{
        //    get
        //    {
        //        CMDB db = new CMDB();
        //        return db.accounts.Where(x => x.Id == account.Id).FirstOrDefault().UltimateId;
        //    }
        //}
    }
}