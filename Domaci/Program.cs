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
                    "10 - Sortiranje\n" +
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
                        statistika(popis);
                        break;
                    case "10":
                        Console.Clear();
                        sortiraj(popis);
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

        static void sortiraj(Dictionary<string, (string, DateTime)> popis)
        {
            string meni = null;
            if (emptyDict(popis)) return;

            do
            {

                Console.WriteLine("Izaberite akciju:\n" +
                    "1 - Sortiraj po prezimenu\n" +
                    "2 - Sortiraj uzlazno po datumu rodenja\n" +
                    "3 - Sortiraj silazno po datumu rodenja\n" +
                    "0 - Povratak za glavni izbornik\n");
                meni = Console.ReadLine();

                switch (meni)
                {
                    case "1":
                        var lista = makeList(popis);
                        popis.Clear();
                        lista.Sort((x, y) => x.Item2.Substring(x.Item2.IndexOf(" ")).CompareTo(y.Item2.Substring(y.Item2.IndexOf(" "))));
                        foreach(var osoba in lista)
                            popis.Add(osoba.Item1, (osoba.Item2, osoba.Item3));
                            
                        Console.WriteLine("Popis uspjesno sortiran!\nZa povratak na izbornik pritisnite bilo koju tipku");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        var listaUzl = makeList(popis);
                        popis.Clear();
                        listaUzl.Sort((x, y) => x.Item3.CompareTo(y.Item3));
                        foreach (var osoba in listaUzl)
                            popis.Add(osoba.Item1, (osoba.Item2, osoba.Item3));

                        Console.WriteLine("Popis uspjesno sortiran!\nZa nastavak pritistnite bilo koju tipku");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        var listaSil = makeList(popis);
                        popis.Clear();
                        listaSil.Sort((x, y) => y.Item3.CompareTo(x.Item3));
                        foreach (var osoba in listaSil)
                            popis.Add(osoba.Item1, (osoba.Item2, osoba.Item3));

                        Console.WriteLine("Popis uspjesno sortiran!\nZa nastavak pritistnite bilo koju tipku");
                        Console.ReadKey();
                        Console.Clear();
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

        static void painter(string oib, string ime, DateTime datum)
        {
            if (DateTime.Now.Year - datum.Year >= 23 && DateTime.Now.Year - datum.Year <= 65)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine($"'{oib}' '{ime}' '{datum.ToShortDateString()}'\n");
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"'{oib}' '{ime}' '{datum.ToShortDateString()}'\n");
                Console.ResetColor();
            }
        }

        static bool emptyDict(Dictionary<string, (string, DateTime)> popis)
        {
            if (popis.Count is 0)
            {
                Console.WriteLine("Popis prazan!\n");
                Console.WriteLine("Za povratak na glavni izbornik pritistnite bilo koju tipku");
                Console.ReadKey();
                Console.Clear();
                return true;
            }
            else return false;
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

        static Dictionary<string,int> searchMostCommon(Dictionary<string, (string, DateTime)> popis, string chosenOption)
        {

            Dictionary<string, int> brojPonavljanja = new Dictionary<string, int>();
            int max = 0;
            string mostCommonItem = null;

            if (chosenOption == "2")
            {
                
                foreach (var osoba in popis)
                {
                    string ime = osoba.Value.Item1.Substring(0, osoba.Value.Item1.IndexOf(" "));
                    if (brojPonavljanja.ContainsKey(ime))
                        brojPonavljanja[ime]++;
                    else brojPonavljanja.Add(ime, 1);
                }
           
            }

            if (chosenOption == "3")
            {

                foreach (var osoba in popis)
                {
                    string prezime = osoba.Value.Item1.Substring(osoba.Value.Item1.IndexOf(" "));
                    if (brojPonavljanja.ContainsKey(prezime))
                        brojPonavljanja[prezime]++;
                    else brojPonavljanja.Add(prezime, 1);
                }

            }

            if (chosenOption == "4")
            {

                foreach (var osoba in popis)
                {
                    var datum = osoba.Value.Item2.ToString();
                    if (brojPonavljanja.ContainsKey(datum))
                        brojPonavljanja[datum]++;
                    else brojPonavljanja.Add(datum, 1);
                }

            }

            if(chosenOption == "5")
            {
                foreach(var osoba in popis)
                {
                    var season = getSeason(osoba.Value.Item2);
                    if (brojPonavljanja.ContainsKey(season))
                        brojPonavljanja[season]++;
                    else brojPonavljanja.Add(season, 1);
                }
                return brojPonavljanja;
            }

            foreach (var item in brojPonavljanja)
            {
                if (item.Value > max)
                {
                    max = item.Value;
                    mostCommonItem = item.Key;
                }
            }

            Console.WriteLine("Najcesci trazeni pojam je: " + mostCommonItem + "\nBroj ponavljanja: " + max + "\nPritisnite bilo koju tipku za povratak!");
            Console.ReadKey();
            Console.Clear();
            return null;
        }

        static string getSeason(DateTime datum)
        {
            float season = datum.Day + datum.Month / 100f;
            if (season < 3.21 || season >= 12.22) return "Zima";
            if (season < 6.21) return "Proljece";
            if (season < 9.23) return "Ljeto";
            else return "Jesen";
        }

        static void statistika(Dictionary<string, (string, DateTime)> popis)
        {
            string meni = null;
            if (emptyDict(popis)) return;

            do
            {
                Console.WriteLine("Izaberite akciju:\n" +
                    "1 - Postotak zaposlenih i nezaposlenih\n" +
                    "2 - Ispis najcesceg imena i koliko ga stanovnika ima\n" +
                    "3 - Ispis najcesceg prezimena i koliko ga stanovnika ima\n" +
                    "4 - Ispis datuma na kojem je roden najveci broj ljudi\n" +
                    "5 - Ispis broja ljudi rodenih u svakom od godisnjih doba\n" +
                    "6 - Ispis najmladeg stanovnika\n" +
                    "7 - Ispis najstarijeg stanovnika\n" +
                    "8 - Prosjecan broj godina\n" +
                    "9 - Medijan godina\n" +
                    "0 - Nazad na glavni izbornik\n");
                meni = Console.ReadLine();

                switch (meni)
                {
                    case "1":
                        int ukupnoOsoba = popis.Count;
                        float zaposleni = 0f;
                        float postotakZaposlenih = 0f;

                        foreach(var osoba in popis)
                        {
                            if(DateTime.Now.Year - osoba.Value.Item2.Year >= 23 && DateTime.Now.Year - osoba.Value.Item2.Year <= 65)
                            {
                                zaposleni++;
                            }
                        }

                        postotakZaposlenih = (zaposleni / ukupnoOsoba) * 100f;
                        Console.WriteLine($"Postotak nezaposlenih: '{(100f - postotakZaposlenih).ToString("n2")}'%\n" +
                            $"Postotak zaposlenih: '{postotakZaposlenih.ToString("n2")}'%\n" +
                            $"Pritisnite bilo koju tipku za povratak na izbornik");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        searchMostCommon(popis, meni);
                        break;
                    case "3":
                        searchMostCommon(popis, meni);
                        break;
                    case "4":
                        searchMostCommon(popis, meni);
                        break;
                    case "5":
                        var seasons = searchMostCommon(popis, meni);
                        foreach(var season in seasons)
                            Console.WriteLine("Broj ljudi u sezoni " + season.Key + " je: " + season.Value + "\n");
                            
                        Console.WriteLine("Pritisnite bilo koju tipku za povratak!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "6":
                        var listaNajmladi = makeList(popis);
                        listaNajmladi.Sort((x, y) => x.Item3.CompareTo(y.Item3));
                        Console.WriteLine("Najmladi stanovnik je: " + listaNajmladi[0].Item2);
                        Console.WriteLine("Pritisnite bilo koju tipku za povratak!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "7":
                        var listaNajstariji = makeList(popis);
                        listaNajstariji.Sort((x, y) => y.Item3.CompareTo(x.Item3));
                        Console.WriteLine("Najstariji stanovnik je: " + listaNajstariji[0].Item2);
                        Console.WriteLine("Pritisnite bilo koju tipku za povratak!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "8":
                        float suma = 0f, prosjek = 0f;
                        foreach(var osoba in popis)
                        {
                            var starost = DateTime.Now.Year - osoba.Value.Item2.Year;
                            suma += (float)starost;
                        }
                        prosjek = suma / popis.Count;
                        Console.WriteLine($"Prosjecna dob stanovnika je: '{prosjek.ToString("n2")}'");
                        Console.WriteLine("Pritisnite bilo koju tipku za povratak!");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case "9":
                        var listaMedijan = makeList(popis);
                        listaMedijan.Sort((x, y) => x.Item3.CompareTo(y.Item3));
                        var medijan = DateTime.Now.Year - listaMedijan[listaMedijan.Count / 2].Item3.Year;
                        Console.WriteLine($"Medijan godina je: '{medijan}'");
                        Console.WriteLine("Pritisnite bilo koju tipku za povratak!");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case "0":
                        meni = "0";
                        Console.Clear();
                        break;
                    default:
                        meni = null;
                        Console.WriteLine("Krivi unos izbornika!\n");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

            } while (meni is not "0");

        }
        static void uredi(Dictionary<string, (string, DateTime)> popis)
        {
            string meni = null, oib = null, noviOIB = null;

            if (emptyDict(popis)) return;

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
                            if (noviOIB.Length is not 11 && !long.TryParse(noviOIB, out long number))
                            {
                                Console.WriteLine("OIB krivo upisan, pritisnite bilo koju tipku za povratak na izbornik!\n");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                var ime = popis[oib].Item1;
                                var datumRod = popis[oib].Item2;

                                Console.WriteLine($"Potvrda unosa:\nNovi OIB: '{noviOIB}'\nStari OIB: '{oib}'\n\nBilo koja tipka - Unesi\n2 - Ponisti");
                                meni = Console.ReadLine();

                                if (meni == "2") {
                                    Console.Clear();
                                    break;
                                }

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
                                Console.WriteLine($"Potvrdite unos:\nNovo ime: '{novoIme}'\nStaro ime: '{popis[oib].Item1}'\n\nBilo koja tipka - Unesi\n2 - Ponisti");
                                meni = Console.ReadLine();

                                if (meni == "2")
                                {
                                    Console.Clear();
                                    break;
                                }

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
                            if (noviDatum is not "" && DateTime.TryParse(noviDatum, out DateTime result))
                            {
                                Console.WriteLine($"Potvrdite unos:\nNovi datum: '{noviDatum}'\nStari datum: '{popis[oib].Item2}'\n\nBilo koja tipka - Unesi\n2 - Ponisti");
                                meni = Console.ReadLine();

                                if (meni == "2")
                                {
                                    Console.Clear();
                                    break;
                                }

                                popis[oib] = (popis[oib].Item1, DateTime.ParseExact(noviDatum, "d", null));
                                Console.WriteLine("Datum rodenja uspjesno promijenjen, pritisnite bilo koju tipku za povratak na izbornik!\n");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("Ime i prezime krivo upisano, pritisnite bilo koju tipku za povratak na izbornik!\n");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                                
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
            if (emptyDict(popis)) return;

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

            if (emptyDict(popis)) return;

            Console.WriteLine("Unesite ime i prezime osobe koju zelite izbrisati:");
            var ime = Console.ReadLine();

            Console.WriteLine("Unesite datum rodenja osobe koju zelite izbrisati:");
            var datumRod = Console.ReadLine();

            foreach (var osoba in popis)
            {
                if (osoba.Value.Item1 == ime && osoba.Value.Item2.ToShortDateString() == datumRod)
                {
                    Console.WriteLine("Pronadeno: " + ime + " " + datumRod + " " + osoba.Key + "\n");
                    oibZaJednuOsobu = osoba.Key;
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
            }
        }

        static void brisiPoOIB(Dictionary<string, (string, DateTime)> popis)
        {

            int flag = 0;

            if (emptyDict(popis)) return;

            Console.WriteLine("Unesite OIB osobe koju zelite izbrisati:");
            var oib = Console.ReadLine();
            foreach (var osoba in popis)
            {
                if (osoba.Key == oib)
                {
                    popis.Remove(osoba.Key);
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
            string ime = null, oib = null, datumRod = null, menu = null;

            do
            {
                Console.WriteLine("Upisite ime i prezime osobe koju zelite unijeti:");
                ime = Console.ReadLine();
                
                Console.WriteLine("Upisite datum rodenja osobe koju zelite unijeti u formatu 'dan.mjesec.godina.':");
                datumRod = Console.ReadLine();

                Console.WriteLine("Upisite OIB:");
                oib = Console.ReadLine();
                
                if (ime is not "" && datumRod is not null && oib.Length is 11 && !popis.ContainsKey(oib) && long.TryParse(oib, out long number) && DateTime.TryParse(datumRod, out DateTime result))
                {
                    Console.WriteLine($"--Potvrdite unos--\nIme i prezime: '{ime}'\nDatum rodenja: '{datumRod}'\nOIB: '{oib}'");
                    Console.WriteLine("1 - Unesi osobu i vrati se na glavni izbornik\n" +
                        "2 - Nakon ove unesi jos osoba\n" +
                        "3 - Ponisti unos\n" +
                        "0 - Vrati se na glavni izbornik\n");
                    menu = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Neki od podataka su krivo uneseni ili OIB vec postoji\n" +
                        "3 - Pokusajte ponovno\n0 - Povratak na glavni izbornik");
                    menu = Console.ReadLine();
                }

                switch (menu)
                {
                    case "1":
                        popis.Add(oib, (ime, DateTime.Parse(datumRod)));
                        Console.Clear();
                        return;
                    case "2":
                        popis.Add(oib, (ime, DateTime.Parse(datumRod)));
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        break;
                    case "0":
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Krivi unos izbornika, pritisnite bilo koju tipku za povratak na unos osobe!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
               
            } while (menu is not "0");
        }
        
        static void ispisPoImenu(Dictionary<string, (string, DateTime)> popis)
        {
            string odgovor = null, izlaz = "1";
            int flag = 0;

            if (emptyDict(popis)) return;

            do
            {
                Console.WriteLine("Upišite ime, prezime i datum rodenja stanovnika kojeg trazite:");
                odgovor = Console.ReadLine();

                if (odgovor is "")
                {
                    Console.WriteLine("Krivi unos, pokusajte ponovo!");
                }
                foreach (var osoba in popis)
                {
                    if (osoba.Value.Item1 + " " + osoba.Value.Item2.ToShortDateString() == odgovor)
                    {
                        Console.WriteLine("Osoba pronadena!\n" + osoba.Key + "\n");
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

            if (emptyDict(popis)) return null;

            do
            {
                Console.WriteLine("Upišite OIB stanovnika kojeg trazite:");
                oib = Console.ReadLine();
                if (oib.Length is not 11 && long.TryParse(oib, out long number)) { 
                    Console.WriteLine("OIB krivo upisan, pokusajte ponovo!");
                }
                foreach (var osoba in popis)
                {
                    if (osoba.Key == oib)
                    {
                        Console.WriteLine($"Osoba pronadena!\n'{osoba.Key}' '{osoba.Value.Item1}' '{osoba.Value.Item2}'\n");
                        flag = 1;
                    }
                }

                if (flag == 0) Console.WriteLine("Osoba nije pronadena!\n");

                Console.WriteLine("1 - Ponovni upis OIB-a\n0 - Izlaz\n");
                izlaz = Console.ReadLine();
                Console.Clear();

            } while (izlaz is "1");

            if (flag == 1) return oib;
            else return null;
        }

        static void ispisStanovnistva(Dictionary<string,(string,DateTime)> popis)
        {
            string menu = null;

            if (emptyDict(popis)) return;

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
                        foreach (var osoba in popis)
                            painter(osoba.Key, osoba.Value.Item1, osoba.Value.Item2);

                        Console.WriteLine("Za nastavak pritistnite bilo koju tipku");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case "2":
                        var listaUzl = makeList(popis);
                        listaUzl.Sort((x, y) => x.Item3.CompareTo(y.Item3));
                        foreach (var osoba in listaUzl)
                            painter(osoba.Item1, osoba.Item2, osoba.Item3);
                        Console.WriteLine("Za nastavak pritistnite bilo koju tipku");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case "3":
                        var listaSil = makeList(popis);
                        listaSil.Sort((x, y) => y.Item3.CompareTo(x.Item3));
                        foreach (var osoba in listaSil)
                            painter(osoba.Item1, osoba.Item2, osoba.Item3);
                        Console.WriteLine("Za nastavak pritistnite bilo koju tipku");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

            } while (menu is not "0");

            Console.Clear();
        }
    }
}
