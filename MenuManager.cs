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
        private char[,] gameWin;

        private char[,] data;

        private enum Menu
        {
            titleScreen,
            menuFrame,
            instructions,
            pauseMenu,
            gameOver,
            gameWin,
        }

        private static Menu menu;

        private static Player player = GameManager.GetPlayer();

        private static List<Enemy> enemyReferences = null;

        private static string taskMessage = "Explore!";
        private static string questMessage = null;

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

            mapData = System.IO.File.ReadAllLines("./Assets/gameWin.txt");
            mapwidth = mapData[0].Length;
            mapheight = mapData.Count();
            mapName = "gameWin";
            gameWin = MapManager.MapEater(mapData, mapheight, mapwidth, mapName);

            menuList = new List<char[,]>();

            menuList.Add(titleScreen);
            menuList.Add(menuFrame);
            menuList.Add(instructions);
            menuList.Add(pauseMenu);
            menuList.Add(gameOver);
            menuList.Add(gameWin);

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

                    Console.SetCursorPosition(17, 28);
                    Console.Write("Before we begin, please enter your name: ");
                    string name = Console.ReadLine();
                    player.SetName(name);

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
            int health = player.GetHealth();
            int healthMax = player.GetHealthMax();
            string name;
            questMessage = QuestManager.CurrentQuest();

            bool go = false;
            while (go == false)
            {

                menu = Menu.pauseMenu;
                Draw();

                Console.SetCursorPosition(12, 5);
                name = player.GetName();
                Console.Write(name);

                Console.SetCursorPosition(42, 5);
                Console.Write(health + "/" + healthMax);

                Console.SetCursorPosition(13, 7);
                Console.Write(lives);

                Console.SetCursorPosition(40, 7);
                Console.Write(gold);

                Console.SetCursorPosition(12, 9);
                Console.Write(questMessage);

                enemyReferences = EnemyManager.GetRef();

                if (enemyReferences.Count >= 1)
                {
                    healthMax = enemyReferences[0].GetHealthMax();
                    Console.SetCursorPosition(8, 13);
                    name = enemyReferences[0].GetName();
                    Console.Write(name + " - Health: " + healthMax);
                }

                if (enemyReferences.Count >= 2)
                {
                    healthMax = enemyReferences[1].GetHealthMax();
                    Console.SetCursorPosition(8, 14);
                    name = enemyReferences[1].GetName();
                    Console.Write(name + " - Health: " + healthMax);
                }

                if (enemyReferences.Count >= 3)
                {
                    healthMax = enemyReferences[2].GetHealthMax();
                    Console.SetCursorPosition(8, 15);
                    name = enemyReferences[2].GetName();
                    Console.Write(name + " - Health: " + healthMax);
                }
                

                ConsoleKeyInfo choice = Console.ReadKey(true);

                switch (choice.Key)
                {
                    case ConsoleKey.Q:

                        menu = Menu.menuFrame;
                        Draw();

                        Console.SetCursorPosition(6, 4);
                        Console.Write("Really quit?");

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
            bool proceed = false;
            while (!proceed)
            {
                string winState = GameManager.GetWinState();

                if (winState == "quit")
                {
                    proceed = true;
                }

                else
                {
                    if (winState == "death")
                    {
                        menu = Menu.gameOver;
                        Draw();
                    }

                    else if (winState == "winner")
                    {
                        menu = Menu.gameWin;
                        Draw();
                    }

                    ConsoleKeyInfo choice = Console.ReadKey(true);

                    if (choice.Key == ConsoleKey.Escape) proceed = true;
                }
            }                  
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
