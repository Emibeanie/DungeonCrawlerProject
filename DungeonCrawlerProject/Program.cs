using DungeonCrawlerProject;
using Microsoft.VisualBasic;
using static System.Console;

class Program
{
    public static void Main(string[] arg)
    {
        CursorVisible = false;
        World world = new();
        world.StartGame();
       
    }
}