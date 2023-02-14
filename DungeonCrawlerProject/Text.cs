using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Console;

namespace DungeonCrawlerProject
{
   
    static class Text
    {
        static public void Welcoming()
        {
            // Icarus in ASCII
            ForegroundColor = ConsoleColor.Blue;
            WriteLine("*                                 (                                    \r\n*                                 )\\ )                                 \r\n*                                (()/(            )   (       (        \r\n*                                 /(_))   (    ( /(   )(     ))\\   (   \r\n*                                (_))     )\\   )(_)) (()\\   /((_)  )\\  \r\n*                                |_ _|   ((_) ((_)_   ((_) (_))(  ((_) \r\n*                                 | |   / _|  / _` | | '_| | || | (_-< \r\n*                                |___|  \\__|  \\__,_| |_|    \\_,_| /__/ \r\n*                                                                      ");
            ResetColor();
            WriteLine("");
            WriteLine($"    Welcome! You are Icarus, imprisioned in a tower by the evil king Minos.");
            Write("    Your mission is to find at least ");
            ForegroundColor = ConsoleColor.Blue;
            Write("30 FEATHERS");
            ResetColor();
            WriteLine(" in the rooms of the tower, and escape from the roof!");
            WriteLine("    Good Luck!");
            WriteLine(" ");
            WriteLine("    ..Enter S to continue..");
            while (true)
            {
                var a = ReadLine();
                if (a == "S" || a == "s")
                {
                    Clear();
                    break;
                }
                else
                {
                    WriteLine("Error! please do as instructed :(");
                }
            }
        }
        static public void Guide()
        {
            WriteLine();
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("* Move - Arrow Keys");
            WriteLine("E - Entrance       | X - Exit");
            WriteLine("¶ - Player         | φ - Enemy");
            WriteLine("§ - Treasure chest | ☺ - Shop");
            WriteLine("@ - Roof           | ? - Secret Passage");
            ResetColor();
        }   
    }
    }
