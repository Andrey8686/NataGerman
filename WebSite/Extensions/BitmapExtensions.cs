using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;

namespace WebSite.Extensions
{
    public static class BitmapExtensions
    {



        public static void SetPiePiece(this Bitmap bmp, int area, double rate)
        {
            if ((int)rate == 0) return;
            
            var size = (int)((bmp.Width > bmp.Height ? bmp.Height : bmp.Width / 10) * rate);
            
            var s = new Size(size, size);
            var p = new Point(bmp.Width / 2 - s.Width / 2, bmp.Height / 2 - s.Height / 2);
            var rect = new Rectangle(p, s);


            using (var g = Graphics.FromImage(bmp))
            {
                switch (area)
                {
                    case 1:
                        g.FillPie(new SolidBrush(Color.FromArgb(234, 32, 49)), rect, -90, 45);
                        break;
                    case 2:
                        g.FillPie(new SolidBrush(Color.FromArgb(3, 17, 235)), rect, -45, 45);
                        break;
                    case 3:
                        g.FillPie(new SolidBrush(Color.FromArgb(10, 84, 8)), rect, 0, 45);
                        break;
                    case 4:
                        g.FillPie(new SolidBrush(Color.FromArgb(243, 120, 30)), rect, 45, 45);
                        break;
                    case 5:
                        g.FillPie(new SolidBrush(Color.FromArgb(23, 155, 195)), rect, 90, 45);
                        break;
                    case 6:
                        g.FillPie(new SolidBrush(Color.FromArgb(94, 15, 234)), rect, 135, 45);
                        break;
                    case 7:
                        g.FillPie(new SolidBrush(Color.FromArgb(229, 1, 236)), rect, 180, 45);
                        break;
                    case 8:
                        g.FillPie(new SolidBrush(Color.FromArgb(20, 126, 235)), rect, 225, 45);
                        break;
                }
            }

        }


    }
}