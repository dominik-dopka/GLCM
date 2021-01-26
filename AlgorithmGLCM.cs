using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLCM
{
    public static class AlgorithmGLCM
    {
        /// <summary>
        /// Zwraca macierz obrazu po kwantyzacji
        /// </summary>
        /// <param name="inputBitmap">wejsciowa bitmapa (skala szarosci)</param>
        /// <param name="intervals">liczba przedziałów</param>
        /// <returns>macierz obrazu po kwantyzacji (int[,])</returns>
        public static int[,] Quantization(Bitmap inputBitmap, int intervals)
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
        public static int[,] CalculateGLCM(int[,] inputMatrix, int intervals, int dirX, int dirY, out int numberOfElements)
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

        public static int[,] MatrixAveraging(List<int[,]> inputMatrixList, int intervals)
        {
            int[,] outputMatrix = new int[intervals, intervals];

            for (int i = 0; i < intervals; i++)
            {
                for (int j = 0; j < intervals; j++)
                {
                    outputMatrix[i, j] = (inputMatrixList[0][i, j] + inputMatrixList[1][i, j] + 
                        inputMatrixList[2][i, j] + inputMatrixList[3][i, j]) / 4;
                }
            }

            return outputMatrix;
        }

        /// <summary>
        /// Zwraca znormalizowaną macierz GLCM
        /// </summary>
        /// <param name="inputMatrix">macierz GLCM</param>
        /// <param name="numberOfElements">liczba elementow (suma wszystkich pol macierzy GLCM, jesli 0, zostanie obliczona)</param>
        /// <returns></returns>
        public static float[,] NormalizeGLCM(int[,] inputMatrix, int numberOfElements)
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

        public static double Energy(float[,] normalizedGLCM)
        {
            double energy = 0;
            for (int y = 0; y < normalizedGLCM.GetLength(0); y++)
            {
                for (int x = 0; x < normalizedGLCM.GetLength(1); x++)
                {
                    energy += Math.Pow(normalizedGLCM[y, x], 2);
                }
            }
            return energy;
        }

        public static double Entropy(float[,] normalizedGLCM)
        {
            double entropy = 0;
            for (int y = 0; y < normalizedGLCM.GetLength(0); y++)
            {
                for (int x = 0; x < normalizedGLCM.GetLength(1); x++)
                {
                    entropy += normalizedGLCM[y, x] * Math.Log(normalizedGLCM[y, x]);
                }
            }
            return -entropy;
        }

        /// <summary>
        /// Metoda obliczająca korelację macierzy GLCM
        /// (Na podstawie: https://support.echoview.com/WebHelp/Windows_and_Dialog_Boxes/Dialog_Boxes/Variable_properties_dialog_box/Operator_pages/GLCM_Texture_Features.htm#Correlation)
        /// Względem sprawozdania, zmieniło się użycie kwadratu wariancji zamiast wariancji po x i y oraz brak mnożenia w liczniku przez zawartość danej komórki.
        /// </summary>
        /// <param name="normalizedGLCM">Znormalizowana macierz GLCM</param>
        /// <param name="mean">Srednia z normalizowanej macierzy GLCM. Jeśli NaN, zostanie obliczona automatycznie</param>
        /// <param name="mean">Wariancja z normalizowanej macierzy GLCM. Jeśli NaN, zostanie obliczona automatycznie</param>
        /// <returns>Korelacja macierzy GLCM</returns>
        public static double Correlation(float[,] normalizedGLCM, double mean, double variance2)
        {
            double correlation = 0;

            if (Double.IsNaN(mean))
            {
                mean = MeanGLCM(normalizedGLCM);
            }

            if (Double.IsNaN(variance2))
            {
                variance2 = Variance2(normalizedGLCM, mean);
            }

            for (int y = 0; y < normalizedGLCM.GetLength(0); y++)
            {
                for (int x = 0; x < normalizedGLCM.GetLength(1); x++)
                {
                    correlation += ((y - mean) * (x - mean)) / variance2;
                }
            }
            return correlation;
        }

        /// <summary>
        /// Oblicza Inverse Difference Moment macierzy GLCM
        /// </summary>
        /// <param name="normalizedGLCM">Znormalizowana macierz GLCM</param>
        /// <returns>Inverse Difference Moment macierzy GLCM</returns>
        public static double InverseDifferenceMoment(float[,] normalizedGLCM)
        {
            double idm = 0;
            for (int y = 0; y < normalizedGLCM.GetLength(0); y++)
            {
                for (int x = 0; x < normalizedGLCM.GetLength(1); x++)
                {
                    idm += (1 / (1 + Math.Pow(y - x, 2))) * normalizedGLCM[y, x];
                }
            }
            return idm;
        }

        /// <summary>
        /// Oblicza parametr Inertia
        /// </summary>
        /// <param name="normalizedGLCM">Znormalizowana macierz GLCM</param>
        /// <returns>Inertia</returns>
        public static double Inertia(float[,] normalizedGLCM)
        {
            double inertia = 0;
            for (int y = 0; y < normalizedGLCM.GetLength(0); y++)
            {
                for (int x = 0; x < normalizedGLCM.GetLength(1); x++)
                {
                    inertia += Math.Pow(y - x, 2) * normalizedGLCM[y, x];
                }
            }
            return inertia;
        }

        /// <summary>
        /// Obliczanie średniej wartości znormalizowanej macierzy GLCM
        /// </summary>
        /// <param name="normalizedGLCM">Znormalizowana macierz GLCM</param>
        /// <returns>Średnia wartość macierzy GLCM</returns>
        public static double MeanGLCM(float[,] normalizedGLCM)
        {
            double mean = 0;
            for (int y = 0; y < normalizedGLCM.GetLength(0); y++)
            {
                for (int x = 0; x < normalizedGLCM.GetLength(1); x++)
                {
                    mean += y * normalizedGLCM[y, x];
                }
            }
            return mean;
        }

        /// <summary>
        /// Zwraca wariancję kwadrat macierzy GLCM
        /// </summary>
        /// <param name="normalizedGLCM">Znormalizowana macierz GLCM</param>
        /// <param name="mean">Srednia z normalizowanej macierzy GLCM. Jeśli NaN, zostanie obliczona automatycznie</param>
        /// <returns></returns>
        public static double Variance2(float[,] normalizedGLCM, double mean)
        {
            double variance2 = 0;

            if (Double.IsNaN(mean))
            {
                mean = MeanGLCM(normalizedGLCM);
            }

            for (int y = 0; y < normalizedGLCM.GetLength(0); y++)
            {
                for (int x = 0; x < normalizedGLCM.GetLength(1); x++)
                {
                    variance2 += normalizedGLCM[y, x] * Math.Pow(y - mean, 2);
                }
            }
            return variance2;
        }
    }
}
