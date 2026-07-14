using System.Diagnostics;

namespace CsOS;

internal abstract class Program
{
    public static int ConsoleWidth = Console.BufferWidth;
    public static int ConsoleHeight = Console.BufferHeight;
    const string Version = "1.4.7";
    private static string[]? _commandOld = ["say","warning","No commands yet!"];

    public static async Task Main()
    {
        await Start();
        await ComandRequied();
        async Task ComandRequied()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(Directory.GetCurrentDirectory());
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(" > ");
                Console.ResetColor();
                string[] tokens = InputKey()!.Trim().Split();
                await TokenAnalyze(tokens);
            }
            string InputKey()
            {
                int startX = Console.CursorLeft;
                string text = "";
                while (true)
                {
                    ConsoleKeyInfo input = Console.ReadKey(true);
                    if (input.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        break;
                    }
                    
                    if (input.Key == ConsoleKey.UpArrow)
                    {
                        text += string.Join(" ",_commandOld!);
                        Console.Write(string.Join(" ",_commandOld!));
                        continue;
                    }

                    if (input.Key == ConsoleKey.LeftArrow)
                    {
                        if (Console.CursorLeft > startX)
                        {
                            Console.CursorLeft--;
                        }
                        continue;
                    }
                    
                    if (input.Key == ConsoleKey.RightArrow)
                    {
                        if (Console.CursorLeft < startX + text.Length)
                        {
                            Console.CursorLeft++;
                        }
                        continue;
                    }
                    
                    if (input.Key == ConsoleKey.Backspace)
                    {
                        if (text.Length > 0)
                        {
                            text = text.Remove(text.Length - 1);
                            Console.Write("\b \b");
                        }
                        continue;
                    }
                    
                    text += input.KeyChar;
                    Console.Write(input.KeyChar);
                }
                return text;
            }
            
