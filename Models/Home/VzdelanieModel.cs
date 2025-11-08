using System;
using System.Collections.Generic;
using System.Linq;


namespace ZivotopisCore.Models.Home
{
    public class VzdelanieModel
    {
        public List<Vzdelanie> Vzdelania { get; set; } = [];
        public List<Kurz> Kurzy { get; set; } = [];
        public List<Skusenost> Skusenosti { get; set; } = [];
    }


    public class Vzdelanie
    {
        public required string Obdobie { get; set; }
        public required string Skola { get; set; }
        public required string Fakulta { get; set; }
        public required string Titul { get; set; }
        public required string Popis { get; set; }
    }

    public class Kurz
    {
        public required string Datum { get; set; }
        public required string Nazov { get; set; }
    }

    public class Skusenost
    {
        public required string Obdobie { get; set; }
        public required string Pozicia { get; set; }
        public required string Miesto { get; set; }
        public List<string> PopisPrace { get; set; } = [];


    }
}
