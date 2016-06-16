using System.Collections.Generic;

using SlidingPuzzle.Models;

namespace SlidingPuzzle.Repositories
{
    /// <summary>
    /// Repository.
    /// </summary>
    public class TileRepository
    {
        /// <summary>
        /// Gets or sets the entities.
        /// </summary>
        /// <value>The entities.</value>
        protected List<Tile> Tiles { get; set; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>The size.</value>
        public int Size
        {
            get { return Tiles.Count; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlidingPuzzle.Repositories.TileRepository"/> class.
        /// </summary>
        public TileRepository()
        {
            Tiles = new List<Tile>();
        }

        /// <summary>
        /// Add the specified tile.
        /// </summary>
        /// <param name="tile">Tile.</param>
        public void Add(Tile tile)
        {
            if (Tiles.Contains(tile))
                throw new RepositoryException("The specified tile already exists");

            Tiles.Add(tile);
        }

        /// <summary>
        /// Get a tile by the specified x and y.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Tile Get(int x, int y)
        {
            Tile tile = Tiles.Find(E => E.X == x && E.Y == y);

            if (tile == null)
                throw new RepositoryException("A tile with the specified coordinates does not exist");

            return tile;
        }

        /// <summary>
        /// Get a tile by the specified number.
        /// </summary>
        /// <param name="number">Number.</param>
        public Tile Get(int number)
        {
            Tile tile = Tiles.Find(E => E.Number == number);

            if (tile == null)
                throw new RepositoryException("A tile with the specified coordinates does not exist");

            return tile;
        }

        /// <summary>
        /// Gets all tiles.
        /// </summary>
        /// <returns>The tiles.</returns>
        public List<Tile> GetAll()
        {
            return Tiles;
        }

        /// <summary>
        /// Update the specified tile.
        /// </summary>
        /// <param name="tile">Tile.</param>
        public void Update(Tile tile)
        {
            Tile oldTile = Get(tile.X, tile.Y);
            oldTile.Number = tile.Number;
        }

        /// <summary>
        /// Remove the specified tile.
        /// </summary>
        /// <param name="tile">Tile.</param>
        public void Remove(Tile tile)
        {
            if (!Contains(tile))
                throw new RepositoryException("The specified tile does not exist");

            Tiles.Remove(tile);
        }

        /// <summary>
        /// Remove the specified x and y.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void Remove(int x, int y)
        {
            if (!Contains(x, y))
                throw new RepositoryException("A tile with the specified coordinates does not exist");

            Tiles.RemoveAll(T => T.X == x && T.Y == y);
        }

        /// <summary>
        /// Clear this instance.
        /// </summary>
        public void Clear()
        {
            Tiles.Clear();
        }

        /// <summary>
        /// Contains the specified tile.
        /// </summary>
        /// <param name="tile">Tile.</param>
        public bool Contains(Tile tile)
        {
            return Tiles.Find(E => E.Equals(tile)) != null;
        }

        /// <summary>
        /// Contains the specified x and y.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public bool Contains(int x, int y)
        {
            return Tiles.Find(E => E.X == x && E.Y == y) != null;
        }

        /// <summary>
        /// Contains the specified number.
        /// </summary>
        /// <param name="number">Number.</param>
        public bool Contains(int number)
        {
            return Tiles.Find(E => E.Number == number) != null;
        }
    }
}
