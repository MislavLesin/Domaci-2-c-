using System;
using System.Collections.Generic;
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



                    default:
                        Console.WriteLine("Greska u unosu!!");
                        break;
                }

                Console.WriteLine("\n \n \n");
            }
           

        }
        static void BrisanjePjesmePoIndexu(IDictionary<int, string> pjesme)
        {
            List<string> obrisane = new List<string>();
            var kolikoIhJeObrisano = 0;
            var cnt = 0;
            IspisListe(pjesme);
            Console.WriteLine("\n \nUnesi indeks za brisanje: ");
            Console.WriteLine("999 za izlaz");
            var unos = int.Parse(Console.ReadLine());
            if (unos > pjesme.Count)
            {
                Console.WriteLine("Nesipravan unos!, ponovi!");
                BrisanjePjesmePoIndexu(pjesme);
            }
            else if (unos == 999)
                Console.WriteLine("Izlaz"); 
            else
            {
                for (var i = unos + 1; i < pjesme.Count;i++)          // spremanje u listu
                {
                    obrisane.Add(pjesme[i]);
                    kolikoIhJeObrisano++;
                }

                foreach(var pjesma in pjesme)            //brisanje iz dictionarya
                {
                    if(pjesma.Key >= unos)
                    {
                        Console.WriteLine("TRenutno brisem "+ pjesma.Value);
                        pjesme.Remove(pjesma.Key);
                    }    
                }
                Console.WriteLine("U dict ih je sad: " + pjesme.Count);
                Console.WriteLine("i je sad: " + pjesme.Count);
                Console.WriteLine("koliko ih je obrisano: " + kolikoIhJeObrisano);
                //vracanje u dictionary
                cnt = obrisane.Count;

               /* 
                pjesme.Add(1, "Druga pisma");
                pjesme.Add(2, "Treca pisma");
                pjesme.Add(3, "Cetvrta pisma");
               */
                
                for (var kljuc = (pjesme.Count + kolikoIhJeObrisano); kljuc >= pjesme.Count; kljuc--)       //triba list vrtit od kraja
                {
                    pjesme.Add(kljuc - 1, obrisane[cnt - 1]);
                    cnt--;
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
                foreach (var pjesma in pjesme)
                {
                    Console.WriteLine(pjesma.Key + " " + pjesma.Value);
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
