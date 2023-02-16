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
        static char[] walkables;
        int mapwidth;
        int mapheight;
        static bool firstTurn = true;
        
        public char[,] menuFrame;

        public char[,] overWorld_map;
        public char[,] overWorld_data;
        public static char[][,] overWorld;



        public MapManager()
        {
            walkables = new char[] { ' ', '▒', '▀', (char)2, (char)1 };

            mapData = System.IO.File.ReadAllLines("./Assets/menuFrame.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();

            menuFrame = mapEater(mapData, mapheight, mapwidth);

            mapData = System.IO.File.ReadAllLines("./Assets/overWorld.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();

            overWorld_map = mapEater(mapData, mapheight, mapwidth);
            overWorld_data = mapEater(mapData, mapheight, mapwidth);

            overWorld = new char[2][,];

            overWorld[0] = overWorld_map;
            overWorld[1] = overWorld_data;
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

        static public void DrawMap(char[][,] input)
        {
            Console.Clear();
            char[,] map = input[0];
            int mapHeight = map.GetLength(0);
            int mapWidth = map.GetLength(1);

            for (int mapX = 0; mapX < mapHeight; mapX++)
            {
                Console.SetCursorPosition(2, mapX + 1);
                for (int mapY = 0; mapY < mapWidth; mapY++)
                {
                    char tile = map[mapX, mapY];

                    DrawTile(tile);

                    // Console.ResetColor();
                }

            }
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

                    DrawTile(tile);
                }
            }
        }

        public static void DrawTile(char tile)
        {
            switch (tile)
            {
                case '█':
                case '▄':
                    Random rand = new Random();
                    int roll = rand.Next(1, 3);
                    if (roll == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    if (roll == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write(tile);
                    break;
                case '▀':
                    //Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(tile);
                    break;

                case '▒':
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    //Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(tile);
                    break;

                case '~':
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(tile);
                    break;

                case '┼':
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(tile);
                    break;

                default:
                    Console.Write(tile);
                    break;

            }
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

        public static void DrawCharacter(char[][,] input, Character subject)
        {
            char[,] map = input[0];
            char[,] data = input[1];

            if (firstTurn == false)
            {
                // draw over character's last position on screen with the approapriate tile from the reference map
                Console.SetCursorPosition(subject.lastX + 2, subject.lastY + 1);
                char tile = map[subject.lastY, subject.lastX];
                DrawTile(tile);

                // make that same change in the map data
                data[subject.lastY, subject.lastX] = tile;

                // send that to the MapManager, I think?
                input[1] = data;
            }

            //draw the character on screen in set position
            Console.SetCursorPosition(subject.x + 2, subject.y + 1);
            Console.ForegroundColor = subject.color;
            Console.Write(subject.character);
            Console.ResetColor();

            // save their new position in the map data
            data[subject.y, subject.x] = subject.character;

            firstTurn = false;
        }
    }
}
