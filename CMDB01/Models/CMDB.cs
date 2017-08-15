using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CMDB01.Models
{
    public class CMDB:DbContext
    {
        public DbSet<account> accounts { get; set; }
        public DbSet<contact> contacts { get; set; }
        public DbSet<contract> contracts { get; set; }
        public DbSet<datasource> datasources { get; set; }
        public DbSet<server> servers { get; set; }
        public DbSet<comment> comments { get; set; }
        public DbSet<ContactLinks> contactlinks { get; set; }
    }
}