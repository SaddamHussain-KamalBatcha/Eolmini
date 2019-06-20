using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WhatsAppBot.Database
{
    public class EolMiniDataContext : DbContext
    {
        public EolMiniDataContext() : base("EolMiniDataContext")
        {
        }

        public DbSet<ChatRecord> ChatRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}