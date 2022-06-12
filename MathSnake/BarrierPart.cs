using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSnake
{
    public class BarrierPart
    {
        private Point _coordinates;
        public Point Coordinates
        {
            get { return _coordinates; }
            set { _coordinates = value; }
        }
        public BarrierPart(int X, int Y)
        {
            _coordinates.X = X;
            _coordinates.Y = Y;
        }
    }
}
