using System.ComponentModel.DataAnnotations;

namespace SlidingPuzzle.Models
{
    public class Tile
    {
        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        /// <value>The X coordinate.</value>
        [Key]
        [Range(0, int.MaxValue)]
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        /// <value>The Y coordinate.</value>
        [Key]
        [Range(0, int.MaxValue)]
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        [Range(0, int.MaxValue)]
        public int Number { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="SlidingPuzzle.Models.Tile"/>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="SlidingPuzzle.Models.Tile"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
        /// <see cref="SlidingPuzzle.Models.Tile"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            Tile other = obj as Tile;
            return X == other?.X && Y == other.Y;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="SlidingPuzzle.Models.Tile"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="SlidingPuzzle.Models.Tile"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="SlidingPuzzle.Models.Tile"/>.</returns>
        public override string ToString()
        {
            return string.Format("{0} #{1},{2}", base.ToString(), X, Y);
        }
    }
}
