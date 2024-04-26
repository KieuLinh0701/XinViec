using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinViec
{
    internal class ImageComparer
    {
        public static bool ImagesAreEqual(Image img1, Image img2)
        {
            if (img1 == null || img2 == null)
                return false;

            if (img1.Width != img2.Width || img1.Height != img2.Height)
                return false;

            using (var bmp1 = new Bitmap(img1))
            using (var bmp2 = new Bitmap(img2))
            {
                for (int x = 0; x < bmp1.Width; x++)
                {
                    for (int y = 0; y < bmp1.Height; y++)
                    {
                        if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                            return false;
                    }
                }
            }

            return true;
        }
    }
}
