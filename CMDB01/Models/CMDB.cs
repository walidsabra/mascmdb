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
        public DbSet<datasource> datasources { get; set; }
        public DbSet<serverFarms> serverFarms { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<comment> comments { get; set; }
        public DbSet<ContactLinks> contactlinks { get; set; }

        public DbSet<PickList> PickLists { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);               

        }

        public System.Data.Entity.DbSet<CMDB01.Controllers.LoginViewModel> LoginViewModels { get; set; }
    }
}