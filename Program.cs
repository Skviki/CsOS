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
                Console.WriteLine("  system os/pc_name/info (Display information about the device.)");
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
            case "" :
                break;
            case "shutdown" :
                return;
        default:
            Error("Invalid command");
            break;
    }
    }
}
void Start()
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("    #########           ###    ###       #########         ###########   ");
    Console.WriteLine(" ######   #####         ##     ##      #####   ######    #####     ##### ");
    Console.WriteLine("#####       ####   ################   ####       #####   ####            ");
    Console.WriteLine("#####                 ###     ##     #####       #####    ##########     ");
    Console.WriteLine("#####                 ##     ##      #####       #####         ######### ");
    Console.WriteLine("#####       ####  #################   ####       #####              #####");
    Console.WriteLine(" ######   ######     ##     ##         #####   ######   ######     ##### ");
    Console.WriteLine("    #########       ###    ###           #########         ###########   ");
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine("Version | 1.0 [Console]");
    Console.WriteLine("Welcome!");
    Warring("If you have forgotten the commands or you are a beginner, then use the 'help' command.");
}

void Bondarchuk()
{
    string[] ascii =
    [
        "^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^```````^^^````````````````````````'``'``''''''''''",
    "ppahbbkhoahhhhhkkkwdhkkkkkpppbpdqmJLmpdbdppdbbbqppddY?i>:''.^\"\"\",,,\"^^^````^\",,`'''''''''``?UOZZZZZZ",
    "bqwdbkabqwwqdkhhhkkkqdkkkkkbbddpm0OwdpddpddddddddwX~^^'''''',::\"^''''''\",\"^'.'''''''.'.'''`^|COZZZZZ",
    "dddbkkbpwmZOOmpkkkkhhkppbkkkbbbbbdmwpbbbpZZZwdbp(^````^\"^^,>](ttjjncvunxrxrrr/}+i;:\"'''...'``<UOZZOZ",
    "bbbkkkbbdqZZwdpbhhkkkkkkpqkkbkbbbbbbbbbdZ0QQQQr\"```^\";>}/nLOmwpdkho**M#*ohkdpqqwZOLYu/]i\"'.''`+UOOOZ",
    "kkkkkkbdOLUzvvzLwdbbbbbbbkbqbbbbbkbbbbbdpqm0O\\\"`^\"\",l]jJL0ZZwqpbha*###oahkdpqqpwwmZO0LCXf~\"'''\"(Q0CU",
    "bkkkbbbpwUYUvJOqdbbdbbbbbbbbbwqbbbbbbbbpOCc)^```^^\"<)nYL00ZZmwqppbbkkkdpqwwwmmZZZOOQLCJXzx{;''`IzLJY",
    "hppbbbkkkkbdbbqdpqmmmpbbbbbkkkbdqbbkkbbbmYn_'````^I[nCQ0OOOZZZwqqpbkbdpqmmOZOOO00QQLJUXcnr/)I'`;xO0Y",
    "kkbbqbbkkbbbbbbdm0mppwwbbkhkkkkbkdqbbbbbbbq|`''`^\">jJQ0OOOOZZmwwwdbbbdpqwmZO0QQQQQLLCJXcnrjf)I``?ccf",
    "bbbbkkppbbbbkdpbbdwwdbkkdwwmpbbbbbbdqpbbbbkw:`''`:}nYCQ0ZZOJcxft////jvJCO0O0QQQLLCJUzcccnxrjt?\"`[OZZ",
    "bbbbbkkkdqbbbbbbbbbbbbbdwOQQQ0wdbkkkbbpdbbkd>'``^<tcUJLLvrttt\\)?~>!!>+{ruzJLLQLJYur(?<!II>]\\\\]'`jZmZ",
    "mdbkkkkkkkkqpbbbbbkbkkbdqqZZmqppbbkbbbbbdqkk)'``^_uUJCYuxxxuxf/(}-~<<~_[1|rzXXvj(?+>i!ii~_--?!`1ZZmm",
    "qpdpdbkbkbbbbwwbkbkkkkkbmQUvrjxYZpbbbbbbbbbpji``^}UCCJcf\\\\({?(J0Cv(!l!<+?{tuvx([_>i!l!i<+-]}}!\\mmZmZ",
    "qmmwqpbddddbbbbdqdkkkkkbpQzcccOwdddddbbbbbdZ-]<:,xCQOOQLu/11[_<;``+-l;Ii_/CQCn[!:\"^``^;!--]}1xZwwmmO",
    "mZqqwmwppdbbbbbbbbppkkbbbbbppbwppwCQmdbbdddZu/~:lvULOmppqmLut|)11}}11{)nOdh*hO1I,,,I!<~~<+[{(QwwwmmZ",
    "ppOZpdpdpqqppdbbbbbbdqbkbbbkbddbq0OpdppdbbbpY)<i+uzC0wpddddbbOj1}}{Ybkbbka#MoqCt~!!>~+_-][1\\tQmmZZZm",
    "pppppdddmZQLQQZpbbbbbbbppkbbbddbbbqpbbbbwZOZCqUc(xuXLZmpbha*#MM##**oohkkha*MokZUCQ0QJzvunxxxr0OZZmmm",
    "qppdbbdpwZOLOwppkbbbbbbbbdqbbbbbbbbbkddpm0QQLuCu/jruXC0mqbkhhahkdq0cObhho*MM*kwQLQZwwqmZ0CXvuZmZQCJJ",
    "pdbbbbbdpOUYnjrvUZbbbbbbbbkbqqdbbbbkkkkbddZCCQQQufxxxvXCL0OO0QQCXftjXJUXzcccnxrfuzcYCCCCYXunUwOQLCJQ",
    "OqdbbddddmzvcrCZwdbddbkkbbbddpwdbbbkkkbw0zujtrzQqUxjrjjrjrrrrjft/tJLc+>i<<?[[-~<_|rrrrjf\\/\\jmwwZQYxj",
    "pdwZddddbddpqpbmdpmQQwbdbdddbbbkqwbbbbkbkkbbppdpqOf/\\\\||)111{}}}1uQQ0t~~<<<~~++__(vx/|1)11)OwwmOXnrx",
    "ddddpOqddbdbbbdddwQ0pppppddbbbbbkbbwpbbbbbpwOOZwOpn(())(1{}[]?+<{cC0LLX)-~<<<~~+_-?]][}}{{CmwwwwmZO0",
    "qddddddqmpdbddppdbdwwpdppm000ZpbbbbbbqpbbbbbdppdwmOf1111111{}]_?fuuccXY|~!lll!iil!~__?[{1YqqmmmmmmmZ",
    "qdddddddddZwdbbbdddddbdqqZLQQZmpbkbbbbbdmpbbbbbddpqJ}1{}}}}}})\\uXXvj|)1{}-+~+_-]]]_-?[{1QwwwwwZmZZZZ",
    "uJwppdddddddpOqpddbkbbbbbpm0UzzXQpbbbbbbbbqqdpdppf''`[)1}}}}1)tjrxxrf(}]_____?]]][[][}/JOZmqwwwmZZZO",
    "CmppqqpddddppppmwdbbbbbbqJcxf/nXOddpdbbbbbdddZdpc....~][}11}{)\\fxuzYXXzx1]???][[[[[[_i[fX0mmmmwmmZZO",
    "wpZ0L0wpppqppdbbdpZdbbbbbbbkbbwdpwmmmwbbdppqpwOz;....;_]]]}1)11|/xuczXXurj\\}}}}}}]!'..!LZmmO0ZmZZOmm",
    "qqOYOqmmwqppddddbdbdmwddbbbbdqwpZ00ZQYnj{~llll;\"^`'`^,+?]]]][[))))))11{{{{}{{{}]?\"    ...;l+xCQOZmmm",
    "wppw0wqqqq0LLLQwdddddddZqdbpmQX\\+!!!!l!iii!I;:;,^\"\"^^^i_----????--][[[]]]--___-__,'...''.'''''^![uYL",
    "qqwwwqpqmZQLCLQmqdddddddqz~i><>ii!II;;Il!iiilll;^^\"\"```I<-_-___+++~~~~~~+~~++_+i'.  ........''.'``^\"",
    "wwqpppppdpmOLvnuvYwppdU)~<>>><<<<<>>iilI;:;!iiIuQ\"\"^```'^l<+__+++~~<<<<<~<<<I^. .. .......'''''```^^",
    "QmqppppppZufft}nvQqpw1+<~~<<<>><<<>ii><\\Yqk*#&WMMki,^`''``\"!<~+~~~+~~<>!:'....... ..;1_'...'..'..''^",
    "qqm0qppppppppppwZpwQ,>><_+<<~+~~~~?xmahkkka*MWWWWWWb-\"^`'''';!>~~~~<i;'..........~1(||()){_+_~:^^\":;",
    "qqqqqOOqppppppqZqmm^`><<++~~+fQd*#*o*MWMMMMohoo#B%8@&o0)I`''`,!~~~<,.'.''.`;?|rr\\((((|(|(\\\\tfxvXLZz{",
    "ZwqqqqqwOmqpppqwqw<`^l>>~nOa*##MMMMMhkoMWWW&W%@B8B@MoWMkZQLUXr^``\"rrxxxxxrrrrrrrrxx\\((((|\\fruXULmppp",
    "0wqqqqqqqqw0wqppp|```lna*ooooo***#MWWMhaa#B@@@$$$@B$@MdwZQJUU\\:_`|nrrrjrrrrxrxnxuvuun/\\/tjrvzYUJC0mw",
    "nrUmwwwqwqqqqZOwz`''`[kpoaaoaao*##*o##*8@@Moho#8B$$$@@@@&hmQL]l;)xrxxxnnunuuuuvvuvvczzXXYnxuvccXCZqk",
    "^\"\"\"\"\"\"\"\",,,,,,,::::::::::::;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;IIIIIIIIIIIllIIIlllllllllllllllll"
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