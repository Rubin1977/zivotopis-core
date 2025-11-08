using System.ComponentModel.DataAnnotations;

namespace ZivotopisCore.Models.Home
{
    public class OdoslanieSpravyModel
    {
        [Required(ErrorMessage = "Priezvisko a meno musí byť zadané!")]
        [Display(Name = "Priezvisko a meno")]
        public string Meno { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email musí byť zadaný!")]
        [Display(Name = "Email")]
        [CustomEmail(ErrorMessage = "Nezadali ste platnú mailovú adresu!")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefón musí byť zadaný!")]
        [Display(Name = "Telefón")]
        [CustomPhone(ErrorMessage = "Zadajte platné telefónne číslo.")]
        public string Telefon { get; set; } = string.Empty;

        [Required(ErrorMessage = "Text správy musí byť zadaný!")]
        [Display(Name = "Text správy")]
        public string Sprava { get; set; } = string.Empty;
    }
}
