using System;
using System.Collections.Generic;
using ZivotopisCore.Models;

namespace ZivotopisCore.Models.ViewModels
{
    public class PacientFormViewModel
    {
        // Hlavný model pacienta
        public PacientModel Pacient { get; set; } = new PacientModel
        {
            Meno = string.Empty,
            Priezvisko = string.Empty,
            Pohlavie = string.Empty,
            RodneCislo = string.Empty,
            DatumNarodenia = DateTime.Today
        };

        // Všetky dostupné možnosti na výber
        public List<DiagnozaModel> VsetkyDiagnozy { get; set; } = [];
        public List<PriznakModel> VsetkyPriznaky { get; set; } = [];
        public List<GenetickeVysetrenieModel> VsetkyVysetrenia { get; set; } = [];


        // Vybrané položky z formulára
        public List<string> SelectedDiagnozy { get; set; } = [];

        public List<string> SelectedPriznaky { get; set; } = [];

        public List<string> SelectedVysetrenia { get; set; } = [];



        // Pridanie novej diagnózy
        public string NovaDiagnoza { get; set; } = string.Empty;

        public string NovaPriznak{ get; set; } = string.Empty;

        public string NovaVysetrenie { get; set; } = string.Empty;
    }
}

