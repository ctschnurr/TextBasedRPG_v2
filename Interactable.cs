using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Interactable
    {
        protected string type;
        protected int worldX;
        protected int worldY;
        protected int localX;
        protected int localY;

        protected ConsoleColor color;
        protected char icon;
        protected string name;
        public static void Draw(Interactable subject)
        {
            char[,] map = MapManager.GetMap();
            char tile;
            string[] colorDat;

            // draw the item on screen in set position

            Console.SetCursorPosition(subject.localX + 2, subject.localY + 1);
            tile = map[subject.localY, subject.localX];
            colorDat = MapManager.GetTileColor(tile);
            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorDat[1]);
            Console.ForegroundColor = subject.color;
            Console.Write(subject.icon);
            Console.ResetColor();
        }

        public int GetX()
        {
            return localX;
        }

        public int GetY()
        {
            return localY;
        }

        public void SetX(int x)
        {
            localX = x;
        }

        public void SetY(int y)
        {
            localY = y;
        }

        public int GetWorldX()
        {
            return worldX;
        }

        public int GetWorldY()
        {
            return worldY;
        }

        public void SetWorldY(int inputY)
        {
            worldY = inputY;
        }

        public void SetWorldX(int inputX)
        {
            worldX = inputX;
        }

        public string GetType()
        {
            return type;
        }

        public void SetName(string inputName)
        {
            name = inputName;
        }

        public string GetName()
        {
            return name;
        }

        public void Interact(Interactable input)
        {
            string inputType = input.GetType();

            switch (inputType)
            {
                case "door":
                    Door.UseDoor(input);
                    return;

                case "gate":
                    Gate.UseGate(input);
                    return;

                case "npc":
                    NPC.TalkTo(input);
                    return;
            }
        }
    }
}
