﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FinchAPI;

namespace FinchControl
{
    class Program
    {

        //...............................
        //Title:
        //Application Type:
        //Description:
        //Author: Sarah Ennis
        //Date Created: 06/03/2021
        //Last Motified: 06/13/2021
        //.................................

        static bool Lights { get; set; }

        static void Main(string[] args)
        {
            DisplayWelcomeScreen("Welcome to the finch controller applicatiion!");
            DisplayMenu();
            DisplayClosingScreen();
        }

        static void DisplayMenu()
        {
            string menuChoice;
            bool exiting = false, connected = false;
            Finch birdie = new Finch();

            while (!exiting)
            {
                DisplayHeader("Main Menu");
                Console.WriteLine("1. Connect Finch");
                Console.WriteLine("2. Talent Show");
                Console.WriteLine("3. Data Recorder");
                Console.WriteLine("4. Alarm System");
                Console.WriteLine("5. User Programming");
                Console.WriteLine("6. Disconnect Finch Robot");
                Console.WriteLine("E. Exit");
                Console.WriteLine("Enter Choice:");
                menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        string tryAgain;

                        while (!connected)
                        {
                            connected = DisplayConnectFinchRobot(birdie);
                            if(!connected)
                            {
                                Console.WriteLine();
                                Console.WriteLine("There seems to be an issue connecting to the finch...");
                                Console.WriteLine("1. Try Again");
                                Console.WriteLine("Enter: Return to the Main Menu");
                                Console.WriteLine("Enter Choice:");
                                tryAgain = Console.ReadLine();
                                switch (tryAgain)
                                {
                                    case "1":
                                        break;
                                    default:
                                        connected = true;
                                        break;
                                }
                            }
                        }
                        break;
                    case "2":
                        TalentShowDisplayMenuScreen(birdie);
                        break;
                    case "3":
                        DataRecorderDisplayMenuScreen(birdie);
                        break;
                    case "4":
                        AlarmSystemDisplayMenuScreen(birdie);
                        break;
                    case "5":
                        UserProgrammingDisplayMenuScreen(birdie);
                        break;
                    case "6":
                        DisplayDisconnectFinchRobot(birdie);
                        break;
                    case "E":
                    case "e":
                        exiting = true;
                        break;
                    default:
                        Console.WriteLine("Please make a valid choice.");
                        DisplayContinuePrompt();
                        break;
                }
            }   
        }

