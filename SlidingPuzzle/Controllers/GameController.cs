using System;

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

        TileRepository repository;

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

            for (int x = 0; x < GameInfo.TableSize; x++)
                for (int y = 0; y < GameInfo.TableSize; y++)
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
            int i;
            Random rnd = new Random();

            for (i = 0; i < GameInfo.TilesCount; i++)
            {
                int x1 = rnd.Next(0, GameInfo.TableSize);
                int y1 = rnd.Next(0, GameInfo.TableSize);

                int x2 = rnd.Next(0, GameInfo.TableSize);
                int y2 = rnd.Next(0, GameInfo.TableSize);

                if ((x1 != GameInfo.TableSize - 1 || y1 != GameInfo.TableSize - 1) &&
                    (x2 != GameInfo.TableSize - 1 || y2 != GameInfo.TableSize - 1))
                    SwapTiles(x1, x2, y1, y2);
            }
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

