using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{

    // TO DO:
    // Map - finalize map areas
    // Items - 3 items. Potion, Coin, Shield?

    internal class Program
    {

        static public Player player = new Player();
        static public EventManager e_manager = new EventManager();

        public static bool gameOver = false;

        public static int turn = 1;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Enemy.initEnemies();
            MenuManager.MainMenu();

            // game loop

            while (gameOver == false)
            {
                if (EventManager.redraw)
                {
                    MapManager.DrawMap();
                    EventManager.MapMessage();
                    Enemy.DrawEnemies();
                    MapManager.DrawCharacter(player);
                    EventManager.redraw = false;
                }
                
                player.ShowHud();
                player.Update(player);
                Enemy.Check();
                EventManager.RefreshWindow();
                EventManager.MapMessage();
                turn++;
            }
        }        



    }
}
