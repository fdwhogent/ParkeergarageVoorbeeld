namespace ParkeergarageVoorbeeld.DomeinLogicaInApp.InMain {
    /*
    * We registreren in deze app wanneer een voertuig binnenkomt en wanneer 
    * een voertuig vertrekt. We zorgen voor de mogelijkheid om te zien 
    * hoeveel voertuigen er in de garage staan, hoeveel plaatsen er bezet 
    * zijn, en of de garage vol is... 
    */
    internal class Program {
        static void Main() {

            const int capaciteit = 20;
            int aantalvoertuigen = 0;

            string laasteSlagboomPassage = string.Empty;
            SlagboomRegistratie?[] slagboomPassages = new SlagboomRegistratie?[32];
            DateTime?[] slagboomPassagesTijdstippen = new DateTime?[32];
            int aantalSlagboomPassages = 0;

            do {
                Console.Clear();
                if (laasteSlagboomPassage != string.Empty)
                    Console.WriteLine($"Laatste slagboompassage: {laasteSlagboomPassage}");
                else
                    Console.WriteLine();

                Console.WriteLine($"De garage heeft plaats voor {capaciteit} voertuig's.");
                Console.WriteLine($"Bezet: {aantalvoertuigen}");

                bool isVol = aantalvoertuigen >= capaciteit;
                if (isVol) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Garage is VOL");

                } else {
                    int aantalVrij = capaciteit - aantalvoertuigen;
                    Console.WriteLine($"Vrij: {aantalVrij}");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Garage is NIET vol");
                }
                Console.ResetColor();

                bool isLeeg = aantalvoertuigen == 0;
                Console.WriteLine();
                if (isVol) {
                    Console.WriteLine("Druk op '-' indien een voertuig vertrekt. (q om te stoppen)");
                } else if (isLeeg) {
                    Console.WriteLine("Druk op '+' indien een voertuig vertrekt. (q om te stoppen)");
                } else {
                    Console.WriteLine("Druk op '+' indien een voertuig binnenrijdt, '-' voor vertrek. (q om te stoppen)");
                }

                var key = Console.ReadKey(true);
                if (key.KeyChar == 'q' || key.KeyChar == 'Q') {
                    break;
                } else if ((!isLeeg && key.KeyChar == '-') || (!isVol && key.KeyChar == '+')) {
                    if (aantalSlagboomPassages + 1 > slagboomPassages.Length) {
                        Array.Resize(ref slagboomPassages, slagboomPassages.Length * 2);
                        Array.Resize(ref slagboomPassagesTijdstippen, slagboomPassagesTijdstippen.Length * 2);
                    }
                    if (!isLeeg && key.KeyChar == '-') {
                        aantalvoertuigen--;
                        slagboomPassages[aantalSlagboomPassages] = SlagboomRegistratie.Vertrekken;
                    } else if (!isVol && key.KeyChar == '+') {
                        aantalvoertuigen++;
                        slagboomPassages[aantalSlagboomPassages] = SlagboomRegistratie.Binnenrijden;
                    }
                    slagboomPassagesTijdstippen[aantalSlagboomPassages] = DateTime.Now;
                    laasteSlagboomPassage = $"{slagboomPassages[aantalSlagboomPassages]} om {slagboomPassagesTijdstippen[aantalSlagboomPassages]}.";
                    aantalSlagboomPassages++;
                }

            } while (true);

            Console.Clear();
            Console.WriteLine("Overzicht van alle slagboompassages:");
            for (int i = 0; i < aantalSlagboomPassages; i++) {
                Console.WriteLine($"> {slagboomPassagesTijdstippen[i]}: {slagboomPassages[i]}");
            }

        }
    }
}
namespace ParkeergarageVoorbeeld {
    public enum SlagboomRegistratie {
        Binnenrijden,
        Vertrekken
    }
}
namespace ParkeergarageVoorbeeld.DomeinLogicaInApp.InMethodsEnMetVelden {
    /*
    * Om de app wat meer te structureren, kunnen we de logica in methodes plaatsen...
    * Methods die aan de slag gaan met informatie die we in velden bewaren...
    */
    internal class Program {
        static void Main() {

            do {
                Console.Clear();
                if (AantalSlagboomPassages() > 0)
                    Console.WriteLine($"Laatste slagboompassage: {LaatsteSlagboomPassage()} om {LaatsteSlagboomPassageTijdstip()}");
                else
                    Console.WriteLine();

                Console.WriteLine($"De garage heeft plaats voor {Capaciteit()} voertuig's.");
                Console.WriteLine($"Bezet: {AantalVoertuigen()}");

                bool isVol = IsVol();
                if (isVol) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Garage is VOL");

                } else {
                    Console.WriteLine($"Vrij: {AantalVrijePlaatsen()}");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Garage is NIET vol");
                }
                Console.ResetColor();

                bool isLeeg = IsLeeg();
                Console.WriteLine();
                if (isVol) {
                    Console.WriteLine("Druk op '-' indien een voertuig vertrekt. (q om te stoppen)");
                } else if (isLeeg) {
                    Console.WriteLine("Druk op '+' indien een voertuig vertrekt. (q om te stoppen)");
                } else {
                    Console.WriteLine("Druk op '+' indien een voertuig binnenrijdt, '-' voor vertrek. (q om te stoppen)");
                }

                var key = Console.ReadKey(true);
                if (key.KeyChar == 'q' || key.KeyChar == 'Q') {
                    break;
                } else {
                    if (!isLeeg && key.KeyChar == '-')
                        RegistreerVertrek();
                    else if (!isVol && key.KeyChar == '+')
                        RegistreerBinnenrijden();
                }

            } while (true);

