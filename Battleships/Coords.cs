using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    public class Coords // "Point2D"
    {
        public int X;
        public int Y;

        public Coords(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Coords FromString(string cell_definition)
        {
            var parts = cell_definition.Split(':');

            // we assume that the input coords are 1 - 10 while the internal grid starts indexing at 0
            // so we decrement the input x/y value
            var x = int.Parse(parts[0]) - 1;
            var y = int.Parse(parts[1]) - 1;

            return new Coords(x, y);
        }
    }

    public class ShipCoords : Coords
    {
        public bool Hit = false;

        public ShipCoords(int x, int y) : base(x, y) { }
    }
}
