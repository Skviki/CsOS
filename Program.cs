using System.Diagnostics;

namespace CsOS;

internal abstract class Program
{
    public static int consoleWidth = Console.BufferWidth;
    public static int consoleHeight = Console.BufferHeight;
    private static readonly HttpClient Client = new();
    const string Version = "1.4.5";
    private static string[]? _commandOld = ["say","hello!"];

    public static async Task Main()
    {
        await Start();
        ComandRequied();
        void ComandRequied()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(Directory.GetCurrentDirectory());
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(" > ");
                Console.ResetColor();
                string? text = Console.ReadLine();
                string[] tokens = text!.Trim().Split();
                TokenAnalize(tokens);
            }
            void TokenAnalize(string[] tokens)
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
                        Help("say none/good/bad/warring |text|      (Output text to the console)");
                        Help("cd |path|                             (Change director)");
                        Help("ls none/|path|                        (Shows what is inside the current folder or the folder chosen by you)");
                        Help("run_cfile |path|                      (Reads a text file and executes each line as a system command. Empty lines and comments (with #) are ignored.)");
                        Help("run |path| or |ArchLinux command|     (Executes the file located at the specified path)");
                        Help("system os/pc_name/info    (Display information about the device)");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Normal");
                        Help("file write/read |path to file|        (Write or read a file)");
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
                                case "warring":
                                    Warring(string.Join(" ", tokens.Skip(2)));
                                    break;
                                case "help":
                                    Help("|text|         (Simply outputs your text to the console)");
                                    Help("good |text|    (Success message)");
                                    Help("bad |text|     (Error message)");
                                    Help("warring |text| (Warning)");
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
                                        Directory.Delete(Path.Combine(Directory.GetCurrentDirectory(), tokens[2]));
                                        Good("Complete!");
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
                                case "":
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
                        Console.WriteLine(DateTime.Now);
                        Warring($"Currently, the OS version shows incorrect time because the default time zone is set to: {TimeZoneInfo.Local.Id} . This will be fixed in future updates.");
                        break;
                    case "file":
                        if (tokens.Length >= 3)
                        {
                            switch (tokens[1])
                            {
                                case "write":
                                    if (tokens[2] == "this")
                                    {
                                        File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "file.txt"),
                                            string.Join(" ", tokens.Skip(3)));
                                        Good("file written" +
                                             Path.Combine(Directory.GetCurrentDirectory(), "file.txt"));
                                    }
                                    else
                                    {
                                        try
                                        {
                                            File.WriteAllText(tokens[2], tokens[3]);
                                        }
                                        catch (Exception e)
                                        {
                                            Error("Fail to write : " + e.Message);
                                        }
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
                                case "help":
                                    Help("write |path to file| or 'this' |content_text|");
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
                                    DriveInfo driveInfo = new DriveInfo(Environment.CurrentDirectory);
                                    Console.WriteLine($"OS         | {Environment.OSVersion}");
                                    Console.WriteLine($"PC         | {Environment.MachineName}");
                                    Console.WriteLine($"USER       | {Environment.UserName}");
                                    Console.WriteLine($"CPU_COUNT  | {Environment.ProcessorCount}");
                                    Console.WriteLine($"RAM        | {(Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0):F1} GB");
                                    Console.WriteLine($"BIT        | {(Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit")}");
                                    
                                    double gb = 1024.0 * 1024 * 1024;
                                    Console.WriteLine($"Disk Space | {(driveInfo.TotalSize - driveInfo.TotalFreeSpace) / gb:F1} GB  /  {driveInfo.TotalSize / gb:F1} GB , Free Space {driveInfo.TotalFreeSpace / gb:F1} GB");
                                    
                                    string kernelVersion = Environment.OSVersion.Version.ToString();
                                    Console.WriteLine($"Kernel     | {kernelVersion}");
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
                    case "wifi":
                        if (tokens.Length >= 2)
                        {
                            switch (tokens[1])
                            {
                                case "list":
                                    Process.Start("nmcli device wifi list");
                                    break;
                                case "connect":
                                    Process.Start($"nmcli device wifi connect '{tokens[1]}' password '{tokens[2]}'");
                                    break;
                            }
                            try
                            {
                                Process.Start($"nmcli device wifi list nmcli device wifi connect '{tokens[1]}' password '{tokens[2]}'");
                            }
                            catch (Exception e)
                            {
                                Error(e.Message);
                            }
                        }
                        else
                        {
                            Tip("wifi help");
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
                                    TokenAnalize(tokenToAnalize);
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
                    case "check_update":
                       CheckVersion(Version);
                       break;
                    case "^":
                        if (_commandOld![0] != "^")
                        {
                            if (_commandOld != null) 
                                TokenAnalize(_commandOld);
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
            Console.ForegroundColor = ConsoleColor.Magenta;
            for (int i = 0; i < consoleHeight / 2 - 3; i++)
            {
                Console.WriteLine();
            }
            for (int i = 0; i < consoleWidth / 2 - 16; i++)
            {
                Console.Write(" ");
            }
            Console.Write("  ██████╗ ██╗ ██╗  ██████╗ ███████╗");
            Console.WriteLine();
            for (int i = 0; i < consoleWidth / 2 - 16; i++)
            {
                Console.Write(" ");
            }
            Console.Write(" ██╔════╝████████╗██╔═══██╗██╔════╝");
            Console.WriteLine();
            for (int i = 0; i < consoleWidth / 2 - 16; i++)
            {
                Console.Write(" ");
            }
            Console.Write(" ██║     ╚██╔═██╔╝██║   ██║███████╗");
            Console.WriteLine();
            for (int i = 0; i < consoleWidth / 2 - 16; i++)
            {
                Console.Write(" ");
            }
            Console.Write(" ██║     ████████╗██║   ██║╚════██║");
            Console.WriteLine();
            for (int i = 0; i < consoleWidth / 2 - 16; i++)
            {
                Console.Write(" ");
            }
            Console.Write(" ╚██████╗╚██╔═██╔╝╚██████╔╝███████║");
            Console.WriteLine();
            for (int i = 0; i < consoleWidth / 2 - 16; i++)
            {
                Console.Write(" ");
            }
            Console.Write("  ╚═════╝ ╚═╝ ╚═╝  ╚═════╝ ╚══════╝");
            Console.WriteLine();
            for (int i = 0; i < consoleWidth / 2 - 4; i++)
            {
                Console.Write(" ");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("! Welcome !");
            await Task.Delay(2000);
            Console.Clear();
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine($" │  Version    │ {Version}[Beta]");
            Console.WriteLine($" │  User Name  │ {Environment.UserName}");
            Good("Remember, the 'help' command is always there to help!");
            Good("Just a reminder that we're on GitHub: https://github.com/Skviki/CsOS");
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
        void Warring(string textWarring)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" [!] " + textWarring);
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

        async void CheckVersion(string versionCur)
        {
            try
            {
                string versionGet = await Client.GetStringAsync("https://raw.githubusercontent.com/Skviki/CsOS/refs/heads/master/v.txt");
                if (versionGet != versionCur)
                {
                    Good($"A new version {versionGet} is available! Current version {versionCur}.");
                    Warring("The update feature has not been implemented yet!");
                    Warring("Follow the link https://github.com/Skviki/CsOS to download a newer version!");
                }
                else
                {
                    Good("There are no updates");
                }
            }
            catch (Exception e)
            {
                Error(e.Message);
            }
        }
    }
}