using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Program
    {

        static public Player player = new Player();

        public static bool gameOver = false;

        public static int turn = 1;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Enemy.initEnemies();
            MenuManager.MainMenu();

            // Game Manager / Game Loop:

            while (gameOver == false)
            {
                // this redraws the map and characters when redraw is true
                if (MapManager.redraw)
                {
                    MapManager.DrawMap();
                    EventManager.MapMessage();
                    Enemy.DrawEnemies();
                    MapManager.DrawCharacter(player);
                    MapManager.redraw = false;
                }

                // this is the primary game loop essentials
                player.ShowHud();
                player.Update(player);
                Enemy.UpdateEnemies(); // testing
                Enemy.Check();
                EventManager.RefreshWindow();
                EventManager.MapMessage();
                turn++;
            }
        }        

    }
}
