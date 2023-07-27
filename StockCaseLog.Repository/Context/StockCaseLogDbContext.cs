using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StockCaseLog.Domain.Base;
using StockCaseLog.Domain.Entities;
using StockCaseLog.Repository.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockCaseLog.Repository.Context
{
    public class StockCaseLogDbContext : DbContext
    {
        public StockCaseLogDbContext(DbContextOptions<StockCaseLogDbContext> options) : base(options)
        { }
        //public StockCaseLogDbContext()
        //{ }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new StockSeed(new int[] { 1, 2, 3, 4 }));
        }

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    var datas = ChangeTracker
        //        .Entries<BaseEntity>();

        //    foreach (var data in datas)
        //    {
        //        _ = data.State switch
        //        {
        //            EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
        //            EntityState.Modified => data.Entity.UpdateDate = DateTime.Now,
        //            _ => DateTime.Now
        //        };
        //    }
        //    return await base.SaveChangesAsync(cancellationToken);
        //}

        public void JsonLogla()
        {
            //Değişiklik olmayan kayıtları alıyoruz.
            var modifiedEntities = ChangeTracker
                .Entries()
                .Where(p => p.State != EntityState.Unchanged)
                .ToList();
            var now = DateTime.Now;

            foreach (var change in modifiedEntities)
            {
                var entityName = change.GetType().Name;
                var primaryKey = change.OriginalValues.Properties.FirstOrDefault(prop => prop.IsPrimaryKey() == true).Name;

                StringBuilder jsonEntityOriginalValues = new();
                jsonEntityOriginalValues.Append("{\"" + entityName + "\":{");

                StringBuilder jsonEntityCurrentValues = new();
                jsonEntityCurrentValues.Append("{\"" + entityName + "\":{");

                foreach (IProperty prop in change.OriginalValues.Properties)
                {
                    var originalValue = change.OriginalValues[prop.Name];
                    jsonEntityOriginalValues.Append(prop.Name + ":{\"" + originalValue + "\"}");

                    var currentValue = change.CurrentValues[prop.Name];
                    jsonEntityCurrentValues.Append(prop.Name + ":{\"" + currentValue + "\"}");
                }
                jsonEntityOriginalValues.Append("}}");
                jsonEntityCurrentValues.Append("}}");

                #region Sadece Değişen kayıt Log'a atılır.
                //if (jsonEntityOriginalValues != jsonEntityCurrentValues) //Sadece Değişen kayıt Log'a atılır.
                //{
                //    ChangeLog log = new ChangeLog()
                //    {
                //        UserName = "technic@deneme.com",
                //        EntityName = entityName,
                //        PrimaryKeyValue = int.Parse(change.OriginalValues[primaryKey].ToString()),
                //        PropertyName = "",
                //        OldValue = jsonEntityOriginalValues.ToString(),
                //        NewValue = jsonEntityCurrentValues.ToString(),
                //        DateChanged = now,
                //        //State = EnumState.Update
                //    };

                //    if (change.State == EntityState.Deleted)
                //    {
                //        log.State = EnumState.Delete;
                //        log.NewValue = "{}";
                //    }
                //    else if (change.State == EntityState.Modified)
                //    {
                //        log.State = EnumState.Update;
                //    }
                //    else if (change.State == EntityState.Added)
                //    {
                //        log.OldValue = "{}";
                //        log.State = EnumState.Added;
                //    }

                //    ChangeLogs.Add(log);
                //}
                #endregion

                ChangeLog log = new ChangeLog()
                {
                    UserName = "mustafa@deneme.com",
                    EntityName = entityName,
                    PrimaryKeyValue = int.Parse(change.OriginalValues[primaryKey].ToString()),
                    PropertyName = "",
                    OldValue = jsonEntityOriginalValues.ToString(),
                    NewValue = jsonEntityCurrentValues.ToString(),
                    DateChanged = now,
                    //State = EnumState.Update
                };

                if (change.State == EntityState.Deleted)
                {
                    log.State = EnumState.Delete;
                    log.NewValue = "{}";
                }
                else if (change.State == EntityState.Modified)
                {
                    log.State = EnumState.Update;
                }
                else if (change.State == EntityState.Added)
                {
                    log.OldValue = "{}";
                    log.State = EnumState.Added;
                }
                ChangeLogs.Add(log);
            }
        }
        public void Logla()
        {
            var modifiedEntities = ChangeTracker
                .Entries()
                .Where(p => p.State != EntityState.Unchanged)
                .ToList();
            var now = DateTime.Now;

            foreach (var change in modifiedEntities)
            {
                var entityName = change.Entity.GetType().Name;
                var primaryKey = change.OriginalValues.Properties.FirstOrDefault(prop => prop.IsPrimaryKey() == true).Name;

                foreach (IProperty prop in change.OriginalValues.Properties)
                {

                    var originalValue = change.OriginalValues[prop.Name].ToString();
                    var currentValue = change.CurrentValues[prop.Name].ToString();

                    if (originalValue != currentValue) //Sadece Değişen kayıt Log'a atılır.
                    {
                        ChangeLog log = new ChangeLog()
                        {
                            UserName = "technic@deneme.com",
                            EntityName = entityName,
                            PrimaryKeyValue = int.Parse(change.OriginalValues[primaryKey].ToString()),
                            PropertyName = prop.Name,
                            OldValue = originalValue,
                            NewValue = currentValue,
                            DateChanged = now,
                            //State = EnumState.Update
                        };

                        if (change.State != EntityState.Unchanged)
                        {
                            if (change.State == EntityState.Deleted)
                            {
                                log.State = EnumState.Delete;
                                log.NewValue = "{}";
                            }
                            else if (change.State == EntityState.Modified)
                            {
                                log.State = EnumState.Update;
                            }
                            else if (change.State == EntityState.Added)
                            {
                                log.OldValue = "{}";
                                log.State = EnumState.Added;
                            }
                            ChangeLogs.Add(log);
                        }
                    }
                }
            }
        }
        public override int SaveChanges()
        {
            JsonLogla();
            return base.SaveChanges();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;");
        //}
    }
}
        
    

