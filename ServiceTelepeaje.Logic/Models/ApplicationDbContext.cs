namespace ServiceTelepeaje.Logic.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        public virtual DbSet<pn_importacion_wsIndra> pn_importacion_wsIndra { get; set; }
        public virtual DbSet<Type_Carril> Type_Carril { get; set; }
        public virtual DbSet<Type_Delegacion> Type_Delegacion { get; set; }
        public virtual DbSet<Type_Operadores> Type_Operadores { get; set; }
        public virtual DbSet<Type_Plaza> Type_Plaza { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<pn_importacion_wsIndra>()
                .Property(e => e.VOIE)
                .IsUnicode(false);

            modelBuilder.Entity<pn_importacion_wsIndra>()
                .Property(e => e.CLASE_MARCADA)
                .IsUnicode(false);

            modelBuilder.Entity<pn_importacion_wsIndra>()
                .Property(e => e.CLASE_DETECTADA)
                .IsUnicode(false);

            modelBuilder.Entity<pn_importacion_wsIndra>()
                .Property(e => e.CONTENU_ISO)
                .IsUnicode(false);

            modelBuilder.Entity<pn_importacion_wsIndra>()
                .Property(e => e.ID_OBS_MP)
                .IsUnicode(false);

            modelBuilder.Entity<Type_Carril>()
                .Property(e => e.Num_Gea)
                .IsUnicode(false);

            modelBuilder.Entity<Type_Carril>()
                .Property(e => e.Num_Capufe)
                .IsUnicode(false);

            modelBuilder.Entity<Type_Carril>()
                .Property(e => e.Num_Tramo)
                .IsUnicode(false);

            modelBuilder.Entity<Type_Delegacion>()
                .Property(e => e.Num_Delegacion)
                .IsUnicode(false);

            modelBuilder.Entity<Type_Delegacion>()
                .Property(e => e.Nom_Delegacion)
                .IsUnicode(false);

            modelBuilder.Entity<Type_Delegacion>()
                .HasMany(e => e.Type_Plaza)
                .WithRequired(e => e.Type_Delegacion)
                .HasForeignKey(e => e.Delegacion_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Type_Operadores>()
                .Property(e => e.Num_Capufe)
                .IsUnicode(false);

            modelBuilder.Entity<Type_Operadores>()
                .Property(e => e.Num_Gea)
                .IsUnicode(false);

            modelBuilder.Entity<Type_Operadores>()
                .Property(e => e.Nom_Operador)
                .IsUnicode(false);

            modelBuilder.Entity<Type_Plaza>()
                .Property(e => e.Num_Plaza)
                .IsUnicode(false);

            modelBuilder.Entity<Type_Plaza>()
                .Property(e => e.Nom_Plaza)
                .IsUnicode(false);

            modelBuilder.Entity<Type_Plaza>()
                .HasMany(e => e.Type_Carril)
                .WithRequired(e => e.Type_Plaza)
                .HasForeignKey(e => e.Plaza_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Type_Plaza>()
                .HasMany(e => e.Type_Operadores)
                .WithRequired(e => e.Type_Plaza)
                .HasForeignKey(e => e.Plaza_Id)
                .WillCascadeOnDelete(false);
        }
    }
}
