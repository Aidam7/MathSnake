﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            snake.GenerateSnake(_gameArea);
            UpdateTileStates(GameAreaGrid, _gameArea);
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
        private void UpdateTileStates(Grid display, TileState[,] gameArea)
        {
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
        /// <summary>
        /// Method InitializeGameArea was split into UpdateTileStates() and CreateGrid()
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="state"></param>
        //private void InitializeGameArea(Grid display, TileState[,] gameArea)
        //{

        //    for (int i = 0; i < _gameSize; i++)
        //    {
        //        display.RowDefinitions.Add(new RowDefinition());
        //    }
        //    for (int i = 0; i < _gameSize; i++)
        //    {
        //        display.ColumnDefinitions.Add(new ColumnDefinition());
        //    }

        //    _tiles = new Rectangle[_gameSize, _gameSize]; //uložím si mapu, kde který rectangle je

        //    for (int x = 0; x < _gameSize; x++)
        //    {
        //        for (int y = 0; y < _gameSize; y++)
        //        {
        //            Rectangle tile = new Rectangle();
        //            RenderTile(tile, gameArea[x, y]);
        //            _tiles[x, y] = tile; //hráčovu dlaždici si poznačím do mapy
        //            Grid.SetRow(tile, y);
        //            Grid.SetColumn(tile, x);
        //            display.Children.Add(tile);
        //        }
        //    }
        //}
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
    }
}