using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class MapManager
    {
        string[] mapData;
        public static List<char> walkables;
        static char[] enemyWalkables;
        int mapwidth;
        int mapheight;
        static bool firstTurn = true;

        public char[,] titleScreen;
        public char[,] menuFrame;
        public char[,] instructions;
        public char[,] pauseMenu;
        public char[,] gameOver;

        public static char[,][,] world;

        public char[,] center_map;
        public char[,] north_map;
        public char[,] south_map;
        public char[,] east_map;
        public char[,] northeast_map;
        public char[,] witchHut;

        public static int worldX;
        public static int worldY;

        public static ConsoleColor tilecolor;

        // 148 176 ░ 177 ▒ 178 ▓
        // 179 │ 180 ┤ 191 ┐ 192 └ 193 ┴ 194 ┬ 195 ├ 196 ─ 197 ┼ 217 ┘ 218 ┌
        // 185 ╣ 186 ║ 187 ╗ 188 ╝ 200 ╚ 201 ╔ 202 ╩ 203 ╦ 204 ╠ 205 ═ 206 ╬ 207 ╧ 208 ╨ 209 ╤ 210 ╥ 211 ╙ 212 ╘ 213 ╒ 214 ╓ 215 ╫ 216 ╪ 217 ┘ 218 ┌
        // 219 █ 220 ▄ 223 ▀ 254 ■ Ø æ
        public MapManager()
        {
            worldX = 1;
            worldY = 1;

            walkables = new List<char> { 'ō', ' ', '░', '▀', '▓', (char)1, '┼', '°' };
            enemyWalkables = new char[] { 'x' };

            // menu related maps

            mapData = System.IO.File.ReadAllLines("./Assets/titleScreen.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            titleScreen = mapEater(mapData, mapheight, mapwidth);

            mapData = System.IO.File.ReadAllLines("./Assets/menuFrame.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            menuFrame = mapEater(mapData, mapheight, mapwidth);

            mapData = System.IO.File.ReadAllLines("./Assets/instructions.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            instructions = mapEater(mapData, mapheight, mapwidth);

            mapData = System.IO.File.ReadAllLines("./Assets/pauseMenu.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            pauseMenu = mapEater(mapData, mapheight, mapwidth);

            mapData = System.IO.File.ReadAllLines("./Assets/gameOver.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            gameOver = mapEater(mapData, mapheight, mapwidth);

            // world maps

            mapData = System.IO.File.ReadAllLines("./Assets/center_map.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            center_map = mapEater(mapData, mapheight, mapwidth);

            mapData = System.IO.File.ReadAllLines("./Assets/north_map.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            north_map = mapEater(mapData, mapheight, mapwidth);

            mapData = System.IO.File.ReadAllLines("./Assets/south_map.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            south_map = mapEater(mapData, mapheight, mapwidth);

            mapData = System.IO.File.ReadAllLines("./Assets/east_map.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            east_map = mapEater(mapData, mapheight, mapwidth);

            mapData = System.IO.File.ReadAllLines("./Assets/northeast_map.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            northeast_map = mapEater(mapData, mapheight, mapwidth);

            mapData = System.IO.File.ReadAllLines("./Assets/witchHut.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            witchHut = mapEater(mapData, mapheight, mapwidth);

            world = new char[3,3][,];

            world[1, 1] = center_map;
            world[0, 1] = north_map;
            world[2, 1] = south_map;
            world[1, 2] = east_map;
            world[0, 2] = northeast_map;
            world[2, 2] = witchHut;

        }

        private char[,] mapEater(string[] data, int height, int width)
        {
            char[,] storage = new char[height, width];

            for (int x = 0; x < data.Length; x++)
            {
                int next = 0;
                foreach (char character in data[x])
                {
                    storage[x, next] = character;
                    next++;
                }
            }

            return storage;
        }

        static public void DrawMap()
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

            Console.SetCursorPosition(3, 40);
            Console.Write("                                         ");
            Console.SetCursorPosition(45, 40);
            Console.Write("                                             ");
        }

        static public void DrawMenu(char[,] input)
        {
            Console.Clear();
            int mapHeight = input.GetLength(0);
            int mapWidth = input.GetLength(1);

            for (int mapX = 0; mapX < mapHeight; mapX++)
            {
                Console.SetCursorPosition(2, mapX + 1);
                for (int mapY = 0; mapY < mapWidth; mapY++)
                {
                    char tile = input[mapX, mapY];

                    Console.Write(tile);
                }
            }
        }

        public static string[] GetTileColor(char tile)
        {
            string[] instance = new string[2];
            // Random rand = new Random();
            // int roll;

            instance[0] = "Gray"; // foreground
            instance[1] = "Black"; // background

            if (worldX == 2 && worldY == 0)
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
                }
            }

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

            if ((worldX == 1 && worldY == 1) || (worldX == 1 && worldY == 0))
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

        public static bool CheckWalkable(char destination, Character walker)
        {
            bool goTime = false;

            foreach (char walkable in walkables)
            {
                if (destination == walkable)
                {
                    goTime = true;
                }
            }

            if (walker.type == "npc")
            {
                foreach (char walkable in enemyWalkables)
                {
                    if (destination == walkable)
                    {
                        goTime = true;
                    }
                }
            }

            return goTime;
        }

        public static void DrawCharacter(Character subject)
        {
            char[,] map = world[worldY, worldX];
            char tile;
            string[] colorDat;

            if (firstTurn == false)
            {
                // draw over character's last position on screen with the approapriate tile from the reference map

                Console.SetCursorPosition(subject.lastX + 2, subject.lastY + 1);
                tile = map[subject.lastY, subject.lastX];
                DrawTile(tile);

            }

            // draw the character on screen in set position

            Console.SetCursorPosition(subject.x + 2, subject.y + 1);
            tile = map[subject.y, subject.x];
            colorDat = GetTileColor(tile);
            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorDat[1]);
            Console.ForegroundColor = subject.color;
            Console.Write(subject.character);
            Console.ResetColor();

            firstTurn = false;
        }
    }
}
