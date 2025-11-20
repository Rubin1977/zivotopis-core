using Microsoft.AspNetCore.Mvc;
using ZivotopisCore.Models.Home;

namespace ZivotopisCore.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction(nameof(Uvod));
    }

    public IActionResult Uvod()
    {
        ViewBag.Message = "Rastislav Osobná Stránka";
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
        };
        // 🔑 Tu pridáš sociálne siete do zoznamu

        model.SocialLinks.Add(new SocialLink
        {
            Nazov = "Facebook",
            Url = "https://www.facebook.com/profile.php?id=100009882270245", // sem daj svoj reálny FB profil
            IkonaClass = "fab fa-facebook"
        });
        model.SocialLinks.Add(new SocialLink {
                Nazov = "LinkedIn",
                Url = "https://www.linkedin.com/in/rastislav-ruzbacky-99a8a645/",
                IkonaClass = "fab fa-linkedin"
            });

        model.SocialLinks.Add(new SocialLink
        {
            Nazov = "GitHub",
            Url = "https://github.com/Rubin1977",
            IkonaClass = "fab fa-github"
        });

        model.SocialLinks.Add(new SocialLink
        {
            Nazov = "Instagram",
            Url = "https://www.instagram.com/rastorubin",
            IkonaClass = "fab fa-instagram"
        });
        return View(model);
    }

    public IActionResult Vzdelanie()
    {
        ViewBag.Message = "Ružbacký vzdelanie";

        var model = new VzdelanieModel
        {
            Vzdelania = new List<Vzdelanie>
            {
                new()
                {
                    Obdobie = "06/09/1995 - 31/05/2000",
                    Skola = "UPJŠ Košice",
                    Fakulta = "Prírodovedecká Fakulta",
                    Titul = "Mgr.",
                    Popis = "Biológia / Antropológia"
                },
                new()
                {
                    Obdobie = "01/10/2000 - 14/02/2002 a 01/04/2003 - 31/10/2003",
                    Skola = "Prešovská Univerzita",
                    Fakulta = "Fakulta humanitných a prírodných vied",
                    Titul = "RNDr.",
                    Popis = "Interná forma postgraduálneho štúdia antropológie. Rigorózna práca z biológie: Detekcia STR lókusov v rómskej populácií východného Slovenska a PCR metóda."
                },
                new()
                {
                    Obdobie = "14/06/2007",
                    Skola = "Slovenská zdravotnícka univerzita v Bratislave",
                    Fakulta = "",
                    Titul = "Diplom",
                    Popis = "Špecializácia v odbore laboratórne a diagnostické metódy v klinickej genetike"
                }
            },
            Kurzy = new List<Kurz>
            {
                new() { Datum = "01/11/2003 - 29/04/2004", Nazov = "Certifikát pre prácu v zdravotníctve pre zdravotníckych pracovníkov v lekárskej genetike" },
                new() { Datum = "2024", Nazov = "Certifikát o absolvovaní základov programovacieho jazyka Python", Url = ""}, // ak nemáš odkaz, nechaj prázdne 
                new() { Datum = "2024", Nazov = "Django Girls Workshop – základy webového vývoja v Pythone/Django\r\n   Praktický výstup: osobná webová aplikácia \r\n      👉 ", Url = "https://rastislavruzbacky.eu.pythonanywhere.com"},
                new() { Datum = "2025", Nazov = "Vytvorenie webovej aplikácie pomocou .NET frameworku kurz DeveloperBoss, implementácia backend logiky a nasadenie do produkcie \r\n      👉", Url = "https://zivotopis-rastislav.onrender.com/Home/Uvod" },
                new() { Datum = "2025", Nazov = "Testovanie softwaru JUNIOR I. – certifikát (IT Learning Slovakia)\r\n  Zameranie: základy QA, bug reporting, manuálne testovanie webových aplikácií 👉", Url =  "https://www.itlearning.sk/detail/testovanie-sw-balik-zaciatocnik/" },
                new() { Datum = "2025", Nazov = "Pokročilý kurz Testovanie softwaru SENIOR II. – prebiehajúci\r\n   Zameranie: automatizované testovanie, Selenium, SOAPUI, praktické QA cvičenia 👉", Url =  "https://www.itlearning.sk/detail/testovanie-sw-balik-zaciatocnik/"  }
            },
            Skusenosti = new List<Skusenost>
            {
                new()
                {
                    Obdobie = "01/06/2006 - 31/12/2013",
                    Pozicia = "Cytogenetik",
                    Miesto = "Národný onkologický ústav Bratislava",
                    PopisPrace = new List<string>
                    {
                        "Zakladanie a spracovanie nenádorových a nádorových bunkových kultúr",
                        "Hodnotenie cytogenetických sekcií metódami priamej mikroskopie",
                        "Spracovanie materiálu metódami DNA/RNA diagnostiky (Nested PCR, Real-time PCR)",
                        "Separácia CD 138+ plazmatických buniek pri mnohopočetnom myelóme"
                    }
                },
                new()
                {
                    Obdobie = "01/11/2003 - 31/05/2006",
                    Pozicia = "Cytogenetik",
                    Miesto = "Nemocnica s poliklinikou Spišská Nová Ves",
                    PopisPrace = new List<string>
                    {
                        "Zakladanie a spracovanie krátkodobých tkanivových kultúr",
                        "Prenatálne hodnotenie z plodovej vody",
                        "Konštitučné karyotypovanie"
                    }
                }
            }
        };

        return View(model);
    }

    public IActionResult Kontakt()
    {
        ViewBag.Message = "Kontakt";
        return View(new OdoslanieSpravyModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult OdoslanieSpravy(OdoslanieSpravyModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Message = "Kontakt";
            return View("Kontakt", model);
        }

        // Tu môžeš spracovať správu (napr. poslať email, uložiť do databázy, logovať)
        TempData["SuccessMessage"] = "Správa bola úspešne odoslaná.";
        return View("OdoslanieSpravy");
    }
}
