using System;
using System.Collections.Generic;
using ZivotopisCore.Models;

namespace ZivotopisCore.Models.ViewModels
{
    public class PacientDetailViewModel
    {
        public int Id { get; set; }

        public string Meno { get; set; } = string.Empty;
        public string Priezvisko { get; set; } = string.Empty;
        public DateTime DatumNarodenia { get; set; }
        public string Pohlavie { get; set; } = string.Empty;
        public string RodneCislo { get; set; } = string.Empty;

        public List<DiagnozaModel> Diagnozy { get; set; } = [];
        public List<PriznakModel> Priznaky { get; set; } = [];
        public List<GenetickeVysetrenieModel> GenetickeVysetrenia { get; set; } = [];


        public bool JeArchivovany { get; set; } = false; // voliteľné
    }
}
