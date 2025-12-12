using Microsoft.EntityFrameworkCore;
using ZivotopisCore.Models;
using ZivotopisCore.Models.BankModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ZivotopisCore.Data;

public class AplikaciaDbContext(DbContextOptions<AplikaciaDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PacientModel>()
            .HasMany(p => p.Diagnozy)
            .WithMany(d => d.Pacienti)
            .UsingEntity(j => j.ToTable("DiagnozaModelPacientModel"));
    }

    public DbSet<PacientModel> Pacienti { get; set; }
    public DbSet<PriznakModel> Priznaky { get; set; }
    public DbSet<GenetickeVysetrenieModel> Vysetrenia { get; set; }
    public DbSet<DiagnozaModel> Diagnozy { get; set; }

    // Nové tabuľky
    public DbSet<UcetModel> Ucty { get; set; }
    public DbSet<TransakciaModel> Transakcie { get; set; }
    public DbSet<ProduktModel> Produkty { get; set; }
}
