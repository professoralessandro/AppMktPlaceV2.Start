#region IMPORT
using Microsoft.EntityFrameworkCore;
using AppMktPlaceV2.Security.Domain.Entities;
using AppMktPlaceV2.Security.Domain.Entities;
#endregion IMPORT

namespace AppMktPlaceV2.Security.Domain.Context.SQLServer
{
    public partial class AppDbContext : DbContext
    {
        #region CONTRUTORES
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        #endregion CONTRUTORES

        #region DBCONTEXT
        public DbSet<Usuario> Users { get; set; }
        #endregion DBCONTEXT

        #region METHODS
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioId);

                entity.Property(e => e.DataInclusao);

                entity.Property(e => e.DataNascimento);

                entity.Property(e => e.DataUltimaAlteracao);

                entity.Property(e => e.DataUltimaTrocaSenha);

                entity.Property(e => e.DataUltimoLogin);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoCivil)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.NmrDocumento)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        #endregion METHODS
    }
}
