using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Vjezbanje_lito2021
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> myDictionary = new Dictionary<int, string>() {
                {0,"First Song" },
                {1,"Second Song" },
                {2,"Third Song" },
                {3,"Fourth Song" },
                {4,"Fifth Song" },
            };

            var input = 0;
            var firstTimeOpened = true;
            while(input != -1)
            {
                if(firstTimeOpened == true)
                {
                    firstTimeOpened = false;
                }
                else
                    Console.ReadKey();

                Console.Clear();
                WriteMenu();
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    Console.Clear();
                    switch (input)
                    {
                        case 0:
                            input = -1;
                            continue;
                        case 1:
                            PrintAllSongs(myDictionary);
                            break;
                        case 2:
                            PrintSongByKey(SelectSongKey(myDictionary),myDictionary);
                            break;
                        case 3:
                            GetKSongKeysFromValue(myDictionary);
                            break;
                        case 4:
                            AddNewSong(myDictionary);
                            break;
                        case 5:
                            DeleteByKey(myDictionary);
                            break;
                        case 6:
                            DeleteByValue(myDictionary);
                            break;
                        case 7:
                            DeleteAllSongs(myDictionary);
                            break;
                        case 8:
                            EditSongValue(myDictionary);
                            break;
                        case 9:
                            ReorderSong(myDictionary);
                            break;
                        case 10:
                            SaveToTxtFile(myDictionary);
                            break;
                        case 11:
                            ReadFromFile(myDictionary);
                            break;
                        case 12:
                            ShufflePlaylist(myDictionary);
                            break;
                        default:
                            PrintFail();
                            break;
                    }
                }
                else
                {
                    PrintFail();
                    continue;
                }
                    


            }
            Console.WriteLine("Good bye");
        }
        public static void WriteMenu()
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
            Console.WriteLine("10 - Spremanje liste u datoteku");
            Console.WriteLine("11 - Citanje liste iz datoteke");
            Console.WriteLine("12 - Shuffle");
            Console.WriteLine("0 - Izlaz iz aplikacije");
            Console.WriteLine("\n \n");

        }
        public static void PrintFail()
        {
            Console.WriteLine("Wrong input");
        }
        public static void PrintAllSongs(Dictionary<int,string> dictionary)
        {
            if(dictionary.Count > 0)
            {
                Console.WriteLine($"All Songs: \n");
                foreach (var song in dictionary)
                {
                    Console.WriteLine($"[{song.Key}] - {song.Value} \n");
                }
            }
            else
                Console.WriteLine("There are no songs in playlist!");
           
        }
        public static int SelectSongKey(Dictionary<int, string> dictionary)
        {
            Console.WriteLine("Enter song number:\n");
            PrintAllSongs(dictionary);
            int key;
            while(!int.TryParse(Console.ReadLine(), out key))
            {
                Console.WriteLine("Wrong Input!!");
                Console.WriteLine("Try again");
            }
            return key;
        }
        public static List<int> GetKSongKeysFromValue(Dictionary<int, string> dictionary)
        {
            Console.Clear();
            Console.WriteLine("Enter name to search for: ");
            var name = Console.ReadLine();
            var keys = new List<int>();
            foreach(var song in dictionary)
            {
                Match match = Regex.Match(song.Value.ToLower(), name.ToLower());
                if(match.Success)
                {
                    keys.Add(song.Key);
                }
            }
            if(keys.Count == 0)
            {
                Console.WriteLine("No songs found");
            }
            else
            {
                foreach(var number in keys)
                {
                    dictionary.TryGetValue(number, out var songName);
                    Console.WriteLine($"[{number}] - {songName} \n");
                }
            }
            return keys;
        }
        public static int GetSongKeyFromValue(Dictionary<int, string> dictionary)
        {
            Console.WriteLine("Enter name to search for: ");
            var name = Console.ReadLine();
            foreach (var song in dictionary)
            {
                Match match = Regex.Match(song.Value.ToLower(), name.ToLower());
                if (match.Success)
                {
                    return song.Key;
                }
            }
            return -1;
        }
        public static void PrintSongByKey(int key, Dictionary<int, string> dictionary)
        {
            if(dictionary.TryGetValue(key, out var songName))
            {
                Console.WriteLine($"The song is: {songName}");
            }
            else
                Console.WriteLine("The song couldn't be found");
        }
        public static bool AddNewSong(Dictionary<int, string> dictionary)
        {
            Console.Clear();
            Console.WriteLine("Enter the new song name, leave emty to exit");
            var songName = Console.ReadLine();
            if(!string.IsNullOrEmpty(songName))
            {
                Console.WriteLine($"Do you want to save {songName} to playlist?");
                if(YesDecision() == true)
                {
                    dictionary.Add(GetNextKey(dictionary), songName);
                    Console.WriteLine($"{songName} added to playlist!");
                    return true;
                }
            }
            Console.WriteLine("Action canceled");
            return false;
        }
        public static bool YesDecision()
        {
            Console.WriteLine("Enter 'y' or 'yes' to save");
            var decision = Console.ReadLine();
            if (string.IsNullOrEmpty(decision))
                return false;
            if (string.Compare(decision.ToLower(), "y") == 0 || string.Compare(decision.ToLower(), "yes") == 0)
                return true;
            else
                return false;
        }
        public static int GetNextKey(Dictionary<int, string> dictionary)
        {
            for(var i = 0; ;i++)
            {
                if(!dictionary.TryGetValue(i,out var name))
                {
                    return i;
                }
            }
        }
        public static bool DeleteByKey(Dictionary<int, string> dictionary)
        {
            Console.Clear();
            var songKey = SelectSongKey(dictionary);
            if(dictionary.TryGetValue(songKey, out var songName) == false)
            {
                Console.WriteLine($"There is no song with key {songKey}");
            }
            else
            {
                Console.WriteLine($"Are You sure you want to delete {songName} from playlist?");
                Console.WriteLine("Enter y or yes to confirm");
                if (YesDecision() == true)
                {
                    dictionary.Remove(songKey);
                    Console.WriteLine($"Removed song {songName} from playlist!");
                    return true;
                }
                else
                {
                    Console.WriteLine("Action canceled");
                    return false;
                }
            }
            return false;
        }
        public static bool DeleteByValue(Dictionary<int, string> dictionary)
        {
            Console.Clear();
            Console.WriteLine("Select songs to delete:");
            var keys = GetKSongKeysFromValue(dictionary);
            if(keys.Count == 0)
            {
                return false;
            }
            else
            {
                Console.WriteLine("\nAre you sure you want to delete these songs?");
                var decision = YesDecision();
                var count = 0;
                if(decision)
                {
                    foreach(var key in keys)
                    {
                        dictionary.Remove(key);
                        count++;
                    }
                    Console.WriteLine($"Deleted {count} songs from playlist");
                    return true;
                }
                else
                {
                    Console.WriteLine("Action canceled");
                    return false;
                }
            }
        }
        public static void DeleteAllSongs(Dictionary<int, string> dictionary)
        {
            Console.WriteLine("Are you sure you want to delete all songs??");
            var decision = YesDecision();
            if(decision)
            {
                dictionary.Clear();
                Console.WriteLine("Deleted all songs from playlist");
            }
            else
                Console.WriteLine("Action canceled!");
        }
        public static bool EditSongValue(Dictionary<int, string> dictionary)
        {
            Console.Clear();
            Console.WriteLine("Select song to edit:");
            var key = SelectSongKey(dictionary);

            if(!dictionary.TryGetValue(key, out var songName))
            {
                Console.WriteLine($"No songs found with key {key}");
                return false;
            }
            Console.WriteLine($"Name: {songName}");
            Console.WriteLine("Enter new name, or leave empty to cancel");
            songName = StringInputValidation(out var newName)
                ? newName
                : songName;
            dictionary[key] = songName;
            Console.WriteLine($"The new song name for song [{key}] is {songName}");
            return true;
        }
        public static bool StringInputValidation(out string input)
        {
            var userInput = Console.ReadLine().Trim();
            if(string.IsNullOrEmpty(userInput))
            {
                input = null;
                return false;
            }
            else
            {
                input = userInput;
                return true;
            }
        }
        public static bool ReorderSong(Dictionary<int, string> dictionary)
        {
            Console.Clear();
            PrintAllSongs(dictionary);
            var key = GetSongKeyFromValue(dictionary);
            if(key < 0)
            {
                Console.WriteLine("There is no song with that name");
                return false;
            }
            var value = dictionary[key];
            Console.WriteLine($"Enter new position to set {dictionary[key]} to");
            var newIndex = -1;
            var addedFlag = false;
            while (!int.TryParse(Console.ReadLine(), out newIndex))
            {
                Console.WriteLine("Wrong input!!\nTry again");
            }
            if (newIndex >= 0 && newIndex <= dictionary.Count) 
            {
                var buffer = new List<string>();
                foreach (var kvp in dictionary)
                {
                    if (kvp.Key != key)
                    {
                        buffer.Add(kvp.Value);
                    }
                }
                dictionary.Clear();
                var j = 0;
                for (int i = 0; i < buffer.Count + 1; i++)
                {
                    if (i != newIndex)
                    {
                        dictionary.Add(i, buffer[j]);
                        j++;
                    }
                    else
                    {
                        dictionary.Add(i, value);
                        addedFlag = true;
                    }

                }
                if(!addedFlag)
                {
                    dictionary.Add(GetNextKey(dictionary), value);
                }
                Console.WriteLine($"Sucessfully moved song {value} to index {newIndex}");
                return true;
            }
            
            Console.WriteLine($"Can't add to index {newIndex}!");
            Console.WriteLine("Action canceled");
            return false;
        }
        public static void SaveToTxtFile(Dictionary<int, string> dictionary)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter("pjesme.txt"))
            {
                foreach(var song in dictionary)
                {
                    file.WriteLine(song.Key + " " + song.Value);
                }
            }
            Console.WriteLine("Playlist saved to pjesme.txt!");
        }
        public static void ReadFromFile(Dictionary<int, string> dictionary)
        {
            List<string> list = new List<string>();
            System.IO.StreamReader file = new System.IO.StreamReader("pjesme.txt");
            while(file.EndOfStream == false)
            {
                list.Add(file.ReadLine());
            }
            file.Close();
            for(var j = 0; j < list.Count; j++)
            {
                var song = list[j];
                var songInformation = song.Split(' ', 2);
                if(dictionary.ContainsValue(songInformation[1]) == false)
                {
                    dictionary.Add(GetNextKey(dictionary), songInformation[1]);
                } 
            }
        }
        public static void ShufflePlaylist(Dictionary<int, string> dictionary)
        {
            if(dictionary.Count > 1)
            {
                List<string> bufferList = new List<string>();
                foreach(var song in dictionary)
                    bufferList.Add(song.Value);
                dictionary.Clear();
                var cnt = bufferList.Count;
                for(var i = 0; i < cnt; i++)
                {
                    Random r = new Random();
                    int rInt = r.Next(0, bufferList.Count);
                    dictionary.Add(i, bufferList[rInt]);
                    bufferList.Remove(dictionary[i]);
                }
            }
        }

    }
}
