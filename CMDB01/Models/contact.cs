using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMDB01.Models
{
    public class contact
    {
        public int Id { get; set; } //Contact Id
        [Required]
        public string Name { get; set; } //Contact Name
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        
        public string phone { get; set; }
        public string company { get; set; }


        public virtual List<ContactLinks> AccountContacts { get; set; }
        //public virtual List<datasource> datasources { get; set; }
        //public virtual List<server> servers { get; set; }
        //public virtual List<account> accounts { get; set; }



        //public List<account> accounts
        //{
        //    get
        //    {
        //        CMDB db = new CMDB();
        //        List<account> contactAccounts = db.contactlinks.Where(a => a.entityType == "Account" && a.account.Id == Id).Select(a => a.account).ToList();
        //        return contactAccounts;
        //    }
        //}

        //public List<server> servers
        //{
        //    get
        //    {
        //        CMDB db = new CMDB();
        //        List<server> contactAccounts = db.contactlinks.Where(a => a.entityType == "Server" && a.account.Id == Id).Select(a => a.server).ToList();
        //        return contactAccounts;
        //    }
        //}

        //public List<datasource> datasources
        //{
        //    get
        //    {
        //        CMDB db = new CMDB();
        //        List<datasource> contactAccounts = db.contactlinks.Where(a => a.entityType == "Datasource" && a.account.Id == Id).Select(a => a.datasource).ToList();
        //        return contactAccounts;
        //    }
        //}

    }
}