using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MathSnake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int score;
        public bool IsGameOver;
        public int _gameSize = 25;
        public TileState[,] _gameArea;
        private Food foodOnMap;
        private Snake snake;
        public Rectangle[,] _tiles;
        public List<SnakePart> SnakeParts = new List<SnakePart>();
        private DispatcherTimer _timer = new DispatcherTimer();
        private int tickCount = 0;
        public Random rnd = new Random();
        public MainWindow()
        {
            score = 0;
            IsGameOver = false;
            _gameArea = new TileState[_gameSize, _gameSize];
            InitializeComponent();
            InitializeGameArea(GameAreaGrid, _gameArea);
            //GenerateBorderBarriers();
            snake = new();
            _timer.Tick += TimerOnTick;
            _timer.Interval = TimeSpan.FromMilliseconds(snake.Speed);
            GenerateSnake(_gameArea, snake);
            //UpdateTileStates(GameAreaGrid, _gameArea);
            for (int x = 0; x < _gameSize; x++)
            {
                for (int y = 0; y < _gameSize; y++)
                {
                    RenderTile(_tiles[x, y], _gameArea[x, y]);
                }
            }
            GenerateFood();
            SnakeMovement(_gameArea, snake, false);
            SnakeMovement(_gameArea, snake, false);
            SnakeMovement(_gameArea, snake, false);
            MessageBox.Show(
                "As of right now, there is a bug in game which causes a Game over when the snake enters the border tiles\nHowever not to worry, we are working hard on a fix. :)",
                "Caution", MessageBoxButton.OK, MessageBoxImage.Error);
            //GenerateBorderBarriers();
        }
        private void TimerOnTick(object? sender, EventArgs e)
        {
            tickCount++;
            if (tickCount % 5 == 0)
            {
                Point foodCord = GetCoordinatesOfTile(_gameArea, TileState.Food);
                if (foodCord.X == -1 || foodCord.Y == -1)
                {
                    ConsumeFood(foodOnMap);
                }
            }

            if (!foodOnMap.IsFoodOnMap)
            {
                GenerateFood();
            }


            SnakeMovement(_gameArea, snake);
        }
        /// <summary>
        /// Fills the grid with Rows and Columns
        /// </summary>
        /// <param name="display"></param>
        private void CreateGrid(Grid display)
        {
            for (int i = 0; i < _gameSize; i++)
            {
                display.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < _gameSize; i++)
            {
                display.ColumnDefinitions.Add(new ColumnDefinition());
            }

            _tiles = new Rectangle[_gameSize, _gameSize];
            for (int x = 0; x < _gameSize; x++)
            {
                for (int y = 0; y < _gameSize; y++)
                {
                    _tiles[x, y] = new Rectangle();
                }
            }
        }
        /// <summary>
        /// Method InitializeGameArea was split into UpdateTileStates() and CreateGrid()
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="state"></param>
        private void InitializeGameArea(Grid display, TileState[,] gameArea)
        {
            for (int i = 0; i < _gameSize; i++)
            {
                display.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < _gameSize; i++)
            {
                display.ColumnDefinitions.Add(new ColumnDefinition());
            }

            _tiles = new Rectangle[_gameSize, _gameSize]; //uložím si mapu, kde který rectangle je

            for (int x = 0; x < _gameSize; x++)
            {
                for (int y = 0; y < _gameSize; y++)
                {
                    Rectangle tile = new Rectangle();
                    RenderTile(tile, gameArea[x, y]);
                    _tiles[x, y] = tile; //hráčovu dlaždici si poznačím do mapy
                    Grid.SetRow(tile, y);
                    Grid.SetColumn(tile, x);
                    display.Children.Add(tile);
                }
            }
        }
        public void RenderTile(Rectangle tile, TileState state)
        {
            switch (state)
            {
                case TileState.Empty:
                    tile.Style = FindResource("EmptyTile") as Style;
                    break;
                case TileState.SnakeHead:
                    tile.Style = FindResource("SnakeHeadTile") as Style;
                    break;
                case TileState.SnakeBody:
                    tile.Style = FindResource("SnakeBodyTile") as Style;
                    break;
                case TileState.SnakeTail:
                    tile.Style = FindResource("SnakeTailTile") as Style;
                    break;
                case TileState.Food:
                    tile.Style = FindResource("FoodTile") as Style;
                    break;
                case TileState.Barrier:
                    tile.Style = FindResource("BarrierTile") as Style;
                    break;
                case TileState.GameOver:
                    tile.Style = FindResource("GameOverTile") as Style;
                    break;
            }
        }
        /// <summary>
        /// Returns the coordinates of the first Tilestate in multidimensional array
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="find"></param>
        /// <returns></returns>
        public Point GetCoordinatesOfTile(TileState[,] grid, TileState find)
        {
            Point coordinates;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == find)
                    {
                        coordinates.X = i;
                        coordinates.Y = j;
                        return coordinates;
                    }
                }
            }
            coordinates.X = -1;
            coordinates.Y = -1;
            return coordinates;
        }
        private void RenderAllTiles()
        {
            for (int x = 0; x < _gameSize; x++)
            {
                for (int y = 0; y < _gameSize; y++)
                {
                    RenderTile(_tiles[x, y], _gameArea[x, y]);
                }
            }
        }
        private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    if (_timer.IsEnabled)
                        _timer.Stop();
                    else if (IsGameOver)
                        _timer.Stop();
                    else
                        _timer.Start();
                    break;
            }
            if (_timer.IsEnabled)
            {
                switch (e.Key)
                {
                    case Key.Up or Key.W: snake.Direction = MovementDirection.Up; break;
                    case Key.Down or Key.S: snake.Direction = MovementDirection.Down; break;
                    case Key.Right or Key.D: snake.Direction = MovementDirection.Right; break;
                    case Key.Left or Key.A: snake.Direction = MovementDirection.Left; break;
                }
            }
        }
        private void MainWindow_OnContentRendered(object? sender, EventArgs e)
        {
            RenderAllTiles();
        }
        public void GenerateSnake(TileState[,] grid, Snake snake)
        {
            int rowCount = grid.GetLength(0);
            int columnCount = grid.GetLength(1);
            SnakePart head = new SnakePart(rowCount / 2, columnCount / 2, true);
            SnakeParts.Add(head);
            while (SnakeParts.Count < snake.SnakeLength)
            {
                SnakePart snakePart = new(rowCount / 2 - SnakeParts.Count, columnCount / 2);
                SnakeParts.Add(snakePart);
            }
            foreach (var Part in SnakeParts)
            {
                if (Part.isHead)
                    grid[(int)Part.Coordinates.X, (int)Part.Coordinates.Y] = TileState.SnakeHead;
                else
                    grid[(int)Part.Coordinates.X, (int)Part.Coordinates.Y] = TileState.SnakeBody;
            }
        }
        public void SnakeMovement(TileState[,] grid, Snake snake, bool doCollisionCheck = true)
        {
            while (SnakeParts.Count >= snake.SnakeLength)
            {
                SnakePart tail = SnakeParts[0];
                grid[(int)tail.Coordinates.X, (int)tail.Coordinates.Y] = TileState.Empty;
                RenderTile(_tiles[(int)tail.Coordinates.X, (int)tail.Coordinates.Y], grid[(int)tail.Coordinates.X, (int)tail.Coordinates.Y]);
                SnakeParts.RemoveAt(0);
            }

            foreach (var Part in SnakeParts)
            {
                Part.isHead = false;
                grid[(int)Part.Coordinates.X, (int)Part.Coordinates.Y] = TileState.SnakeBody;
                RenderTile(_tiles[(int)Part.Coordinates.X, (int)Part.Coordinates.Y], grid[(int)Part.Coordinates.X, (int)Part.Coordinates.Y]);
            }
            SnakePart head = SnakeParts[SnakeParts.Count - 1];
            int newX = (int)head.Coordinates.X;
            int newY = (int)head.Coordinates.Y;
            switch (snake.Direction)
            {
                case MovementDirection.Up: newY--; break;
                case MovementDirection.Right: newX++; break;
                case MovementDirection.Down: newY++; break;
                case MovementDirection.Left: newX--; break;
            }
            SnakeParts.Add(new SnakePart(newX, newY, true));
                grid[(int)newX, (int)newY] = TileState.SnakeHead;
                RenderTile(_tiles[(int)newX, (int)newY], grid[(int)newX, (int)newY]);
                if (doCollisionCheck)
                    CollisionCheck();
        }
        public void CollisionCheck()
        {
            int incrementX = 0;
            int incrementY = 0;
            switch (snake.Direction)
            {
                case MovementDirection.Up: incrementY--; break;
                case MovementDirection.Right: incrementX++; break;
                case MovementDirection.Down: incrementY++; break;
                case MovementDirection.Left: incrementX--; break;
            }
            SnakePart head = SnakeParts[SnakeParts.Count - 1];
            if (head.Coordinates.X + incrementX >= _gameSize || head.Coordinates.Y + incrementY >= _gameSize || head.Coordinates.X + incrementX < 0 || head.Coordinates.Y + incrementY < 0)
            {
                GameOverHandler("Out of map");
                return;
            }
            foreach (var snakePart in SnakeParts)
            {
                if (snakePart.Coordinates.X == head.Coordinates.X && snakePart.Coordinates.Y == head.Coordinates.Y && snakePart.isHead == false)
                    GameOverHandler("Collided with itself");
            }
            if (_gameArea[(int)head.Coordinates.X + incrementX, (int)head.Coordinates.Y + incrementY] == TileState.Barrier)
                GameOverHandler("Hit a barrier");
            if (_gameArea[(int)head.Coordinates.X + incrementX, (int)head.Coordinates.Y + incrementY] == TileState.Food)
                ConsumeFood(foodOnMap);
        }
        public void GameOverHandler(string deathMessage)
        {
            string causeOfDeath = deathMessage;
            _timer.Stop();
            for (int i = 0; i < _gameArea.GetLength(0); i++)
            {
                for (int j = 0; j < _gameArea.GetLength(1); j++)
                {
                    if (_gameArea[i, j] == TileState.Empty)
                        _gameArea[i, j] = TileState.GameOver;
                    RenderTile(_tiles[i, j], TileState.GameOver);
                }
            }
            MessageBox.Show($"You lost!\nYour score was: {score}\n\n{causeOfDeath}", "Uh oh", MessageBoxButton.OK, MessageBoxImage.Hand);
            this.Close();
        }

        public void GenerateFood()
        {
            while (true)
            {
                int cordX = rnd.Next(1,_gameSize-1);
                int cordY = rnd.Next(1,_gameSize-1);
                if (_gameArea[cordX, cordY] == TileState.Empty)
                {
                    foodOnMap = new Food(cordX, cordY);
                    _gameArea[(int)foodOnMap.Coordinates.X, (int)foodOnMap.Coordinates.Y] = TileState.Food;
                    RenderTile(_tiles[(int)foodOnMap.Coordinates.X, (int)foodOnMap.Coordinates.Y], TileState.Food);
                    foodOnMap.IsFoodOnMap = true;
                    break;
                }
            }
        }
        public void ConsumeFood(Food food)
        {
            score++;
            if (score % 5 == 0)
                GenerateBarrier();
            Score.Text = $"Score: {score}";
            foodOnMap.IsFoodOnMap = false;
            snake.SnakeLength++;
            RenderTile(_tiles[(int)foodOnMap.Coordinates.X, (int)foodOnMap.Coordinates.Y], _gameArea[(int)foodOnMap.Coordinates.X, (int)foodOnMap.Coordinates.Y]);
            GenerateFood();
        }

        public void GenerateBarrier()
        {
            List<BarrierPart> barrierParts = new List<BarrierPart>();
            Barrier barrier = new Barrier(rnd.Next(1, 6));
            barrierParts.Add(new BarrierPart(rnd.Next(_gameSize), rnd.Next(_gameSize)));
            _gameArea[(int)barrierParts[0].Coordinates.X, (int)barrierParts[0].Coordinates.Y] = TileState.Barrier;
            RenderTile(_tiles[(int)barrierParts[0].Coordinates.X, (int)barrierParts[0].Coordinates.Y], TileState.Barrier);
            while (barrierParts.Count < barrier.Lenght)
            {
                int incrementX = rnd.Next(1);
                int incrementY = rnd.Next(1);
                if (_tiles.GetLength(0) > barrierParts[barrierParts.Count - 1].Coordinates.X + incrementX ||
                    _tiles.GetLength(1) > barrierParts[barrierParts.Count - 1].Coordinates.X + incrementY)
                {
                    barrierParts.Add(new BarrierPart((int)barrierParts[barrierParts.Count - 1].Coordinates.X + incrementX, (int)barrierParts[barrierParts.Count - 1].Coordinates.X + incrementY));
                    _gameArea[(int)barrierParts[barrierParts.Count - 1].Coordinates.X, (int)barrierParts[barrierParts.Count - 1].Coordinates.Y] = TileState.Barrier;
                    RenderTile(_tiles[(int)barrierParts[barrierParts.Count - 1].Coordinates.X, (int)barrierParts[barrierParts.Count - 1].Coordinates.Y], TileState.Barrier);
                }
            }
        }

        //public void GenerateBorderBarriers()
        //{
        //    int x = 0;
        //    int y = 0;
        //    for (x = 0; x < _gameSize; x++)
        //    {
        //        _gameArea[x, y] = TileState.Barrier;
        //        RenderTile(_tiles[x, y], TileState.Barrier);
        //    }
        //    for (y = 0; y < _gameSize; y++)
        //    {
        //        _gameArea[x, y] = TileState.Barrier;
        //        RenderTile(_tiles[x, y], TileState.Barrier);
        //    }
        //    x = 0;
        //    for (y = 0; y < _gameSize; y++)
        //    {
        //        _gameArea[x, y] = TileState.Barrier;
        //        RenderTile(_tiles[x, y], TileState.Barrier);
        //    }
        //    for (x = 0; x < _gameSize; x++)
        //    {
        //        _gameArea[x, y] = TileState.Barrier;
        //        RenderTile(_tiles[x, y], TileState.Barrier);
        //    }
        //}
    }
}