        static void TalentShowDisplayMenuScreen(Finch finch)
        {
            string menuChoice;
            bool exiting = false;

            while (!exiting)
            {
                DisplayHeader("Main Menu");
                Console.WriteLine("1. Light and Sound");
                Console.WriteLine("2. Dance");
                Console.WriteLine("3. Mixing It Up");
                Console.WriteLine("4. Return to Main Menu");
                Console.WriteLine("Enter Choice:");
                menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        TalentShowDisplayLightAndSound(finch);
                        break;
                    case "2":
                        TalentShowDisplayDance(finch);
                        break;
                    case "3":
                        TalentShowDisplayMixingItUp(finch);
                        break;
                    case "4":
                        exiting = true;
                        break;
                    default:
                        Console.WriteLine("Please make a valid choice.");
                        DisplayContinuePrompt();
                        break;
                }
            }
        }
        
        static void TalentShowDisplayLightAndSound(Finch finch)
        {
            DisplayHeader("Light and Sound");
            Console.WriteLine("Enjoy the light and sound features of the finch!");
            for (int i = 0; i < 10; i++)
            {
                RandomColorAndNote(finch);
            }
            DisplayContinuePrompt();
        }

        static void TalentShowDisplayDance(Finch finch)
        {
            DisplayHeader("Dance");
            Console.WriteLine("Enjoy this little dance from the finch!");

            BetterMotors(finch, -255, 255, 250);
            BetterMotors(finch, 255, -255, 250);
            BetterMotors(finch,-255, 255, 1200);
            BetterMotors(finch, 255, -255, 250);
            BetterMotors(finch, -255, 255, 250);
            BetterMotors(finch, 255, -255, 1200);

            DisplayContinuePrompt();
        }

        static void TalentShowDisplayMixingItUp(Finch finch)
        {
            DisplayHeader("Mixing it Up!");
            Console.WriteLine("Enjoy this little song and dance from the finch!");
            
            BetterMotors(finch, -255, 255, 250);
            RandomColorAndNote(finch);
            BetterMotors(finch, 255, -255, 250);
            RandomColorAndNote(finch);
            BetterMotors(finch, -255, 255, 1200);
            RandomColorAndNote(finch);
            BetterMotors(finch, 255, -255, 250);
            RandomColorAndNote(finch);
            BetterMotors(finch, -255, 255, 250);
            RandomColorAndNote(finch);
            BetterMotors(finch, 255, -255, 1200);

            DisplayContinuePrompt();
        }

        static void DataRecorderDisplayMenuScreen(Finch finch)
        {
            string menuChoice;
            bool exiting = false;
            int numberOfDataPoints = 0;
            double dataPointFrequency = 0;
            double[] data = new double[numberOfDataPoints];

            DisplayHeader("Data Recorder");
            while (!exiting)
            {
                DisplayHeader("Data Recorder Menu");
                Console.WriteLine("1. Number of Data Points");
                Console.WriteLine("2. Frequency of Data Points");
                Console.WriteLine("3. Get Data");
                Console.WriteLine("4. Show Data");
                Console.WriteLine("5. Return to Main Menu");
                Console.WriteLine("Enter Choice:");
                menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;
                    case "2":
                        dataPointFrequency = DataRecorderDisplayGetDataPointFrequency(); 
                        break;
                    case "3":
                        data = DataRecorderDisplayGetData(numberOfDataPoints, dataPointFrequency, finch);
                        break;
                    case "4":
                        DataRecorderDisplayData(data);
                        break;
                    case "5":
                        exiting = true;
                        break;
                    default:
                        Console.WriteLine("Please make a valid choice.");
                        DisplayContinuePrompt();
                        break;
                }
            }
        }

        static double DataRecorderDisplayGetDataPointFrequency()
        {
            double seconds;

            DisplayHeader("Data Point Frequency");
            Console.WriteLine("Please enter the frequency of readings in seconds");
            while (!double.TryParse(Console.ReadLine(), out seconds) || seconds < 1)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid. Must be a number, and must be 1 or greater. Please try again:");
            }
            Console.WriteLine();
            Console.WriteLine($"You chose {seconds} seconds.");
            DisplayContinuePrompt();

            return  seconds * 1000;
        }

        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            int number;

            DisplayHeader("Number of Data Points");
            Console.WriteLine("Please enter the number of readings");
            while (!int.TryParse(Console.ReadLine(), out number) || number < 1)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid. Must be a number, and must be 1 or greater. Please try again:");
            }
            Console.WriteLine();
            Console.WriteLine($"You chose {number} times.");
            DisplayContinuePrompt();

            return number;
        }

        static double[] DataRecorderDisplayGetData(int numberOfDataPoints, double dataPointFrequency, Finch finch)
        {
            double[] data = new double[numberOfDataPoints];
            int seconds = Convert.ToInt32(Math.Floor(dataPointFrequency));
            double readingData;
            bool exiting = false;
            string menuChoice, readingType = "";

            DisplayHeader("Get Data");

            while (!exiting)
            {
                DisplayHeader("Data Recorder Menu");
                Console.WriteLine("1. Temperature");
                Console.WriteLine("2. Lights");
                Console.WriteLine("Enter Choice:");
                menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        readingType = "temperature";
                        exiting = true;
                        Lights = false;
                        break;
                    case "2":
                        readingType = "light";
                        Lights = true;
                        exiting = true;
                        break;
                    default:
                        Console.WriteLine("Please make a valid choice.");
                        DisplayContinuePrompt();
                        break;
                }
            }

            Console.WriteLine($"The program will now take a {readingType} reading every {dataPointFrequency / 1000} seconds, {numberOfDataPoints} times.");
            DisplayContinuePrompt();

            for (int i = 0; i < numberOfDataPoints; i++)
            {
                readingData = Lights ? finch.getLightSensors().Average() : finch.getTemperature();
                data[i] = readingData;
                Console.WriteLine($"{readingData}");
                finch.wait(seconds);
            }
            Console.WriteLine();
            Console.WriteLine("Data recording is complete");
            DisplayContinuePrompt();

            return data;
        }

        static void DataRecorderDisplayDataTable(double[] data)
        {
            Console.WriteLine(Lights ? "Reading | Light Value" : "Reading | Celcius | Fahrenheit");
            Console.WriteLine();
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine(Lights ? $"reading {i + 1} | {data[i]}" : $"reading {i + 1} | {data[i]} | {CelciusToFahrenheit(data[i])}");
            }
        }

        static void DataRecorderDisplayData(double[] data)
        {
            DisplayHeader("Data Display");
            DataRecorderDisplayDataTable(data);
            DisplayContinuePrompt();
        }
        static void AlarmSystemDisplayMenuScreen(Finch finch)
        {
            DisplayHeader("Alarm System");
            Console.WriteLine("This module is under development.");
            Console.WriteLine();
            DisplayContinuePrompt();
        }

        static void UserProgrammingDisplayMenuScreen(Finch finch)
        {
            DisplayHeader("User Programming");
            Console.WriteLine("This module is under development.");
            Console.WriteLine();
            DisplayContinuePrompt();
        }

        static void DisplayDisconnectFinchRobot(Finch finch)
        {
            DisplayHeader("Disconnect the Finch!");

            Console.WriteLine("The application is about to disconnect the finch.");
            Console.WriteLine();
            DisplayContinuePrompt();


            finch.disConnect();
            Console.WriteLine("Your Finch Robot is now disconnected!!");
            DisplayContinuePrompt();
        }

        static bool DisplayConnectFinchRobot(Finch finch)
        {
            bool connected = false;

            DisplayHeader("Connect the Finch!");

            Console.WriteLine("Please plug your Finch Robot into the computer.");
            Console.WriteLine();
            DisplayContinuePrompt();

            Console.WriteLine("Attempting to connect to the Finch.");
            for(int i = 1; i < 4; i++)
            {
                Console.WriteLine($"Attempt {i}...");
                if(finch.connect())
                {
                    connected = true;
                    Console.WriteLine("Your Finch Robot is now connected");
                    DisplayContinuePrompt();
                    break;
                }
                Thread.Sleep(1000);
            }

            return connected;
        }

        static void DisplayHeader(string headText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(headText);
            Console.WriteLine();
        }

        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void DisplayWelcomeScreen(string text)
        {
            Console.Clear();
            Console.WriteLine(text);
            DisplayContinuePrompt();
        }

        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine("Thank you, goodbye.");
            DisplayContinuePrompt();
        }

        static void BetterMotors(Finch finch, int left, int right, int wait)
        {
            finch.setMotors(left, right);
            finch.wait(wait); 
            finch.setMotors(0, 0);
        }

        static void RandomColorAndNote(Finch finch)
        {
            var random = new Random();

            int r = random.Next(0, 256);
            int g = random.Next(0, 256);
            int b = random.Next(0, 256);

            int note = random.Next(200, 20000);

            finch.setLED(r, g, b);
            finch.noteOn(note);
            finch.wait(500);
            finch.noteOff();
        }

        static double CelciusToFahrenheit(double celcius)
        {
            return celcius * 1.8 + 32;
        }
    }
}