            Console.Clear();
            Console.WriteLine("Overzicht van alle slagboompassages:");
            for (int i = 0; i < AantalSlagboomPassages(); i++) {
                Console.WriteLine($"> {SlagboomPassageTijdstip(i)}: {SlagboomPassage(i)}");
            }

        }
        // Velden die de informatie bewaren... (merk op: velden die we nooit in de Main methode gebruiken)
        static private int _aantalVoertuigen = 0;
        static private readonly int _capaciteit = 20;
        static private SlagboomRegistratie[] _slagboomPassages = new SlagboomRegistratie[32];
        static private DateTime[] _slagboomPassagesTijdstippen = new DateTime[32];
        static private int _aantalSlagboomPassages = 0;
        // Commando methods die de informatie in de velden aanpassen...
        static private void RegistreerBinnenrijden() {
            _aantalVoertuigen++;
            RegistreerSlagboomPassage(SlagboomRegistratie.Binnenrijden);
        }
        static private void RegistreerVertrek() {
            _aantalVoertuigen--;
            RegistreerSlagboomPassage(SlagboomRegistratie.Vertrekken);
        }
        static private void RegistreerSlagboomPassage(SlagboomRegistratie type) {
            if (AantalSlagboomPassages() + 1 > _slagboomPassages.Length) {
                Array.Resize(ref _slagboomPassages, _slagboomPassages.Length * 2);
                Array.Resize(ref _slagboomPassagesTijdstippen, _slagboomPassagesTijdstippen.Length * 2);
            }
            _slagboomPassages[_aantalSlagboomPassages] = type;
            _slagboomPassagesTijdstippen[_aantalSlagboomPassages] = DateTime.Now;
            _aantalSlagboomPassages++;
        }
        // Query methods die informatie teruggeven op basis van de informatie in de velden...
        static private SlagboomRegistratie? SlagboomPassage(int index) {
            if (index < 0 || index >= AantalSlagboomPassages())
                return null;
            return _slagboomPassages[index];
        }
        static private DateTime? SlagboomPassageTijdstip(int index) {
            if (index < 0 || index >= AantalSlagboomPassages())
                return null;
            return _slagboomPassagesTijdstippen[index];
        }
        static private SlagboomRegistratie? LaatsteSlagboomPassage() {
            if (AantalSlagboomPassages() <= 0)
                return null;
            return _slagboomPassages[AantalSlagboomPassages() - 1];
        }
        static private DateTime? LaatsteSlagboomPassageTijdstip() {
            if (AantalSlagboomPassages() <= 0)
                return null;
            return _slagboomPassagesTijdstippen[AantalSlagboomPassages() - 1];
        }
        static private int AantalSlagboomPassages() { return _aantalSlagboomPassages; }
        static private int Capaciteit() { return _capaciteit; }
        static private int AantalVoertuigen() { return _aantalVoertuigen; }
        static private int AantalVrijePlaatsen() { return Capaciteit() - AantalVoertuigen(); }
        static private bool IsVol() { return AantalVrijePlaatsen() <= 0; }
        static private bool IsLeeg() { return AantalVoertuigen() <= 0; }
    }
}
namespace ParkeergarageVoorbeeld.DomeinLogicaInApp.MeerdereGarages {
    // In deze app kunnen we meerdere garages beheren... 
    // Per garage zien we de details...
    // Toch willen we niet de voordelen niet verliezen van vorige versie, die een heel leesbare Main methode had...
    internal class Program {
        static void Main() {
            do {
                Console.WriteLine($"Garage X: (capaciteit: {CapaciteitX()})");
                Console.WriteLine($"   {AantalVoertuigenX()} voertuigen aanwezig => {(IsVolX() ? "vol" : IsLeegX() ? "leeg" : $"{AantalVrijePlaatsenX()} beschikbaar")}");

                Console.WriteLine($"Garage Y: (capaciteit: {CapaciteitY()})");
                Console.WriteLine($"   {AantalVoertuigenY()} voertuigen aanwezig => {(IsVolY() ? "vol" : IsLeegY() ? "leeg" : $"{AantalVrijePlaatsenY()} beschikbaar")}");

                Console.Write("Keuze ( x+ / x- / y+ / y- ): ");
                string keuze = Console.ReadLine();
                if (keuze == "x+" && !IsVolX()) {
                    RegistreerBinnenrijdenX();
                } else if (keuze == "x-" && !IsLeegX()) {
                    RegistreerVertrekX();
                } else if (keuze == "y+" && !IsVolY()) {
                    RegistreerBinnenrijdenY();
                } else if (keuze == "y-" && !IsLeegY()) {
                    RegistreerVertrekY();
                }
                Console.Clear();
            } while (true);
        }
        // Voor garage X...
        static private int _aantalVoertuigenX = 0;
        static private readonly int _capaciteitX = 20;
        static private void RegistreerBinnenrijdenX() {
            _aantalVoertuigenX++;
        }
        static private void RegistreerVertrekX() {
            _aantalVoertuigenX--;
        }
        static private int CapaciteitX() { return _capaciteitX; }
        static private int AantalVoertuigenX() { return _aantalVoertuigenX; }
        static private int AantalVrijePlaatsenX() { return CapaciteitX() - AantalVoertuigenX(); }
        static private bool IsVolX() { return AantalVrijePlaatsenX() <= 0; }
        static private bool IsLeegX() { return AantalVoertuigenX() <= 0; }
        // Voor garage Y...
        static private int _aantalVoertuigenY = 0;
        static private readonly int _capaciteitY = 10;
        static private void RegistreerBinnenrijdenY() {
            _aantalVoertuigenY++;
        }
        static private void RegistreerVertrekY() {
            _aantalVoertuigenY--;
        }
        static private int CapaciteitY() { return _capaciteitY; }
        static private int AantalVoertuigenY() { return _aantalVoertuigenY; }
        static private int AantalVrijePlaatsenY() { return CapaciteitY() - AantalVoertuigenY(); }
        static private bool IsVolY() { return AantalVrijePlaatsenY() <= 0; }
        static private bool IsLeegY() { return AantalVoertuigenY() <= 0; }
    }
    // Deze aanpak is niet doenbaar als we vele garages (hoeveel?) moeten beheren... 
}
namespace ParkeergarageVoorbeeld.DomeinLogicaUitApp.ModulaireAanpak {
    // De oplossing bestaat erin de logica om te beginnen we te schuiven uit de App naar een 
    // aparte "module" (hier: klasse) die de informatie bewaart en de nodige methodes aanbiedt
    // om die informatie te manipuleren en te bevragen...
    class Parkeergarage { // "module"
        // State (gegevens/informatie):
        // Velden die de informatie bewaren... (merk op: steeds private, toch louter intern (hier binnen de klasse) aangesproken)
        static private int _aantalVoertuigen = 0;
        static private readonly int _capaciteit = 20;
        static private SlagboomRegistratie[] _slagboomPassages = new SlagboomRegistratie[32];
        static private DateTime[] _slagboomPassagesTijdstippen = new DateTime[32];
        static private int _aantalSlagboomPassages = 0;

