using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Media.Animation;
using Microsoft.Win32;
using Timer = System.Timers.Timer;

namespace MathSnake
{
    public enum MovementDirection : byte
    {
        Up,
        Right,
        Down,
        Left
    };
    public class Snake
    {
        public string SnakeName { get; set; }
        public int SnakeLength { get; set; }
        private MovementDirection _direction;
        public MovementDirection Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                MovementDirection currentDirection = _direction;
                bool changeDirection = true;
                switch (value)
                {
                    case MovementDirection.Up:
                        if(currentDirection == MovementDirection.Down)
                            changeDirection = false;
                        break;
                    case MovementDirection.Down:
                        if(currentDirection == MovementDirection.Up)
                            changeDirection = false;
                        break;
                    case MovementDirection.Right:
                        if (currentDirection == MovementDirection.Left)
                            changeDirection = false;
                        break;
                    case MovementDirection.Left:
                        if (currentDirection == MovementDirection.Right)
                            changeDirection = false;
                        break;
                }
                if(changeDirection)
                    _direction = value;
            }
        }
        public double Speed { get; set; }
        public Snake(int snakeLength = 4, string snakeName = "Arnold", MovementDirection movementDirection = MovementDirection.Right, double movementSpeed = 100)
        {
            SnakeLength = snakeLength;
            SnakeName = snakeName;
            Direction = movementDirection;
            Speed = movementSpeed;
        }
    }
}
