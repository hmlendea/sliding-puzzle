using Gtk;

using SlidingPuzzle.Views;

namespace SlidingPuzzle
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();

            GameWindow window = new GameWindow();
            window.Show();

            Application.Run();
        }
    }
}