        // Behavior (gedrag): (merk op: vaak public, hierdoor kan de buitenwereld er gebruik van maken (interactie mee hebben))
        // Commando methods ("manipulatie") die de informatie in de velden aanpassen...
        static public void RegistreerBinnenrijden() {
            _aantalVoertuigen++;
            RegistreerSlagboomPassage(SlagboomRegistratie.Binnenrijden);
        }
        static public void RegistreerVertrek() {
            _aantalVoertuigen--;
            RegistreerSlagboomPassage(SlagboomRegistratie.Vertrekken);
        }
        static private void RegistreerSlagboomPassage(SlagboomRegistratie type) { // deze eerder hulpmethod => private
            if (AantalSlagboomPassages() + 1 > _slagboomPassages.Length)
                Array.Resize(ref _slagboomPassages, _slagboomPassages.Length * 2);
            _slagboomPassages[_aantalSlagboomPassages] = type;
            _slagboomPassagesTijdstippen[_aantalSlagboomPassages] = DateTime.Now;
            _aantalSlagboomPassages++;
        }
        // Query methods ("bevragen") die informatie teruggeven op basis van de informatie in de velden...
        static public SlagboomRegistratie? SlagboomPassage(int index) {
            if (index < 0 || index >= AantalSlagboomPassages())
                return null;
            return _slagboomPassages[index];
        }
        static public DateTime? SlagboomPassageTijdstip(int index) {
            if (index < 0 || index >= AantalSlagboomPassages())
                return null;
            return _slagboomPassagesTijdstippen[index];
        }
        static public SlagboomRegistratie? LaatsteSlagboomPassage() {
            if (AantalSlagboomPassages() <= 0)
                return null;
            return _slagboomPassages[AantalSlagboomPassages() - 1];
        }
        static public DateTime? LaatsteSlagboomPassageTijdstip() {
            if (AantalSlagboomPassages() <= 0)
                return null;
            return _slagboomPassagesTijdstippen[AantalSlagboomPassages() - 1];
        }
        static public int AantalSlagboomPassages() { return _aantalSlagboomPassages; }
        static public int Capaciteit() { return _capaciteit; }
        static public int AantalVoertuigen() { return _aantalVoertuigen; }
        static public int AantalVrijePlaatsen() { return Capaciteit() - AantalVoertuigen(); }
        static public bool IsVol() { return AantalVrijePlaatsen() <= 0; }
        static public bool IsLeeg() { return AantalVoertuigen() <= 0; }
    }
    internal class Program1 {
        static void Main() {

            do {
                Console.Clear();
                if (Parkeergarage.AantalSlagboomPassages() > 0)
                    Console.WriteLine($"Laatste slagboompassage: {Parkeergarage.LaatsteSlagboomPassage()} om {Parkeergarage.LaatsteSlagboomPassageTijdstip()}");
                else
                    Console.WriteLine();

                Console.WriteLine($"De garage heeft plaats voor {Parkeergarage.Capaciteit()} voertuig's.");
                Console.WriteLine($"Bezet: {Parkeergarage.AantalVoertuigen()}");

                bool isVol = Parkeergarage.IsVol();
                if (isVol) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Garage is VOL");

                } else {
                    Console.WriteLine($"Vrij: {Parkeergarage.AantalVrijePlaatsen()}");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Garage is NIET vol");
                }
                Console.ResetColor();

                bool isLeeg = Parkeergarage.IsLeeg();
                Console.WriteLine();
                if (isVol) {
                    Console.WriteLine("Druk op '-' indien een voertuig vertrekt. (q om te stoppen)");
                } else if (isLeeg) {
                    Console.WriteLine("Druk op '+' indien een voertuig vertrekt. (q om te stoppen)");
                } else {
                    Console.WriteLine("Druk op '+' indien een voertuig binnenrijdt, '-' voor vertrek. (q om te stoppen)");
                }

                var key = Console.ReadKey(true);
                if (key.KeyChar == 'q' || key.KeyChar == 'Q') {
                    break;
                } else {
                    if (!isLeeg && key.KeyChar == '-')
                        Parkeergarage.RegistreerVertrek();
                    else if (!isVol && key.KeyChar == '+')
                        Parkeergarage.RegistreerBinnenrijden();
                }

            } while (true);

