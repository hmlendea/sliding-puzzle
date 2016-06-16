using System;

using Gtk;

namespace SlidingPuzzle.Views
{
    public partial class GameWindow : Window
    {
        public GameWindow()
            : base(WindowType.Toplevel)
        {
            Build();
            GameDrawArea.DoubleBuffered = true;
        }

        protected void OnDeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
            a.RetVal = true;
        }

        protected void OnRetryActionActivated(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void OnNewActionActivated(object sender, EventArgs e)
        {
            FileChooserDialog dialog = new FileChooserDialog(
                                           "Choose an image", this, FileChooserAction.Open,
                                           "Cancel", ResponseType.Cancel,
                                           "Select", ResponseType.Ok);
            
            dialog.Filter = new FileFilter();
            dialog.Filter.AddPattern("*.png");
            dialog.Filter.AddPattern("*.jpg");
            dialog.Filter.AddPattern("*.jpeg");
            dialog.Filter.AddPattern("*.bmp");

            if (dialog.Run() == (int)ResponseType.Accept)
            {
                // TODO: Load the image
            }

            dialog.Destroy();
        }

        protected void OnKeyPressEvent(object o, KeyPressEventArgs args)
        {
            bool validKeyPressed = false;

            switch (args.Event.Key)
            {
                case Gdk.Key.w:
                case Gdk.Key.W:
                case Gdk.Key.Up:
                    validKeyPressed = true;
                    break;

                case Gdk.Key.a:
                case Gdk.Key.A:
                case Gdk.Key.Left:
                    validKeyPressed = true;
                    break;

                case Gdk.Key.s:
                case Gdk.Key.S:
                case Gdk.Key.Down:
                    validKeyPressed = true;
                    break;

                case Gdk.Key.d:
                case Gdk.Key.D:
                case Gdk.Key.Right:
                    validKeyPressed = true;
                    break;
            }
        }
    }
}
