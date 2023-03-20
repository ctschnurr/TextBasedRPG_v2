using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// todo:

// optimise classes:
// a class should 'ask' another class for things, not get its info - player should do all its own collision detection, only getting tiles from map 'get tile' etc
// TODO:
// Character: ask about if the variables need to be private to adhere to OOP principles
// Player

// enemy movement method could be cleaner code-wise

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
