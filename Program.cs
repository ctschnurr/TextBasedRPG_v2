using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// todo:

// build Game Manager class
// build item entity/class system
// build Enemy Manager class
// reorganize methods and classes, player should draw itself, HUD should draw itself, character draw should be in Character class - regular draw, optimized draw
    // a class should 'ask' another class for things, not get its info - player should do all its own collision detection, only getting tiles from map 'get tile' etc
// move redraw function out of Program/GameManager and into its own class
// enemy movement method could be cleaner code-wise

// rebuild map data and loading functions to hold tile and color info too?
// make WorldManager that actually draws the game screen, while MapManager just handles map digestion and storage?

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
