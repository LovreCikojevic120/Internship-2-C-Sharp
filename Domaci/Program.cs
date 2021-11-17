using System;
using System.Collections.Generic;

namespace Domaci
{
    class Program
    {
        static void Main(string[] args)
        {
            string options = null;

            var popis = new Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)>();
            popis.Add("123", ("Ivan bakotin", new DateTime(2021,10,10)));
            popis.Add("12335436", ("Ivan debil", DateTime.UtcNow));

            do
            {
                Console.WriteLine("Odaberite akciju:\n" +
                    "1 - Ispis stanovnistva\n" +
                    "2 - Ispis stanovnika po OIB-u\n" +
                    "3 - Ispis OIB-a po unosu imena i prezimena\n" +
                    "4 - Unos novog stanovnika\n" +
                    "5 - Brisanje stanovnika po OIB-u\n" +
                    "6 - Brisanje stanovnika po imenu, prezimenu i datumu rodenja\n" +
                    "7 - Brisanje svih stanovnika\n" +
                    "8 - Uredivanje stanovnika\n" +
                    "9 - Statistika\n" +
                    "0 - Izlaz iz aplikacije");
                options = Console.ReadLine();

                switch (options)
                {
                    case "1":
                        Console.Clear();
                        ispisStanovnistva(popis);
                        break;
                    case "2":
                        Console.Clear();
                        ispisPoOIB(popis);
                        break;
                    case "3":
                        Console.Clear();
                        ispisPoImenu(popis);
                        break;
                    case "4":
                        Console.Clear();
                        unesiNovog(popis);
                        break;
                    case "5":
                        Console.Clear();
                        //brisiPoOIB();
                        break;
                    case "6":
                        Console.Clear();
                        //brisiPoImenu();
                        break;
                    case "7":
                        Console.Clear();
                        //brisiSve();
                        break;
                    case "8":
                        Console.Clear();
                        //uredi();
                        break;
                    case "9":
                        Console.Clear();
                        //statistika();
                        break;
                    case "0":
                        Console.Clear();
                        Console.WriteLine("Dovidenja!\n");
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Krivi unos, pokusajte ponovno!\n\n");
                        break;
                }

            } while (options != "0");
            
        }

        static List<(string, string, DateTime)> makeList(Dictionary<string,(string,DateTime)> popis)
        {
            var lista = new List<(string, string, DateTime)>();
            foreach (var lik in popis)
            {
                lista.Add((lik.Key, lik.Value.Item1, lik.Value.Item2));
            }
            return lista;
        }

