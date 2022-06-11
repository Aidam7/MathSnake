using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSnake
{
    public class SnakePart
    {
        private Point _coordinates;
        public Point Coordinates {
            get { return _coordinates; }
            set { _coordinates = value; }
        }
        public bool isHead { get; set; }

        public SnakePart(int X, int Y, bool isSnakeHead = false)
        {
            _coordinates.X = X;
            _coordinates.Y = Y;
            isHead = isSnakeHead;
        }
    }
}
