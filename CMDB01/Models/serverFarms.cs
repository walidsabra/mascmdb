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
        public string SLA { get; set; }
        [Display(Name = "Custom SLA")]
        public string CustomSLA { get; set; }
        //GoLiveDateSubmitted
        //public string Role { get; set; } 
        public string Architecture { get; set; }

        public string Role { get; set; }  
        public string Status { get; set; }
        public string ServerFarmIPAddress { get; set; }

        [Display(Name = "Deployment Id")]
        public string DeploymentId { get; set; }


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

        public string AdminServerFQDN { get; set; }
        public string SQLServerFQDN { get; set; }
        public string SQLServerFQDNMirror { get; set; }
        public string Monitored { get; set; }
        public string Provider { get; set; }





        public virtual List<datasource> datasources { get; set; }
        public virtual List<Server> servers { get; set; }
        public virtual List<ContactLinks> ServerContacts { get; set; }
        //public virtual List<contact> contacts { get; set; }
        //public virtual contract contract { get; set; }

        public virtual account account { get; set; }

        public virtual string SFramEmails
        {
            get
            {
                string emails = string.Empty;
                if (ServerContacts != null)
                {
                    foreach (ContactLinks mail in ServerContacts)
                    {
                        emails = emails + mail.contact.email + ";";
                    }
                }
                return emails;
            }
        }
        public virtual string SFramEmailsAll
        {
            get
            {
                string emails = string.Empty;
                if (datasources !=null)
                {
                    foreach (datasource ds in datasources)
                    {
                        if (ds.DatasourceContacts !=null)
                        {
                            foreach (ContactLinks mail in ds.DatasourceContacts)
                            {
                                emails = emails + mail.contact.email + ";";
                            }
                        }
                    }
                }

                if (ServerContacts != null)
                {
                    foreach (ContactLinks mail in ServerContacts)
                    {
                        emails = emails + mail.contact.email + ";";
                    }
                }
                return emails;
            }
        }

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