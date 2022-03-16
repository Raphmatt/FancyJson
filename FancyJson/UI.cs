using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace FancyJson
{
    public class UI
    {
        /// <summary>
        /// Shows Menu and starts the program
        /// </summary>
        /// <returns>Returns false if the program closes. Else it returns true</returns>
        public static bool Menu()
        {
            Console.Clear();
            Console.WriteLine($"" +
                $"|--------------------------|\n" +
                $"| 1. Create new Json       |\n" +
                $"| 2. Delete Json           |\n" +
                $"|                          |\n" +
                $"| Enter Number             |\n" +
                $"|                          |\n" +
                $"| Press q to Quit          |\n" +
                $"|--------------------------|\n");

            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    UI.CreateJson();
                    break;
                case ConsoleKey.D2:
                    UI.DeleteJson();
                    break;
                case ConsoleKey.Q:
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Shows CreateJson and starts the process of creating a Json file with entries.
        /// </summary>
        public static void CreateJson()
        {
            Console.Clear();

            Console.Write("| Enter json filename \n| (to cancel leave empty and press enter)\n| ");
            int curserLocation = Console.CursorTop;

            DirectoryInfo dirInfo = new DirectoryInfo(Utility.GetPathJson());

            if (!Directory.Exists(Utility.GetPathJson()))
            {
                Directory.CreateDirectory(Utility.GetPathJson()); //erstellt den json ordner
            }
            else if (dirInfo.GetFiles("*.json").Any()) // schaut nach 
            {
                FileInfo[] fileNames = dirInfo.GetFiles("*.json");
                Console.WriteLine("\n\n\n\n| Already existing json files:\n|");
                foreach (FileInfo file in fileNames)
                {
                    Console.WriteLine($"| {file.Name}"); //lists the exist jsons
                }
            }

            Console.SetCursorPosition(2, curserLocation);
            string fileName = Console.ReadLine().ToLower();

            if (fileName == "") return;
            if (!fileName.EndsWith(".json")) fileName = fileName + ".json"; //checks extension

            string filePath = Path.Combine(Utility.GetPathJson(), fileName);
            if (!File.Exists(filePath))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("| Creating file...");
                Console.ResetColor();
                FileStream currentFile = File.Create(filePath);//creates the file
                currentFile.Close();//closes the file to get the write acces

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("| Successfully created file ");
                Console.ResetColor();
                Console.Write($"({fileName})");
                Console.ReadKey(true);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n| Cannot create file, because file already exist ");
                Console.ResetColor();
                Console.Write($"({fileName})");
                Console.ReadKey(false);
                return;
            }

            Console.Clear();
            Console.WriteLine($"| Do you want to fill {fileName} with data? (y/n or any other key)");
            ConsoleKey key = Console.ReadKey().Key;
            if (key != ConsoleKey.Y) return;
            Console.Clear();

            List<Person> people = new List<Person>();
            int countPeople = 1;
            const int blockSize = 5;
            const int instructionSize = 1;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("| Enter Credentials: (to cancel leave empty and press enter)");
            Console.ResetColor();
            while (true)
            {
                int currentLine;
                bool cancel = false; ;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(@"/--------------------
| Forename  :                     
| Surname   :                     
| Age       :                     
| Gender m/f:                     ");
                Console.ResetColor();

                string forename;
                string surname;
                int age = 0;
                bool isMan;

                Console.SetCursorPosition(14, blockSize * countPeople + instructionSize - 4);
                currentLine = Console.CursorTop;
                forename = Console.ReadLine().TrimEnd();
                if (forename == "") break;

                Console.SetCursorPosition(14, currentLine + 1);
                currentLine = Console.CursorTop;
                surname = Console.ReadLine().TrimEnd();
                if (surname == "") break;

                Console.SetCursorPosition(14, currentLine + 1);
                currentLine = Console.CursorTop;
                while (true)
                {
                    try
                    {
                        string input = Console.ReadLine().TrimEnd();
                        if (input == "" || input == "0")
                        {
                            cancel = true;
                            break;
                        }
                        age = Convert.ToInt32(input);
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.SetCursorPosition(0, blockSize * countPeople + instructionSize + 1);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("| Age can only be a natural number                    ");
                        Console.ResetColor();
                        Console.SetCursorPosition(14, currentLine);
                        Console.Write("                    ");
                        Console.SetCursorPosition(14, currentLine);
                    }
                }
                if (cancel) break;

                Console.SetCursorPosition(14, currentLine + 1);
                currentLine = Console.CursorTop;
                while (true)
                {
                    string input = Console.ReadLine().ToLower();
                    if (input == "m" || input == "male")
                    {
                        isMan = true;
                        break;
                    }
                    else if (input == "f" || input == "female")
                    {
                        isMan = false;
                        break;
                    }
                    Console.SetCursorPosition(0, blockSize * countPeople + instructionSize + 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("| Please enter f/female or m/male                    ");
                    Console.ResetColor();
                    Console.SetCursorPosition(14, currentLine);
                    Console.Write("                    ");
                    Console.SetCursorPosition(14, currentLine);
                }
                if (cancel) break;


                Person person = new Person { Forename = forename, Surname = surname, Age = age, IsMan = isMan };
                people.Add(person);
                countPeople++;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("| Writing to file... ");

            string jsonPeople = JsonConvert.SerializeObject(people, Formatting.Indented);
            File.WriteAllText(filePath, jsonPeople);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("done!\n| Path to file:\n| ");
            Console.ResetColor();
            Console.WriteLine($"{filePath}");

            Console.ReadKey(true);

            return;
        }

        /// <summary>
        /// Shows DeleteJson and deletes the specified json file.
        /// </summary>
        public static void DeleteJson()
        {
            Console.Clear();
            DirectoryInfo dirInfo = new DirectoryInfo(Utility.GetPathJson());

            //Check if the Direcctory exists
            if (!Directory.Exists(Utility.GetPathJson()))
            {
                Console.WriteLine("| There is no json folder resulting in no json files.");
                Console.ReadKey(true);
                return;
            }//checks  if there are any files in the directory
            if (!dirInfo.GetFiles("*.json").Any())
            {
                Console.WriteLine($"| There are no json files in the json folder.\n| Folderpath: {Utility.GetPathJson()}");
                Console.ReadKey(true);
                return;
            }
            else //there are files in the directory
            {
                while (true)
                {
                    Console.Clear();
                    Console.Write("| Enter json filename to delete \n| (to cancel leave empty and press enter)\n| ");

                    FileInfo[] fileNames = dirInfo.GetFiles("*.json");
                    Console.WriteLine("\n\n\n\n| Available files to delete:\n|"); //Lists the  Files which already exists
                    foreach (FileInfo file in fileNames)
                    {
                        Console.WriteLine($"| {file.Name}");
                    }

                    Console.SetCursorPosition(2, 2);
                    string fileName = Console.ReadLine().ToLower();

                    if (fileName == "") return;
                    if (!fileName.EndsWith(".json")) fileName = fileName + ".json"; //checks extension
                    if (!fileNames.Where(f => f.Name == fileName).Any())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"| There is no file with the name {fileName}");
                        Console.ResetColor();
                    }
                    else
                    {
                        string deleteFile = fileNames.Where(f => f.Name == fileName).FirstOrDefault().FullName;
                        int currentLine = Console.CursorTop;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"| Are you sure to delete {fileName}? y/n or any other key"); // double check 
                        Console.ResetColor();
                        ConsoleKey key = Console.ReadKey().Key;
                        if (key != ConsoleKey.Y) return;
                        Console.SetCursorPosition(0, currentLine);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"| Deleting {fileName}...                                                ");
                        File.Delete(deleteFile);                                     // deleting process
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("| Done!");
                        Console.ResetColor();
                        Console.ReadKey(true);
                        return;
                    }
                }
            }
        }
    }
}
