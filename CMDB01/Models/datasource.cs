using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMDB01.Models
{
    public class datasource
    {
        public int Id { get; set; } //Datasource Id
        [Required]
        public string Name { get; set; } //Datasource Name

        public string Description { get; set; } //Datasource Description
        public string GUID { get; set; } //Datasource GUID

        public string Monitored { get; set; } //Datasource Monitored?
        public int ActiveUsers120Days { get; set; }
        public int ActiveUsers30Days { get; set; }
        public int Activeusers7Days { get; set; }
        public int ActiveUsers90Days { get; set; }
        //public string ADUS { get; set; }
        //public string Migration { get; set; }
        public decimal FileStorageSpace { get; set; }
        //public string PSS { get; set; }
        public string Purpose { get; set; }
        public string Status { get; set; }
    
        public virtual List<ContactLinks> DatasourceContacts { get; set; }

        //public virtual List<contact> contacts { get; set; }
        // public virtual List<comment> comments { get; set; }
        public virtual serverFarms ServerFarm { get; set; }

        public virtual string datasourceEmails
        {
            get
            {
                string emails = string.Empty;
                if (DatasourceContacts != null)
                {
                    foreach (ContactLinks mail in DatasourceContacts)
                    {
                        emails = emails + mail.contact.email + ";";
                    }
                }
                return emails;
            }
        }

        //public string serverName
        //{
        //    get
        //    {
        //        CMDB db = new CMDB();
        //        return db.serverFarms.Where(x => x.Id == ServerFarm.Id).FirstOrDefault().Name;
        //    }
        //}

        //public string DeployedVersion
        //{
        //    get
        //    {
        //        CMDB db = new CMDB();
        //        return db.serverFarms.Where(x => x.Id == ServerFarm.Id).FirstOrDefault().DeployedVersion;
        //    }
        //}

        //public string DataCenter
        //{
        //    get
        //    {
        //        CMDB db = new CMDB();
        //        return db.serverFarms.Where(x => x.Id == ServerFarm.Id).FirstOrDefault().DataCenter;
        //    }
        //}

        //public string FQDN
        //{
        //    get
        //    {
        //        CMDB db = new CMDB();
        //        return db.serverFarms.Where(x => x.Id == ServerFarm.Id).FirstOrDefault().FQDN;
        //    }
        //}

        ////One Level Up
        //public string AccountName
        //{
        //    get
        //    {
        //        CMDB db = new CMDB();
        //        return db.accounts.Where(x => x.Id == ServerFarm.account.Id).FirstOrDefault().Name;
        //    }
        //}

    }
}