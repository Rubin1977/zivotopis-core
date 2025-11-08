using ZivotopisCore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ZivotopisCore.Models.Home;


namespace ZivotopisCore.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return RedirectToAction("Uvod");
        }

        public IActionResult Uvod()
        {
            ViewBag.Message = ("Rastislav Osobná Stránka");
            return View();
        }
        public IActionResult OsobneUdaje()
        {
            ViewBag.Message = "Ružbacký osobné údaje";

            var model = new OsobneUdajeModel
            {
                Titul = "RNDr.",
                Meno = "Rastislav",
                Priezvisko = "Ružbacký",
                Obec = "Bratislava",
                Stav = "Ženatý",
                Email = "ruzbacky@yahoo.com",
                Telefon = "+421 948 900 850",
                LinkedIn = "https://www.linkedin.com/in/rastislav-ruzbacky-99a8a645/",
                GitHub = "https://github.com/Rubin1977"
            };

            return View(model);
        }

        public IActionResult Vzdelanie()
        {
            ViewBag.Message = "Ružbacký vzdelanie";

            var model = new VzdelanieModel();

            // Vzdelanie
            model.Vzdelania.Add(new Vzdelanie
            {
                Obdobie = "06/09/1995 - 31/05/2000",
                Skola = "UPJŠ Košice",
                Fakulta = "Prírodovedecká Fakulta",
                Titul = "Mgr.",
                Popis = "Biológia / Antropológia"
            });

            model.Vzdelania.Add(new Vzdelanie
            {
                Obdobie = "01/10/2000 - 14/02/2002 a 01/04/2003 - 31/10/2003",
                Skola = "Prešovská Univerzita",
                Fakulta = "Fakulta humanitných a prírodných vied",
                Titul = "RNDr.",
                Popis = "Interná forma postgraduálneho štúdia antropológie. Rigorózna práca z biológie: Detekcia STR lókusov v rómskej populácií východného Slovenska a PCR metóda."
            });

            model.Vzdelania.Add(new Vzdelanie
            {
                Obdobie = "14/06/2007",
                Skola = "Slovenská zdravotnícka univerzita v Bratislave",
                Fakulta = "",
                Titul = "Diplom",
                Popis = "Špecializácia v odbore laboratórne a diagnostické metódy v klinickej genetike"
            });

            // Kurzy
            model.Kurzy.Add(new Kurz
            {
                Datum = "01/11/2003 - 29/04/2004",
                Nazov = "Certifikát pre prácu v zdravotníctve pre zdravotníckych pracovníkov v lekárskej genetike"
            });

            model.Kurzy.Add(new Kurz
            {
                Datum = "2024",
                Nazov = "Certifikát o absolvovaní základov jazyka Python"
            });

            model.Kurzy.Add(new Kurz
            {
                Datum = "2025",
                Nazov = "Vytvorenie webovej aplikácie pomocou .NET frameworku, implementácie backend logiky a nasadenia do produkcie"
            });

            // Skúsenosti
            model.Skusenosti.Add(new Skusenost
            {
                Obdobie = "01/06/2006 - 31/12/2013",
                Pozicia = "Cytogenetik",
                Miesto = "Národný onkologický ústav Bratislava",
                PopisPrace = [
                    "Zakladanie a spracovanie nenádorových a nádorových bunkových kultúr",
                    "Hodnotenie cytogenetických sekcií metódami priamej mikroskopie",
                    "Spracovanie materiálu metódami DNA/RNA diagnostiky (Nested PCR, Real-time PCR)",
                    "Separácia CD 138+ plazmatických buniek pri mnohopočetnom myelóme"
                ]
            });

            model.Skusenosti.Add(new Skusenost
            {
                Obdobie = "01/11/2003 - 31/05/2006",
                Pozicia = "Cytogenetik",
                Miesto = "Nemocnica s poliklinikou Spišská Nová Ves",
                PopisPrace = [       
                "Zakladanie a spracovanie krátkodobých tkanivových kultúr",
                "Prenatálne hodnotenie z plodovej vody",
                "Automatické karyotypovanie (LUCIA)"
                ]       
            });

            return View(model);
        }
        public IActionResult Kontakt()
        {
            ViewBag.Message = "Kontakt";
            var model = new OdoslanieSpravyModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OdoslanieSpravy(OdoslanieSpravyModel model)
        {
            if (ModelState.IsValid)
            {
                // Tu môžeš spracovať správu (napr. poslať email, uložiť do databázy, logovať)
                return View("OdoslanieSpravy"); // Zobrazí OdoslanieSpravy.cshtml
            }

            // Ak validácia zlyhá, vráti späť formulár s chybami
            return View("Kontakt", model);
        }


    }

}