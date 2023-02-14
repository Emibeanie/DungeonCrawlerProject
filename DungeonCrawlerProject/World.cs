using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace DungeonCrawlerProject
{
    class World
    {
        Map map = new();
        public void StartGame()
        {
            Text.Welcoming();
            map.MapUpdater();
            
        }
    }
}
