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
            popis.Add("11111111111", ("Ivan bakotin", new DateTime(2021,10,10)));
            popis.Add("12345678910", ("Ivan bakotin", new DateTime(2021,10,10)));
            popis.Add("00000000000", ("Ivan debil", new DateTime(2021,1,1)));

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
                        brisiPoOIB(popis);
                        break;
                    case "6":
                        Console.Clear();
                        brisiPoImenu(popis);
                        break;
                    case "7":
                        Console.Clear();
                        brisiSve(popis);
                        break;
                    case "8":
                        Console.Clear();
                        uredi(popis);
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

        static void uredi(Dictionary<string, (string, DateTime)> popis)
        {
            string meni = null, oib = null, noviOIB = null;

            do
            {
                Console.WriteLine("Izaberite akciju:\n" +
                    "1 - Uredi OIB\n" +
                    "2 - Uredi ime i prezime\n" +
                    "3 - Uredi datum rodenja\n" +
                    "0 - Povratak na glavni izbornik\n");
                meni = Console.ReadLine();

                switch (meni)
                {
                    case "1":
                        oib = ispisPoOIB(popis);
                        if(oib is null)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Upisite novi OIB:\n");
                            noviOIB = Console.ReadLine();
                            if (noviOIB.Length is not 11)
                            {
                                Console.WriteLine("OIB krivo upisan, vracam na izbornik!\n");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                var ime = popis[oib].Item1;
                                var datumRod = popis[oib].Item2;
                                popis.Remove(oib);
                                popis.Add(noviOIB, (ime, datumRod));
                                Console.WriteLine("OIB uspjesno promijenjen, pritisnite bilo koju tipku za povratak na izbornik!\n");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        break;

                    case "2":
                        oib = ispisPoOIB(popis);
                        if (oib is null)
                        {
                            meni = null;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Upisite novo ime:\n");
                            var novoIme = Console.ReadLine();
                            if (novoIme is "")
                            {
                                Console.WriteLine("Ime i prezime krivo upisano, pritisnite bilo koju tipku za povratak na izbornik!\n");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                popis[oib] = (novoIme, popis[oib].Item2);
                                Console.WriteLine("Ime i prezime uspjesno promijenjeni, pritisnite bilo koju tipku za povratak na izbornik!\n");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        break;

                    case "3":
                        oib = ispisPoOIB(popis);
                        if (oib is null)
                        {
                            meni = null;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Upisite novi datum rodenja:\n");
                            var noviDatum = Console.ReadLine();
                            if (noviDatum is "")
                            {
                                Console.WriteLine("Ime i prezime krivo upisano, pritisnite bilo koju tipku za povratak na izbornik!\n");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                popis[oib] = (popis[oib].Item1, DateTime.ParseExact(noviDatum, "d", null));
                                Console.WriteLine("Datum rodenja uspjesno promijenjen, pritisnite bilo koju tipku za povratak na izbornik!\n");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        break;

                    case "0":
                        meni = "0";
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Krivi unos izbornika, pritisnite bilo koju tipku za povratak na izbornik!\n");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

            } while (meni is not "0");
        }

        static void brisiSve(Dictionary<string, (string, DateTime)> popis)
        {
            Console.WriteLine("Jeste li sigurni da zelite izbrisati cijeli popis?\n1 - DA\n0 - NE\n");
            var izbor = Console.ReadLine();
            if (izbor == "1") {
                popis.Clear();
                Console.WriteLine("Popis izbrisan, pritisnite bilo koju tipku za povratak\n");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            if (izbor == "0")
            {
                Console.WriteLine("Brisanje prekinuto, pritisnite bilo koju tipku za povratak na glavni izbornik!");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            else
            {
                Console.WriteLine("Krivi unos izbornika, pritisnite bilo koju tipku za povratak na glavni izbornik!");
                Console.ReadKey();
                Console.Clear();
                return;
            }
        }

        static void brisiPoImenu(Dictionary<string, (string, DateTime)> popis)
        {
            int count = 0;
            string oibZaJednuOsobu = null;
            Console.WriteLine("Unesite ime i prezime osobe koju zelite izbrisati:");
            var ime = Console.ReadLine();
            Console.WriteLine("Unesite datum rodenja osobe koju zelite izbrisati:");
            var datumRod = Console.ReadLine();

            foreach (var lik in popis)
            {
                if (lik.Value.Item1 == ime && lik.Value.Item2.ToShortDateString() == datumRod)
                {
                    Console.WriteLine("Pronadeno: " + ime + " " + datumRod + " " + lik.Key + "\n");
                    oibZaJednuOsobu = lik.Key;
                    count++;
                }
            }
            if (count > 1)
            {
                Console.WriteLine("Pronadeno vise osoba, upisite OIB osobe koju zelite izbrisati!\n");
                var oib = Console.ReadLine();
                popis.Remove(oib);
                Console.WriteLine("Osoba is OIB-om " + oib + " izbrisana, pritisnite bilo koju tipku za povratak\n");
                Console.ReadKey();
                Console.Clear();
            }
            if (count == 1)
            {
                popis.Remove(oibZaJednuOsobu);
                Console.WriteLine("Osoba is OIB-om " + oibZaJednuOsobu + " izbrisana, pritisnite bilo koju tipku za povratak\n");
                Console.ReadKey();
                Console.Clear();
            }

            if (count == 0)
            {
                Console.WriteLine("Osoba sa tim OIB-om nije pronadena, pritisnite bilo koju tipku za povratak");
                Console.ReadKey();
                Console.Clear();
                return;
            }
        }

        static void brisiPoOIB(Dictionary<string, (string, DateTime)> popis)
        {

            int flag = 0;
            Console.WriteLine("Unesite OIB osobe koju zelite izbrisati:");
            var oib = Console.ReadLine();
            foreach (var lik in popis)
            {
                if (lik.Key == oib)
                {
                    popis.Remove(lik.Key);
                    Console.WriteLine("Osoba is OIB-om " + oib + " izbrisana, pritisnite bilo koju tipku za povratak\n");
                    Console.ReadKey();
                    Console.Clear();
                    flag = 1;
                    break;
                }
                else flag = 0;
            }

            if (flag == 0)
            {
                Console.WriteLine("Osoba sa tim OIB-om nije pronadena, pritisnite bilo koju tipku za povratak");
                Console.ReadKey();
                Console.Clear();
                return;
            }
        }

        static void unesiNovog(Dictionary<string, (string, DateTime)> popis)
        {
            string ime = null,oib = null, izlaz = "0", datumRod = null, menu = null;

            do
            {
                izlaz = "0";
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
                    else if (izlaz == "1") menu = "3";
                    else menu = null;
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
                        Console.WriteLine("Krivi unos izbornika, pritisnite bilo koju tipku za povratak na unos osobe!");
                        izlaz = "1";
                        Console.ReadKey();
                        Console.Clear();
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

        static string ispisPoOIB(Dictionary<string, (string, DateTime)> popis)
        {
            string oib = null;
            string izlaz = "1";
            int flag = 0;

            do
            {
                Console.WriteLine("Upišite OIB stanovnika kojeg trazite:");
                oib = Console.ReadLine();
                if (oib.Length is not 11) { 
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

            if (flag == 1) return oib;
            else return null;
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
