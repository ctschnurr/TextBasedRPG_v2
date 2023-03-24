using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Door : Interactable
    {
        protected int destinationWorldX = 0;
        protected int destinationWorldY = 0;
        protected int destinationLocalX = 0;
        protected int destinationLocalY = 0;

        protected bool isLocked;
        protected bool playerHasKey;

        public Door()
        {
            type = "door";
            worldX = 0;
            worldY = 0;
            localX = 0;
            localY = 0;

            icon = '▀';
            color = ConsoleColor.DarkGray;
        }

        public int GetDestinationWorldX()
        {
            return destinationWorldX;
        }

        public int GetDestinationWorldY()
        {
            return destinationWorldY;
        }

        public int GetDestinationLocalX()
        {
            return destinationLocalX;
        }

        public int GetDestinationLocalY()
        {
            return destinationLocalY;
        }

        public void SetDestinationWorldX(int x)
        {
            destinationWorldX = x;
        }

        public void SetDestinationWorldY(int y)
        {
           destinationWorldY = y;
        }

        public void SetDestinationLocalX(int x)
        {
            destinationLocalX = x;
        }

        public void SetDestinationLocalY(int y)
        {
            destinationLocalY = y;
        }

        public static void UseDoor(Interactable input)
        {
            List<Door> doors = WorldManager.GetDoors();
            Door reference = null;
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
                    reference = door;

                }
            }

            reference.RunDoor(destinationWorldX, destinationWorldY, destinationLocalX, destinationLocalY);
        }

        public virtual void RunDoor(int WX, int WY, int LX, int LY)
        {
            Player player = GameManager.GetPlayer();

            player.SetWorldX(destinationWorldX);
            player.SetWorldY(destinationWorldY);
            player.SetX(destinationLocalX);
            player.SetY(destinationLocalY);
            MapManager.SetWorld(destinationWorldX, destinationWorldY);
            MapManager.SetRedraw(true);
        }
    }
}
