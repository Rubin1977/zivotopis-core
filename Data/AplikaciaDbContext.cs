using Microsoft.EntityFrameworkCore;
using ZivotopisCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ZivotopisCore.Data
{
    public class AplikaciaDbContext : DbContext
    {
        public AplikaciaDbContext(DbContextOptions<AplikaciaDbContext> options)
            : base(options)
        {
        }
        public DbSet<PacientModel> Pacienti { get; set; }
        public DbSet<PriznakModel> Priznaky { get; set; }
        public DbSet<GenetickeVysetrenieModel> Vysetrenia { get; set; }
        public DbSet<DiagnozaModel> Diagnozy { get; set; } // ← Toto pridaj
    }
}