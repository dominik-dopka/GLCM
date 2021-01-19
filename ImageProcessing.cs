using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GLCM
{
    public static class ImageProcessing
    {
        public static Bitmap ConvertToBitmap(string fileName)
        {
            Bitmap bitmap;

            using (Stream bmpStream = File.Open(fileName, System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);

                bitmap = new Bitmap(image);
            }

            return bitmap;
        }

        public static Bitmap MakeGrayscale(Bitmap image)
        {
            Bitmap grayscaleImage = new Bitmap(image.Width, image.Height);
            Graphics graphics = Graphics.FromImage(grayscaleImage);

            ColorMatrix colorMatrix = new ColorMatrix(
               new[]
               {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
               });

            
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);

            graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 
                0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);

            graphics.Dispose();

            return grayscaleImage;
        }

        public static Bitmap MakeGrayscaleSlow (Bitmap image)
        {
            Color pixel;

            Bitmap newImage = image;

            for (int y = 0; y < newImage.Height; y++)
            {
                for (int x = 0; x < newImage.Width; x++)
                {
                    pixel = newImage.GetPixel(x, y);

                    int average = (pixel.R + pixel.G + pixel.B) / 3;

                    newImage.SetPixel(x, y, Color.FromArgb(pixel.A, average, average, average));
                }
            }

            return newImage;
        }
    }
}
