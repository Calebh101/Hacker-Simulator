internal class Program
{
    private static void Main(string[] args)
    {
        string configfile = Path.GetFullPath("settings.ini");
        string ver = "0.1.1C";
        
        int AccessPromptsShow = 1;
        int WelcomeScreen = 1;
        int FastMode = 0;
        int HighContrast = 1;
        int ActionCompleteShow = 0;
        int AccessPromptsRarity = 75;

#if RELEASE
        AccessPromptsShow = Convert.ToInt32(ReadLine(configfile, 21));
        WelcomeScreen = Convert.ToInt32(ReadLine(configfile, 26));
        FastMode = Convert.ToInt32(ReadLine(configfile, 32));
        HighContrast = Convert.ToInt32(ReadLine(configfile, 37));
        ActionCompleteShow = Convert.ToInt32(ReadLine(configfile, 42));
        AccessPromptsRarity = Convert.ToInt32(ReadLine(configfile, 50));
#endif

        Console.Title = "Hacker Simulator V. " + ver;
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPressHandler);

        if (WelcomeScreen == 1)
        {
            Console.Write("Hacker Simulator\nVersion " + ver + "\n\nTo change settings, open settings.ini at " + configfile + ".\n\nControls\nF11: Fullscreen\nF1: Trigger Access Denied\nF2: Trigger Access Granted\nF3: Trigger Action Complete (the program will immediately stop, so no more keys can be pressed)\nF11: Fullscreen\nF12: Complete current line (only when Fast Mode is off)\nAny key: Hack!\n\nNote: If enabled, access prompts will show randomly, so there is no need to press the trigger keys.\n\nStart hacking!\n");
        }

        Random random = new();

        Console.ReadKey();
        Console.Clear();

        Console.Write("Initiating HACK.exe...");

        for (; ; )
        {
            Thread.Sleep(1);

            string[] code = ["IPAddress.Handle()", "Firewall(x + 2yh == " + random.Next(100, 999) + "x ux)", "if (firewallPort == " + random.Next(1, 65535) + " && firewallAccess == true) {\n  install vir_setup.config.exe\n} HACK.exe.ssh.php", "var access;", "gainAccess.Handle(IPAddress);", "call dir_setup.reg", "process.Process(data + '" + random.Next(1, 65535) + "')", "server.php - HACK.sub.exe", "call pause_action.exe.msi type silentInstall()", "insert data(" + random.Next(10000000, 99999999) + ");", "data = INFO.json_extractor.exe, serverinfo.json", "HACK.exe.py (execute HACK.exe.py)", "HackPort == use.IPPort(ip." + random.Next(1, 65535) + ")", "try bypass_passcode.cmd {number.Random(000001 / 999999)}; var result == passcode.json", "while ([type silentInstall()] status=(execute) + string[" + random.Next(0, 100) + "]) {\n   try forceInstall (Interaction.User[disabled] startAutomatic = true;\n} keep loop i++;", "Access.Gain(port %HackPort%, Input[serverinfo.json, passcode.json]);", "fun grantAccess();", "convert passcode.json >> passcode.log", "start(force);", "try f(x)=mx+b, x == %HackPort%, b == passcode.log, m == %DATA_RATE%", "DATA_RATE == " + random.Next(0, 100) + ";", "var victimLocation, hackerLocation;"];

            string delim = "blank";
            string CurrentLine = "blank";
            int action = 0;
            int listenForKeyPress = 1;

            if (FastMode == 2)
            {
                CurrentLine = "\n" + code[random.Next(0, code.Length)] + "\n" + code[random.Next(0, code.Length)] + "\n" + code[random.Next(0, code.Length)];
            }
            else
            {
                CurrentLine = "\n" + code[random.Next(0, code.Length)];
            }

            if (FastMode == 0)
            {
                delim = " ";
            }
            else
            {
                delim = string.Empty;
            }

            action = random.Next(0, AccessPromptsRarity);

            string[] words = CurrentLine.Split(delim, StringSplitOptions.RemoveEmptyEntries);

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
                    else if (keyInfo.Key == ConsoleKey.F12 && FastMode == 0)
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

    static string ReadLine(string FilePath, int LineNumber)
    {
        try
        {
            string line = File.ReadLines(FilePath).ElementAtOrDefault(LineNumber - 1)!;
            if (string.IsNullOrEmpty(line))
            {
                throw new Exception("The setting at line '" + LineNumber + "' was empty.");
            }
            return line;
        }
        catch (FileNotFoundException)
        {
            throw new Exception("File " + FilePath + " does not exist in the current location. (Did you forget the settings file?)");
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: " + ex.Message);
        }
    }

    static void CancelKeyPressHandler(object? sender, ConsoleCancelEventArgs e)
    {
        e.Cancel = true;
        Console.WriteLine("\nQuitting HACK.exe...");
        Environment.Exit(0);
    }
}