            async Task TokenAnalyze(string[] tokens)
            {
                switch (tokens[0])
                {
                    case "help":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Super easy");
                        Help("date                                  (Your date now)");
                        Help("clear                                 (Clear the console)");
                        Help("bondarchuk                            (Shows you a cute little boy Bondarchuk)");
                        Help("shutdown                              (Shut down the OS)");
                        Help("exit                                  (The command you can use to exit our C#OS shell.)");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("Easy");
                        Help("say none/good/bad/warning |text|      (Output text to the console)");
                        Help("cd |path|                             (Change directory)");
                        Help("wait |time|                           (Wait for a certain amount of time, where 1000 = 1 second)");
                        Help("ls this/|path|                        (Shows what is inside the current folder or the folder chosen by you)");
                        Help("run_cfile |path|                      (Reads a text file and executes each line as a system command. Empty lines and comments (with #) are ignored.)");
                        Help("run |ArchLinux command|               (Executes the specified Arch Linux command)");
                        Help("system pc/info                        (Display information about the device)");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Normal");
                        Help("file write/read/delete |content_text|        (Write or read a file)");
                        Help("dir create/delet |folder name|        (Create or delete a folder in the current directory)");
                        break;
                    case "say":
                        if (tokens.Length >= 2)
                        {
                            switch (tokens[1])
                            {
                                case "good":
                                    Good(string.Join(" ", tokens.Skip(2)));
                                    break;
                                case "bad":
                                    Error(string.Join(" ", tokens.Skip(2)));
                                    break;
                                case "warning":
                                    Warning(string.Join(" ", tokens.Skip(2)));
                                    break;
                                case "help":
                                    Help("|text|         (Simply outputs your text to the console)");
                                    Help("good |text|    (Success message)");
                                    Help("bad |text|     (Error message)");
                                    Help("Warning |text| (Warning)");
                                    break;
                                default:
                                    Console.WriteLine(string.Join(" ", tokens.Skip(1)));
                                    break;
                            }
                        }
                        break;
                    case "dir":
                        if (tokens.Length >= 2)
                        {
                            switch (tokens[1])
                            {
                                case "create":
                                    Directory.CreateDirectory(tokens[2]);
                                    Good(Path.Combine(Directory.GetCurrentDirectory(), tokens[2]));
                                    break;
                                case "delete":
                                    try
                                    {
                                        if (Directory.GetFiles(tokens[2]).Length == 0)
                                        {
                                            Directory.Delete(Path.Combine(Directory.GetCurrentDirectory(), tokens[2]));
                                            Good("Complete!");
                                        }
                                        else
                                        {
                                            Warning("This directory contains files inside. Do you confirm the deletion? This will also irreversibly delete the files inside this folder!");
                                            switch(QuestionYesOrNo())
                                            {
                                                case "yes":
                                                    Directory.Delete(Path.Combine(Directory.GetCurrentDirectory(), tokens[2]), true);
                                                    Good("Complete!");
                                                    break;
                                                case "no":
                                                    Warning("Canceled");
                                                    break;
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Error(e.Message);
                                    }
                                    break;
                                case "help":
                                    Help("create |folder name| (Create a folder in the current directory)");
                                    Help("delete |folder name| (Delete a folder in the current directory.)");
                                    break;
                                default:
                                    Tip("dir help");
                                    break;
                            }
                        }
                        else
                        {
                            Tip("dir help");
                        }
                        break;
                    case "ls":
                        if (tokens.Length >= 2)
                        {
                            switch (tokens[1])
                            {
                                case "this":
                                    string[] directories = Directory.GetDirectories(Directory.GetCurrentDirectory());
                                    string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    for (int i = 0; i < directories.Length; i++)
                                    {
                                        Console.WriteLine($"  ^ {directories[i]}");
                                    }

                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    for (int i = 0; i < files.Length; i++)
                                    {
                                        Console.WriteLine($"  > {files[i]}");
                                    }

                                    break;
                                default:
                                    try
                                    {
                                        string[] directoriesUser = Directory.GetDirectories(tokens[1]);
                                        string[] filesUser = Directory.GetFiles(tokens[1]);
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        for (int i = 0; i < directoriesUser.Length; i++)
                                        {
                                            Console.WriteLine($"  ^ {directoriesUser[i]}");
                                        }

                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        for (int i = 0; i < filesUser.Length; i++)
                                        {
                                            Console.WriteLine($"  > {filesUser[i]}");
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Error(e.Message);
                                    }
                                    break;
                            }
                        }
                
                        break;
                    case "cd":
                        if (tokens.Length >= 2)
                        {
                            try
                            {
                                Directory.SetCurrentDirectory(tokens[1]);
                                Good("Current directory : " + Directory.GetCurrentDirectory());
                            }
                            catch (Exception e)
                            {
                                Error(e.Message);
                            }
                        }
                        else
                        {
                            Tip("cd help");
                        }
                        break;
                    case "date":
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));
                        break;
                    case "file":
                        if (tokens.Length >= 3)
                        {
                            switch (tokens[1])
                            {
                                case "write":
                                    try
                                    {
                                        File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "file.txt"), string.Join(" ", tokens.Skip(2)));
                                        Good("file written : " + Path.Combine(Directory.GetCurrentDirectory(), "file.txt"));
                                    }
                                    catch (Exception e)
                                    {
                                        Error(e.Message);
                                    }
                                    break;
                                case "read":
                                    try
                                    {
                                        Console.WriteLine(File.ReadAllText(tokens[2]));
                                    }
                                    catch (Exception e)
                                    {
                                        Error(e.Message);
                                    }
                                    break;
                                case "delete":
                                    try
                                    {
                                        File.Delete(tokens[2]);
                                        Good("File deleted");
                                    }
                                    catch (Exception e)
                                    {
                                        Error(e.Message);
                                    }
                                    break;
                                case "help":
                                    Help("write |content_text|");
                                    Help("read |path to file|");
                                    break;
                                default:
                                    Tip("file help");
                                    break;
                            }
                        }
                        else
                        {
                            Tip("file help");
                        }
                        break;
                    case "clear":
                        Console.Clear();
                        break;
                    case "test_tokens":
                        for (int i = 0; i < tokens.Length; i++)
                        {
                            if (i == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.WriteLine(i + " | " +tokens[i]);
                        }
                        break;
                    case "system":
                        if (tokens.Length >= 2)
                        {
                            switch (tokens[1])
                            {
                                case "pc":
                                    Console.WriteLine($"Os : {Environment.OSVersion}");
                                    Console.WriteLine($"Pc name : {Environment.MachineName}");
                                    Console.WriteLine($"User : {Environment.UserName}");
                                    Console.WriteLine($"Bit : {(Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit")}");
                                    break;
                                case "info":
                                    Console.WriteLine($"OS         | {Environment.OSVersion}");
                                    Console.WriteLine($"PC         | {Environment.MachineName}");
                                    Console.WriteLine($"USER       | {Environment.UserName}");
                                    Console.WriteLine($"CPU_COUNT  | {Environment.ProcessorCount}");
                                    Console.WriteLine($"RAM        | {(Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0):F1} GB");
                                    Console.WriteLine($"BIT        | {(Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit")}");
                                    Console.WriteLine($"Kernel     | {Environment.OSVersion.Version.ToString()}");
                                    break;
                                case "help":
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Help("pc");
                                    Help("info");
                                    break;
                                default:
                                    Tip("system help");
                                    break;
                            }
                        }
                        else
                        {
                            Tip("system help");
                        }
                        break;
                    case "bondarchuk":
                        Bondarchuk();
                        break;
                    case "run":
                        if (tokens.Length >= 2)
                        {
                            try
                            {
                                Process process = new Process();
                                process.StartInfo.FileName = tokens[1];
                                process.Start();
                                process.WaitForExit();
                            }
                            catch (Exception e)
                            {
                                Error(e.Message);
                            }
                        }
                        break;
                    case "run_cfile":
                        if (tokens.Length >= 2)
                        {
                            try
                            {
                                string[] tokenAnalize = File.ReadAllLines(tokens[1]);
                                for (int i = 0; i < tokenAnalize.Length; i++)
                                {
                                    string[] tokenToAnalize = tokenAnalize[i].Split(' ');
                                    await TokenAnalyze(tokenToAnalize);
                                }
                            }
                            catch (Exception e)
                            {
                                Error(e.Message);
                            }
                        }
                        break;
                    case "exit":
                        Process.GetCurrentProcess().Kill();
                        break;
                    case "^":
                        if (_commandOld![0] != "^")
                        {
                            if (_commandOld != null) 
                                await TokenAnalyze(_commandOld);
                        }
                        break;
                    case "wait":
                        if (tokens.Length == 2)
                        {
                            try
                            {
                                await Task.Delay(Convert.ToInt32(tokens[1]));
                            }
                            catch (Exception e)
                            {
                                Error(e.Message);
                            }
                        }
                        break;
                    case "#":
                        break;
                    case "":
                        break;
                    case "shutdown":
                        Process.Start("poweroff");
                        break;
                    default:
                        Error("The system does not recognize this command : " + string.Join(" ", tokens));
                        break;
                }
                if (tokens[0] != "^")
                {
                    _commandOld = tokens;
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        
        async Task Start()
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Magenta;
            for (int i = 0; i < ConsoleHeight / 2 - 5; i++)
            {
                Console.WriteLine();
            }
            for (int i = 0; i < ConsoleWidth / 2 - 16; i++)
            {
                Console.Write(" ");
            }
            Console.Write("  ██████╗ ██╗ ██╗  ██████╗ ███████╗");
            Console.WriteLine();
            for (int i = 0; i < ConsoleWidth / 2 - 16; i++)
            {
                Console.Write(" ");
            }
            Console.Write(" ██╔════╝████████╗██╔═══██╗██╔════╝");
            Console.WriteLine();
            for (int i = 0; i < ConsoleWidth / 2 - 16; i++)
            {
                Console.Write(" ");
            }
            Console.Write(" ██║     ╚██╔═██╔╝██║   ██║███████╗");
            Console.WriteLine();
            for (int i = 0; i < ConsoleWidth / 2 - 16; i++)
            {
                Console.Write(" ");
            }
            Console.Write(" ██║     ████████╗██║   ██║╚════██║");
            Console.WriteLine();
            for (int i = 0; i < ConsoleWidth / 2 - 16; i++)
            {
                Console.Write(" ");
            }
            Console.Write(" ╚██████╗╚██╔═██╔╝╚██████╔╝███████║");
            Console.WriteLine();
            for (int i = 0; i < ConsoleWidth / 2 - 16; i++)
            {
                Console.Write(" ");
            }
            Console.Write("  ╚═════╝ ╚═╝ ╚═╝  ╚═════╝ ╚══════╝");
            Console.WriteLine();
            for (int i = 0; i < (ConsoleWidth / 2 - Version.Length / 2) + 1; i++)
            {
                Console.Write(" ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Version);
            Console.WriteLine();
            for (int i = 0; i < ConsoleWidth / 2 - 4; i++)
            {
                Console.Write(" ");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            await Task.Delay(1500);
            Console.WriteLine("! Welcome !");
            await Task.Delay(1500);
            Console.Clear();
            Good("Remember, the 'help' command is always there to help!");
            Good("Just a reminder that we're on GitHub: https://github.com/Skviki/CsOS");
            Console.CursorVisible = true;
        }

        void Bondarchuk()
        {
            string[] ascii =
            [
                "█████████████▓▓▓▓▓█▓▓▓█▓▓▓▓███████████████▓▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▓▓▓▓▓▓",
                "████████████████████▓▓▓█████████████████▓▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▓▓▓▓▓",
                "█████████████████████▓▓███████▓▓▓▓████▓▒░░░░░░░░░░░░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░░░░░░░░░░░░░░░░░▒▒▓▓▓",
                "█████████████████████████████▓▓▓▓▓▓▓▓▒░░░░░░░░░░░░▒▒▓▓▓▓▓▓████████████████▓▓▓▓▓▓▓▒▒▒▒░░░░░░░░░░░▒▓▓▓",
                "███████████████████████████▓▓▓▓▓▓▓▓▓▒░░░░░░░░░▒▒▓▓▓▓▓▓▓▓█████████████████████▓▓▓▓▓▓▓▓▓▒▒▒░░░░░░░░▒▓▓",
                "███████████████████████████████▓▓▓▓▒░░░░░░░░░▒▒▓▓▓▓▓▓▓▓██████████████████████▓▓▓▓▓▓▓▓▓▓▓▓▒░░░░░░░░▓▓",
                "█████████████████████████████▓▓▓▓▒░░░░░░░░░░▒▒▓▓▓▓▓▓▓▓▓▓▓███████████████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒░░░░░░▒▓",
                "████████████████████████████▓▓▓▓▒░░░░░░░░░░▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓██████████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒▒░░░░░▒▓",
                "█▓▓▓█████████████████████████▓▓▓▒░░░░░░░░░░▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓██████████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒▒▒░░░░░▓",
                "▓▓███▓██████████████████████████▓░░░░░░░░░▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██████████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒▒▒▒▒░░░░▒",
                "█▓▓█████████████████████████████▓▒░░░░░░░░▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒▒▒▒░░░░▒",
                "██████████▓▓▓▓▓██████████████████▓░░░░░░░▒▒▓▓▓▓▓▓▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒░░░▓",
                "█████████▓▓▓▓▓▓▓▓████████████████▓░░░░░░░▒▓▓▓▓▓▓▓▓▒▒▒▒▒▒▒▒▒░░░░░░▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓▒▒▒▒░░░░░░▒▒▒▒▒░░░▓",
                "████████▓▓▓▓▓▓▓██████████████████▓░░░░░░▒▒▓▓▓▓▓▓▓▓▒▓▓▓▓▒▒▒▒▒▒░░░░░▒▒▒▒▒▒▓▓▓▓▓▓▓▒▒▒░░░░░░░░░░▒▒▒░░░▒▓",
                "███████████▓▓▓▓▓▓▓▓███████████████▒░░░░░▒▒▓▓▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░░░░▒▒▒▒▒▓▓▓▓▒▒▒▒▒░░░░░░░░▒▒▒▒▒▒░░▒▓▓",
                "█████████▓▓▓▓▓▒▒▒▓▓▓█████████████▓▒░░░░░▒▓▓▓▓▓▓▒▒▒▒▒▒▒▒▒▓▒▒▒▒▒░░░░░░▒▒▒▒▓▓▓▒▒▒▒░░░░░░░░░░▒▒▒▒▒▒░▒▓▓▓",
                "█████████▓▓▓▓▓▒▓▓▓▓██████████████▓░░░░░░▒▓▓▓▓▓▓▓▓▒▒▒▒▒▒▒░░░░░░▒░░░░░░▒▒▓▓▓▓▒▒░░░░░░░░░░░░▒▒▒▒▒▒▒▓▓▓▓",
                "████████████████▓████▓▓▓█████████▓▒▒▒░░▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒▒▒▒▒▒▒▒░░▒▓▓▓██▓▓▒░░░░░░░░░░░░░░▒▒▒▒▓▓▓▓▓",
                "█████████████▓▓▓▓▓██▓▓▓▓█████████▓▒▒▒░░▒▓▓▓▓▓▓████▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓██████▓▓▒░░░░░░▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓",
                "███████████████████▓▓▓▓██████████▓▓▒▒░░▒▓▓▓▓▓▓████████▓▓▓▒▒▒▒▒▒▓████████████▓▓▒░░░░░░▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓",
                "█████████████████████▓▓███████▓▓▓▓▓▓▒▒▒▒▓▓▓▓▓▓▓█████████████████████████████▓▓▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓",
                "█████████████████████████████▓▓▓▓▓▓▓▓▓▒▒▒▓▓▓▓▓▓▓█████████████████████████████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
                "████████████████████████████▓▓▓▓▓▓▓▓▓▓▒▒▒▒▓▓▓▓▓▓▓███████████▓▓▓▓█████████████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
                "▓█████████████████████████████▓▓▓▓▓▓▓▓▓▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
                "▓███████████████████████████▓▓▓▓▓▒▒▒▓▓▒▓▓▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▒▒▒▒▓▓▒▒▒░░░▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓▒▒▒▓▓▓▓▓▓▓",
                "████████████████████████████▓▓▓▓▓▒▓▓▓▓██▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▒▒░░░░░░▒▒▒▒░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓",
                "█▓▓▓▓▓██████████████▓█████████████▓████▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▒▒░░░░░░░░░░░▒▒▒▒▓▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓",
                "▓▓▓██▓▓█████████████████████████▓▓▓▓▓█▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▒▒░░░░░░░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓",
                "██▓▓▓█████████████████████████████████▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░▒▒▒▒▒▒▒▒▒▒░░░░░░░░░░░▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓▓",
                "███▓██████▓▓▓▓▓▓▓██████████████████████▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓▒▒▒░░░░░░░░░░░░░▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓▓▓",
                "█████████▓▓▓▓▓▓▓▓▓██████████▓█████████████▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▒▒▒▒▒▒▒▒░░░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓",
                "█████████▓▓▓▓▓▓▓▓▓██████████████████████▓▓░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓",
                "██████████▓▓▓▓▓▓▓▓▓▓█████████████▓█████▓░░░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓",
                "█████████▓▓▓▓▒▒▒▒▓▓▓███████████████▓███▒░░░░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░▒▒▓▓▓▓▓▓▓▓▓▓▓▓",
                "▓▓████████▓▓▓▓▓▓▓▓█████▓████████████▓▓▓░░░░░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒░░░░▒▓▓▓▓▓▓▓▓▓▓▓▓▓",
                "██▓███████████▓▓█▓██▓▓▓▓▓████▓▓▓▓▒▒▒▒▒░░░░░░░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░░░░░░░▒▓▓▓▓▓▓▓▓▓▓▓",
                "████▓▓█████████▓▓█▓▓▓▓▓▓▓▓▒▒▒▒▒░░░░░░░░░░░░░░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░░░░░░░░░░░░░▒▒▓▓▓▓▓",
                "███████▓██████▓▓▓▓▓▒▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░░░░░░░░░░░░░░░░▒▒▒▓",
                "█████████▓▓▓▓▒▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▒▒▒▒▒▒▒▒▒▒▒▒░░░░░░░░░░░░▒▒▒▒░░░░░░░░░░░░░░░░░░░░░░",
                "████████▓▓▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▒▒▒▒▒▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░",
                "▓▓████▓▓▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▓▒░░░░░░░░░░░░░▒▒▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░",
                "▓▓██▓▓▒▒░░░░░░░░░░░░░░░░░░░░░▒▒▒▒▒▓▓████▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░",
                "▓▓█▓▒▒░░░▒░░░░░░░░░░░░░░░▒▒▓█████████████▓▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▒▒▒░░░░░░░░░░░░",
                "█▓▓▒░░░░░▒░░░░░░░░░▒▒▒▒▓███████████████████▓▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░░░",
                "▓▓▓░░░░░░░░░░░▒▒▒▓▓▓█████████████████████████▓▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▒",
                "█▓░░░░░░░░░▒▒▓▓█████████████████████████████████▓▓▓▒▒▒▒░░░░░░░░░░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓",
                "▓▒░░░░░░▒▓▓▓█████████████████████████████████████▓▓▓▓▓▓▒░░░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓",
                "▒░░░░░▒▓████████████████████████████████████████▓▓▓▓▓▓▓▒░░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓"
            ];

            foreach (string line in ascii) Console.WriteLine(line);
        }

        void Error(string textError)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" [x] " + textError);
            Console.ResetColor();
        }
        void Warning(string textWarning)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" [!] " + textWarning);
            Console.ResetColor();
        }
        void Good(string textGood)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" [+] " + textGood);
            Console.ResetColor();
        }
        void Help(string textHelp)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("  > " + textHelp);
        }
        void Tip(string textTip)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("  [?] Something went wrong. Try using ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(textTip);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" - it will show you which arguments are supported.");
            Console.WriteLine();
        }

        string QuestionYesOrNo()
        {
            Console.Write("[ yes / no ] : ");
            string answer = "none";
            try
            {
                answer = Console.ReadLine()!.ToLower();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (answer != "yes" && answer != "no")
            {
                return "no";
            }
            else
            {
                return answer;
            }
        }
    }
}