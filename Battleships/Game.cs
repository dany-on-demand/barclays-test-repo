using System;
using System.Collections.Generic;
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
        public static int Play(string[] ships, string[] guesses)
        {
            var grid = new bool[10, 10];

            foreach (string ship_definition in ships)
            {
                var ship = Ship.FromString(ship_definition);

                // iterate over each cell in the ship
                foreach (var cell in ship.Cells)
                {
                    // set the cell to true
                    grid[cell.X, cell.Y] = true;
                }
            }

            // track how many ships sunk
            var ships_sunk = 0;

            foreach (string guess in guesses)
            {
                var cell = Cell.FromString(guess);

                if (grid[cell.X, cell.Y])
                {
                    ships_sunk++;
                }
            }

            return ships_sunk;
        }
    }

    public class Ship {
        public List<Cell> Cells = new List<Cell>();

        public static Ship FromString( string ship_definition )
        {
            var new_ship = new Ship();

            var parts = ship_definition.Split(',');

            var beginning = Cell.FromString(parts[0]);
            var end = Cell.FromString(parts[1]);

            for (int x = beginning.X; x <= end.X; x++)
            {
                for (int y = beginning.Y; y <= end.Y; y++)
                {
                    new_ship.Cells.Add(new Cell(x, y));
                }
            }

            return new_ship;
        }
    }

    public class Cell
    {
        public int X;
        public int Y;

        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Cell FromString(string cell_definition)
        {
            var parts = cell_definition.Split(':');

            var x = int.Parse(parts[0]);
            var y = int.Parse(parts[1]);

            return new Cell(x, y);
        }
    }

}
