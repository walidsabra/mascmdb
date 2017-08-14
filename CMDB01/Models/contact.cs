using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMDB01.Models
{
    public class contact
    {
        public int Id { get; set; } //Contact Id
        public string Name { get; set; } //Contact Name
        public string email { get; set; }
        public string phone { get; set; }
        public string company { get; set; }


        public virtual List<ContactLinks> AccountContacts { get; set; }
        public virtual List<datasource> datasources { get; set; }
        public virtual List<server> servers { get; set; }
        public virtual List<contract> contracts { get; set; }
    }
}