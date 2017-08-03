using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMDB01.Models
{
    public class comment
    {
        public int Id { get; set; }
        public int entity_Id { get; set; }
        public string entity {get; set;}
        public string user { get; set; }
        public DateTime timestamp
        {
            get
            {
                return this.dateCreated.HasValue
                   ? this.dateCreated.Value
                   : DateTime.Now;
            }

            set
            {
                this.dateCreated = value;
            }
        }
        public string Comment { get; set; }

        private DateTime? dateCreated = null;
    }
}