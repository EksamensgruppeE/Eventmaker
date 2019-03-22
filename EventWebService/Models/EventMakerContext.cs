namespace EventWebService.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EventMakerContext : DbContext
    {
        public EventMakerContext()
            : base("name=EventMakerContext")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Event> Event { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
