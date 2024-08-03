#region REFERENCE
using AppMktPlaceV2.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
#endregion REFERENCE

namespace AppMktPlaceV2.Security.Domain.Context.Postgre
{
    public partial class APContextPostgre : DbContext
    {
        #region CONTRUTORES
        public APContextPostgre()
        {
        }

        public APContextPostgre(DbContextOptions<APContextPostgre> options)
            : base(options)
        {
        }
        #endregion

        #region DBCONTEXT
        public virtual DbSet<Log> Logs { get; set; }
        #endregion

        #region METHODS
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Log>(entity =>
        //    {
        //        entity.HasKey(e => e.LogId);

        //        entity.Property(e => e.DateAdded);

        //        entity.Property(e => e.Message)
        //            .IsRequired()
        //            .IsUnicode(false);

        //        entity.Property(e => e.Method)
        //            .IsRequired()
        //            .HasMaxLength(10)
        //            .IsUnicode(false);

        //        entity.Property(e => e.Request)
        //            .IsRequired()
        //            .HasMaxLength(150)
        //            .IsUnicode(false);
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        #endregion
    }
}
