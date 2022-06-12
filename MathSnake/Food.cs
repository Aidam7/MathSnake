using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MathSnake
{
    public class Food
    {
        private bool _isFoodOnMap;

        public bool IsFoodOnMap
        {
            get
            {
                return _isFoodOnMap;
            }
            set
            {
                _isFoodOnMap = value;
            }
        }
        private Point _coordinates;
        public Point Coordinates
        {
            get
            {
                return _coordinates;
            }
            set
            {
                _coordinates = value;
            }
        }

        public Food(int X, int Y)
        {
            _coordinates.X = X;
            _coordinates.Y = Y;
        }
    }
}
