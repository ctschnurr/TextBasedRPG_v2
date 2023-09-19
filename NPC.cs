using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class NPC : Interactable
    {

        string message = " ";
        Gate myGate;
        public NPC()
        {
            type = "npc";
            worldX = 0;
            worldY = 0;
            localX = 0;
            localY = 0;

            icon = '☻';
            color = ConsoleColor.Blue;
        }

        public void SetMyGate(Gate input)
        {
            myGate = input;
        }

        public Gate GetMyGate()
        {
            return myGate;
        }

        public static void TalkTo(Interactable input)
        {
            Player player = GameManager.GetPlayer();
            List<NPC> npcs = WorldManager.GetNPCs();
            NPC reference = null;

            foreach (NPC npc in npcs)
            {
                if (npc == input)
                {
                    reference = npc;
                }
            }
            // checks if the reference is null
            if (reference.GetName() == "witch")
            {
                bool hasKey = reference.myGate.CheckHasKey();
                int gold = player.GetGold();

                if (hasKey == false)
                {
                    if (gold < 100)
                    {
                        reference.message = "\'I'll trade 100 gold for the key!\'";
                        HUD.SetMessage(reference.message);
                    }

                    if (gold >= 100)
                    {
                        reference.message = "\'Here is your key!\'";
                        HUD.SetMessage(reference.message);
                        reference.myGate.SetHasKey(true);
                        Player.AddGold(-100);
                    }
                }

                else if (hasKey)
                {
                    reference.message = "\'You have your key! Now begone!\'";
                    HUD.SetMessage(reference.message);
                }

                player.StepBack();                
            }
            else
            {
                int gold = player.GetGold();
                
                if (gold < 20)
                {
                    reference.message = "\'20 gold for a potion of power!\'";
                    HUD.SetMessage(reference.message);
                }

                if (gold >= 20)
                {
                    reference.message = "\'Here is your potion!\'";
                    HUD.SetMessage(reference.message);
                    Player.AddGold(-20);
                    Player.AddStrength(1);
                }
                player.StepBack();
            }
        }
    }
}
