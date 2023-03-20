using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class MenuManager
    {
        private string[] mapData;
        private int mapwidth;
        private int mapheight;
        private string mapName;

        private static List<char[,]> menuList;

        private char[,] titleScreen;
        private char[,] menuFrame;
        private char[,] instructions;
        private char[,] pauseMenu;
        private char[,] gameOver;

        private char[,] data;

        private enum Menu
        {
            titleScreen,
            menuFrame,
            instructions,
            pauseMenu,
            gameOver,
        }

        private static Menu menu;

        private static Player player = GameManager.GetPlayer();

        private static List<Enemy> enemyReferences = null;

        private static string taskMessage = "Explore!";

        public MenuManager()
        {
            // load in menu related maps from files

            mapData = System.IO.File.ReadAllLines("./Assets/titleScreen.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            mapName = "titleScreen";
            titleScreen = MapManager.MapEater(mapData, mapheight, mapwidth, mapName);

            mapData = System.IO.File.ReadAllLines("./Assets/menuFrame.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            mapName = "menuFrame";
            menuFrame = MapManager.MapEater(mapData, mapheight, mapwidth, mapName);

            mapData = System.IO.File.ReadAllLines("./Assets/instructions.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            mapName = "instructions";
            instructions = MapManager.MapEater(mapData, mapheight, mapwidth, mapName);

            mapData = System.IO.File.ReadAllLines("./Assets/pauseMenu.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            mapName = "pauseMenu";
            pauseMenu = MapManager.MapEater(mapData, mapheight, mapwidth, mapName);

            mapData = System.IO.File.ReadAllLines("./Assets/gameOver.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            mapName = "gameOver";
            gameOver = MapManager.MapEater(mapData, mapheight, mapwidth, mapName);

            menuList = new List<char[,]>();

            menuList.Add(titleScreen);
            menuList.Add(menuFrame);
            menuList.Add(instructions);
            menuList.Add(pauseMenu);
            menuList.Add(gameOver);

            data = titleScreen;


        }
        public static void MainMenu()
        {
            menu = Menu.titleScreen;
            Draw();

            ConsoleKeyInfo choice = Console.ReadKey(true);

            switch (choice.Key)
            {
                default:
                    menu = Menu.instructions;
                    Draw();

                    Console.SetCursorPosition(6, 17);
                    Console.Write("Before we begin, please enter your name: ");
                    player.name = Console.ReadLine();

                    MapManager.SetRedraw(true);
                    break;

                case ConsoleKey.Q:
                    GameManager.SetGameOver();
                    break;
            }
        }

        // this shows the pause / stats menu when escape is pressed
        public static void PauseMenu()
        {
            int lives = player.GetLives();
            int gold = player.GetGold();

            bool go = false;
            while (go == false)
            {

                menu = Menu.pauseMenu;
                Draw();

                Console.SetCursorPosition(12, 5);
                Console.Write(player.name);

                Console.SetCursorPosition(42, 5);
                Console.Write(player.health + "/" + player.healthMax);

                Console.SetCursorPosition(13, 7);
                Console.Write(lives);

                Console.SetCursorPosition(40, 7);
                Console.Write(gold);

                Console.SetCursorPosition(12, 9);
                Console.Write(taskMessage);

                enemyReferences = EnemyManager.GetRef();

                if (enemyReferences.Count >= 1)
                {
                    Console.SetCursorPosition(8, 13);
                    Console.Write(enemyReferences[0].name + " - Health: " + enemyReferences[0].healthMax);
                }

                if (enemyReferences.Count >= 2)
                {
                    Console.SetCursorPosition(8, 14);
                    Console.Write(enemyReferences[1].name + " - Health: " + enemyReferences[1].healthMax);
                }

                if (enemyReferences.Count >= 3)
                {
                    Console.SetCursorPosition(8, 15);
                    Console.Write(enemyReferences[2].name + " - Health: " + enemyReferences[2].healthMax);
                }
                

                ConsoleKeyInfo choice = Console.ReadKey(true);

                switch (choice.Key)
                {
                    case ConsoleKey.Q:

                        menu = Menu.menuFrame;
                        Draw();

                        Console.SetCursorPosition(6, 4);
                        Console.Write("Really quit? Save is not yet implimented.");

                        Console.SetCursorPosition(4, 40);
                        Console.Write("(C)ontinue                    (Q)uit");

                        ConsoleKeyInfo choice2 = Console.ReadKey(true);

                        if (choice2.Key == ConsoleKey.Q)
                        {
                            GameManager.SetGameOver();
                            go = true;
                        }
                        break;

                    case ConsoleKey.Escape:
                        go = true;
                        break;
                }

            }

        }

        // this displays the Game Over screen when lives reach 0, and sets the gameOver trigger
        public static void GameOver()
        {
            menu = Menu.gameOver;
            Draw();

            Console.ReadKey(true);

            GameManager.SetGameOver();          
        }

        public static void SetTaskMessage(string message)
        {
            taskMessage = message;
        }

        static public void Draw()
        {
            int reference = Convert.ToInt32(menu);
            char[,] data = menuList[reference];

            Console.Clear();
            Console.SetWindowSize(data.GetLength(1) + 3, data.GetLength(0) + 2);
            int mapHeight = data.GetLength(0);
            int mapWidth = data.GetLength(1);

            for (int mapX = 0; mapX < mapHeight; mapX++)
            {
                Console.SetCursorPosition(2, mapX + 1);
                for (int mapY = 0; mapY < mapWidth; mapY++)
                {
                    char tile = data[mapX, mapY];

                    Console.Write(tile);
                }
            }
        }

        static public void SetMenu(string input)
        {
            menu = (Menu)Enum.Parse(typeof(Menu), input);
        }
    }
}
