using System;
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

namespace WpfAnimationDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage spriteImg;
        Image player;
        int x = 0, y = 0;
        int dx = 15;
        int totalPlayerFrames = 8;
        int playerCurrentFrame = 0;

        public MainWindow()
        {
            InitializeComponent();

            spriteImg = new BitmapImage();
            spriteImg.BeginInit();
            spriteImg.UriSource = new Uri("sprite.png", UriKind.RelativeOrAbsolute);
            spriteImg.EndInit();

            InitPictures();
        }

        private void InitPictures()
        {
            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                Rectangle r = GenerateRectangle(rnd);
                this.cnvField.Children.Add(r);
            }

            player = new Image();
            player.Width = 58;
            player.Height = 66;
            player.Source = GetFrame(0);

            Canvas.SetTop(player, 450 - player.Height);
            this.cnvField.Children.Add(player);
        }

        private CroppedBitmap GetFrame(int frame)
        {
            const int w = 58, h = 66;

            int frameX = (frame % totalPlayerFrames) * w;
            return new CroppedBitmap(spriteImg, new Int32Rect(frameX, 0, w, h));
        }

        private static Rectangle GenerateRectangle(Random rnd)
        {
            Rectangle r = new Rectangle();
            r.Stroke = new SolidColorBrush(Colors.Green);
            r.StrokeThickness = 5;
            r.Width = rnd.Next(500);
            r.Height = rnd.Next(400);

            Canvas.SetLeft(r, rnd.Next(50, 800));
            Canvas.SetTop(r, rnd.Next(50, 400));
            return r;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Up))
            {
                scroll.LineUp();
            }
            if (Keyboard.IsKeyDown(Key.Down))
            {
                scroll.LineDown();
            }
            if (Keyboard.IsKeyDown(Key.Left))
            {
                scroll.LineLeft();
            }
            if (Keyboard.IsKeyDown(Key.Right))
            {
                scroll.LineRight();
            }

            if (Keyboard.IsKeyDown(Key.D))
            {
                if (x + dx <= this.cnvField.ActualWidth)
                {
                    x += dx;
                }
                Canvas.SetLeft(player, x);

                playerCurrentFrame = (playerCurrentFrame + 1) % totalPlayerFrames;
                player.Source = GetFrame(playerCurrentFrame);
                player.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
            }

            if (Keyboard.IsKeyDown(Key.A))
            {
                if (x - dx > 0)
                {
                    x -= dx;
                }
                Canvas.SetLeft(player, x);

                playerCurrentFrame--;
                if (playerCurrentFrame < 0)
                {
                    playerCurrentFrame += totalPlayerFrames;
                }

                player.Source = GetFrame(playerCurrentFrame);
                player.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
            }
        }
    }
}
