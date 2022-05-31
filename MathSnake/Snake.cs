using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
        public string snakeName = "Arnold";
        public int snakeLength = 3;
        public void GenerateSnake(TileState[,] grid)
        {
            int rowCount = grid.GetLength(0);
            int columnCount = grid.GetLength(1);
            grid[rowCount / 2, columnCount / 2] = TileState.SnakeHead;
            for (int i = 1; i < snakeLength; i++)
            {
                grid[rowCount / 2 - i, columnCount / 2] = TileState.SnakeBody;
            }
            grid[rowCount / 2 - snakeLength, columnCount / 2] = TileState.SnakeTail;
        }
    }
}