        static void unesiNovog(Dictionary<string, (string, DateTime)> popis)
        {
            string ime = null,oib = null, izlaz = "0", datumRod = null, menu = null;

            do
            {
                Console.WriteLine("Upisite ime i prezime osobe koju zelite unijeti:");
                ime = Console.ReadLine();
                
                Console.WriteLine("Upisite datum rodenja osobe koju zelite unijeti:");
                datumRod = Console.ReadLine();

                Console.WriteLine("Upisite OIB:");
                oib = Console.ReadLine();
                
                if (ime is not "" && datumRod is not null && oib.Length is 11)
                {
                    Console.WriteLine("Ime i prezime:" + ime + "\n" + "Datum rodenja:" + datumRod + "\n" + "OIB:" + oib);
                    Console.WriteLine("1 - Unesi osobu i vrati se na glavni izbornik\n" +
                        "2 - Nakon ove unesi jos osoba\n" +
                        "3 - Ponisti unos\n" +
                        "0 - Vrati se na glavni izbornik");
                    menu = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Neki od podataka su krivo uneseni\n" +
                        "1 - Pokusajte ponovno\n0 - Povratak na glavni izbornik");
                    izlaz = Console.ReadLine();
                    if (izlaz == "0") menu = "0";
                }

                switch (menu)
                {
                    case "1":
                        popis.Add(oib, (ime, DateTime.Parse(datumRod)));
                        Console.Clear();
                        return;
                    case "2":
                        popis.Add(oib, (ime, DateTime.Parse(datumRod)));
                        izlaz = "1";
                        Console.Clear();
                        break;
                    case "3":
                        izlaz = "1";
                        Console.Clear();
                        break;
                    case "0":
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Krivi unos izbornika!");
                        break;
                }
               
            } while (izlaz is "1");
        }
        static void ispisPoImenu(Dictionary<string, (string, DateTime)> popis)
        {
            string odgovor = null, izlaz = "1";
            int flag = 0;

            do
            {
                Console.WriteLine("Upišite ime, prezime i datum rodenja stanovnika kojeg trazite:");
                odgovor = Console.ReadLine();

                if (odgovor.Length < 10 || odgovor is "")
                {
                    Console.WriteLine("Krivi unos, pokusajte ponovo!");
                }
                foreach (var lik in popis)
                {
                    if (lik.Value.Item1 + " " + lik.Value.Item2.ToShortDateString() == odgovor)
                    {
                        Console.WriteLine("Osoba pronadena!\n" + lik.Key + "\n");
                        flag = 1;
                    }
                }

                if (flag == 0) Console.WriteLine("Osoba nije pronadena!\n");

                Console.WriteLine("1 - Ponovni upis\n0 - Izlaz\n");
                izlaz = Console.ReadLine();
                Console.Clear();

            } while (izlaz is "1");
        }

        static void ispisPoOIB(Dictionary<string, (string, DateTime)> popis)
        {
            string oib = null;
            string izlaz = "1";
            int flag = 0;

            do
            {
                Console.WriteLine("Upišite OIB stanovnika kojeg trazite:");
                oib = Console.ReadLine();
                if (oib is "" || oib.Length is not 11) { 
                    Console.WriteLine("OIB krivo upisan, pokusajte ponovo!");
                }
                foreach (var lik in popis)
                {
                    if (lik.Key == oib)
                    {
                        Console.WriteLine("Osoba pronadena!\n" + lik.Key + " " + lik.Value.Item1 + " " + lik.Value.Item2 + "\n");
                        flag = 1;
                    }
                }

                if (flag == 0) Console.WriteLine("Osoba nije pronadena!\n");

                Console.WriteLine("1 - Ponovni upis\n0 - Izlaz\n");
                izlaz = Console.ReadLine();
                Console.Clear();

            } while (izlaz is "1");
        }

        static void ispisStanovnistva(Dictionary<string,(string,DateTime)> popis)
        {
            string menu = null;
            
            do
            {
                Console.WriteLine("--Ispis stanovnistva--\n" +
                    "1 - Onako kako su spremljeni\n" +
                    "2 - Po datumu rodenja uzlazno\n" +
                    "3 - Po datumu rodenja silazno\n" +
                    "0 - Nazad na glavni izbornik\n");
                menu = Console.ReadLine();
                switch (menu)
                {
                    case "1":
                        foreach (var lik in popis)
                        {
                            Console.WriteLine(lik.Key + " " + lik.Value + "\n");
                        }
                        Console.WriteLine("Za nastavak pritistnite bilo koji botun");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case "2":
                        var listaUzl = makeList(popis);
                        listaUzl.Sort((x, y) => x.Item3.CompareTo(y.Item3));
                        foreach (var item in listaUzl) Console.WriteLine(item.Item1+" "+item.Item2+" "+ item.Item3+" "+"\n");
                        Console.WriteLine("Za nastavak pritistnite bilo koji botun");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case "3":
                        var listaSil = makeList(popis);
                        listaSil.Sort((x, y) => y.Item3.CompareTo(x.Item3));
                        foreach (var item in listaSil) Console.WriteLine(item.Item1 + " " + item.Item2 + " " + item.Item3 + " " + "\n");
                        Console.WriteLine("Za nastavak pritistnite bilo koji botun");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

            } while (menu != "0");

            Console.Clear();
        }
    }
}
