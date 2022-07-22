using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AeroSpace.Models
{
    public partial class AeroSpaceContext : DbContext
    {
        public AeroSpaceContext()
        {
        }

        public AeroSpaceContext(DbContextOptions<AeroSpaceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Avion> Avions { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Estadium> Estadia { get; set; } = null!;
        public virtual DbSet<Hangar> Hangars { get; set; } = null!;
        public virtual DbSet<Modelo> Modelos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Piloto> Pilotos { get; set; } = null!;
        public virtual DbSet<Propietario> Propietarios { get; set; } = null!;
        public virtual DbSet<Vuelo> Vuelos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=Evangelista; Database=AeroSpace; Trusted_Connection=True;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avion>(entity =>
            {
                entity.HasKey(e => e.IdAvion)
                    .HasName("PK__Avion__5CBC7B3F4A58A45C");

                entity.ToTable("Avion");

                entity.Property(e => e.EstadoAvion).HasDefaultValueSql("((1))");

                entity.Property(e => e.Siglas)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.TipoAvion)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.HasOne(d => d.Propietario)
                    .WithMany(p => p.Avions)
                    .HasForeignKey(d => d.PropietarioId)
                    .HasConstraintName("FK__Avion__Propietar__20C1E124");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PK__Empleado__CE6D8B9E892E6E1C");

                entity.ToTable("Empleado");

                entity.Property(e => e.EstadoEmpleado).HasDefaultValueSql("((1))");

                entity.Property(e => e.RolEmpleado)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Salario).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.PersonaId)
                    .HasConstraintName("FK__Empleado__Person__1CF15040");
            });

            modelBuilder.Entity<Estadium>(entity =>
            {
                entity.HasKey(e => e.IdEstadia)
                    .HasName("PK__Estadia__C7C8C9558A91F89B");

                entity.Property(e => e.EstadoEstadia).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaEntrada).HasColumnType("date");

                entity.Property(e => e.FechaSalida).HasColumnType("date");

                entity.Property(e => e.MontoEtadia).HasColumnType("decimal(17, 2)");

                entity.HasOne(d => d.Avion)
                    .WithMany(p => p.Estadia)
                    .HasForeignKey(d => d.AvionId)
                    .HasConstraintName("FK__Estadia__AvionId__2C3393D0");
            });

            modelBuilder.Entity<Hangar>(entity =>
            {
                entity.HasKey(e => e.IdHangar)
                    .HasName("PK__Hangar__E7D42F2778C83336");

                entity.ToTable("Hangar");

                entity.Property(e => e.EstadoHangar).HasDefaultValueSql("((1))");

                entity.Property(e => e.Ubicacion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Avion)
                    .WithMany(p => p.Hangars)
                    .HasForeignKey(d => d.AvionId)
                    .HasConstraintName("FK__Hangar__AvionId__300424B4");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Hangars)
                    .HasForeignKey(d => d.PersonaId)
                    .HasConstraintName("FK__Hangar__PersonaI__30F848ED");
            });

            modelBuilder.Entity<Modelo>(entity =>
            {
                entity.HasKey(e => e.IdModelo)
                    .HasName("PK__Modelo__CC30D30C11A3CD1A");

                entity.ToTable("Modelo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoModelo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Avion)
                    .WithMany(p => p.Modelos)
                    .HasForeignKey(d => d.AvionId)
                    .HasConstraintName("FK__Modelo__AvionId__24927208");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona)
                    .HasName("PK__Persona__2EC8D2AC918C8B66");

                entity.ToTable("Persona");

                entity.Property(e => e.CedulaPersona)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoPersona).HasDefaultValueSql("((1))");

                entity.Property(e => e.NombrePersona)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Piloto>(entity =>
            {
                entity.HasKey(e => e.IdPiloto)
                    .HasName("PK__Piloto__DB35379FADFEB922");

                entity.ToTable("Piloto");

                entity.Property(e => e.EstadoPiloto).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaRev)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Rev")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LicenciaPiloto)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Pilotos)
                    .HasForeignKey(d => d.PersonaId)
                    .HasConstraintName("FK__Piloto__PersonaI__15502E78");
            });

            modelBuilder.Entity<Propietario>(entity =>
            {
                entity.HasKey(e => e.IdPropietario)
                    .HasName("PK__Propieta__4D581B5028ABF3F5");

                entity.ToTable("Propietario");

                entity.Property(e => e.EstadoPropietario).HasDefaultValueSql("((1))");

                entity.Property(e => e.Rif)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Propietarios)
                    .HasForeignKey(d => d.PersonaId)
                    .HasConstraintName("FK__Propietar__Perso__1920BF5C");
            });

            modelBuilder.Entity<Vuelo>(entity =>
            {
                entity.HasKey(e => e.IdVuelo)
                    .HasName("PK__Vuelo__21761726CD2F666B");

                entity.ToTable("Vuelo");

                entity.Property(e => e.EstadoVuelo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.Avion)
                    .WithMany(p => p.Vuelos)
                    .HasForeignKey(d => d.AvionId)
                    .HasConstraintName("FK__Vuelo__AvionId__286302EC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
