using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZivotopisCore.Models.Home
{

    public class SocialLink
    {
        public string ?Nazov { get; set; }   // napr. "GitHub"
        public string ?Url { get; set; }     // napr. "https://github.com/Rubin1977"
        public string ?IkonaClass { get; set; } // napr. "fab fa-github"
    }
    public class OsobneUdajeModel
    {
        public required string Titul { get; set; }
        public required string Meno { get; set; }
        public required string Priezvisko { get; set; }
        public required string Obec { get; set; }
        public string? Stav { get; set; }
        public required string Email { get; set; }
        public required string Telefon { get; set; }

        public List<SocialLink> SocialLinks { get; set; } = new();
    }

}