using System;

namespace ZivotopisCore.Models.BankModels
{
    public class ProduktModel
    {
        public int Id { get; set; }

        // Názov produktu (napr. Kreditná karta, Úver, Poistenie)
        public string Nazov { get; set; } = string.Empty;

        // Popis produktu
        public string Popis { get; set; } = string.Empty;

        // Cena alebo poplatok za produkt
        public decimal Cena { get; set; }

        // Voliteľne: dátum pridania produktu
        public DateTime DatumPridania { get; set; } = DateTime.Now;

        // Voliteľne: či je produkt aktívny
        public bool Aktivny { get; set; } = true;
    }
}
