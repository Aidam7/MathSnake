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

namespace MathSnake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Rectangle[,] _tiles;
        private int _gameSize = 25;
        private TileState[,] _gameArea;
        public MainWindow()
        {
            _gameArea = new TileState[_gameSize, _gameSize];
            InitializeComponent();
            CreateGrid(GameAreaGrid);
            Snake snake = new();
            snake.GenerateSnake(_gameArea, snake);
            UpdateTileStates(GameAreaGrid, _gameArea);
            snake.HeadPosition = GetCoordinatesOfTile(_gameArea, TileState.SnakeHead);
            snake.TailPosition = GetCoordinatesOfTile(_gameArea, TileState.SnakeTail);
            snake.SnakeMovement(_gameArea,MovementDirection.Right);
            UpdateTileStates(GameAreaGrid,_gameArea);
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
        }
        /// <summary>
        /// Refreshes TileStates on the display
        /// </summary>
        /// <param name="display"></param>
        /// <param name="gameArea"></param>
        public void UpdateTileStates(Grid display, TileState[,] gameArea)
        {
            _tiles = new Rectangle[_gameSize, _gameSize];

            for (int x = 0; x < _gameSize; x++)
            {
                for (int y = 0; y < _gameSize; y++)
                {
                    Rectangle tile = new Rectangle();
                    RenderTile(tile, gameArea[x, y]);
                    _tiles[x, y] = tile;
                    Grid.SetRow(tile, y);
                    Grid.SetColumn(tile, x);
                    display.Children.Add(tile);
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
            CreateGrid(display);
            UpdateTileStates(display, gameArea);
            #region Merged code of CreateGrid and UpdateTileStates
            //DO NOT USE
            //for (int i = 0; i < _gameSize; i++)
            //{
            //    display.RowDefinitions.Add(new RowDefinition());
            //}
            //for (int i = 0; i < _gameSize; i++)
            //{
            //    display.ColumnDefinitions.Add(new ColumnDefinition());
            //}

            //_tiles = new Rectangle[_gameSize, _gameSize]; //uložím si mapu, kde který rectangle je

            //for (int x = 0; x < _gameSize; x++)
            //{
            //    for (int y = 0; y < _gameSize; y++)
            //    {
            //        Rectangle tile = new Rectangle();
            //        RenderTile(tile, gameArea[x, y]);
            //        _tiles[x, y] = tile; //hráčovu dlaždici si poznačím do mapy
            //        Grid.SetRow(tile, y);
            //        Grid.SetColumn(tile, x);
            //        display.Children.Add(tile);
            //    }
            //}
            #endregion
        }
        private void RenderTile(Rectangle tile, TileState state)
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
    }
}