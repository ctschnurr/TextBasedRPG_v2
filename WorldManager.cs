using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class WorldManager
    {
        private static List<Interactable> interactables = new List<Interactable>();
        private static List<Door> doors = new List<Door>();
        private static List<Gate> gates = new List<Gate>();
        private static List<NPC> npcs = new List<NPC>();

        public static void ConstructInteractables()
        {
            Door witchDoorIn = new Door();
            witchDoorIn.SetWorldX(2);
            witchDoorIn.SetWorldY(1);
            witchDoorIn.SetX(51);
            witchDoorIn.SetY(22);

            witchDoorIn.SetDestinationWorldX(2);
            witchDoorIn.SetDestinationWorldY(2);
            witchDoorIn.SetDestinationLocalX(42);
            witchDoorIn.SetDestinationLocalY(14);

            doors.Add(witchDoorIn);
            interactables.Add(witchDoorIn);

            Door witchDoorOut = new Door();
            witchDoorOut.SetWorldX(2);
            witchDoorOut.SetWorldY(2);
            witchDoorOut.SetX(42);
            witchDoorOut.SetY(15);

            witchDoorOut.SetDestinationWorldX(2);
            witchDoorOut.SetDestinationWorldY(1);
            witchDoorOut.SetDestinationLocalX(51);
            witchDoorOut.SetDestinationLocalY(23);

            doors.Add(witchDoorOut);
            interactables.Add(witchDoorOut);

            Gate bossGate = new Gate();
            bossGate.SetName("bossGate");
            bossGate.SetIcon('|');
            bossGate.SetWorldX(2);
            bossGate.SetWorldY(0);
            bossGate.SetX(2);
            bossGate.SetY(18);

            gates.Add(bossGate);
            interactables.Add(bossGate);

            NPC witch = new NPC();
            witch.SetName("witch");
            witch.SetWorldX(2);
            witch.SetWorldY(2);
            witch.SetX(43);
            witch.SetY(10);
            witch.SetMyGate(bossGate);

            npcs.Add(witch);
            interactables.Add(witch);

            // Adding a shopkeeper NPC
            NPC shopkeep = new NPC();
            shopkeep.SetName("shopkeep");
            shopkeep.SetWorldX(2);
            shopkeep.SetWorldY(2);
            shopkeep.SetX(41);
            shopkeep.SetY(13);
            shopkeep.SetMyGate(null);

            npcs.Add(shopkeep); 
            interactables.Add(shopkeep);
        }

        public static List<Interactable> GetInteractables()
        {
            return interactables;
        }

        public static List<Door> GetDoors()
        {
            return doors;
        }

        public static List<Gate> GetGates()
        {
            return gates;
        }

        public static List<NPC> GetNPCs()
        {
            return npcs;
        }

        public static void Update()
        {
            int[] worldCoords = MapManager.GetWorldCoords();
            int worldX = worldCoords[0];
            int worldY = worldCoords[1];

            Character player = GameManager.GetPlayer();

            foreach (Interactable interactable in interactables)
            {
                int playerX = player.GetX();
                int playerY = player.GetY();

                int interactableX = interactable.GetX();
                int interactableY = interactable.GetY();

                int interactableWX = interactable.GetWorldX();
                int interactableWY = interactable.GetWorldY();

                bool playerOn = false;

                if (playerX == interactableX && playerY == interactableY) playerOn = true;

                if (worldX == interactableWX && worldY == interactableWY && !playerOn)
                {
                    Interactable.Draw(interactable);
                }
            }




        }
    }
}
