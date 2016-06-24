using System;
using System.Collections.Generic;

using SlidingPuzzle.Models;
using SlidingPuzzle.Repositories;

namespace SlidingPuzzle.Controllers
{
    /// <summary>
    /// Game controller.
    /// </summary>
    public class GameController
    {
        /// <summary>
        /// Gets the game info.
        /// </summary>
        /// <value>The game info.</value>
        public GameInfo GameInfo { get; private set; }

        /// <summary>
        /// Gets a value indicating whether thie game is completed.
        /// </summary>
        /// <value><c>true</c> if completed; otherwise, <c>false</c>.</value>
        public bool Completed { get { return GetNumberOfInversions() == 0; } }

        TileRepository repository;
        Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlidingPuzzle.Controllers.GameController"/> class.
        /// </summary>
        /// <param name="tableSize">Table size.</param>
        public GameController(int tableSize)
        {
            GameInfo = new GameInfo
            {
                TableSize = tableSize,
                IsRunning = true
            };

            repository = new TileRepository();
            random = new Random();

            InitializeTable();
            ShuffleTable();
            
            GLib.Timeout.Add(1000, new GLib.TimeoutHandler(OnGameTimerTick));
        }

        /// <summary>
        /// Moves the tile.
        /// </summary>
        /// <param name="xDelta">X delta.</param>
        /// <param name="yDelta">Y delta.</param>
        public void MoveTile(int xDelta, int yDelta)
        {
            Tile tile = repository.Get(GameInfo.TilesCount);

            if (tile.X + xDelta >= GameInfo.TableSize || tile.X + xDelta < 0)
                return; //throw new ArgumentException("Invalid X delta");

            if (tile.Y + yDelta >= GameInfo.TableSize || tile.Y + yDelta < 0)
                return; //throw new ArgumentException("Invalid Y delta");

            SwapTiles(tile.X, tile.Y, tile.X + xDelta, tile.Y + yDelta);
        }

        /// <summary>
        /// Gets the tile.
        /// </summary>
        /// <returns>The tile.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Tile GetTile(int x, int y)
        {
            return repository.Get(x, y);
        }

        void InitializeTable()
        {
            int k = 1;

            for (int y = 0; y < GameInfo.TableSize; y++)
                for (int x = 0; x < GameInfo.TableSize; x++)
                {
                    Tile tile = new Tile
                    {
                        X = x,
                        Y = y,
                        Number = k
                    };

                    k += 1;

                    repository.Add(tile);
                }
        }

        void ShuffleTable()
        {
            for (int i = 0; i < GameInfo.TilesCount; i++)
                DoRandomSwap();

            if (GameInfo.TableSize % 2 != 0)
                while (GetNumberOfInversions() % 2 != 0)
                    DoRandomSwap();
            else
            {
                Tile blank = repository.Get(GameInfo.TilesCount);

                while (GetNumberOfInversions() % 2 == blank.Y % 2)
                    DoRandomSwap();
            }
        }

        int GetNumberOfInversions()
        {
            List<Tile> tiles = repository.GetAll();
            int inversions = 0;


            for (int i = 0; i < tiles.Count; i++)
            {
                int current = tiles[i].Number;

                if (current == GameInfo.TilesCount)
                    continue;

                for (int j = i; j < tiles.Count; j++)
                    if (tiles[i].Number != GameInfo.TilesCount)
                    if (current > tiles[j].Number)
                        inversions += 1;
            }

            return inversions;
        }

        void DoRandomSwap()
        {
            int x1 = random.Next(0, GameInfo.TableSize);
            int y1 = random.Next(0, GameInfo.TableSize);

            int x2 = random.Next(0, GameInfo.TableSize);
            int y2 = random.Next(0, GameInfo.TableSize);

            SwapTiles(x1, y1, x2, y2);
        }

        void SwapTiles(int x1, int y1, int x2, int y2)
        {
            Tile tile1 = repository.Get(x1, y1);
            Tile tile2 = repository.Get(x2, y2);

            int aux = tile1.Number;
            tile1.Number = tile2.Number;
            tile2.Number = aux;

            repository.Update(tile1);
            repository.Update(tile2);
        }

        bool OnGameTimerTick()
        {
            if (GameInfo.IsRunning)
                GameInfo.GameTime += 1;

            return true;
        }
    }
}

