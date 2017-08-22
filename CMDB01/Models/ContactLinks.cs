using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMDB01.Models
{
    public class ContactLinks
    {
        public int Id { get; set; }
        public string entityType { get; set; }
        public string entityCategory { get; set; }

        public virtual account account { get; set; }
        public virtual serverFarms server { get; set; }
        public virtual datasource datasource { get; set; }
        public virtual contact contact { get; set; }

    }
}