using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DungeonCrawlerProject
{

    class Map
    {
        // E - entrance
        // X - exit
        // φ - enemy  
        // ¶ - player
        // § - treasure chest
        // ☺ - shop
        // @ - roof 
        // ? - secret passage

        CombatActor player;
        CombatActor enemyCombat;
        public Map()
        {
            Weapon sword = new("Rusty Sword", 10, 0.80f);
            Weapon greatAxe = new("Great Axe", 10, 0.20f);
            Shield sunShield = new("Sun Shield", 10, 0.50f);
            SpecialAbility skills = new("Hot Wax", 0.60f);
            player = new("Player", 100, sword, sunShield, skills);
            enemyCombat = new("Guard", 10, greatAxe);
        }

        //map files
        public string map1 = "levelMaps\\level1.text.txt";
        public string map2 = "levelMaps\\level2.text.txt";
        public string map3 = "levelMaps\\level3.text.txt";
        public string map4 = "levelMaps\\level4.text.txt";
        public string map5 = "levelMaps\\level5.text.txt";
        public string map6 = "levelMaps\\level6.text.txt";
        public string map7 = "levelMaps\\level7.text.txt";
        public string map8 = "levelMaps\\level8.text.txt";
        public string map9 = "levelMaps\\level9.text.txt";
        public string map10 = "levelMaps\\level10.text.txt";

        string character = "¶";
        public int gridCounter = 1;
        string currentMap;
        static public int featherCount;
        public bool key = false;
        public int goldCount;

        public void MapUpdater() // generate map and move
        {
            currentMap = map1;
            string filePath = currentMap;
            MapFileToArray(filePath);
            RenderMap(mapGrid);
            HUD();
            Text.Guide();
            CharacterPosition(filePath);

            while (true)
            {
                enemyMovment(mapGrid);
                Movement(mapGrid);
                // update the map grid with the new character location
                mapGrid[player.x, player.y] = character;
                // clear the console
                Clear();
                // render the updated map grid
                RenderMap(mapGrid);
                ForegroundColor = ConsoleColor.Cyan;
                HUD();
                Text.Guide();
                // add a delay to slow down the movement, smoother refresh?
                Thread.Sleep(400);
            }
        }
        public void HUD()
        {
            ForegroundColor = ConsoleColor.Cyan;
            if (key == true)
                WriteLine($"Level:{gridCounter}/10 | HP:{player.CurrentHP} | Feathers:{featherCount} | Gold:{goldCount} | Key:1");
            else
                WriteLine($"Level:{gridCounter}/10 | HP:{player.CurrentHP} | Feathers:{featherCount} | Gold:{goldCount}");
            ResetColor();
        }
        public void RenderMap(string[,] map) // prints map with colors
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] == "§" || map[x, y] == "☺" || map[x, y] == "?")
                    {
                        ForegroundColor = ConsoleColor.Yellow;
                        Write(map[x, y]);
                        ResetColor();
                    }
                    else if (map[x, y] == "φ")
                    {
                        ForegroundColor = ConsoleColor.Red;
                        Write(map[x, y]);
                        ResetColor();
                    }
                    else if (map[x, y] == "T")
                    {
                        ForegroundColor = ConsoleColor.Black;
                        Write(map[x, y]);
                        ResetColor();
                    }
                    else if (map[x, y] == "X")
                    {
                        ForegroundColor = ConsoleColor.Green;
                        Write(map[x, y]);
                        ResetColor();
                    }
                    else if (map[x, y] == character)
                    {
                        ForegroundColor = ConsoleColor.Blue;
                        Write(map[x, y]);
                        ResetColor();
                    }
                    else
                        Write(map[x, y]);
                }
                WriteLine();
            }
        }

        string[,] mapGrid;
        public void MapFileToArray(string filePath) // convert map file to 2D array
        {
            string[] lines = File.ReadAllLines(filePath);
            string firstLine = lines[0];
            int rows = lines.Length;
            int colums = firstLine.Length;
            string[,] grid = new string[rows, colums];

            for (int y = 0; y < rows; y++)
            {
                string line = lines[y];
                for (int x = 0; x < colums; x++)
                {
                    char currentChar = line[x];
                    grid[y, x] = currentChar.ToString();
                }

            }
            mapGrid = grid;
        }

        public void CharacterPosition(string filePath) // different starting point on every level and gridCounter reset
        {
            if (filePath == map1)
            {
                player.x = 1;
                player.y = 1;
                gridCounter = 1;
            }
            if (filePath == map2)
            {
                player.x = 1;
                player.y = 1;
                gridCounter = 2;

            }
            if (filePath == map3)
            {
                player.x = 1;
                player.y = 4;
                gridCounter = 3;

            }
            if (filePath == map4)
            {
                player.x = 1;
                player.y = 1;
                gridCounter = 4;

            }
            if (filePath == map5)
            {
                player.x = 1;
                player.y = 1;
                gridCounter = 5;

            }
            if (filePath == map6)
            {
                player.x = 7;
                player.y = 12;
                gridCounter = 6;

            }
            if (filePath == map7)
            {
                player.x = 1;
                player.y = 1;
                gridCounter = 7;

            }
            if (filePath == map8)
            {
                player.x = 3;
                player.y = 1;
                gridCounter = 8;

            }
            if (filePath == map9)
            {
                player.x = 1;
                player.y = 1;
                gridCounter = 9;

            }
            if (filePath == map10)
            {
                player.x = 3;
                player.y = 9;
                gridCounter = 10;

            }
            SetCursorPosition(player.x, player.y);
            ForegroundColor = ConsoleColor.Blue;
            Write(character);
            ResetColor();
        }

        public void Movement(string[,] filePath) // character movment
        {
            
            int prevX = player.x;
            int prevY = player.y;
            ConsoleKeyInfo keyInfo = ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (filePath[player.x - 1, player.y] != "|" && filePath[player.x - 1, player.y] != "-" && filePath[player.x - 1, player.y] != "E")
                    {
                        if (filePath[player.x - 1, player.y] == "?")
                        {
                            if (key == true)
                            {
                                player.x--;
                            }
                        }
                        else
                        player.x--;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (filePath[player.x + 1, player.y] != "|" && filePath[player.x + 1, player.y] != "-" && filePath[player.x + 1, player.y] != "E")
                    {
                        if (filePath[player.x + 1, player.y] == "?")
                        {
                            if(key == true)
                            {
                                player.x++;
                            }
                        }
                        else
                        player.x++;
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (filePath[player.x, player.y - 1] != "|" && filePath[player.x, player.y - 1] != "-" && filePath[player.x, player.y - 1] != "E")
                    {
                        if (filePath[player.x, player.y - 1] == "?")
                        {
                            if(key == true)
                            {
                                player.y--;
                            }
                        }
                        else
                        player.y--;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (filePath[player.x, player.y + 1] != "|" && filePath[player.x, player.y + 1] != "-" && filePath[player.x, player.y + 1] != "E")
                    {
                        if (filePath[player.x, player.y + 1] == "?")
                        {
                            if(key == true)
                            {
                                player.y++;
                            }
                        }
                        else
                        player.y++;
                    }
                    break;
            }

            // clear the previous character position
            mapGrid[prevX, prevY] = " ";

            // check if reached special spot
            if (filePath[player.x, player.y] != " ")
            {
                switch (filePath[player.x, player.y])
                {
                    case "φ": //enemy
                        // scary beep
                        Beep();
                        Clear();
                        Combat.CombatAlgorithm(player, enemyCombat);
                        break;
                    case "X": // exit
                        gridCounter++;

                        switch (gridCounter)
                        {
                            case 1:
                                currentMap = map1;
                                MapFileToArray(currentMap);
                                CharacterPosition(currentMap);
                                break;
                            case 2:
                                currentMap = map2;
                                MapFileToArray(currentMap);
                                CharacterPosition(currentMap);
                                break;
                            case 3:
                                currentMap = map3;
                                MapFileToArray(currentMap);
                                CharacterPosition(currentMap);
                                break;
                            case 4:
                                currentMap = map4;
                                MapFileToArray(currentMap);
                                CharacterPosition(currentMap);
                                break;
                            case 5:
                                currentMap = map5;
                                MapFileToArray(currentMap);
                                CharacterPosition(currentMap);
                                break;
                            case 6:
                                currentMap = map6;
                                MapFileToArray(currentMap);
                                CharacterPosition(currentMap);
                                break;
                            case 7:
                                currentMap = map7;
                                MapFileToArray(currentMap);
                                CharacterPosition(currentMap);
                                break;
                            case 8:
                                currentMap = map8;
                                MapFileToArray(currentMap);
                                CharacterPosition(currentMap);
                                break;
                            case 9:
                                currentMap = map9;
                                MapFileToArray(currentMap);
                                CharacterPosition(currentMap);
                                break;
                            case 10:
                                currentMap = map10;
                                MapFileToArray(currentMap);
                                CharacterPosition(currentMap);
                                break;
                        }
                        break;

                    case "§": //chest
                        Clear();
                        SpecialEvents.TreasureEvent();
                        goldCount += 4;
                        break;

                    case "T": //trap
                        Clear();
                        SpecialEvents.Trap();
                        player.CurrentHP -= 2;
                        break;

                    case "☺": //shop
                        Clear();
                        WriteLine("Wow! You have encountered a bribed guard!");
                        WriteLine("You can choose ONE offer from him:");
                        WriteLine("1 - 1 feather for 4 gold.");
                        WriteLine("2 - 2 feathers for 7 gold.");
                        WriteLine("3 - 1 health potion (20 HP) for 6 gold.");
                        WriteLine("4 - a key for a secret passage, for 10 gold.");
                        ForegroundColor = ConsoleColor.Yellow;
                        WriteLine($"You have {goldCount} gold coins");
                        ResetColor();
                        Write("You choose: ");
                        int selection = int.Parse(ReadLine());
                        switch (selection)
                        {
                            case 1:
                                if (goldCount >= 4)
                                {
                                    goldCount -= 4;
                                    featherCount += 1;
                                    WriteLine($"You have {goldCount} gold coins left");
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    WriteLine("You lied and lost the deal :(");
                                    WriteLine($"You have {goldCount} gold coins left");
                                    Thread.Sleep(2000);
                                }
                                break;
                            case 2:
                                if (goldCount >= 7)
                                {
                                    goldCount -= 7;
                                    featherCount += 2;
                                    WriteLine($"You have {goldCount} gold coins left");
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    WriteLine("You lied and lost the deal :(");
                                    WriteLine($"You have {goldCount} gold coins left");
                                    Thread.Sleep(2000);
                                }
                                break;
                            case 3:
                                if (goldCount >= 6)
                                {
                                    goldCount -= 6;
                                    player.CurrentHP += 20;
                                    if(player.CurrentHP > 100)
                                        player.CurrentHP = 100;
                                    WriteLine($"You have {goldCount} gold coins left");
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    WriteLine("You lied and lost the deal :(");
                                    WriteLine($"You have {goldCount} gold coins left");
                                    Thread.Sleep(2000);
                                }
                                break;
                            case 4:
                                if (goldCount >= 10)
                                {
                                    goldCount -= 10;
                                    key = true;
                                    WriteLine($"You have {goldCount} gold coins left");
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    WriteLine("You lied and lost the deal :(");
                                    WriteLine($"You have {goldCount} gold coins left");
                                    Thread.Sleep(2000);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case "@": // roof - end
                        Clear();
                        if (featherCount >= 30)
                            SpecialEvents.EndingGood();
                        else
                            SpecialEvents.EndingBad();
                        break;
                    default:
                        break;
                }
            }
        }

        public void enemyMovment(string[,] mapGrid)
        {

            // Find the player's position
            int playerRow = 0;
            int playerCol = 0;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (mapGrid[i, j] == "¶")
                    {
                        playerRow = i;
                        playerCol = j;
                        break;
                    }
                }
            }
            // Move the characters represented by φ towards the player when the player is in range of 5
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (mapGrid[i, j] == "φ")
                    {
                        int deltaRow = playerRow - i;
                        int deltaCol = playerCol - j;
                        // Check if the character is within range of 10
                        if (Math.Abs(deltaRow) <= 5 && Math.Abs(deltaCol) <= 5)
                        {
                            // Move the character one step closer to the player
                            if (Math.Abs(deltaRow) > Math.Abs(deltaCol))
                            {
                                if (deltaRow > 0)
                                {
                                    mapGrid[i + 1, j] = "φ";
                                    mapGrid[i, j] = " ";
                                }
                                else
                                {
                                    mapGrid[i - 1, j] = "φ";
                                    mapGrid[i, j] = " ";
                                }
                            }
                            else
                            {
                                if (deltaCol > 0)
                                {
                                    mapGrid[i, j + 1] = "φ";
                                    mapGrid[i, j] = " ";
                                }
                                else
                                {
                                    mapGrid[i, j - 1] = "φ";
                                    mapGrid[i, j] = " ";
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
