using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class HUD
    {
        private static int messageCountdown;
        private static string messageContent = null;
        private static bool messageNew = false;
        public static void Draw(Character activeCharacter)
        {
            string activeType = activeCharacter.GetCharType();
            Player player = GameManager.GetPlayer();
            int lives = player.GetLives();
            int health = activeCharacter.GetHealth();
            string name = activeCharacter.GetName();

            if (activeType == "player") Console.SetCursorPosition(4, 40);
            if (activeType != "player") Console.SetCursorPosition(46, 40);

            string hudHealth = health.ToString();
            Console.Write(name.PadRight(name.Length + 1) + ": Health: " + hudHealth.PadRight(5));

            if (activeType == "player") Console.Write("Lives: " + lives);

        }

        public static void Update()
        {
            if (messageNew == true)
            {
                messageCountdown = 10;
                messageNew = false;

                Console.SetCursorPosition(45, 40);
                //Console.Write("                                             ");
            }

            if (messageCountdown > 0)
            {
                Console.SetCursorPosition(46, 40);
                Console.Write(messageContent);
                messageCountdown--;
            }

            if (messageCountdown == 0)
            {
                Console.SetCursorPosition(45, 40);
                Console.Write("                                             ");
            }
        }

        public static void SetMessage(string message)
        {
            messageContent = message;
            messageNew = true;
        }
    }
}
