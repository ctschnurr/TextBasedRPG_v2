using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Gate : Door
    {
        bool isLocked = true;

        public Gate()
        {
            type = "door";
            worldX = 0;
            worldY = 0;
            localX = 0;
            localY = 0;

            icon = '▀';
            color = ConsoleColor.Gray;

            destinationWorldX = 0;
            destinationWorldY = 0;
            destinationLocalX = 0;
            destinationLocalY = 0;

        }

        public override void Interact(Interactable input)
        {
            Player player = GameManager.GetPlayer();

            List<Door> doors = WorldManager.GetDoors();
            int destinationWorldX = 1;
            int destinationWorldY = 1;
            int destinationLocalX = 1;
            int destinationLocalY = 1;

            foreach (Door door in doors)
            {
                if (door == input)
                {
                    destinationWorldX = door.GetDestinationWorldX();
                    destinationWorldY = door.GetDestinationWorldY();
                    destinationLocalX = door.GetDestinationLocalX();
                    destinationLocalY = door.GetDestinationLocalY();
                }
            }

            player.SetWorldX(destinationWorldX);
            player.SetWorldY(destinationWorldY);
            player.SetX(destinationLocalX);
            player.SetY(destinationLocalY);
            MapManager.SetWorld(destinationWorldX, destinationWorldY);
            MapManager.SetRedraw(true);

        }
    }
}
