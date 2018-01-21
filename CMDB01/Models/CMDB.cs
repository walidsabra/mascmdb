using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Common;

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
        public virtual DbSet<changelog> ChangeLogs { get; set; }

        object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);               

        }
        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries()
            .Where(p => p.State == EntityState.Modified).ToList();
            var now = DateTime.UtcNow;

            foreach (var change in modifiedEntities)
            {
                var entityName = change.Entity.GetType().Name;
                var primaryKey = GetPrimaryKeyValue(change);

                CMDB _db = new CMDB();
                account originalValues = _db.accounts.Where(x => x.Id.ToString() == primaryKey.ToString()).FirstOrDefault();

                foreach (var prop in change.OriginalValues.PropertyNames)
                {
                    var originalValue = change.OriginalValues[prop].ToString();
                    var currentValue = change.CurrentValues[prop].ToString();
                    
                    if (true)//originalValue != currentValue)
                    {
                        changelog log = new changelog()
                        {
                            EntityName = entityName,
                            PrimaryKeyValue = primaryKey.ToString(),
                            PropertyName = prop,
                            OldValue = originalValue,
                            NewValue = currentValue,
                            DateChanged = now,
                            UserName = HttpContext.Current.User.Identity.Name
                        };
                        ChangeLogs.Add(log);
                    }
                }
            }
            return base.SaveChanges();
        }
        public System.Data.Entity.DbSet<CMDB01.Models.SystemReport> SystemReports { get; set; }

        //public System.Data.Entity.DbSet<CMDB01.Controllers.LoginViewModel> LoginViewModels { get; set; }
    }
}