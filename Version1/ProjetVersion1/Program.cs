using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic.FileIO; //Libraries that we've used in our project 'more specifically in the class program'
using Microsoft.VisualBasic.Logging;

namespace ProjetVersion1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process(); //System.Diagnostics to delete the Logs daily
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/C ForFiles /p ""C:\000\log"" /s /d -1 /c ""cmd /c del /q @file"""; //LogFolder where logs are created
            process.StartInfo = startInfo;
            process.Start();

            bool idk = true;
            bool language = false;
            string destinationfolder = @"C:\000\destination\"; //Destination  folder where file copies will be saved
            bool start = false;
            while (idk == true)
            {
                Logo.WriteLogo(); //Call WriteLogo method in the Logo class to display the logo saying : 'ProSoft-EasySave Version 1.0'
                printcolormessage(ConsoleColor.White, " Hi ! Welcome to EasySave app", "Bonjour ! Bienvenue dans l'application EasySave", language);
                printcolormessage(ConsoleColor.White, " Please choose one of the following options :", "Veuillez choisir l'une des options suivantes :", language);
                printcolormessage(ConsoleColor.Blue, "            [1] Change the language to french.", "            [1] Changer la langue en anglais.", language);
                printcolormessage(ConsoleColor.Blue, "            [2] Make a backup set of copies (up to 5 files at once).", "            [2] Effectuer un ensemble de copies de sauvegarde (jusqu'à 5 fichiers à la fois).", language);
                string zac = Console.ReadLine();
                if (zac == "1") //Switching between the languages (EN/FR)
                {
                    if (language == true)
                    {
                        language = false;
                    }
                    else
                    {
                        language = true;
                    }
                    Console.Clear();
                }
                else if (zac == "2") //Enter the saving process
                {
                    idk = false;
                }
                else if (zac != "2" || zac != "1")
                {
                    Console.Clear();
                    continue;
                }

            }
            //Save process starts now
            int j;
            bool filexistj = File.Exists(@"C:\000\log\j.txt"); //The program will check if a save was already made before in the Log folder
            TextWriter savej;
            if (filexistj == true)
            {
                var path = @"C:\000\log\j.txt";
                string content = File.ReadAllText(path, Encoding.UTF8);
                j = Int32.Parse(content); //Get j value which is th key element while naming our saves
                var pathjournal = @"C:\000\log\journal.json"; //Path where daily log will be saved
                string contentjournal = File.ReadAllText(pathjournal, Encoding.UTF8);
                var charsToRemovejournal = new string[] { "]" }; //Remove the closing bracket when adding new save info to the daily log
                foreach (var f in charsToRemovejournal)
                {
                    contentjournal = contentjournal.Replace(f, string.Empty);
                }

                savej = new StreamWriter(@"C:\000\log\journal.json"); //Write Save n info to the daily log
                savej.Write(contentjournal);
            }
            else
            {
                j = 0;
                savej = new StreamWriter(@"C:\000\log\journal.json"); //Write Save 1 info to the daily log
                savej.WriteLine("[");
            }
            while (start == false)
            {
                j++;
                if (j > 1)
                {
                    printcolormessage(ConsoleColor.Yellow, " Do you want to make save(" + j + ") ? [Y or N]", " Voulez-vous faire une sauvegarde(" + j + ") ? [O ou N]", language); //After the second save the program asks you if you want to add more saves
                    string answerYesOrNo = Console.ReadLine().ToUpper(); //Get the answer of the user in upper key
                    if (answerYesOrNo != "Y" && answerYesOrNo != "YE" && answerYesOrNo != "YES" && language == false) //If the selected language was English and we get an answer other than "'Y' or 'YE' or 'YES'"
                    {
                        savej.Write("]"); //The program will add the closing bracket
                        savej.Close(); //The program closes the daily log
                        TextWriter savej2 = new StreamWriter(@"C:\000\log\j.txt");
                        savej2.WriteLine(j - 1);//The program will save the variable j-1 in j.txt file
                        savej2.Close(); //The program closes the j.txt file
                        return; //App runtime will end
                    }
                    else if (answerYesOrNo != "O" && answerYesOrNo != "OU" && answerYesOrNo != "OUI" && language == true) //If the selected language was French and we get an answer other than "'O' or 'OU' or 'OUI'" same thing happens to the rest as above
                    {
                        savej.Write("]");
                        savej.Close();
                        TextWriter savej2 = new StreamWriter(@"C:\000\log\j.txt");
                        savej2.WriteLine(j - 1);
                        savej2.Close();
                        return;
                    }

                }

                TextWriter savef = new StreamWriter(@"C:\000\log\save(" + j + ").json"); //A new save log will be created
                savef.Write("["); //The program will add the opening bracket to the save file

                printcolormessage(ConsoleColor.Red, " Please enter the files of save(" + j + ") :", " Veuillez entrer les fichiers de la sauvegarde(" + j + ") :", language); //The program will ask the user to enter the source path of the files he wants to make a copy file
                int y = 0;
                int z;
                long x = 0;
                int filenumber = 0;
                for (int i = 1; i < 6; i++)
                {

                    string file = Console.ReadLine(); //The program will read the path of the file to be copied
                    bool filexist = File.Exists(file); //Variable to check if the file already exists
                    if (filexist)
                    {
                        var watch = System.Diagnostics.Stopwatch.StartNew(); //Variable to start a timer in order to calculate the time that the program has taken to create the save
                        long length = new System.IO.FileInfo(file).Length;  //Variable to read the file's size
                        x = x + length;
                        z = 5;
                        z = z - i;
                        filenumber = 5 - z;
                        printcolormessage(ConsoleColor.Yellow, "files left: " + z, "fichies restant: " + z, language); //Show the user how many additional files he can copy in this particular save
                        FileSystem.CopyFile(file, $"{destinationfolder}{Path.GetFileName(file)}", UIOption.AllDialogs); //Copy the file
                        string destination = destinationfolder + Path.GetFileName(file); //Put the file in the wished destination
                        DateTime timenowfile = DateTime.Now;  //Get both date and time info when the file was finally copied
                        watch.Stop(); //Timer has ended
                        var elapsedMs = watch.ElapsedMilliseconds; //Duration time is stored inside of the watch variable

                        y = y + ((int)elapsedMs);
                        List<File_details> _datafile = new List<File_details>();
                        _datafile.Add(new File_details() //Assigning the different data values to the some variables as you can see down below
                        {
                            Name = Path.GetFileNameWithoutExtension(file),
                            FileSource = Path.GetFullPath(file),
                            FileTarget = destination,
                            FileSize = length,
                            FileSaveDuration = (int)elapsedMs,
                            SavedAt = timenowfile
                        });
                        string StrResultJsonfile = JsonConvert.SerializeObject(_datafile.ToArray(), Formatting.Indented); //Filling the save JSON file
                        var charsToRemovef = new string[] { "[", "]" };
                        foreach (var c in charsToRemovef)
                        {
                            StrResultJsonfile = StrResultJsonfile.Replace(c, string.Empty);
                        }
                        savef.WriteLine(StrResultJsonfile); //The program will finish saving the save log file
                        printcolormessage(ConsoleColor.Green, "Your save has been made succesfully !", "Votre sauvegarde a été effectuée avec succès !", language); //The program will display a success message
                    }
                    else
                    {
                        if (i == 1) //Non existing file
                        {
                            DateTime timenowfile = DateTime.Now;
                            List<File_details> _datafile = new List<File_details>();
                            _datafile.Add(new File_details()
                            {
                                Name = "",
                                FileSource = "",
                                FileTarget = "",
                                FileSize = 0,
                                FileSaveDuration = 0,
                                SavedAt = timenowfile
                            });
                            string StrResultJsonfile = JsonConvert.SerializeObject(_datafile.ToArray(), Formatting.Indented);
                            var charsToRemovef = new string[] { "[", "]" };
                            foreach (var c in charsToRemovef)
                            {
                                StrResultJsonfile = StrResultJsonfile.Replace(c, string.Empty);
                            }
                            savef.WriteLine(StrResultJsonfile); //The program will finish saving the save log file
                            printcolormessage(ConsoleColor.Red, "Error ! this file does not exist", "Erreur ! Ce fichier n'existe pas", language); //The program will display an error message, but still a log save is created and empty
                    }
                        i = 6;
                    }

                }
                savef.Write("]"); //Add the closing bracket
                savef.Close();  //The program closes the save log
                DateTime timenowsave = DateTime.Now; //Get both date and time info when the daily log was created
                List<Save_details> _datasave = new List<Save_details>();
                _datasave.Add(new Save_details() //Assigning the different data values to the some variables as you can see down below
                {
                    Name = "save" + j,
                    NbFiles = filenumber,
                    TotalSaveSize = x,
                    SaveDuration = y,
                    CreatedAt = timenowsave

                });
                string StrResultJsonsave = JsonConvert.SerializeObject(_datasave.ToArray(), Formatting.Indented); //Filling the daily log file

                var charsToRemovej = new string[] { "[", "]" };
                foreach (var k in charsToRemovej)
                {
                    StrResultJsonsave = StrResultJsonsave.Replace(k, string.Empty);
                }
                savej.WriteLine(StrResultJsonsave); //The program will finish saving the daily log file
            }

        }
        static void printcolormessage(ConsoleColor color, string message_en, string message_fr, bool language) //Custom function that dispalys colorful messages in two different languages
        {
            Console.ForegroundColor = color;
            if (language == false)
            {
                Console.WriteLine(message_en);
            }
            else
            {
                Console.WriteLine(message_fr);
            }
            Console.ResetColor();
        }
    }
}