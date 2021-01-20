using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLCM
{
    class AlgorithmGLCM
    {
        /// <summary>
        /// Zwraca macierz obrazu po kwantyzacji
        /// </summary>
        /// <param name="inputBitmap">wejsciowa bitmapa (skala szarosci)</param>
        /// <param name="intervals">liczba przedziałów</param>
        /// <returns></returns>
        public int[,] Quantization(Bitmap inputBitmap, int intervals)
        {
            int treshold = 255 / intervals;
            int[,] outputMatrix = new int[inputBitmap.Height, inputBitmap.Width];

            for (int y = 0; y < inputBitmap.Height; y++)
            {
                for (int x = 0; x < inputBitmap.Width; x++)
                {
                    int pixelValue = inputBitmap.GetPixel(y, x).R;
                    int newValue = pixelValue / treshold;
                    outputMatrix[y, x] = newValue;
                }
            }
            return outputMatrix;
        }

        /// <summary>
        /// Obliczanie macierzy GLCM
        /// </summary>
        /// <param name="inputMatrix">tablica wejsciowa, kwantyzowany obraz</param>
        /// <param name="intervals">liczba przedzialow kwantyzacji</param>
        /// <param name="dirX">kierunek X z (x,y)</param>
        /// <param name="dirY">kierunek Y z (x,y)</param>
        /// <param name="numberOfElements">zwracana ilosc elementów (suma) w tablicy GLCM</param>
        /// <returns></returns>
        public int[,] CalculateGLCM(int[,] inputMatrix, int intervals, int dirX, int dirY, out int numberOfElements)
        {
            int[,] GLCMMatrix = new int[intervals, intervals];
            numberOfElements = 0;

            int y = (dirY < 0 ? -dirY : 0);
            int x = (dirX < 0 ? -dirX : 0);
            int endY = (dirY < 0 ? 0 : -dirY);
            int endX = (dirX < 0 ? 0 : -dirX);

            for (y = 0; y < inputMatrix.GetLength(0) - endY; y++)
            {
                for (x = 0; x < inputMatrix.GetLength(1) - endX; x++)
                {
                    int first = inputMatrix[y, x];
                    int second = inputMatrix[y + dirY, x + dirX];
                    GLCMMatrix[first, second] += 1;
                    GLCMMatrix[second, first] += 1;
                    numberOfElements += 2;
                }
            }
            return GLCMMatrix;
        }
        /// <summary>
        /// Zwraca znormalizowaną macierz GLCM
        /// </summary>
        /// <param name="inputMatrix">macierz GLCM</param>
        /// <param name="numberOfElements">liczba elementow (suma wszystkich pol macierzy GLCM, jesli 0, zostanie obliczona)</param>
        /// <returns></returns>
        public float[,] NormalizeGLCM(int[,] inputMatrix, int numberOfElements)
        {
            float[,] normalizedGLCMMatrix = new float[inputMatrix.GetLength(0), inputMatrix.GetLength(1)];

            if (numberOfElements <= 0)
            {
                numberOfElements = 0;
                for (int y = 0; y < inputMatrix.GetLength(0); y++)
                {
                    for (int x = 0; x < inputMatrix.GetLength(1); x++)
                    {
                        numberOfElements += inputMatrix[y, x];
                    }
                }
            }

            for (int y = 0; y < inputMatrix.GetLength(0); y++)
            {
                for (int x = 0; x < inputMatrix.GetLength(1); x++)
                {
                    normalizedGLCMMatrix[y, x] = (float)(inputMatrix[y, x] / numberOfElements);
                }
            }
            return normalizedGLCMMatrix;
        }
    }
}
