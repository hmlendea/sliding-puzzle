namespace SlidingPuzzle.Models
{
    public class GameInfo
    {
        /// <summary>
        /// Gets or sets the game time.
        /// </summary>
        /// <value>The game time.</value>
        public int GameTime { get; set; }

        /// <summary>
        /// Gets or sets the size of the table.
        /// </summary>
        /// <value>The size of the table.</value>
        public int TableSize { get; set; }

        /// <summary>
        /// Gets or sets the moves.
        /// </summary>
        /// <value>The moves.</value>
        public int Moves { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is running.
        /// </summary>
        /// <value><c>true</c> if this instance is running; otherwise, <c>false</c>.</value>
        public bool IsRunning { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SlidingPuzzle.Models.GameInfo"/> is completed.
        /// </summary>
        /// <value><c>true</c> if completed; otherwise, <c>false</c>.</value>
        public bool Completed { get; set; }
    }
}
