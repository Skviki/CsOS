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
        string? text = Console.ReadLine();
        string[] tokens = text!.Split();
        switch (tokens[0])
        {
            case "help":
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Super easy");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  dir        (Your current directory where you are located)");
                Console.WriteLine("  date        (Your date now)");
                Console.WriteLine("  clear      (Clear the console)");
                Console.WriteLine("  optimize   (Free up RAM. Not recommended for frequent use)");
                Console.WriteLine("  bondarchuk (Shows you a cute little boy Bondarchuk)");
                Console.WriteLine("  shutdown   (Shut down the OS)");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Easy");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  say |text|             (Output text to the console)");
                Console.WriteLine("  cd |path|              (Change director)");
                Console.WriteLine("  run |path|             (Executes the file located at the specified path)");
                Console.WriteLine("  system os/pc_name/info (Display information about the device)");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Normal");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  file write/read (Write or read a file)");
                break;
            case "say":
                string textsay = string.Join(" ", tokens.Skip(1));
                Console.WriteLine(textsay);
                break;
            case "dir":
                Console.WriteLine(Directory.GetCurrentDirectory());
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
                break;
            case "file":
                switch (tokens[1])
                {
                    case "write":
                        if (tokens[2] == "this")
                        {
                            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "file.txt"), string.Join(" ", tokens.Skip(2)));
                        }
                        else
                        {
                            try
                            {
                                File.WriteAllText(tokens[2],tokens[3]);
                            }
                            catch (Exception e)
                            {
                                Error("Fail to write : " + e);
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
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("  > write |path to file| / this |content_text|");
                        Console.WriteLine("  > read |path to file|");
                        break;
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
            case "optimize":
                long beforeRam = GC.GetTotalMemory(false) / 1024;
                GC.Collect();
                long afterRam = GC.GetTotalMemory(false) / 1024;
                Console.WriteLine($"Optimized RAM : -{beforeRam - afterRam} KB");
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
                        Console.WriteLine($"OS        | {Environment.OSVersion}");
                        Console.WriteLine($"PC        | {Environment.MachineName}");
                        Console.WriteLine($"USER      | {Environment.UserName}");
                        Console.WriteLine($"CPU_COUNT | {Environment.ProcessorCount}");
                        Console.WriteLine($"BIT       | {(Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit")}");
                        break;
                    case "help":
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("  > os");
                        Console.WriteLine("  > pc_name");
                        Console.WriteLine("  > info");
                        break;
                    default:
                        Console.Write("Please specify what exactly you would like to know? (You can use ");
                        Console.ForegroundColor = ConsoleColor.Cyan; 
                        Console.Write("system help");
                        Console.ForegroundColor = ConsoleColor.White; 
                        Console.Write(")"); 
                        Console.WriteLine();
                        break;
                }
                break;
            case "bondarchuk":
                Bondarchuk();
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
                    throw;
                }
                
                break;
            case "" :
                break;
            case "shutdown" :
                return;
        default:
            Error("Invalid command : " + string.Join(" ", tokens));
            break;
    }
    }
}
void Start()
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("   █████████    ███  ███      ███████     █████████ \n  ███▒▒▒▒▒███ ████████████  ███▒▒▒▒▒███  ███▒▒▒▒▒███\n ███     ▒▒▒ ▒▒▒███▒▒███▒  ███     ▒▒███▒███    ▒▒▒ \n▒███          ████████████▒███      ▒███▒▒█████████ \n▒███         ▒▒▒███▒▒███▒ ▒███      ▒███ ▒▒▒▒▒▒▒▒███\n▒▒███     ███  ▒▒▒  ▒▒▒   ▒▒███     ███  ███    ▒███\n ▒▒█████████               ▒▒▒███████▒  ▒▒█████████ \n  ▒▒▒▒▒▒▒▒▒                  ▒▒▒▒▒▒▒     ▒▒▒▒▒▒▒▒▒");
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine(" [-------------------------------------------------------]");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("   Version    | 1.0 [Console]");
    Console.WriteLine($"   User Name  | {Environment.UserName}");
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine(" [-------------------------------------------------------]");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("                        ! Welcome !                       ");
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine(" [-------------------------------------------------------]");
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
    Console.WriteLine(" ! " + textError);
}
void Warring(string textWarring)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(" ! " + textWarring);
}