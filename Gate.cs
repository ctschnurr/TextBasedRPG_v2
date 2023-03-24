using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Gate : Interactable
    {
        bool isLocked;
        bool playerHasKey;

        public Gate()
        {

            type = "gate";
            worldX = 0;
            worldY = 0;
            localX = 0;
            localY = 0;

            icon = '-';
            color = ConsoleColor.DarkGray;

            isLocked = true;
            playerHasKey = false;
        }

        public void SetIcon(char input)
        {
            icon = input;
        }
        public bool CheckHasKey()
        {
            return playerHasKey;
        }

        public void SetHasKey(bool input)
        {
            playerHasKey = input;
        }

        public static void UseGate(Interactable input)
        {
            List<Gate> gates = WorldManager.GetGates();
            Gate reference = null;
            foreach (Gate gate in gates)
            {
                if (gate == input)
                {
                    reference = gate;

                }
            }

            string message = " ";
            Player player = GameManager.GetPlayer();

            if (reference.isLocked && !reference.playerHasKey)
            {
                message = "The gate is locked!";
                HUD.SetMessage(message);
                player.StepBack();
            }

            if (reference.isLocked && reference.playerHasKey)
            {
                message = "You unlocked the gate!";
                HUD.SetMessage(message);
                player.StepBack();
                reference.isLocked = false;
            }
        }
    }
}
