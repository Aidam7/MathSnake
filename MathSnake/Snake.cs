using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace MathSnake
{
    public enum MovementDirection : byte
    {
        Up,
        Right,
        Down,
        Left
    };
    class Snake
    {
        public string SnakeName { get; set; }
        public int SnakeLength { get; set; }
        public MovementDirection Direction { get; set; }
        public Point HeadPosition { get; set; }
        public Point TailPosition { get; set; }
        public Snake(int snakeLength = 3, string snakeName = "Arnold", MovementDirection movementDirection = MovementDirection.Right)
        {
            SnakeLength = snakeLength;
            SnakeName = snakeName;
            Direction = movementDirection;
        }
        public void GenerateSnake(TileState[,] grid, Snake snake)
        {
            int rowCount = grid.GetLength(0);
            int columnCount = grid.GetLength(1);
            grid[rowCount / 2, columnCount / 2] = TileState.SnakeHead;
            for (int i = 1; i < SnakeLength; i++)
            {
                grid[rowCount / 2 - i, columnCount / 2] = TileState.SnakeBody;
            }

            grid[rowCount / 2 - SnakeLength, columnCount / 2] = TileState.SnakeTail;
        }
        
        public void SnakeMovement(TileState[,] grid, Snake snake, MovementDirection direction)
        {
            
        }
    }
}
