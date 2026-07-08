using System.Diagnostics;

Start();
ComandRequied();
void ComandRequied()
{
    while (true)
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Write("> ");
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
            Help("dir                       (Your current directory where you are located)");
            Help("make_dir |folder name|    (Your current directory where you are located)");
            Help("date                      (Your date now)");
            Help("clear                     (Clear the console)");
            Help("optimize                  (Free up RAM. Not recommended for frequent use)");
            Help("bondarchuk                (Shows you a cute little boy Bondarchuk)");
            Help("shutdown                  (Shut down the OS)");
            Help("go_to_arch                (The command you can use to exit our C#OS shell.)");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Easy");
            Help("say none/good/bad/warring |text|  (Output text to the console)");
            Help("cd |path|                         (Change director)");
            Help("ls none/|path|                    (Shows what is inside the current folder or the folder chosen by you)");
            Help("run_csfile |path|                 (Reads a text file and executes each line as a system command. Empty lines and comments (with #) are ignored.)");
            Help("run |path| or |ArchLinux command| (Executes the file located at the specified path)");
            Help("system os/pc_name/info            (Display information about the device)");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Normal");
            Help("file write/read (Write or read a file)");
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
                default:
                    Console.WriteLine(string.Join(" ", tokens.Skip(1)));
                    break;
            }
            break;
        case "dir":
            Console.WriteLine(Directory.GetCurrentDirectory());
            if (tokens[1] != "")
                Warring("The remaining commands have been ignored because this command does not require any arguments. We recommend not wasting your energy on typing extra words ;)");
            break;
        case "make_dir":
            Directory.CreateDirectory(tokens[1]);
            Console.WriteLine(Path.Combine(Directory.GetCurrentDirectory(), tokens[1]));
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
                Console.WriteLine("Current directory : " + Directory.GetCurrentDirectory());
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Directory not found!");
            }
            break;
        case "date":
            Console.WriteLine(DateTime.Now);
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
                    catch (Exception)
                    {
                        Error("Not found file");
                    }
                    break;
                case "help":
                    Help("write |path to file| or this |content_text|");
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
            Console.WriteLine($"Optimized RAM : -{beforeRam - afterRam} KB");
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
                case "disk_space":
                    DriveInfo diskSpace = new DriveInfo(Environment.CurrentDirectory);
                    Console.WriteLine($"Disk Space | {(diskSpace.TotalSize - diskSpace.TotalFreeSpace) / 1024 / 1024 / 1024}GB / {diskSpace.TotalSize / 1024 / 1024 / 1024}GB");
                    Good($"{diskSpace.TotalFreeSpace / 1024 / 1024 / 1024}GB Free Space");
                    break;
                case "info":
                    DriveInfo driveInfo = new DriveInfo(Environment.CurrentDirectory);
                    Console.WriteLine($"OS         | {Environment.OSVersion}");
                    Console.WriteLine($"PC         | {Environment.MachineName}");
                    Console.WriteLine($"USER       | {Environment.UserName}");
                    Console.WriteLine($"CPU_COUNT  | {Environment.ProcessorCount}");
                    Console.WriteLine($"BIT        | {(Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit")}");
                    Console.WriteLine($"Disk Space | {(driveInfo.TotalSize - driveInfo.TotalFreeSpace) / 1024 / 1024 / 1024}GB / {driveInfo.TotalSize / 1024 / 1024 / 1024}GB , Free Space {driveInfo.TotalFreeSpace / 1024 / 1024 / 1024}GB");
                    break;
                case "help":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Help("os");
                    Help("pc_name");
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
                Process.Start(tokens[1]);
            }
            catch (Exception e)
            {
                Error("Unable to start this process");
                Warring(e.Message);
            }
            break;
        case "run_csfile":
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
                Error("Failed to start this process");
                Warring(e.Message);
            }
            break;
        case "go_to_arch":
            return;
        case "#" :
            break;
        case "" :
            break;
        case "shutdown" :
            Process.Start("poweroff -f");
            break;
    default:
        Error("Invalid command : " + string.Join(" ", tokens));
        break;
        }
    }
}
void Start()
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("  ██████╗ ██╗ ██╗  ██████╗ ███████╗\n ██╔════╝████████╗██╔═══██╗██╔════╝\n ██║     ╚██╔═██╔╝██║   ██║███████╗\n ██║     ████████╗██║   ██║╚════██║\n ╚██████╗╚██╔═██╔╝╚██████╔╝███████║\n  ╚═════╝ ╚═╝ ╚═╝  ╚═════╝ ╚══════╝");
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine(" ┌─────────────┬─────────────────────────────────────────┐");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine(" │  Version    │ 1.4                                     │");
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