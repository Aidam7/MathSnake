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
        public Snake(int snakeLength = 4, string snakeName = "Arnold", MovementDirection movementDirection = MovementDirection.Right, double movementSpeed = 250)
        {
            SnakeLength = snakeLength;
            SnakeName = snakeName;
            Direction = movementDirection;
            Speed = movementSpeed;
        }
        //public void GenerateSnake(TileState[,] grid, Snake snake)
        //{
        //    int rowCount = grid.GetLength(0);
        //    int columnCount = grid.GetLength(1);
        //    SnakePart head = new SnakePart(rowCount / 2, columnCount / 2, true);
        //    grid[(int)head.Coordinates.X, (int)head.Coordinates.Y] = TileState.SnakeHead;
        //    for (int i = 1; i < SnakeLength; i++)
        //    {
        //        grid[rowCount / 2 - i, columnCount / 2] = TileState.SnakeBody;
        //    }
        //    grid[rowCount / 2 - SnakeLength, columnCount / 2] = TileState.SnakeTail;
        //}
        //public void SnakeMovement(TileState[,] grid, MovementDirection direction)
        //{
        //    int headPositionX = Convert.ToInt32(this.HeadPosition.X);
        //    int headPositionY = Convert.ToInt32(this.HeadPosition.Y);
        //    int tailPositionX = Convert.ToInt32(this.TailPosition.X);
        //    int tailPositionY = Convert.ToInt32(this.TailPosition.Y);
        //    if (direction == MovementDirection.Up)
        //    {
        //        grid[headPositionX, headPositionY] = TileState.SnakeBody;
        //        grid[headPositionX, headPositionY + 1] = TileState.SnakeHead;
        //        grid[tailPositionX, tailPositionY] = TileState.SnakeTail;
        //        grid[tailPositionX, tailPositionY] = TileState.Empty;
        //        _headPosition.Y = headPositionY + 1;
        //        _tailPosition.Y = tailPositionY + 1;
        //    }
        //    else if (direction == MovementDirection.Down)
        //    {
        //        grid[headPositionX, headPositionY] = TileState.SnakeBody;
        //        grid[headPositionX, headPositionY - 1] = TileState.SnakeHead;
        //        grid[tailPositionX, tailPositionY - 1] = TileState.SnakeTail;
        //        grid[tailPositionX, tailPositionY] = TileState.Empty;
        //        _headPosition.Y = headPositionY - 1;
        //        _tailPosition.Y = tailPositionY - 1;
        //    }
        //    else if (direction == MovementDirection.Left)
        //    {
        //        grid[headPositionX, headPositionY] = TileState.SnakeBody;
        //        grid[headPositionX - 1, headPositionY] = TileState.SnakeHead;
        //        grid[tailPositionX - 1, tailPositionY] = TileState.SnakeTail;
        //        grid[tailPositionX, tailPositionY] = TileState.Empty;
        //        _headPosition.X = headPositionX - 1;
        //        _tailPosition.X = tailPositionX - 1;
        //    }
        //    else if(direction == MovementDirection.Right)
        //    {
        //        grid[headPositionX, headPositionY] = TileState.SnakeBody;
        //        grid[headPositionX + 1, headPositionY] = TileState.SnakeHead;
        //        grid[tailPositionX + 1, tailPositionY] = TileState.SnakeTail;
        //        grid[tailPositionX, tailPositionY] = TileState.Empty;
        //        _headPosition.X = headPositionX + 1;
        //        _tailPosition.X = tailPositionX + 1;
        //    }
        //    else
        //    {
        //        throw new IndexOutOfRangeException();
        //    }
        //}
        //public void ContinuousMovement(TileState[,] grid, MovementDirection direction, double speed)
        //{
        //    didTimerTick = false;
        //    Timer timer = new Timer();
        //    timer.Interval = speed;
        //    timer.Start();
        //    timer.Elapsed += HandleTimerElapsed;
        //    timer.AutoReset = true;
        //    if (didTimerTick)
        //    {
        //        SnakeMovement(grid, direction);
        //    }
        //}
        //public void HandleTimerElapsed(object sender, ElapsedEventArgs e)
        //{
        //    didTimerTick = true;
        //}
    }
}
