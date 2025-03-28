using System;
using System.Collections.Generic;

public class Pacjent
{
    private string pesel;
    private string imie;
    private string nazwisko;
    private int wiek;
    private List<double> ocenyWizyt = new List<double>();

    public Pacjent(string pesel, string imie, string nazwisko, int wiek)
    {
        this.pesel = pesel;
        this.imie = imie;
        this.nazwisko = nazwisko;
        this.wiek = wiek;
    }

    public string Pesel => pesel;
    public string Imie => imie;
    public string Nazwisko => nazwisko;
    public int Wiek => wiek;
    public List<double> OcenyWizyt => ocenyWizyt;

    public void WyswietlPacjenta()
    {
        Console.WriteLine($"PESEL: {pesel}, Imię: {imie}, Nazwisko: {nazwisko}, Wiek: {wiek}");
        Console.WriteLine("Oceny wizyt: " + string.Join(", ", ocenyWizyt));
    }
}

public class Przychodnia
{
    private List<double> ListaDopuszczalnychOcen = new List<double> { 1, 2, 3, 4, 5 };
    private List<Pacjent> ListaPacjentow = new List<Pacjent>();

    // Metoda dodająca pacjenta
    public void DodajPacjenta()
    {
        Console.Write("Podaj PESEL pacjenta: ");
        string pesel = Console.ReadLine();
        
        // Walidacja PESEL ( jest poprawny, jeżeli ma 11 cyfr)
        if (pesel.Length != 11 || !long.TryParse(pesel, out _))
        {
            Console.WriteLine("Błąd! PESEL musi zawierać 11 cyfr.");
            return;
        }

        Console.Write("Podaj imię: ");
        string imie = Console.ReadLine();
        Console.Write("Podaj nazwisko: ");
        string nazwisko = Console.ReadLine();
        Console.Write("Podaj wiek: ");
        int wiek;
        while (!int.TryParse(Console.ReadLine(), out wiek) || wiek <= 0)
        {
            Console.Write("Błąd! Podaj poprawny wiek: ");
        }

        Pacjent nowyPacjent = new Pacjent(pesel, imie, nazwisko, wiek);

        // Wprowadzanie ocen wizyt
        bool dodawanieOcen = true;
        while (dodawanieOcen)
        {
            Console.WriteLine("Wprowadź ocenę wizyty (dopuszczalne: 1, 2, 3, 4, 5). Wpisz '0' aby zakończyć:");
            double ocena;
            while (!double.TryParse(Console.ReadLine(), out ocena) || (ocena != 0 && !ListaDopuszczalnychOcen.Contains(ocena)))
            {
                Console.WriteLine("Błąd! Wprowadzona ocena jest niedopuszczalna. Spróbuj ponownie.");
            }

            if (ocena == 0)
            {
                dodawanieOcen = false;
            }
            else
            {
                nowyPacjent.OcenyWizyt.Add(ocena);
            }
        }

        ListaPacjentow.Add(nowyPacjent);
        Console.WriteLine("Pacjent został dodany pomyślnie.");
    }

    // Metoda usuwająca pacjenta
    public void UsunPacjenta()
    {
        Console.Write("Podaj PESEL pacjenta do usunięcia: ");
        string pesel = Console.ReadLine();

        Pacjent pacjentDoUsuniecia = ListaPacjentow.Find(p => p.Pesel == pesel);
        if (pacjentDoUsuniecia != null)
        {
            ListaPacjentow.Remove(pacjentDoUsuniecia);
            Console.WriteLine("Pacjent został usunięty.");
        }
        else
        {
            Console.WriteLine("Pacjent o podanym numerze PESEL nie istnieje.");
        }
    }

    // Metoda wyświetlająca wszystkich pacjentów
    public void WyswietlPacjentow()
    {
        if (ListaPacjentow.Count > 0)
        {
            foreach (var pacjent in ListaPacjentow)
            {
                pacjent.WyswietlPacjenta();
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Brak pacjentów w systemie.");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Przychodnia przychodnia = new Przychodnia();
        bool dzialanie = true;

        while (dzialanie)
        {
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1 - Dodaj pacjenta");
            Console.WriteLine("2 - Usuń pacjenta");
            Console.WriteLine("3 - Wyświetl wszystkich pacjentów");
            Console.WriteLine("0 - Zakończ");
            string wybor = Console.ReadLine();

            try
            {
                switch (wybor)
                {
                    case "1":
                        przychodnia.DodajPacjenta();
                        break;
                    case "2":
                        przychodnia.UsunPacjenta();
                        break;
                    case "3":
                        przychodnia.WyswietlPacjentow();
                        break;
                    case "0":
                        dzialanie = false;
                        break;
                    default:
                        Console.WriteLine("Nieznana opcja, spróbuj ponownie.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
        }
    }
}
