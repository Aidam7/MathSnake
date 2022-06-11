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
using System.Timers;
using Timer = System.Timers.Timer;

namespace MathSnake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int _gameSize = 25;
        public TileState[,] _gameArea;
        private Snake snake;
        public Timer timer;
        public Rectangle[,] _tiles;
        public List<SnakePart> SnakeParts = new List<SnakePart>();
        public MainWindow()
        {
            _gameArea = new TileState[_gameSize, _gameSize];
            InitializeComponent();
            InitializeGameArea(GameAreaGrid, _gameArea);
            snake = new();
            timer = new();
            timer.Interval = snake.Speed;
            timer.AutoReset = true;
            timer.Enabled = true;
            GenerateSnake(_gameArea, snake);
            //UpdateTileStates(GameAreaGrid, _gameArea);
            for (int x = 0; x < _gameSize; x++)
            {
                for (int y = 0; y < _gameSize; y++)
                {
                    RenderTile(_tiles[x, y], _gameArea[x, y]);
                }
            }
            snake.HeadPosition = GetCoordinatesOfTile(_gameArea, TileState.SnakeHead);
            snake.TailPosition = GetCoordinatesOfTile(_gameArea, TileState.SnakeTail);
            //SnakeMovement(_gameArea, snake, MovementDirection.Right);
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
            //This method should not be used, instead use the methods separately
            //CreateGrid(display);
            //UpdateTileStates(display, gameArea);


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
                        break;
                    }
                }
            }
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SnakeMovement(_gameArea, snake, MovementDirection.Right);
            for (int x = 0; x < _gameSize; x++)
            {
                for (int y = 0; y < _gameSize; y++)
                {
                    RenderTile(_tiles[x, y], _gameArea[x, y]);
                }
            }

        }

        private void Window_OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down: pomoc.Content = "Tvoje máma"; SnakeMovement(_gameArea, snake, MovementDirection.Up); RenderAllTiles(); break;
                case Key.Up: pomoc.Content = "Tvoje sestra"; break;

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
        public void SnakeMovement(TileState[,] grid, Snake snake, MovementDirection direction)
        {
            
        }
    }
}