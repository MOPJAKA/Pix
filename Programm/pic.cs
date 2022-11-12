using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Programm
{
    internal class pic
    {
        private int _width;
        private int _height;
        private Color[,] _pixel;

        public pic()
        {
            _width = 0;
            _height = 0;
        }

        public pic(Image orig)
        {
            Bitmap Borig = new Bitmap(orig);
            _width = orig.Width;
            _height = orig.Height;
            _pixel = new Color[_width, _height];
            Filling(Borig);
        }

        private void Filling(System.Drawing.Bitmap Borig)
        {
            for (int i = 0; i < Borig.Width; i++)
            {
                for (int j = 0; j < Borig.Height; j++)
                {
                    _pixel[i, j] = Borig.GetPixel(i, j);
                    if (_pixel[i, j].A == 0)
                    {
                        _pixel = null;
                        MessageBox.Show("Изображение не должно содержать пустых пикселей");
                        return;
                    }
                }
            }
        }

        public void Pixilization(Image Original, List<Bitmap> _bitmap)
        {
            Bitmap Bitmap_Original = new Bitmap(Original);
            
            int r, g, b, K;
            if (Original.Width + Original.Height <= 1000)
                K = 2;
            else if (Original.Height + Original.Width <= 3500)
                K = 4;
            else
                K = 8;
            int width_O = 0, height_O = 0, width_P, height_P;

            for (int n = 1; n < 11; n++)
            {
                width_P = Original.Width / K;
                height_P = Original.Height / K;
                _bitmap.Add(new Bitmap(width_P * K, height_P * K));
                width_P = 0;
                height_P = 0;
                while (width_O < _width && width_O + K < _width)
                {
                    while (height_O < _height && height_O + K < _height)
                    {
                        r = g = b = 0;
                        for (int m = width_O; m <= width_O + K; m++)
                        {
                            for (int l = height_O; l <= height_O + K; l++)
                            {
                                r += _pixel[m, l].R;
                                g += _pixel[m, l].G;
                                b += _pixel[m, l].B;
                            }
                        }
                        r /= (K + 1) * (K + 1);
                        g /= (K + 1) * (K + 1);
                        b /= (K + 1) * (K + 1);

                        for (int m = width_O; m < width_O + K; m++)
                        {
                            for (int l = height_O; l < height_O + K; l++)
                            {
                                _bitmap[n].SetPixel(m, l, Color.FromArgb(255, r, g, b));
                            }
                        }
                        height_P += K;
                        height_O += K;
                    }
                    width_P += K;
                    height_P = 0;
                    width_O += K;
                    height_O = 0;
                }
                width_O = 0;
                K = K * 4 / 3;
            }
        }
    }
}
