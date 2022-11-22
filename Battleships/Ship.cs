using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{

    public class Ship
    {
        public List<ShipCoords> Coords = new List<ShipCoords>();

        public static Ship FromString(string ship_definition)
        {
            var new_ship = new Ship();

            var parts = ship_definition.Split(',');

            var beginning = Battleships.Coords.FromString(parts[0]);
            var end = Battleships.Coords.FromString(parts[1]);

            // bounds & diagonal check @Prettify - Daniel 22.11.2022
            if (!ShipIsValid(beginning, end))
            {
                throw new InvalidDataException();
            }

            for (int x = beginning.X; x <= end.X; x++)
            {
                for (int y = beginning.Y; y <= end.Y; y++)
                {
                    new_ship.Coords.Add(new ShipCoords(x, y));
                }
            }

            return new_ship;
        }

        private static bool ShipIsValid(Coords beginning, Coords end)
        {
            var isInvalid = beginning.X != end.X && beginning.Y != end.Y;
            isInvalid = isInvalid || (beginning.X < 0 || beginning.X > Game.MAX_GRID_SIZE_X - 1);
            isInvalid = isInvalid || (beginning.Y < 0 || beginning.Y > Game.MAX_GRID_SIZE_Y - 1);
            isInvalid = isInvalid || (end.X < 0 || end.X > Game.MAX_GRID_SIZE_X - 1);
            isInvalid = isInvalid || (end.Y < 0 || end.Y > Game.MAX_GRID_SIZE_Y - 1);


            // + 1 because the beginning/end is part of the length
            var delta_x = Math.Abs(end.X - beginning.X) + 1;
            var delta_y = Math.Abs(end.Y - beginning.Y) + 1;

            if (delta_y > delta_x) isInvalid = isInvalid || (delta_y < 2 || delta_y > 4);
            else isInvalid = isInvalid || (delta_x < 2 || delta_x > 4);



            return !isInvalid;
        }

        // could return bool if needed
        public void HitShip(Coords guess)
        {
            foreach (var coord in this.Coords)
            {
                if (guess.X == coord.X && guess.Y == coord.Y)
                {
                    coord.Hit = true;
                }
            }
        }

        public bool IsSunk()
        {
            return this.Coords.All(coord => coord.Hit);
        }
    }
}