            Console.Clear();
            Console.WriteLine("Overzicht van alle slagboompassages:");
            for (int i = 0; i < Parkeergarage.AantalSlagboomPassages(); i++) {
                Console.WriteLine($"> {Parkeergarage.SlagboomPassageTijdstip(i)}: {Parkeergarage.SlagboomPassage(i)}");
            }

        }
    }
    // Deze aanpak is interessant, want nu kan de logica van de parkeergarage ook hergebruikt worden in andere apps... (denk aan een webapp, of een mobiele app...)
    internal class Program2 {
        static void Main() {
            Parkeergarage.RegistreerBinnenrijden();
            Console.WriteLine(Parkeergarage.LaatsteSlagboomPassage() + " op " + Parkeergarage.LaatsteSlagboomPassageTijdstip());
        }

    }
}
namespace ParkeergarageVoorbeeld.DomeinLogicaUitApp.ObjectGeoriënteerdeAanpak {
    // We kunnen de logica van de parkeergarage ook met non-static members uitschrijven, hierdoor
    // maken we van de Parkeergarage code-eenheid/module een echt datatype.  Een type dat we kunnen
    // gebruiken om objecten/instanties van te maken, objecten die elk hun eigen informatie bewaren
    // en manipuleren...
    class Parkeergarage {
        // State (gegevens/informatie):
        // Velden die de informatie bewaren... (merk op: steeds private, toch louter intern (hier binnen de klasse) aangesproken)
        private int _aantalVoertuigen = 0;
        private int _capaciteit = 100;
        private SlagboomRegistratie[] _slagboomPassages = new SlagboomRegistratie[32];
        private DateTime[] _slagboomPassagesTijdstippen = new DateTime[32];
        private int _aantalSlagboomPassages = 0;

