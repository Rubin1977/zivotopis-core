using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZivotopisCore.Models.Home
{
    public class OsobneUdajeModel
    {
        public required string Titul { get; set; }
        public required string Meno { get; set; }
        public required string Priezvisko { get; set; }
        public required string Obec { get; set; }
        public string? Stav { get; set; }
        public required string Email { get; set; }
        public required string Telefon { get; set; }
        public required string LinkedIn { get; set; }
        public required string GitHub { get; set; }
    }

}