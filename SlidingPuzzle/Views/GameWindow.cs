using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using Gtk;

using SlidingPuzzle.Models;
using SlidingPuzzle.Controllers;

using Image = System.Drawing.Image;

namespace SlidingPuzzle.Views
{
    public partial class GameWindow : Window
    {
        GameController game;
        Image image;

        public GameWindow()
            : base(WindowType.Toplevel)
        {
            Build();
            GameDrawArea.DoubleBuffered = true;

            GameDrawArea.ExposeEvent += delegate
            {
                if (game != null)
                    DrawTable();
            };
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
                                           "Select", ResponseType.Accept);
            
            dialog.Filter = new FileFilter();
            dialog.Filter.AddPattern("*.png");
            dialog.Filter.AddPattern("*.jpg");
            dialog.Filter.AddPattern("*.jpeg");
            dialog.Filter.AddPattern("*.bmp");

            if (dialog.Run() == (int)ResponseType.Accept)
            {
                LoadImage(dialog.Filename);
                StartGame(3);
            }

            dialog.Destroy();
        }

        protected void OnKeyPressEvent(object o, KeyPressEventArgs args)
        {
            switch (args.Event.Key)
            {
                case Gdk.Key.w:
                case Gdk.Key.W:
                case Gdk.Key.Up:
                    game.MoveTile(0, -1);
                    break;

                case Gdk.Key.a:
                case Gdk.Key.A:
                case Gdk.Key.Left:
                    game.MoveTile(-1, 0);
                    break;

                case Gdk.Key.s:
                case Gdk.Key.S:
                case Gdk.Key.Down:
                    game.MoveTile(0, 1);
                    break;

                case Gdk.Key.d:
                case Gdk.Key.D:
                case Gdk.Key.Right:
                    game.MoveTile(1, 0);
                    break;
            }

            DrawTable();
        }

        void StartGame(int tableSize)
        {
            game = new GameController(tableSize);
            DrawTable();
        }

        void LoadImage(string path)
        {
            Image original = Image.FromFile(path);
            int width, height;

            GameDrawArea.GdkWindow.GetSize(out width, out height);

            image = new Bitmap(original, new Size(width, height));
        }

        void DrawTable()
        {
            Gdk.Drawable drawable = GameDrawArea.GdkWindow;
            int tableWidth, tableHeight, tileWidth, tileHeight;

            drawable.GetSize(out tableWidth, out tableHeight);
            tileWidth = tableWidth / game.GameInfo.TableSize;
            tileHeight = tableHeight / game.GameInfo.TableSize;

            Font font = new Font("Sans", (int)(Math.Min(tileWidth, tileHeight) * 0.5), FontStyle.Regular);
            StringFormat strFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            using (Graphics g = Gtk.DotNet.Graphics.FromDrawable(GameDrawArea.GdkWindow))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                for (int y = 0; y < game.GameInfo.TableSize; y++)
                    for (int x = 0; x < game.GameInfo.TableSize; x++)
                    {
                        Tile tile = game.GetTile(x, y);

                        Rectangle desRectangle = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                        Rectangle srcRectangle = new Rectangle(
                                                     (tile.Number - 1) % game.GameInfo.TableSize * tileWidth,
                                                     (tile.Number - 1) / game.GameInfo.TableSize * tileHeight,
                                                     desRectangle.Width, desRectangle.Height);

                        if (tile.Number != game.GameInfo.TilesCount)
                        {
                            g.DrawImage(image, desRectangle.X, desRectangle.Y, srcRectangle, GraphicsUnit.Pixel);
                            g.DrawString(tile.Number.ToString(), font, Brushes.IndianRed, desRectangle, strFormat);
                        }
                        else
                            g.FillRectangle(Brushes.Gray, desRectangle);
                    }
            }
        }
    }
}
