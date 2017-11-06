using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMDB01.Models
{
    public class comment
    {
        public int Id { get; set; }
        public int entity_Id { get; set; }
        public string entity {get; set;}
        [Display(Name = "User Name")]
        [Required]
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
        [AllowHtml]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        public string Type { get; set; }
        [DataType(DataType.Url)]
        public string Link { get; set; }
        [Display(Name = "Featured")]
        public bool featured { get; set; }


        private DateTime? dateCreated = null;
    }
}