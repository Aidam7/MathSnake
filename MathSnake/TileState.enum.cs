using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSnake
{
    public enum TileState : byte { Empty, SnakeHead, SnakeBody, SnakeTail, Food, Barrier, GameOver };
}
