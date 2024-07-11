using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ReservaProject.Domain.Entities;

namespace ReservaProject.Infraestructura.Context;

public partial class ReservasContext : DbContext
{
    public ReservasContext()
    {
    }

    public ReservasContext(DbContextOptions<ReservasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Habitacion> Habitacions { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<ServicioReservado> ServicioReservados { get; set; }

    public virtual DbSet<TipoServicio> TipoServicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Cliente");

            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(250);
            entity.Property(e => e.Telefono).HasMaxLength(20);

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cliente__Usuario__40F9A68C");
        });

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Habitaci__3214EC274D283733");

            entity.ToTable("Habitacion");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.NumeroCuarto)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mesa__3214EC2725ABD41F");

            entity.ToTable("Mesa");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.NumeroMesa)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reserva__3214EC27D5825529");

            entity.ToTable("Reserva");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");

            entity.HasOne(d => d.ClienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.Cliente)
                .HasConstraintName("FK__Reserva__Cliente__4D5F7D71");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.ToTable("Servicio");

            entity.Property(e => e.Descripcion).HasMaxLength(250);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(250);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.TipoServicioNavigation).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.TipoServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Servicio__TipoSe__498EEC8D");
        });

        modelBuilder.Entity<ServicioReservado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Servicio__3214EC271522206C");

            entity.ToTable("ServicioReservado");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.PrecioReal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.HabitacionNavigation).WithMany(p => p.ServicioReservados)
                .HasForeignKey(d => d.Habitacion)
                .HasConstraintName("FK__ServicioR__Habit__58D1301D");

            entity.HasOne(d => d.MesaNavigation).WithMany(p => p.ServicioReservados)
                .HasForeignKey(d => d.Mesa)
                .HasConstraintName("FK__ServicioRe__Mesa__59C55456");

            entity.HasOne(d => d.ReservaNavigation).WithMany(p => p.ServicioReservados)
                .HasForeignKey(d => d.Reserva)
                .HasConstraintName("FK__ServicioR__Estad__56E8E7AB");

            entity.HasOne(d => d.ServicioNavigation).WithMany(p => p.ServicioReservados)
                .HasForeignKey(d => d.Servicio)
                .HasConstraintName("FK__ServicioR__Servi__57DD0BE4");
        });

        modelBuilder.Entity<TipoServicio>(entity =>
        {
            entity.ToTable("TipoServicio");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(250);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
