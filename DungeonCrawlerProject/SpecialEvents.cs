using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonCrawlerProject
{
    internal class SpecialEvents
    {
        
        static public void Trap()
        {
            while (true)
            {
                WriteLine("Whoops! you encountered a TRAP!");
                ForegroundColor = ConsoleColor.Red;
                WriteLine("You lose 2 HP");
                ResetColor();
                Thread.Sleep(2000);
                break;
            }
        }

        static public void TreasureEvent()
        {
            while (true)
            {
                WriteLine("Wow! You found a treasure chest!");
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine("You got 4 Gold coins!");
                ResetColor();
                Beep();
                Thread.Sleep(300);
                Beep();
                Thread.Sleep(300);
                Beep();
                Thread.Sleep(2000);
                break;
            }
        }

        static public void EndingGood() //to be triggered in END
        { // feather drawing in ascii
            ForegroundColor = ConsoleColor.Blue;
            Write("                                  \r\n                                                                                \r\n                                                  &&*                           \r\n                                              &/      #                         \r\n                                            &         &                         \r\n                                          &           &                         \r\n                                        &             &                         \r\n                                       *      &      ,                          \r\n                                    &    ,  &        & &                        \r\n                                  &  ,  &      (    &  &                        \r\n                                 &         / &         &                        \r\n                               *&      & &&&          &                         \r\n                               &       &&&            &                         \r\n                              &        &             &                          \r\n                              &        &&           &&                          \r\n                           & &        &, &        &  &                          \r\n                           & &        &&#        &  /                           \r\n                           & &      &. (       &    &                           \r\n                           &  %   ,  &.       %    &                            \r\n                           &  &    & &&  ,  %     &                             \r\n                           &   *     %& &       &,                              \r\n                           *         .&&       &                                \r\n                        & & &         &       &    #                            \r\n                          &  &        &      &   .                              \r\n                        &&&&  &  %    &     &  && &                             \r\n                          &        &//&    &     #                              \r\n                            &        & &       /                                \r\n                               &     & &      &                                 \r\n                             &&&&.   & &  *,   &                                \r\n                               &       &       &                                \r\n                              &&     && &&                                      \r\n                                      & &                                       \r\n                                       & (                                      \r\n                                       & &                                      \r\n                                        & &                                     \r\n                                                                                \r\n                                         & &                                    \r\n                                           /&                                   \r\n                                                                                \r\n                                                                                \r\n                                                                    ");
            ResetColor();
            WriteLine();
            WriteLine("Congratulations! You have escaped from the prison and flew to freedom!");
            WriteLine("And the rest is a well known story...");
            WriteLine();
            Write("Press ");
            ForegroundColor = ConsoleColor.Blue;
            Write("S");
            ResetColor();
            WriteLine(" to Start Over!");
            Write("Press ");
            ForegroundColor = ConsoleColor.Red;
            Write("E");
            ResetColor();
            WriteLine(" to Exit");
            var selection = ReadLine();
            switch (selection)
            {
                case "S":
                    Clear();
                    Thread.Sleep(300);
                    World world = new();
                    world.StartGame();
                    break;
                case "s":
                    Clear();
                    Thread.Sleep(300);
                    World world2 = new();
                    world2.StartGame();
                    break;
                case "e":
                    Clear();
                    Environment.Exit(0);
                    break;
                case "E":
                    Clear();
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        static public void EndingBad()
        { // you failed in ascii
            ForegroundColor = ConsoleColor.Red;
            WriteLine("*            )      )              (                (      (            (      \r\n*         ( /(   ( /(              )\\ )     (       )\\ )   )\\ )         )\\ )   \r\n*         )\\())  )\\())      (     (()/(     )\\     (()/(  (()/(   (    (()/(   \r\n*        ((_)\\  ((_)\\       )\\     /(_)) ((((_)(    /(_))  /(_))  )\\    /(_))  \r\n*       __ ((_)   ((_)   _ ((_)   (_))_|  )\\ _ )\\  (_))   (_))   ((_)  (_))_   \r\n*       \\ \\ / /  / _ \\  | | | |   | |_    (_)_\\(_) |_ _|  | |    | __|  |   \\  \r\n*        \\ V /  | (_) | | |_| |   | __|    / _ \\    | |   | |__  | _|   | |) | \r\n*         |_|    \\___/   \\___/    |_|     /_/ \\_\\  |___|  |____| |___|  |___/  \r\n*                                                                         ");
            ResetColor();
            WriteLine("You found the roof, but didnt gather enough feathers to make wings and successfully escape the prison!");
            Write("Press ");
            ForegroundColor = ConsoleColor.Blue;
            Write("S");
            ResetColor();
            WriteLine(" to Start Over!");
            Write("Press ");
            ForegroundColor = ConsoleColor.Red;
            Write("E");
            ResetColor();
            WriteLine(" to Exit");
            var selection = ReadLine();
            switch (selection)
            {
                case "S":
                    Clear();
                    Thread.Sleep(300);
                    World world = new();
                    world.StartGame();
                    break;
                case "s":
                    Clear();
                    Thread.Sleep(300);
                    World world2 = new();
                    world2.StartGame();
                    break;
                case "e":
                    Clear();
                    Environment.Exit(0);
                    break;
                case "E":
                    Clear();
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
        static public void EndingDead() //to be triggered when dead
        { // game over in ascii
            ForegroundColor = ConsoleColor.Red;
            WriteLine("*                                                      *                 )                   (     \r\n*                                 (          (       (  `             ( /(                   )\\ )  \r\n*                                 )\\ )       )\\      )\\))(    (       )\\())   (   (    (    (()/(  \r\n*                                (()/(    ((((_)(   ((_)()\\   )\\     ((_)\\    )\\  )\\   )\\    /(_)) \r\n*                                 /(_))_   )\\ _ )\\  (_()((_) ((_)      ((_)  ((_)((_) ((_)  (_))   \r\n*                                (_)) __|  (_)_\\(_) |  \\/  | | __|    / _ \\  \\ \\ / /  | __| | _ \\  \r\n*                                  | (_ |   / _ \\   | |\\/| | | _|    | (_) |  \\ V /   | _|  |   /  \r\n*                                   \\___|  /_/ \\_\\  |_|  |_| |___|    \\___/    \\_/    |___| |_|_\\  \r\n*                                                                                                  ");
            ResetColor();
            WriteLine();
            WriteLine("Oh man,You DIED! :(");
            Write("Press ");
            ForegroundColor = ConsoleColor.Blue;
            Write("S");
            ResetColor();
            WriteLine(" to Start Over!");
            Write("Press ");
            ForegroundColor = ConsoleColor.Red;
            Write("E");
            ResetColor();
            WriteLine(" to Exit");
            var selection =ReadLine();
            switch (selection)
            {
                case "S":
                    Clear();
                    Thread.Sleep(300);
                    World world = new();
                    world.StartGame();
                    break;
                case "s":
                    Clear();
                    Thread.Sleep(300);
                    World world2 = new();
                    world2.StartGame();
                    break;
                case "e":
                    Clear();
                    Environment.Exit(0);
                    break;
                case "E":
                    Clear();
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
}