        // Behavior (gedrag): (merk op: vaak public, hierdoor kan de buitenwereld er gebruik van maken (interactie mee hebben))
        // Commando methods ("manipulatie") die de informatie in de velden aanpassen...
        public void RegistreerBinnenrijden() {
            _aantalVoertuigen++;
            RegistreerSlagboomPassage(SlagboomRegistratie.Binnenrijden);
        }
        public void RegistreerVertrek() {
            _aantalVoertuigen--;
            RegistreerSlagboomPassage(SlagboomRegistratie.Vertrekken);
        }
        private void RegistreerSlagboomPassage(SlagboomRegistratie type) { // deze eerder hulpmethod => private
            if (AantalSlagboomPassages() + 1 > _slagboomPassages.Length)
                Array.Resize(ref _slagboomPassages, _slagboomPassages.Length * 2);
            _slagboomPassages[_aantalSlagboomPassages] = type;
            _slagboomPassagesTijdstippen[_aantalSlagboomPassages] = DateTime.Now;
            _aantalSlagboomPassages++;
        }
        public void SetCapaciteit(int capaciteit) { _capaciteit = capaciteit; }
        // Query methods ("bevragen") die informatie teruggeven op basis van de informatie in de velden...
        public SlagboomRegistratie? SlagboomPassage(int index) {
            if (index < 0 || index >= AantalSlagboomPassages())
                return null;
            return _slagboomPassages[index];
        }
        public DateTime? SlagboomPassageTijdstip(int index) {
            if (index < 0 || index >= AantalSlagboomPassages())
                return null;
            return _slagboomPassagesTijdstippen[index];
        }
        public SlagboomRegistratie? LaatsteSlagboomPassage() {
            if (AantalSlagboomPassages() <= 0)
                return null;
            return _slagboomPassages[AantalSlagboomPassages() - 1];
        }
        public DateTime? LaatsteSlagboomPassageTijdstip() {
            if (AantalSlagboomPassages() <= 0)
                return null;
            return _slagboomPassagesTijdstippen[AantalSlagboomPassages() - 1];
        }
        public int AantalSlagboomPassages() { return _aantalSlagboomPassages; }
        public int GetCapaciteit() { return _capaciteit; }
        public int AantalVoertuigen() { return _aantalVoertuigen; }
        public int AantalVrijePlaatsen() { return GetCapaciteit() - AantalVoertuigen(); }
        public bool IsVol() { return AantalVrijePlaatsen() <= 0; }
        public bool IsLeeg() { return AantalVoertuigen() <= 0; }
    }
    internal class Program1 {
        static void Main() {
            Parkeergarage garageX = new Parkeergarage();
            Parkeergarage garageY = new Parkeergarage();
            do {
                Console.WriteLine($"Garage X: (capaciteit: {garageX.GetCapaciteit()})");
                Console.WriteLine($"   {garageX.AantalVoertuigen()} voertuigen aanwezig => {(garageX.IsVol() ? "vol" : garageX.IsLeeg() ? "leeg" : $"{garageX.AantalVrijePlaatsen()} beschikbaar")}");

                Console.WriteLine($"Garage Y: (capaciteit: {garageY.GetCapaciteit()})");
                Console.WriteLine($"   {garageY.AantalVoertuigen()} voertuigen aanwezig => {(garageY.IsVol() ? "vol" : garageY.IsLeeg() ? "leeg" : $"{garageY.AantalVrijePlaatsen()} beschikbaar")}");

                Console.Write("Keuze ( x+ / x- / y+ / y- ): ");
                string keuze = Console.ReadLine();
                if (keuze == "x+" && !garageX.IsVol()) {
                    garageX.RegistreerBinnenrijden();
                } else if (keuze == "x-" && !garageX.IsLeeg()) {
                    garageX.RegistreerVertrek();
                } else if (keuze == "y+" && !garageY.IsVol()) {
                    garageY.RegistreerBinnenrijden();
                } else if (keuze == "y-" && !garageY.IsLeeg()) {
                    garageY.RegistreerVertrek();
                }
                Console.Clear();
            } while (true);
        }
    }
    internal class Program2 {
        static void Main() {
            Parkeergarage[] garages = new Parkeergarage[10];
            for (int i = 0; i < garages.Length; i++) {
                garages[i] = new Parkeergarage();
                garages[i].SetCapaciteit(10 * (i+1));
            }

            do {
                for (int i = 0; i < garages.Length; i++) {
                    Parkeergarage garage = garages[i];
                    Console.WriteLine($"Garage {i + 1}: (capaciteit: {garage.GetCapaciteit()})");
                    Console.WriteLine($"   {garage.AantalVoertuigen()} voertuigen aanwezig => {(garage.IsVol() ? "vol" : garage.IsLeeg() ? "leeg" : $"{garage.AantalVrijePlaatsen()} beschikbaar")}");
                }

                Console.Write("Garage-nummer ?: ");
                string garageNummer = Console.ReadLine();

                Parkeergarage garageDieWeGaanAanpassen = garages[int.Parse(garageNummer) - 1];

                Console.Write("Keuze ( + / - ): ");
                string keuze = Console.ReadLine();

                if (keuze == "+" && !garageDieWeGaanAanpassen.IsVol()) {
                    garageDieWeGaanAanpassen.RegistreerBinnenrijden();
                } else if (keuze == "-" && !garageDieWeGaanAanpassen.IsLeeg()) {
                    garageDieWeGaanAanpassen.RegistreerVertrek();
                }
                Console.Clear();
            } while (true);
        }
    }
}