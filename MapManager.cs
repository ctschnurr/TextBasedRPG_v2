using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class MapManager
    {
        // map related variables

        private string[] mapData;
        private static List<char> walkables;
        private int mapwidth;
        private int mapheight;
        private static bool redraw = true;
        private static bool gateLocked = true;

        private int windowWidth;
        private int windowHeight;

        // world array to hold map arrays

        private static char[,][,] world;

        // map arrays

        private char[,] center_map;
        private char[,] north_map;
        private char[,] south_map;
        private char[,] east_map;
        private char[,] northeast_map;
        private char[,] witchHut;

        // variables to track which map we are in, which element within world array to read

        private static int worldX;
        private static int worldY;

        // stores map name temporarily to pass into error handler

        public static string mapName;

        // some characters used in making maps:

        // 148 176 ░ 177 ▒ 178 ▓
        // 179 │ 180 ┤ 191 ┐ 192 └ 193 ┴ 194 ┬ 195 ├ 196 ─ 197 ┼ 217 ┘ 218 ┌
        // 185 ╣ 186 ║ 187 ╗ 188 ╝ 200 ╚ 201 ╔ 202 ╩ 203 ╦ 204 ╠ 205 ═ 206 ╬ 207 ╧ 208 ╨ 209 ╤ 210 ╥ 211 ╙ 212 ╘ 213 ╒ 214 ╓ 215 ╫ 216 ╪ 217 ┘ 218 ┌
        // 219 █ 220 ▄ 223 ▀ 254 ■ Ø æ ö

        public MapManager()
        {
            worldX = 1;
            worldY = 1;

            walkables = new List<char> { ' ', '░', '▒', '▀', '▓', (char)1, '┼', '°' };

            // load in world maps from files

            mapData = System.IO.File.ReadAllLines("./Assets/center_map.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            mapName = "center_map";
            center_map = MapEater(mapData, mapheight, mapwidth, mapName);

            mapData = System.IO.File.ReadAllLines("./Assets/north_map.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            mapName = "north_map";
            north_map = MapEater(mapData, mapheight, mapwidth, mapName);

            mapData = System.IO.File.ReadAllLines("./Assets/south_map.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            mapName = "south_map";
            south_map = MapEater(mapData, mapheight, mapwidth, mapName);

            mapData = System.IO.File.ReadAllLines("./Assets/east_map.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            mapName = "east_map";
            east_map = MapEater(mapData, mapheight, mapwidth, mapName);

            mapData = System.IO.File.ReadAllLines("./Assets/northeast_map.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            mapName = "northeast_map";
            northeast_map = MapEater(mapData, mapheight, mapwidth, mapName);

            mapData = System.IO.File.ReadAllLines("./Assets/witchHut.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            mapName = "witchHut";
            witchHut = MapEater(mapData, mapheight, mapwidth, mapName);

            // set up and fill the List of world maps

            world = new char[3,3][,];

            world[1, 1] = center_map;
            world[0, 1] = north_map;
            world[2, 1] = south_map;
            world[1, 2] = east_map;
            world[0, 2] = northeast_map;
            world[2, 2] = witchHut;

            // windowWidth = center_map.GetLength(1) + 3;
            // windowHeight = center_map.GetLength(0) + 2;
        }

        // take the file data and digest it into an array
        public static char[,] MapEater(string[] data, int height, int width, string name)
        {
            char[,] storage = new char[height, width];

            for (int x = 0; x < data.Length; x++)
            {
                int next = 0;
                foreach (char character in data[x])
                {
                    if (data[x].Length != width)
                    {
                        ErrorScreen(data[x], name);
                        break;
                    }

                    storage[x, next] = character;
                    next++;
                }
            }

            return storage;
        }

        // here we draw the map on the screen
        static public void Draw()
        {
            if (redraw == true)
            {
                Console.Clear();
                char[,] map = world[worldY, worldX];

                int mapHeight = map.GetLength(0);
                int mapWidth = map.GetLength(1);

                for (int mapX = 0; mapX < mapHeight; mapX++)
                {
                    Console.SetCursorPosition(2, mapX + 1);
                    for (int mapY = 0; mapY < mapWidth; mapY++)
                    {
                        char tile = map[mapX, mapY];

                        DrawTile(tile);
                    }
                }

                // here we make sure the hud areas are void of color

                Console.SetCursorPosition(3, 40);
                Console.Write("                                         ");
                Console.SetCursorPosition(45, 40);
                Console.Write("                                             ");

                redraw = false;

                Character player = GameManager.GetPlayer();
                Character.Draw(player);
                EnemyManager.Draw();
                ItemManager.Draw();
                WorldManager.Update();
                HUD.Draw(player);
            }
        }


        // holds color info for each tile on the various maps. Set up so different maps can have different color schemes
        // I'll likely build this out to be read from a file too and not hard coded, perhaps even built into the map files?
        public static string[] GetTileColor(char tile)
        {
            string[] instance = new string[2];

            instance[0] = "Gray"; // foreground
            instance[1] = "Black"; // background

            // northeast map, the graveyard

            if ((worldX == 2 && worldY == 0)  || (worldX == 1 && worldY == 0))
            {
                switch (tile)
                {
                    case ' ':
                        instance[1] = "DarkYellow";
                        break;

                    case '▲':
                        instance[0] = "DarkGray";
                        instance[1] = "DarkYellow";
                        break;

                    case '♠':
                        instance[0] = "DarkGreen";
                        instance[1] = "DarkYellow";
                        break;

                    case '■':
                        instance[0] = "Gray";
                        instance[1] = "DarkYellow";
                        break;

                    case '░':
                        instance[0] = "Gray";
                        instance[1] = "DarkYellow";
                        break;

                    case 'ō':
                        instance[0] = "Blue";
                        instance[1] = "DarkYellow";
                        break;

                    case '┼':
                        instance[0] = "Gray";
                        instance[1] = "DarkYellow";
                        break;

                    case '°':
                        instance[0] = "Yellow";
                        instance[1] = "DarkYellow";
                        break;

                    case '▒':
                        instance[0] = "Gray";
                        instance[1] = "DarkYellow";
                        break;

                    case '▀':
                    case '█':
                    case '▄':
                        instance[0] = "DarkGray";
                        instance[1] = "DarkYellow";
                        break;
                }
            }

            // east side map

            if (worldX == 2 && worldY == 1)
            {
                switch (tile)
                {
                    case ' ':
                        instance[1] = "Green";
                        break;

                    case '▲':
                        instance[0] = "DarkGray";
                        instance[1] = "Green";
                        break;

                    case '♠':
                        instance[0] = "DarkGreen";
                        instance[1] = "Green";
                        break;

                    case '▄':
                    case '█':
                    case '▀':
                        instance[0] = "DarkYellow";
                        instance[1] = "Green";
                        break;

                    case '│':
                    case '└':
                    case '┴':
                    case '─':
                    case '┘':
                    case '┌':
                    case '┐':
                        instance[0] = "DarkGray";
                        instance[1] = "Green";
                        break;

                    case '░':
                        instance[1] = "DarkYellow";
                        break;

                    case 'ō':
                        instance[0] = "Blue";
                        instance[1] = "Green";
                        break;

                    case '°':
                        instance[0] = "Yellow";
                        instance[1] = "Green";
                        break;
                }
            }

            // currently this is the scheme for inside the witch's house

            if (worldX == 2 && worldY == 2)
            {
                switch (tile)
                {
                    case ' ':
                        instance[1] = "Black";
                        break;

                    case '▀':
                        instance[0] = "DarkGray";
                        instance[1] = "Green";
                        break;

                    case '▄':
                    case '█':
                        instance[0] = "DarkGray";
                        instance[1] = "Black";
                        break;

                    case '░':
                        instance[1] = "DarkYellow";
                        break;

                    case '☻':
                        instance[0] = "Blue";
                        instance[1] = "DarkYellow";
                        break;
                }
            }

            // scheme for the center map

            if (worldX == 1 && worldY == 1)
            {
                switch (tile)
                {
                    case ' ':
                        instance[1] = "Green";
                        break;

                    case '░':
                        instance[1] = "DarkYellow";
                        break;

                    case '█':
                    case '▄':
                    case '♠':
                        instance[0] = "DarkGreen";
                        instance[1] = "Green";
                        break;

                    case '╨':
                        instance[0] = "DarkYellow";
                        instance[1] = "Green";
                        break;

                    case '▲':
                        instance[0] = "DarkGray";
                        instance[1] = "Green";
                        break;

                    case '─':
                    case '(':
                    case ')':
                        instance[0] = "Black";
                        instance[1] = "DarkYellow";
                        break;

                    case 'ō':
                        instance[0] = "Blue";
                        instance[1] = "Green";
                        break;

                    case '°':
                        instance[0] = "Yellow";
                        instance[1] = "Green";
                        break;

                    default:
                        break;
                }
            }
            return instance;

        }

        public static void DrawTile(char tile)
        {
            string[] tileColor = GetTileColor(tile);

            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), tileColor[0]);
            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), tileColor[1]);

            Console.Write(tile);
            Console.ResetColor();
        }

        public static bool CheckWalkable(char destination)
        {
            bool goTime = false;

            foreach (char walkable in walkables)
            {
                if (destination == walkable)
                {
                    goTime = true;
                }
            }

            return goTime;
        }

        public static char GetTile(int characterY, int characterX)
        {
            char[,] referenceMap = GetWorld();
            char tile = referenceMap[characterY, characterX];
            return tile;
        }

        static void ErrorScreen(string badstring, string badname)
        {
            Console.Clear();
            Console.WriteLine("There was an error loading map " + badname + ". This is the bad line:");
            Console.WriteLine("");
            Console.WriteLine(badstring);
            Console.WriteLine("");
            Console.WriteLine("This line will be omitted and the game may not work correctly. Please notify the developer.");
            Console.ReadKey(true);
            // Program.gameOver = true;
        }

        public static char[,] GetMap()
        {
            return world[worldY, worldX];
        }

        public static void SetRedraw(bool input)
        {
            redraw = input;
        }

        public static bool GetGateLocked()
        {
            return gateLocked;
        }
        public static void SetGateLocked(bool input)
        {
            gateLocked = input;
        }

        public static void AddWalkables(char input)
        {
            walkables.Add(input);
        }

        public static char[,] GetWorld()
        {
            return world[worldY, worldX];
        }

        public static void SetWorld(int X, int Y)
        {
            worldX = X;
            worldY = Y;
        }

        public static int[] GetWorldCoords()
        {
            int[] coords = new int[2];
            coords[0] = worldX;
            coords[1] = worldY;
            return coords;
        }
    }
}
