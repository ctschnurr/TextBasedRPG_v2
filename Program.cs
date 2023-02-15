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
        static public EventManager e_manager = new EventManager();
        static public Enemy enemy = new Enemy();

        // make a multiple enemy system
        //static public Enemy enemyB = new Enemy();
        //static public Enemy enemyC = new Enemy();
        //static public List<Enemy> enemies;

        public static bool gameOver = false;


        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            EventManager.MainMenu();

            while (gameOver == false)
            {
                if (EventManager.redraw)
                {
                    MapManager.DrawMap(EventManager.atlas.overWorld);
                    EventManager.redraw = false;
                }

                player.Draw(player.x + 2, player.y + 1, player.character);
                player.ShowHud();
                enemy.Draw(enemy.x + 2, enemy.y + 1, enemy.character);
                player.Update(EventManager.atlas.overWorld, player);
                EventManager.RefreshWindow();
                // EventManager.HealCheck(player); // build this into EventCheck
                //Battle.BattleCheck(player, enemy); // build this into EventCheck
                enemy.Update(player.x, player.y, EventManager.atlas.overWorld, enemy);
                //Battle.BattleCheck(enemy, player);
            }

        }
    }
}
