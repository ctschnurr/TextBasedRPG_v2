using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// todo:

// build item entity/class system

// optimise classes:
    // a class should 'ask' another class for things, not get its info - player should do all its own collision detection, only getting tiles from map 'get tile' etc
    // TODO:
    // BattleSystem
    // Character
    // Enemy
    // HUD
    // MapManager
    // MenuManager
    // Player

// enemy movement method could be cleaner code-wise

// rebuild map data and loading functions to hold tile and color info too?
// make WorldManager that actually draws the game screen, while MapManager just handles map digestion and storage?

// POLYMORPHISM

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
