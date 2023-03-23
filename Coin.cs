﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Coin : Item
    {
        public Coin()
        {
            bool isWalkable = false;
            char tile = ' ';

            name = "coin";
            color = ConsoleColor.Yellow;
            icon = '°';

            worldX = 1;
            worldY = 1;

            Random rand = ItemManager.GetRandom();

            while (isWalkable == false)
            {
                localX = rand.Next(50, 70);
                localY = rand.Next(10, 30);

                tile = MapManager.GetTile(localY, localX);
                isWalkable = MapManager.CheckWalkable(tile);
            }
        }
    }
}
