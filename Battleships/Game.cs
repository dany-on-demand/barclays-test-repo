using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    // Imagine a game of battleships.
    //   The player has to guess the location of the opponent's 'ships' on a 10x10 grid
    //   Ships are one unit wide and 2-4 units long, they may be placed vertically or horizontally
    //   The player asks if a given co-ordinate is a hit or a miss
    //   Once all cells representing a ship are hit - that ship is sunk.
    public class Game
    {
        // ships: each string represents a ship in the form first co-ordinate, last co-ordinate
        //   e.g. "3:2,3:5" is a 4 cell ship horizontally across the 4th row from the 3rd to the 6th column
        // guesses: each string represents the co-ordinate of a guess
        //   e.g. "7:0" - misses the ship above, "3:3" hits it.
        // returns: the number of ships sunk by the set of guesses

        public const uint MAX_GRID_SIZE_X = 10;
        public const uint MAX_GRID_SIZE_Y = 10;

        public static int Play(string[] ships, string[] guesses)
        {
            var grid = new Ship[MAX_GRID_SIZE_X, MAX_GRID_SIZE_Y];

            foreach (string ship_definition in ships)
            {
                var ship = Ship.FromString(ship_definition);

                // iterate over each ship coordinate
                foreach (var coord in ship.Coords)
                {
                    // add the ship _reference_ to the grid
                    grid[coord.X, coord.Y] = ship;
                }
            }

            // track how many ships sunk
            var ships_sunk = 0;

            foreach (string guess in guesses)
            {
                var cell = Coords.FromString(guess);

                var ship = grid[cell.X, cell.Y];
                if (ship != null && !ship.IsSunk())
                {
                    // @Note: kind of needlessly complicated logic here - we could reach into ship.Coords
                    // and do the hit check ourselves here but whatever we're sticking to OOP - Daniel 22.11.2022
                    ship.HitShip(cell);
                    if (ship.IsSunk()) ships_sunk++;
                }
            }

            return ships_sunk;
        }
    }
}
