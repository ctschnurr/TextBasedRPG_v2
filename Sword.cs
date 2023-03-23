using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Sword : Item
    {
        public Sword()
        {
            name = "sword";
            color = ConsoleColor.Gray;
            icon = '┼';

            worldX = 2;
            worldY = 0;

            localX = 63;
            localY = 1;
        }
    }
}
