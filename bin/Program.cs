using System;
using Microsoft.Extensions.Configuration;

internal class Program
{
    static bool debug = false;
    static int AccessPromptsShow = 1;
    static int WelcomeScreen = 1;
    static int Speed = 3;
    static int HighContrast = 1;
    static int ActionCompleteShow = 0;
    static int AccessPromptsRarity = 75;
    static int idLength = 12;

    static void Error(string message) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ERROR: " + message);
        Console.ResetColor();
        Environment.Exit(1);
    }

    static void Warn(string message) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("WARNING: " + message);
        Console.ResetColor();
        Console.ReadKey();
    }

    static void CheckBoolean(int value, string name, bool advanced) {
        string type = advanced ? "advanced" : "simple";

        if (value == 0 || value == 1) {
            Console.WriteLine("Setting " + name + " passed: " + type + " (simple) boolean");
            return;
        }

        if (advanced && value == 2) {
            Console.WriteLine("Setting " + name + " passed: " + type + " (advanced) boolean");
            return;
        }

        Error("Invalid value for setting " + name + ": not " + type + " boolean. (Make sure to follow the instructions in the settings file, or replace the file if it is corrupt.)");
    }

    static void CheckInt(int value, string name, bool negative) {
        string type = negative ? "generic" : "positive";

        if (value >= 0) {
            return;
        }

        if (value < 0 && negative == true) {
            return;
        }

        Error("Invalid value for setting " + name + ": not " + type + " int. (Make sure to follow the instructions in the settings file, or replace the file if it is corrupt.)");
    }

    private static void Main(string[] args)
    {
        string configfile = Path.GetFullPath("settings.ini");
        string ver = "0.1.1C";
        Console.WriteLine("Initiating program...");
        if (Array.Exists(args, arg => arg == "--debug"))
        {
            debug = true;
            Console.WriteLine("Debug mode enabled");
        }

        Console.WriteLine("Finding settings file at " + configfile);
        if (CheckFile(configfile)) {
            //#if RELEASE
                var data = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddIniFile(configfile, optional: false, reloadOnChange: true)
                    .Build();

                AccessPromptsShow = Convert.ToInt32(data["General:AccessPromptsShow"]);
                WelcomeScreen = Convert.ToInt32(data["General:WelcomeScreen"]);
                Speed = Convert.ToInt32(data["General:Speed"]);
                HighContrast = Convert.ToInt32(data["General:HighContrast"]);
                ActionCompleteShow = Convert.ToInt32(data["General:ActionCompleteShow"]);
                AccessPromptsRarity = Convert.ToInt32(data["Advanced:AccessPromptsRarity"]);
                idLength = Convert.ToInt32(data["Advanced:idLength"]);
            //#endif
        } else {
            Warn("WARNING: File " + configfile + " does not exist in the current location. (Did you forget the settings file?)");
        }

        Console.WriteLine("Running setting checks...");
        Console.WriteLine("Running generic boolean checks...");

        CheckBoolean(AccessPromptsShow, "General.AccessPromptsShow", false);
        CheckBoolean(WelcomeScreen, "General.WelcomeScreen", false);
        CheckBoolean(HighContrast, "General.HighContrast", false);
        CheckBoolean(ActionCompleteShow, "General.ActionCompleteShow", false);

        CheckInt(Speed, "General.Speed", false);
        CheckInt(AccessPromptsRarity, "Advanced.AccessPromptsRarity", false);
        CheckInt(idLength, "Advanced.idLength", false);

        Console.WriteLine("Loading application...");
        Clear();

        Console.Title = "Hacker Simulator V. " + ver;
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPressHandler);

        if (WelcomeScreen == 1)
        {
            Console.Write("Hacker Simulator\nVersion " + ver + "\n\nTo change settings, open settings.ini at " + configfile + ".\n\nControls\nF11: Fullscreen\nF1: Trigger Access Denied\nF2: Trigger Access Granted\nF3: Trigger Action Complete (the program will immediately stop, so no more keys can be pressed)\nF11: Fullscreen\nF12: Complete current line (only when Fast Mode is off)\nAny key: Hack!\n\nNote: If enabled, access prompts will show randomly, so there is no need to press the trigger keys.\n\nStart hacking!\n");
        }

        Console.ReadKey();
        Console.BackgroundColor = ConsoleColor.Black;
        Random random = new();
        Clear();

        Console.Write("Initiating HACK.exe...");
        while (true)
        {
            Thread.Sleep(1);

            string[] code = [
                "IPAddress.Handle()", 
                "Firewall(x + 2yh == " + random.Next(100, 999) + "x ux)", 
                "if (firewallPort == " + random.Next(1, 65535) + " && firewallAccess == true) {\n  install vir_setup.config.exe\n} HACK.exe.ssh.php", 
                "var access;", 
                "gainAccess.Handle(IPAddress);", 
                "call dir_setup.reg", 
                "process.Process(data + '" + random.Next(1, 65535) + "')", 
                "server.php - HACK.sub.exe", 
                "call pause_action.exe.msi type silentInstall()", 
                "insert data(" + GenerateId() + ");", 
                "data = INFO.json_extractor.exe, serverinfo.json", 
                "HACK.exe.py (execute HACK.exe.py)", 
                "HackPort == use.IPPort(ip." + random.Next(1, 65535) + ")", 
                "try bypass_passcode.cmd {number.Random(000001 / 999999)}; var result == passcode.json", 
                "while ([type silentInstall()] status=(execute) + string[" + random.Next(0, 100) + "]) {\n   try forceInstall (Interaction.User[disabled] startAutomatic = true;\n} keep loop i++;", 
                "Access.Gain(port %HackPort%, Input[serverinfo.json, passcode.json]);", 
                "fun grantAccess();", 
                "convert passcode.json >> passcode.log", 
                "start(force);", 
                "try f(x)=mx+b, x == %HackPort%, b == passcode.log, m == %DATA_RATE%", 
                "DATA_RATE == " + random.Next(0, 100) + ";", 
                "var victimLocation, hackerLocation;",
                "deviceId = " + string.Join(":", Enumerable.Range(0, 4).Select(i => GenerateId()).ToArray()),
            ];

            string delim = "";
            string CurrentLine = "";
            int action = 0;
            int listenForKeyPress = 1;

            if (Speed >= 3)
            {
                CurrentLine = string.Join("\n", Enumerable.Repeat(0, Speed).Select(_ => code[random.Next(code.Length)])); // get 3 lines
            }
            else
            {
                CurrentLine = "\n" + code[random.Next(0, code.Length)]; // get 1 line
            }

            if (Speed == 1)
            {
                delim = " "; // split by spaces
            }
            else if (Speed == 0)
            {
                delim = ""; // no value
            }
            else
            {
                delim = string.Empty; // no value
            }

            action = random.Next(0, AccessPromptsRarity);
            string[] words = Speed == 0 ? CurrentLine.Select(c => c.ToString()).ToArray() : CurrentLine.Split(delim, StringSplitOptions.RemoveEmptyEntries); // if the speed is 0, then split by each character; otherwise split by the delimiter

            for (int i = 0; i < words.Length; i++)
            {
                if (listenForKeyPress == 1)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);


                    if (keyInfo.Key == ConsoleKey.F1)
                    {
                        action = 1;
                        listenForKeyPress = 0;
                    }
                    else if (keyInfo.Key == ConsoleKey.F2)
                    {
                        action = 0;
                        listenForKeyPress = 0;
                    }
                    else if (keyInfo.Key == ConsoleKey.F3)
                    {
                        if (ActionCompleteShow == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\n\nACTION COMPLETE");
                            Console.ForegroundColor = ConsoleColor.DarkGreen;

                            while (true) {/**/}
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.F12 && Speed == 0)
                    {
                        listenForKeyPress = 0;

                    }
                }

                if (i == 0)
                {
                    Console.Write(words[i]);
                }
                else
                {
                    Console.Write(delim + words[i]);
                }
            }

            if (AccessPromptsShow == 1)
            {
                if (action == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\nACCESS DENIED");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                else if (action == 0)
                {

                    if (HighContrast == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;

                    }

                    Console.WriteLine("\n\nACCESS GRANTED");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
            }
        }
    }

    static string? ReadLine(string FilePath, int LineNumber)
    {
        try
        {
            string line = File.ReadLines(FilePath).ElementAtOrDefault(LineNumber - 1)!;
            if (string.IsNullOrEmpty(line))
            {
                Error("The setting at line '" + LineNumber + "' was empty.");
            }
            return line;
        }
        catch (FileNotFoundException)
        {
            Error("File " + FilePath + " does not exist in the current location. (Did you forget the settings file?)");
        }
        catch (Exception ex)
        {
            Error($"An error occurred: " + ex.Message);
        }

        return null;
    }

    static bool CheckFile(string FilePath) {
        try
        {
            string line = File.ReadLines(FilePath).ElementAtOrDefault(0)!;
            return true;
        }
        catch (FileNotFoundException)
        {
            return false;
        }
    }

    static void CancelKeyPressHandler(object? sender, ConsoleCancelEventArgs e)
    {
        e.Cancel = true;
        Console.WriteLine("\nQuitting HACK.exe...");
        Environment.Exit(0);
    }
    
    static void Clear() {
        if (debug) {
            Console.WriteLine(new string('-', Console.WindowWidth));
        } else {
            Console.Clear();
        }
    }

    static string GenerateId() {
        Random random = new();
        char[] digits = new char[idLength];
        digits[0] = (char)('1' + random.Next(9));

        for (int i = 1; i < idLength; i++) {
            digits[i] = (char)('0' + random.Next(10)); 
        }

        return new string(digits);
    }
}