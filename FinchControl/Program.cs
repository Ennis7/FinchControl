using FinchAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FinchControl
{
    public enum FinchCommand
    {
        NONE,
        DONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        DELAY,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF,
        GETTEMPERATURE,
        LIGHTLEVEL,
        RANDOMCOLORANDNOTE
    }

    class Program
    {

        //...............................
        // Title: Finch Control
        // Application Type: Console App
        // Description: Application to control amd demonstrate the Finch robots various functions.
        // Author: Sarah Ennis
        // Date Created: 06/03/2021
        // Last Motified: 06/25/2021
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
                            if (!connected)
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
                        birdie.disConnect();
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
            BetterMotors(finch, -255, 255, 1200);
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

            return seconds * 1000;
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
            string[] rangeTypes = new string[] { "", "" };
            int[] minMaxThresholdValues = new int[] { 0, 0 };
            string menuChoice, sensorsToMonitor = "", sensorType = "", lightThresholdType = "", tempThresholdType = "";
            bool exiting = false;
            int lightThresholdValue = 0, tempThresholdValue = 0, timeToMonitor = 0;

            DisplayHeader("Alarm System");

            while (!exiting)
            {
                DisplayHeader("Main Menu");
                Console.WriteLine("1. Set Sensor Type");
                Console.WriteLine("2. Set Sensors To Monitor [light sensors only]");
                Console.WriteLine("3. Set Range Type");
                Console.WriteLine("4. Set Maximum/Minimum Threshold");
                Console.WriteLine("5. Set Time To Monitor");
                Console.WriteLine("6. Set Alarm");
                Console.WriteLine("7. Return To Main Menu");
                Console.WriteLine("Enter Choice:");
                menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        sensorType = AlarmDisplaySetSensorType(finch);
                        break;
                    case "2":
                        sensorsToMonitor = AlarmDisplaySetSensorsToMonitor(finch);
                        break;
                    case "3":
                        rangeTypes = AlarmDisplaySetRangeType(sensorType);
                        lightThresholdType = rangeTypes[0];
                        tempThresholdType = rangeTypes[1];
                        break;
                    case "4":
                        minMaxThresholdValues = AlarmDisplaySetMinMaxThresholdValue(finch, lightThresholdType, tempThresholdType, sensorType);
                        lightThresholdValue = minMaxThresholdValues[0];
                        tempThresholdValue = minMaxThresholdValues[1];
                        break;
                    case "5":
                        timeToMonitor = AlarmDisplaySetMaximumTimeToMonitor();
                        break;
                    case "6":
                        AlarmDisplaySetAlarm(finch, sensorsToMonitor, sensorType, lightThresholdType, tempThresholdType, lightThresholdValue, tempThresholdValue, timeToMonitor);
                        break;
                    case "7":
                        exiting = true;
                        break;
                    default:
                        Console.WriteLine("Please make a valid choice.");
                        DisplayContinuePrompt();
                        break;
                }
            }
        }

        static string AlarmDisplaySetSensorType(Finch finch)
        {
            string answer = "";
            bool exiting = false;

            DisplayHeader("Set Sensors Type");
            Console.WriteLine();
            Console.WriteLine($"Left light sensor: {finch.getLeftLightSensor()}");
            Console.WriteLine($"Right light sensor: {finch.getRightLightSensor()}");
            Console.WriteLine($"Temperature: {finch.getTemperature()}");
            Console.WriteLine();

            Console.WriteLine("Which sensor(s) would you like to use? [lights, temperature, both]");
            while (!exiting)
            {
                answer = Console.ReadLine();
                switch (answer.ToLower())
                {
                    case "lights":
                        exiting = true;
                        break;
                    case "temperature":
                        exiting = true;
                        break;
                    case "both":
                        exiting = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid. Answer must be lights, temperature or both. Please try again:");
                        break;
                }
            }

            Console.WriteLine($"Sensors set to {answer}");
            DisplayContinuePrompt();

            return answer;
        }

        static string[] AlarmDisplaySetRangeType(string sensorType)
        {
            string[] answer = new string[] { "", "" };
            bool exitingLights = false, exitingTemp = false;

            DisplayHeader("Alarm Range Type");

            switch (sensorType)
            {
                case "lights":
                    Console.WriteLine("Would you like to use a minimum or maximum light threshold?");
                    while (!exitingLights)
                    {
                        answer[0] = Console.ReadLine();
                        switch (answer[0].ToLower())
                        {
                            case "minimum":
                                exitingLights = true;
                                break;
                            case "maximum":
                                exitingLights = true;
                                break;
                            default:
                                Console.WriteLine();
                                Console.WriteLine("Invalid. Answer must be minimum or maximum. Please try again:");
                                break;
                        }
                    }
                    Console.WriteLine($"Light Threshold Type: {answer[0]}");
                    DisplayContinuePrompt();
                    break;
                case "temperature":
                    Console.WriteLine("Would you like to use a minimum or maximum temperature threshold?");
                    while (!exitingTemp)
                    {
                        answer[1] = Console.ReadLine();
                        switch (answer[1].ToLower())
                        {
                            case "minimum":
                                exitingTemp = true;
                                break;
                            case "maximum":
                                exitingTemp = true;
                                break;
                            default:
                                Console.WriteLine();
                                Console.WriteLine("Invalid. Answer must be minimum or maximum. Please try again:");
                                break;
                        }
                    }
                    Console.WriteLine($"Temperature Threshold Type: {answer[1]}");
                    DisplayContinuePrompt();
                    break;
                default:
                    Console.WriteLine("Would you like to use a minimum or maximum light threshold?");
                    while (!exitingLights)
                    {
                        answer[0] = Console.ReadLine();
                        switch (answer[0].ToLower())
                        {
                            case "minimum":
                                exitingLights = true;
                                break;
                            case "maximum":
                                exitingLights = true;
                                break;
                            default:
                                Console.WriteLine();
                                Console.WriteLine("Invalid. Answer must be minimum or maximum. Please try again:");
                                break;
                        }
                    }
                    Console.WriteLine("Would you like to use a minimum or maximum temperature threshold?");
                    while (!exitingTemp)
                    {
                        answer[1] = Console.ReadLine();
                        switch (answer[1].ToLower())
                        {
                            case "minimum":
                                exitingTemp = true;
                                break;
                            case "maximum":
                                exitingTemp = true;
                                break;
                            default:
                                Console.WriteLine();
                                Console.WriteLine("Invalid. Answer must be minimum or maximum. Please try again:");
                                break;
                        }
                    }
                    Console.WriteLine($"Lights Threshold Type: {answer[0]}");
                    Console.WriteLine($"Temperature Threshold Type: {answer[1]}");
                    DisplayContinuePrompt();
                    break;
            }


            return answer;
        }

        static int[] AlarmDisplaySetMinMaxThresholdValue(Finch finch, string lightThresholdType, string tempThresholdType, string sensorType)
        {
            int[] answer = new int[] { 0, 0 };

            DisplayHeader($"Set Threshold Values");
            switch (sensorType)
            {
                case "lights":
                    Console.WriteLine($"Set value for {lightThresholdType} light threshold");
                    while (!int.TryParse(Console.ReadLine(), out answer[0]))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid. Threshold must be a number. Please try again:");
                    }
                    Console.WriteLine($"Light threshold set to {answer[0]}");
                    DisplayContinuePrompt();
                    break;
                case "temperature":
                    Console.WriteLine($"Set value for {tempThresholdType} temperature threshold");
                    while (!int.TryParse(Console.ReadLine(), out answer[1]))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid. Threshold must be a number. Please try again:");
                    }
                    Console.WriteLine($"Temperature threshold set to {answer[1]}");
                    DisplayContinuePrompt();
                    break;
                default:
                    Console.WriteLine($"Set value for {lightThresholdType} light threshold");
                    while (!int.TryParse(Console.ReadLine(), out answer[0]))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid. Threshold must be a number. Please try again:");
                    }
                    Console.WriteLine($"Set value for {tempThresholdType} temperature threshold");
                    while (!int.TryParse(Console.ReadLine(), out answer[1]))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid. Threshold must be a number. Please try again:");
                    }
                    Console.WriteLine($"Light threshold set to {answer[0]}");
                    Console.WriteLine($"Temperature threshold set to {answer[1]}");
                    DisplayContinuePrompt();
                    break;
            }

            return answer;
        }

        static string AlarmDisplaySetSensorsToMonitor(Finch finch)
        {
            string answer = "";
            bool exiting = false;

            DisplayHeader("Set Sensors To Monitor");
            Console.WriteLine();
            Console.WriteLine($"Left light sensor: {finch.getLeftLightSensor()}");
            Console.WriteLine($"Right light sensor: {finch.getRightLightSensor()}");
            Console.WriteLine();

            Console.WriteLine("Which light sensor would you like to monitor? [left, right, both]");
            while (!exiting)
            {
                answer = Console.ReadLine();
                switch (answer.ToLower())
                {
                    case "left":
                        exiting = true;
                        break;
                    case "right":
                        exiting = true;
                        break;
                    case "both":
                        exiting = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid. Answer must be left, right or both. Please try again:");
                        break;
                }
            }

            Console.WriteLine($"Sensors set to {answer}");
            DisplayContinuePrompt();

            return answer;
        }

        static int AlarmDisplaySetMaximumTimeToMonitor()
        {
            int answer;

            DisplayHeader("Set Monitor Time");
            Console.WriteLine($"How many seconds would you like to monitor for");
            while (!int.TryParse(Console.ReadLine(), out answer) || answer <= 0)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid. Time must be a number greater than zero. Please try again:");
            }
            Console.WriteLine($"Time to monitor set to {answer}");
            DisplayContinuePrompt();

            return answer;
        }

        static void AlarmDisplaySetAlarm(Finch finch, string sensorsToMonitor, string sensorType, string lightThresholdType, string tempThresholdType, int lightThresholdValue, int tempThresholdValue, int timeToMonitor)
        {
            double startingLightLevel = GetLightLevel(finch, sensorsToMonitor), lightThreshold, tempThreshold;
            double startingTempLevel = finch.getTemperature();

            DisplayHeader("Alarm Start");
            Console.WriteLine($"Sensors to monitor: {sensorsToMonitor}");
            Console.WriteLine($"Light Threshold Type: {lightThresholdType}");
            Console.WriteLine($"Temp Threshold Type: {tempThresholdType}");
            Console.WriteLine($"Light Threshold Value: {lightThresholdValue}");
            Console.WriteLine($"Temp Threshold Value: {tempThresholdValue}");
            Console.WriteLine($"Time: {timeToMonitor}");

            Console.WriteLine("The program is ready to monitor, press any key to start.");
            Console.ReadKey();
            Console.Clear();

            switch (sensorType)
            {
                case "lights":
                    switch (lightThresholdType.ToLower())
                    {
                        case "minimum":
                            lightThreshold = startingLightLevel - lightThresholdValue;
                            for (int i = 0; i < timeToMonitor; i++)
                            {
                                SameLineData($"Current light level: {GetLightLevel(finch, sensorsToMonitor)}", 1);
                                SameLineData($"Elapsed Time: {i + 1}", 3);
                                if (GetLightLevel(finch, sensorsToMonitor) < lightThreshold)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Alert! Light has dropped below the threshold!!!");
                                    Console.WriteLine("Press Escape to turn alarm off.");
                                    while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                                    {
                                        Alarm(finch, 1046, 125);
                                    }
                                    break;
                                }
                                finch.wait(1000);
                            }
                            break;
                        default:
                            lightThreshold = startingLightLevel + lightThresholdValue;
                            for (int i = 0; i < timeToMonitor; i++)
                            {
                                SameLineData($"Current light level: {GetLightLevel(finch, sensorsToMonitor)}", 1);
                                SameLineData($"Elapsed Time: {i + 1}", 3);
                                if (GetLightLevel(finch, sensorsToMonitor) > lightThreshold)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Alert! Light has exceeded threshold!!!");
                                    Console.WriteLine("Press Escape to turn alarm off.");
                                    while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                                    {
                                        Alarm(finch, 1046, 125);
                                    }
                                    break;
                                }
                                finch.wait(1000);
                            }
                            break;
                    }
                    break;
                case "temperature":
                    switch (tempThresholdType.ToLower())
                    {
                        case "minimum":
                            tempThreshold = startingTempLevel - tempThresholdValue;
                            for (int i = 0; i < timeToMonitor; i++)
                            {
                                SameLineData($"Current Temperature: {finch.getTemperature()}", 1);
                                SameLineData($"Elapsed Time: {i + 1}", 3);
                                if (GetLightLevel(finch, sensorsToMonitor) < tempThreshold)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Alert! Temperature has dropped below the threshold!!!");
                                    Console.WriteLine("Press Escape to turn alarm off.");
                                    while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                                    {
                                        Alarm(finch, 1046, 125);
                                    }
                                    break;
                                }
                                finch.wait(1000);
                            }
                            break;
                        default:
                            tempThreshold = startingTempLevel + tempThresholdValue;
                            for (int i = 0; i < timeToMonitor; i++)
                            {
                                SameLineData($"Current Temperature: {finch.getTemperature()}", 1);
                                SameLineData($"Elapsed Time: {i + 1}", 3);
                                if (finch.getTemperature() > tempThreshold)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Alert! Temperature has exceeded threshold!!!");
                                    Console.WriteLine("Press Escape to turn alarm off.");
                                    while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                                    {
                                        Alarm(finch, 1046, 125);
                                    }
                                    break;
                                }
                                finch.wait(1000);
                            }
                            break;
                    }
                    break;
                default:
                    if (tempThresholdType == "maximum" && lightThresholdType == "maximum")
                    {
                        tempThreshold = startingTempLevel + tempThresholdValue;
                        lightThreshold = startingLightLevel + lightThresholdValue;
                        for (int i = 0; i < timeToMonitor; i++)
                        {
                            SameLineData($"Current Temperature: {finch.getTemperature()}", 1);
                            SameLineData($"Current Light Level: {GetLightLevel(finch, sensorsToMonitor)}", 3);
                            SameLineData($"Elapsed Time: {i + 1}", 5);
                            if (finch.getTemperature() > tempThreshold || GetLightLevel(finch, sensorsToMonitor) > lightThreshold)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Alert! Temperature or light has exceeded threshold!!!");
                                Console.WriteLine("Press Escape to turn alarm off.");
                                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                                {
                                    Alarm(finch, 1046, 125);
                                }
                                break;
                            }
                            finch.wait(1000);
                        }
                    }
                    else if (tempThresholdType == "minimum" && lightThresholdType == "maximum")
                    {
                        tempThreshold = startingTempLevel - tempThresholdValue;
                        lightThreshold = startingLightLevel + lightThresholdValue;
                        for (int i = 0; i < timeToMonitor; i++)
                        {
                            SameLineData($"Current Temperature: {finch.getTemperature()}", 1);
                            SameLineData($"Current Light Level: {GetLightLevel(finch, sensorsToMonitor)}", 3);
                            SameLineData($"Elapsed Time: {i + 1}", 5);
                            if (finch.getTemperature() < tempThreshold || GetLightLevel(finch, sensorsToMonitor) > lightThreshold)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Alert! Temperature has dropped below or light has exceeded threshold!!!");
                                Console.WriteLine("Press Escape to turn alarm off.");
                                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                                {
                                    Alarm(finch, 1046, 125);
                                }
                                break;
                            }
                            finch.wait(1000);
                        }
                    }
                    else if (tempThresholdType == "maximum" && lightThresholdType == "minimum")
                    {
                        tempThreshold = startingTempLevel + tempThresholdValue;
                        lightThreshold = startingLightLevel - lightThresholdValue;
                        for (int i = 0; i < timeToMonitor; i++)
                        {
                            SameLineData($"Current Temperature: {finch.getTemperature()}", 1);
                            SameLineData($"Current Light Level: {GetLightLevel(finch, sensorsToMonitor)}", 3);
                            SameLineData($"Elapsed Time: {i + 1}", 5);
                            if (finch.getTemperature() > tempThreshold || GetLightLevel(finch, sensorsToMonitor) < lightThreshold)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Alert! Temperature has exceeded or light has dropped below threshold!!!");
                                Console.WriteLine("Press Escape to turn alarm off.");
                                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                                {
                                    Alarm(finch, 1046, 125);
                                }
                                break;
                            }
                            finch.wait(1000);
                        }
                    }
                    else
                    {
                        tempThreshold = startingTempLevel - tempThresholdValue;
                        lightThreshold = startingLightLevel - lightThresholdValue;
                        for (int i = 0; i < timeToMonitor; i++)
                        {
                            SameLineData($"Current Temperature: {finch.getTemperature()}", 1);
                            SameLineData($"Current Light Level: {GetLightLevel(finch, sensorsToMonitor)}", 3);
                            SameLineData($"Elapsed Time: {i + 1}", 5);
                            if (finch.getTemperature() < tempThreshold || GetLightLevel(finch, sensorsToMonitor) < lightThreshold)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Alert! Temperature or light has dropped below threshold!!!");
                                Console.WriteLine("Press Escape to turn alarm off.");
                                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                                {
                                    Alarm(finch, 1046, 125);
                                }
                                break;
                            }
                            finch.wait(1000);
                        }
                    }
                    break;
            }

            DisplayContinuePrompt();
        }

        static void UserProgrammingDisplayMenuScreen(Finch finch)
        {
            (int motorSpeed, int ledBrightness, double wait) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.wait = 0;
            string menuChoice;
            bool exiting = false;

            List<FinchCommand> commands = new List<FinchCommand>();

            DisplayHeader("User Programming");
            while (!exiting)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine();

                Console.WriteLine("1. Get Command Parameters");
                Console.WriteLine("2. Get Finch Robot Commands");
                Console.WriteLine("3. Display Finch Robot Command");
                Console.WriteLine("4. Display Execute");
                Console.WriteLine("E. Exit");
                Console.WriteLine();
                Console.Write("Enter Choice:");
                menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        commandParameters = DisplayGetCommandParameters();
                        break;
                    case "2":
                        DisplayGetFinchCommands(commands);
                        break;
                    case "3":
                        DisplayFinchCommands(commands);
                        break;
                    case "4":
                        DisplayExecuteFinchCommands(finch, commands, commandParameters);
                        break;
                    case "e":
                    case "E":
                        exiting = true;
                        break;
                    default:
                        break;
                }
            }
        }

        static void DisplayExecuteFinchCommands(Finch finch, List<FinchCommand> commands, (int motorSpeed, int ledBrightness, double wait) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int wait = (int)(commandParameters.wait * 1000);

            DisplayHeader("Execute Finch Commands");
            Console.WriteLine("Click any key when ready to execute commands.");
            DisplayContinuePrompt();

            foreach (var command in commands)
            {
                Console.WriteLine($"Running command: {command}");

                switch (command)
                {
                    case FinchCommand.NONE:
                        break;
                    case FinchCommand.DONE:
                        break;
                    case FinchCommand.MOVEFORWARD:
                        finch.setMotors(motorSpeed, motorSpeed);
                        break;
                    case FinchCommand.MOVEBACKWARD:
                        finch.setMotors(-motorSpeed, -motorSpeed);
                        break;
                    case FinchCommand.STOPMOTORS:
                        finch.setMotors(0, 0);
                        break;
                    case FinchCommand.DELAY:
                        finch.wait(wait);
                        break;
                    case FinchCommand.TURNRIGHT:
                        finch.setMotors(motorSpeed, -motorSpeed);
                        break;
                    case FinchCommand.TURNLEFT:
                        finch.setMotors(-motorSpeed, motorSpeed);
                        break;
                    case FinchCommand.LEDON:
                        finch.setLED(ledBrightness, ledBrightness, ledBrightness);
                        break;
                    case FinchCommand.LEDOFF:
                        finch.setLED(0, 0, 0);
                        break;
                    case FinchCommand.GETTEMPERATURE:
                        Console.WriteLine($"Current temp: {finch.getTemperature()}");
                        break;
                    case FinchCommand.LIGHTLEVEL:
                        Console.WriteLine($"Current light level: {finch.getLightSensors().Average()}");
                        break;
                    case FinchCommand.RANDOMCOLORANDNOTE:
                        RandomColorAndNote(finch);
                        break;
                    default:
                        break;
                }
            }

            DisplayContinuePrompt();
        }

        static void DisplayGetFinchCommands(List<FinchCommand> commands)
        {
            FinchCommand command = FinchCommand.NONE;

            DisplayHeader("Get Finch Commands");

            Console.WriteLine("Available commands: ");

            foreach (var item in Enum.GetValues(typeof(FinchCommand)))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            Console.WriteLine("Enter a command, or type 'Done' to exit:");
            while (command != FinchCommand.DONE)
            {
                while (!Enum.TryParse(Console.ReadLine().ToUpper(), out command))
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid. Answer must be one of the commands (not case sensitive). Please try again:");
                }
                commands.Add(command);
            }


            Console.WriteLine();
            Console.WriteLine("The commands:");
            foreach (var com in commands)
            {
                Console.WriteLine(com);
            }

            DisplayContinuePrompt();
        }

        static void DisplayFinchCommands(List<FinchCommand> commands)
        {
            DisplayHeader("Finch Commands");

            Console.WriteLine("The commands:");
            foreach (var com in commands)
            {
                Console.WriteLine(com);
            }

            DisplayContinuePrompt();
        }

        static (int motorSpeed, int ledBrightness, double wait) DisplayGetCommandParameters()
        {
            (int motorSpeed, int ledBrightness, double wait) commandParameters;

            DisplayHeader("Get Command Parameters");

            Console.Write("Enter the motor speed [1 - 255]:");
            while (!int.TryParse(Console.ReadLine(), out commandParameters.motorSpeed))
            {
                Console.WriteLine();
                Console.WriteLine("Invalid. Answer must be a number. Please try again:");
            }

            Console.Write("Enter the LED brightness [1 - 255]:");
            while (!int.TryParse(Console.ReadLine(), out commandParameters.ledBrightness))
            {
                Console.WriteLine();
                Console.WriteLine("Invalid. Answer must be a number. Please try again:");
            }

            Console.Write("Enter Length of Delay (seconds):");

            while (!double.TryParse(Console.ReadLine(), out commandParameters.wait))
            {
                Console.WriteLine();
                Console.WriteLine("Invalid. Answer must be a number. Please try again:");
            }

            DisplayContinuePrompt();

            return commandParameters;
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
            for (int i = 1; i < 4; i++)
            {
                Console.WriteLine($"Attempt {i}...");
                if (finch.connect())
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

        static string Capitalize(string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }

        static double GetLightLevel(Finch finch, string sensor)
        {
            switch (sensor)
            {
                case "left":
                    return finch.getLeftLightSensor();
                case "right":
                    return finch.getRightLightSensor();
                default:
                    return Convert.ToInt32(finch.getLightSensors().Average());
            }
        }

        static void SameLineData(string stuff, int top)
        {
            Console.SetCursorPosition(0, top);
            Console.WriteLine(stuff);
        }

        static void Alarm(Finch finch, int frequency, int wait)
        {
            finch.noteOn(frequency);
            finch.wait(wait);
            finch.noteOff();
            finch.wait(wait);
        }

    }
}