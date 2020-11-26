using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var unos = 11;
            var pjesme = new Dictionary< int, string>()
            {
                { 0, "Prva pisma" },
                {1 , "Druga pisma" },
                {2, "Treca pisma" },
                {3, "Cetvrta pisma" }
            };

           

            while (unos != 0)
            {

                IspisMenija();

                Console.WriteLine("\n \n");
                unos = int.Parse(Console.ReadLine());
                Console.WriteLine("\n \n");

                switch (unos)
                {
                    case 0:
                        break;

                    case 1:
                        IspisListe(pjesme);
                        break;

                    case 2:
                        IspisImenaPoIndexu(pjesme);
                        break;

                    case 3:
                        IspisIndexaPoImenu(pjesme);
                        break;
                    case 4:
                        UnosNovePjesme(pjesme);
                        break;

                    case 5:
                        BrisanjePjesmePoIndexu(pjesme);
                        break;

                    case 6:
                        if (pjesme.Count > 0)
                        {
                            BrisanjePjesmePoImenu(pjesme);
                        }
                        else
                            Console.WriteLine("Prazna lista!");
                       
                        break;

                    case 7:
                        BrisanjeCijeleListe(pjesme);
                        break;

                    case 8:
                        UredivanjeImenaPjesme(pjesme);
                        break;

                    case 9:
                        PremijestanjePjesmePoListi(pjesme);
                        break;

                    default:
                        Console.WriteLine("Greska u unosu!!");
                        break;
                }

                Console.WriteLine("\n \n \n");
            }
           

        }

        static void PremijestanjePjesmePoListi(IDictionary<int, string> pjesme)
        {
            List<string> pjesmeUListi = new List<string>();
            string odabrana = "";
            IspisListe(pjesme);
            Console.WriteLine("Unesi ime pjesme kojoj zelis promijenit poziciju");
            var unos = Console.ReadLine();
            var index = -1;
            var ukupniBrojPjesama = pjesme.Count;

            foreach (var pjesma in pjesme)
            {
                if (String.Compare(unos.ToLower(), pjesma.Value.ToLower()) == 0)
                    index = pjesma.Key;
            }

            if (index == -1)
            {
                Console.WriteLine("Nema pjesme tog imena");
                Console.WriteLine("Dali zelis ponovit?");
                Console.WriteLine("yes za ponovit");
                var ponovit = Console.ReadLine();
                if (String.Compare(ponovit.ToLower(), "yes") == 0)
                    PremijestanjePjesmePoListi(pjesme);
            }
            else
            {
                Console.WriteLine("Pijesma imena " + unos + " se nalazi ne meijstu " + index);
                Console.Write("Unesi novi index: ");
                var noviIndex = int.Parse(Console.ReadLine());
                if(noviIndex > pjesme.Count)
                {
                    Console.WriteLine("Neispravan unos!!");
                }
                else
                {
                    foreach (var pjesma in pjesme)                   //sve pisme u listu, a odabrana u varijablu
                        if (pjesma.Key == index)
                            odabrana = pjesma.Value;
                        else
                            pjesmeUListi.Add(pjesma.Value);
                    pjesme.Clear();

                    var j = 0;
                    for(var i = 0; i < ukupniBrojPjesama; i++)
                    {
                        if (i == noviIndex)
                            pjesme.Add(i, odabrana);
                        else
                        {
                            pjesme.Add(i, pjesmeUListi[j]);
                            j++;
                        }
                    }


                }

            }
        }

        static void UredivanjeImenaPjesme(IDictionary<int, string> pjesme)
        {
            Console.WriteLine("Unesi ime pjesme koju zelis promijenit");
            var unos = Console.ReadLine();
            var index = -1;
            foreach(var pjesma in pjesme)
            {
                if (String.Compare(unos.ToLower(), pjesma.Value.ToLower()) == 0)
                    index = pjesma.Key;

            }

            if (index == -1)
            {
                Console.WriteLine("Nema pjesme tog imena");
                Console.WriteLine("Dali zelis ponovit?");
                Console.WriteLine("yes za ponovit");
                var ponovit = Console.ReadLine();
                if (String.Compare(ponovit.ToLower(), "yes") == 0)
                    UredivanjeImenaPjesme(pjesme);
            }
            else
            {
                Console.WriteLine("Pijesma postoji i ima index " + index);
                Console.WriteLine("\n");
                Console.Write("Unesi novo ime pjesme: ");
                var novoIme = Console.ReadLine();
                pjesme[index] = novoIme;    
            }
               


        }

        static void BrisanjeCijeleListe(IDictionary<int, string> pjesme)
        {
            Console.WriteLine("Jeste li sigurni da zelite obrisat cijelu listu!?");
            Console.WriteLine("yes za potvrdit");
            var unos = Console.ReadLine();
            if (String.Compare(unos, "yes") == 0)
            {
                pjesme.Clear();
            }
            else
                Console.WriteLine("Povratka na meni");
        }

        static void BrisanjePjesmePoImenu(IDictionary<int, string> pjesme)
        {
            List<string> obrisane = new List<string>();
            var i = 0;
            var cnt = pjesme.Count;
            IspisListe(pjesme);
            Console.WriteLine("Unesi ime pjesme za obrisat: ");
            var imePjesme = Console.ReadLine();
            var unos = -1;
            foreach (var pjesma in pjesme)
            {
                if (String.Compare(imePjesme.ToLower(), pjesma.Value.ToLower()) == 0)
                {
                    unos = pjesma.Key;
                    break;
                }
            }
            if(unos != -1)
            {
                for (i = unos + 1; i < pjesme.Count; i++)
                {
                    obrisane.Add(pjesme[i]);
                }

                for (i = unos; i < cnt; i++)
                {
                    pjesme.Remove(i);
                }
                cnt = pjesme.Count;
                i = 0;
                for (var j = pjesme.Count; j < (obrisane.Count + cnt); j++)
                {
                    pjesme.Add(j, obrisane[i]);
                    i++;
                }
                Console.WriteLine("Uspjesno obrisana pjesma imena " + imePjesme);
            }

               
        }
        static void BrisanjePjesmePoIndexu(IDictionary<int, string> pjesme)
        {
            List<string> obrisane = new List<string>();
            var i = 0;
            var cnt = pjesme.Count;
            IspisListe(pjesme);
            Console.WriteLine("\n\n999 za izlaz");
            Console.WriteLine("\n \nUnesi indeks za brisanje: ");
            var unos = int.Parse(Console.ReadLine());

            if (unos > pjesme.Count && unos != 999)
            {
                Console.WriteLine("Nesipravan unos!, ponovi!");
                BrisanjePjesmePoIndexu(pjesme);
            }
            else if (unos == 999)
                Console.WriteLine("Izlaz");
            else
            {
                for (i = unos + 1; i < pjesme.Count; i++)
                {
                    obrisane.Add(pjesme[i]);
                }

                for (i = unos; i < cnt; i++)
                {
                    pjesme.Remove(i);
                }
                cnt = pjesme.Count;
                i = 0;
                for (var j = pjesme.Count; j < (obrisane.Count + cnt); j++)
                {
                    pjesme.Add(j, obrisane[i]);
                    i++;
                }
            }
        }

        static void IspisIndexaPoImenu(IDictionary<int, string> pjesme)
        {
            var pjesamaIma = 0;
            var index = 0;
            foreach (var pjesma in pjesme)
                pjesamaIma++;
            Console.WriteLine("U listi ima " + pjesamaIma + " pjesama \n");
            if(pjesamaIma > 0)
            {
                Console.WriteLine("Unesi ime pjesme za trazit: ");
                var imePjesme = Console.ReadLine();

                foreach (var pjesma in pjesme)
                {
                    if (String.Compare(imePjesme.ToLower(), pjesma.Value.ToLower()) == 0)
                    {
                        index = pjesma.Key;
                        break;
                    }
                }
                Console.WriteLine("Pjesma imena " + imePjesme + " ima index " + index);
                
            }
        }
        static void IspisImenaPoIndexu(IDictionary<int,string> pjesme)
        {
            var pjesamaIma = 0;
            foreach (var pjesma in pjesme)
                pjesamaIma++;
            Console.WriteLine("U listi ima " + pjesamaIma + " pjesama");
            Console.Write("Unesi index trazene pjesme: ");
            var unos = int.Parse(Console.ReadLine());
            if(pjesme.ContainsKey(unos) == true)
            {
                Console.WriteLine("Pijesma sa indeksom " + unos + " je " + pjesme[unos]);
            }else
            {
                Console.WriteLine("Nema pjesme sa tim indexom!");
            }
           
            
        }

        static void IspisListe(IDictionary<int, string> pjesme)
        {
            if (pjesme.Count > 0)
            {
                for(var i = 0; i < pjesme.Count;i++)
                {
                    Console.WriteLine(i + " " +pjesme[i]);
                }
            }
            else
                Console.WriteLine("Prazna lista");
           
        }
        static void UnosNovePjesme(IDictionary<int, string> pjesme)     //unos na kraj
        {
            var postoji = false;
            Console.Write("Unesi ime pjesme: ");
            var imePjesme = Console.ReadLine();
            foreach (var pjesma in pjesme)                                                            //dali postoji vec?
            {
                if(String.Compare(pjesma.Value,imePjesme) == 0)
                {
                    Console.WriteLine("Pijesma vec postoji u listi!");
                    postoji = true;
                }
            }
            if(postoji == false)
            {
                pjesme.Add(pjesme.Count, imePjesme);
                Console.WriteLine("Uspjesno izvrseno");
            }
          


        }
        static void IspisMenija()
        {
            Console.WriteLine("ODABERI AKCIJU: ");
            Console.WriteLine("1 - Ispis cijele liste");
            Console.WriteLine("2 - Ispis imena pjesme unosom pripadajuceg rednog broja");
            Console.WriteLine("3 - Ispis rednog broja pjesme unosom pripadajuceg imena");
            Console.WriteLine("4 - Unos nove pjesme");
            Console.WriteLine("5 - Brisanje pjesme po rednom broju");
            Console.WriteLine("6 - Brisanje pjesme po imenu");
            Console.WriteLine("7 - Brisanje cijele liste");
            Console.WriteLine("8 - Uredivanje imena pjesme");
            Console.WriteLine("9 - Uredivanje rednog broja pjesme"); //Znaci premjestanje pjesme po listi
            Console.WriteLine("0 - Izlaz iz aplikacije");
          
        }
    }
}
