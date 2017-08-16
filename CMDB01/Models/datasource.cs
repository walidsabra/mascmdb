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

        public string BeingMonitored { get; set; } //Datasource Monitored?
        public virtual List<ContactLinks> DatasourceContacts { get; set; }

        //public virtual List<contact> contacts { get; set; }
        // public virtual List<comment> comments { get; set; }
        public virtual server server { get; set; }

        public string serverName
        {
            get
            {
                CMDB db = new CMDB();
                return db.servers.Where(x => x.Id == server.Id).FirstOrDefault().Name;
            }
        }

        public string DeployedVersion
        {
            get
            {
                CMDB db = new CMDB();
                return db.servers.Where(x => x.Id == server.Id).FirstOrDefault().DeployedVersion;
            }
        }

        public string DataCenter
        {
            get
            {
                CMDB db = new CMDB();
                return db.servers.Where(x => x.Id == server.Id).FirstOrDefault().DataCenter;
            }
        }

        public string FQDN
        {
            get
            {
                CMDB db = new CMDB();
                return db.servers.Where(x => x.Id == server.Id).FirstOrDefault().FQDN;
            }
        }

        //One Level Up
        public string AccountName
        {
            get
            {
                CMDB db = new CMDB();
                return db.accounts.Where(x => x.Id == server.account.Id).FirstOrDefault().Name;
            }
        }

    }
}