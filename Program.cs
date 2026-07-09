using System.Diagnostics;

Start();
ComandRequied();

void ComandRequied()
{
    while (true)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(Directory.GetCurrentDirectory());
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Write(" > ");
        Console.ForegroundColor = ConsoleColor.White;
        string? text = Console.ReadLine() + " ";
        string[] tokens = text!.Split();
        TokenAnalize(tokens);
}
void TokenAnalize(string[] tokens)
{
    switch (tokens[0])
    {
        case "help":
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Super easy");
            Help("dir create/delet |folder name|        (Your current directory where you are located)");
            Help("date                                  (Your date now)");
            Help("clear                                 (Clear the console)");
            Help("optimize                              (Free up RAM. Not recommended for frequent use)");
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
            if (tokens[1] != "")
                Warring("The remaining commands have been ignored because this command does not require any arguments. We recommend not wasting your energy on typing extra words ;)");
            break;
        case "say":
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
            break;
        case "dir":
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
            break;
        case "ls":
            switch (tokens[1])
            {
                case  "":
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
            
            break;
        case "cd":
            try
            {
                Directory.SetCurrentDirectory(tokens[1]);
                Good("Current directory : " + Directory.GetCurrentDirectory());
            }
            catch (Exception e)
            {
                Error(e.Message);
            }
            break;
        case "date":
            Console.WriteLine(DateTime.Now);
            Warring($"Currently, the OS version shows incorrect time because the default time zone is set to: {TimeZoneInfo.Local.Id} . This will be fixed in future updates.");
            if (tokens[1] != "")
                Warring("The remaining commands have been ignored because this command does not require any arguments. We recommend not wasting your energy on typing extra words ;)");
            break;
        case "file":
            switch (tokens[1])
            {
                case "write":
                    if (tokens[2] == "this")
                    {
                        File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "file.txt"), string.Join(" ", tokens.Skip(3)));
                        Good("file written" +  Path.Combine(Directory.GetCurrentDirectory(), "file.txt"));
                    }
                    else
                    {
                        try
                        {
                            File.WriteAllText(tokens[2],tokens[3]);
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
            break;
        case "clear":
            Console.Clear();
            if (tokens[1] != "")
                Warring("The remaining commands have been ignored because this command does not require any arguments. We recommend not wasting your energy on typing extra words ;)");
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
        case "optimize":
            long beforeRam = GC.GetTotalMemory(false) / 1024;
            GC.Collect();
            long afterRam = GC.GetTotalMemory(false) / 1024;
            Good($"Optimized RAM : -{beforeRam - afterRam} KB");
            if (tokens[1] != "")
                Warring("The remaining commands have been ignored because this command does not require any arguments. We recommend not wasting your energy on typing extra words ;)");
            break;
        case "system":
            switch (tokens[1])
            { 
                case "os":
                    Console.WriteLine($"Os : {Environment.OSVersion}");
                    break;
                case "pc_name":
                    Console.WriteLine($"Pc name : {Environment.MachineName}");
                    break;
                case "info":
                    DriveInfo driveInfo = new DriveInfo(Environment.CurrentDirectory);
                    Console.WriteLine($"OS         | {Environment.OSVersion}");
                    Console.WriteLine($"PC         | {Environment.MachineName}");
                    Console.WriteLine($"USER       | {Environment.UserName}");
                    Console.WriteLine($"CPU_COUNT  | {Environment.ProcessorCount}");
                    Console.WriteLine($"BIT        | {(Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit")}");
                    double gb = 1024.0 * 1024 * 1024;
                    Console.WriteLine($"Disk Space | {(driveInfo.TotalSize - driveInfo.TotalFreeSpace) / gb:F2}GB / {driveInfo.TotalSize / gb:F2}GB , Free Space {driveInfo.TotalFreeSpace / gb:F2}GB");
                    break;
                case "help":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Help("os");
                    Help("pc_name");
                    Help("disk_space");
                    Help("info");
                    break;
                default:
                    Tip("system help");
                    break;
            }
            break;
        case "bondarchuk":
            Bondarchuk();
            if (tokens[1] != "")
                Warring("The remaining commands have been ignored because this command does not require any arguments. We recommend not wasting your energy on typing extra words ;)");
            break;
        case "run":
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = tokens[1];
                process.StartInfo.Arguments = tokens[2];
                process.Start();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                Error(e.Message);
            }
            break;
        case "run_cfile":
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
            break;
        case "exit":
            Process.GetCurrentProcess().Kill();
            break;
        case "check_update":
            CheckVersion("1.4.2");
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
    }
}
void Start()
{
    string version = "1.4.2 [Beta]";
    
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("  ██████╗ ██╗ ██╗  ██████╗ ███████╗\n ██╔════╝████████╗██╔═══██╗██╔════╝\n ██║     ╚██╔═██╔╝██║   ██║███████╗\n ██║     ████████╗██║   ██║╚════██║\n ╚██████╗╚██╔═██╔╝╚██████╔╝███████║\n  ╚═════╝ ╚═╝ ╚═╝  ╚═════╝ ╚══════╝");
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine(" ┌─────────────┬─────────────────────────────────────────┐");
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write($" │  Version    │ {version}");
    for (int i = 0; i < 40 - version.Length; i++)
    {
        Console.Write(" ");
    }
    Console.Write("│");
    Console.WriteLine();
    Console.Write($" │  User Name  │ {Environment.UserName}");
    for (int i = 0; i < 40 - Environment.UserName.Length; i++)
    {
        Console.Write(" ");
    }
    Console.Write("│");
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine();
    Console.WriteLine(" ├─────────────┴─────────────────────────────────────────┤");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(" │                       ! Welcome !                     │");
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine(" └───────────────────────────────────────────────────────┘");
    Warring("If you have forgotten the commands or you are a beginner, then use the 'help' command.");
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
}
void Warring(string textWarring)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(" [!] " + textWarring);
}
void Good(string textGood)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(" [+] " + textGood);
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

async void CheckVersion(string version)
{
    using HttpClient client = new();
    string versionGet = await client.GetStringAsync("https://github.com/Skviki/CsOS/blob/master/v.txt");
    if (versionGet != version)
    {
        Good($"A new version {versionGet} is available! Current version {version}.");
        Warring("The update feature has not been implemented yet!");
    }
}