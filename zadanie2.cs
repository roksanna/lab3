using System;
using System.Collections.Generic;

public class Student
{
    private string nr_indeksu;
    private string imie;
    private string nazwisko;
    private int rok_st;
    private List<double> oceny = new List<double>();

    public Student(string nrIndeksu, string imie, string nazwisko, int rokSt)
    {
        nr_indeksu = nrIndeksu;
        this.imie = imie;
        this.nazwisko = nazwisko;
        this.rok_st = rokSt;
    }

    public string NrIndeksu => nr_indeksu;
    public string Imie => imie;
    public string Nazwisko => nazwisko;
    public int RokSt => rok_st;

    public List<double> Oceny => oceny;

    public void WyswietlStudenta()
    {
        Console.WriteLine($"Nr Indeksu: {nr_indeksu}, Imię: {imie}, Nazwisko: {nazwisko}, Rok studiów: {rok_st}");
        Console.WriteLine("Oceny: " + string.Join(", ", oceny));
    }
}

public class Uni
{
    private List<double> ListaDopuszczalnychOcen = new List<double> { 2, 3, 3.5, 4, 4.5, 5 };
    private List<Student> ListaStudentow = new List<Student>();

    // Metoda dodająca studenta
    public void DodajStudenta()
    {
        Console.Write("Podaj nr indeksu: ");
        string nrIndeksu = Console.ReadLine();
        Console.Write("Podaj imię: ");
        string imie = Console.ReadLine();
        Console.Write("Podaj nazwisko: ");
        string nazwisko = Console.ReadLine();
        Console.Write("Podaj rok studiów: ");
        int rokSt = int.Parse(Console.ReadLine());

        Student nowyStudent = new Student(nrIndeksu, imie, nazwisko, rokSt);


        // Wprowadzanie ocen
        List<double> ocenyStudenta = new List<double>();
        bool dodawanieOcen = true;

        while (dodawanieOcen)
        {
            Console.WriteLine("Wprowadź ocenę (dopuszczalne: 2, 3, 3.5, 4, 4.5, 5). Wpisz '0' aby zakończyć:");
            double ocena = double.Parse(Console.ReadLine());

            if (ocena == 0)
            {
                dodawanieOcen = false;
            }
            else if (ListaDopuszczalnychOcen.Contains(ocena))
            {
                ocenyStudenta.Add(ocena);
            }
            else
            {
                Console.WriteLine("Błąd! Wprowadzona ocena jest niedopuszczalna.");
            }
        }

        nowyStudent.Oceny.AddRange(ocenyStudenta);
        ListaStudentow.Add(nowyStudent);
        Console.WriteLine("Student został dodany pomyślnie.");
    }

    // Metoda usuwająca studenta
    public void UsunStudenta()
    {
        Console.Write("Podaj nr indeksu studenta do usunięcia: ");
        string nrIndeksu = Console.ReadLine();

        Student studentDoUsuniecia = ListaStudentow.Find(s => s.NrIndeksu == nrIndeksu);
        if (studentDoUsuniecia != null)
        {
            ListaStudentow.Remove(studentDoUsuniecia);
            Console.WriteLine("Student został usunięty.");
        }
        else
        {
            Console.WriteLine("Student o podanym numerze indeksu nie istnieje.");
        }
    }

    // Metoda obliczająca średnią ocenę dla danego studenta
    public void ObliczSrednia()
    {
        Console.Write("Podaj nr indeksu studenta: ");
        string nrIndeksu = Console.ReadLine();

        Student student = ListaStudentow.Find(s => s.NrIndeksu == nrIndeksu);
        if (student != null && student.Oceny.Count > 0)
        {
            double srednia = 0;
            foreach (var ocena in student.Oceny)
            {
                srednia += ocena;
            }
            srednia /= student.Oceny.Count;
            Console.WriteLine($"Średnia ocen studenta {student.Imie} {student.Nazwisko}: {srednia:F2}");
        }
        else
        {
            Console.WriteLine("Student o podanym numerze indeksu nie istnieje lub nie ma ocen.");
        }
    }


    // Metoda obliczająca średnią ocen dla wszystkich studentów
    public void ObliczSredniaAll()
    {
        if (ListaStudentow.Count > 0)
        {
            double sumaSrednich = 0;
            int liczbaStudentow = 0;
            foreach (var student in ListaStudentow)
            {
                if (student.Oceny.Count > 0)
                {
                    double srednia = 0;
                    foreach (var ocena in student.Oceny)
                    {
                        srednia += ocena;
                    }
                    sumaSrednich += srednia / student.Oceny.Count;
                    liczbaStudentow++;
                }
            }

            if (liczbaStudentow > 0)
            {
                double sredniaAll = sumaSrednich / liczbaStudentow;
                Console.WriteLine($"Średnia ocen wszystkich studentów: {sredniaAll:F2}");
            }
            else
            {
                Console.WriteLine("Brak ocen w systemie.");
            }
        }
        else
        {
            Console.WriteLine("Brak studentów w systemie.");
        }
    }

    // Wyświetlanie wszystkich studentów
    public void WyswietlStudentow()
    {
        if (ListaStudentow.Count > 0)
        {
            foreach (var student in ListaStudentow)
            {
                student.WyswietlStudenta();
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Brak studentów w systemie.");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Uni uni = new Uni();
        bool dzialanie = true;

        while (dzialanie)
        {
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1 - Dodaj studenta");
            Console.WriteLine("2 - Usuń studenta");
            Console.WriteLine("3 - Oblicz średnią ocen dla studenta");
            Console.WriteLine("4 - Oblicz średnią ocen dla wszystkich studentów");
            Console.WriteLine("5 - Wyświetl wszystkich studentów");
            Console.WriteLine("0 - Zakończ");
            string wybor = Console.ReadLine();

            try
            {
                switch (wybor)
                {
                    case "1":
                        uni.DodajStudenta();
                        break;
                    case "2":
                        uni.UsunStudenta();
                        break;
                    case "3":
                        uni.ObliczSrednia();
                        break;
                    case "4":
                        uni.ObliczSredniaAll();
                        break;
                    case "5":
                        uni.WyswietlStudentow();
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
