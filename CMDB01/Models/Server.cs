using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMDB01.Models
{
    public class Server
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual serverFarms ServerFarm { get; set; }
    }
}