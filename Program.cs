using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// todo:

// make WorldManager that actually draws the game screen, while MapManager just handles map digestion and storage?

// global or settings class that stores game data (alleviates hardcoding) - variables could be set as constants

// build item entity/class system < POLYMORPHISM

// Build in more maps and story, improve menu screens and game over

namespace TextBasedRPG_v2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager.GameLoop();
        }        

    }
}
