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
        public char[,] menuFrame;
        char[,] overWorld;

        public MapManager()
        {
            walkables = new char[] { ' ', '▒', '▀' };

            mapData = System.IO.File.ReadAllLines(@"/Assets/menuFrame.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();

            menuFrame = mapEater(mapData, mapheight, mapwidth);

            mapData = System.IO.File.ReadAllLines(@"/Assets/overWorld.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();

            overWorld = mapEater(mapData, mapheight, mapwidth);
        }

        private char[,] mapEater(string[] data, int height, int width)
        {
            char[,] storage = new char[height, width];

            for (int x = 0; x < data.Length; x++)
            {
                char[] eatmap = data[x].ToCharArray();
                Array.Copy(eatmap, 0, storage, x, eatmap.Length);
            }

            return storage;
        }

        public void DrawMap(char[,] map)
        {
            Console.Clear();
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

        private void DrawTile(char tile)
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
    }
}
