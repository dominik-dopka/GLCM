﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GLCM
{
    public partial class MainForm : Form
    {

        private List<String> imagePaths;
        private List<Bitmap> imagesBitmaps;
        private int imageIndex;
        private CSVData csv;

        private List<double> energies;

        public MainForm()
        {
            InitializeComponent();


            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            InitializeValues();
        }

        public void InitializeValues()
        {
            pictureBox1.BackColor = Color.Black;
            nextImageButton.Enabled = false;
            previousImageButton.Enabled = false;
            imageIndex = 0;

            imagePaths = new List<String>();
            imagesBitmaps = new List<Bitmap>();
            csv = new CSVData();

            energies = new List<double>();
        }

        private void progressBarInit(int maximumValue)
        {
            progressBar1.Visible = true;
            progressBar1.Minimum = 1;
            progressBar1.Maximum = maximumValue;
            progressBar1.Value = 1;
            progressBar1.Step = 1;
        }

        private void chooseImagesButton_Click(object sender, EventArgs e)
        {
            string message = "";

            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                InitializeValues();
                progressBarInit(openFileDialog1.FileNames.Length);

                for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
                {
                    imagePaths.Add(openFileDialog1.FileNames[i]);

                    //message += openFileDialog1.SafeFileNames[i] + Environment.NewLine;
                    Bitmap bitmap = ImageProcessing.ConvertToBitmap(openFileDialog1.FileNames[i]);
                    bitmap = ImageProcessing.MakeGrayscale(bitmap);
                    imagesBitmaps.Add(bitmap);

                    csv.AddRow(openFileDialog1.SafeFileNames[i], openFileDialog1.FileNames[i]);

                    progressBar1.PerformStep();
                }

                label1.Text = imagesBitmaps.Count.ToString() + " images chosen";
                pictureBox1.BackColor = DefaultBackColor;
                pictureBox1.Image = imagesBitmaps.First();

                if (imagePaths.Count > 1)
                    nextImageButton.Enabled = true;

                //MessageBox.Show(message);
                DataTableForm dataTableForm = new DataTableForm(csv);
                dataTableForm.Show();


                Calculations();
                energyValueLabel.Text = energies.First().ToString();



                //csv.ExportToCSV("test.csv");
            }
        }

        private void Calculations()
        {
            for (int i = 0; i < imagesBitmaps.Count; i++) {
                int intervals = 8;
                int[,] matrix = AlgorithmGLCM.Quantization(imagesBitmaps[i], intervals);
                List<int[,]> GLCMList = new List<int[,]>();
                int[] numberOfElements = new int[4];
                GLCMList.Add(AlgorithmGLCM.CalculateGLCM(matrix, intervals, 1, 0, out numberOfElements[0]));
                GLCMList.Add(AlgorithmGLCM.CalculateGLCM(matrix, intervals, 0, 1, out numberOfElements[1]));
                GLCMList.Add(AlgorithmGLCM.CalculateGLCM(matrix, intervals, 1, 1, out numberOfElements[2]));
                GLCMList.Add(AlgorithmGLCM.CalculateGLCM(matrix, intervals, -1, 1, out numberOfElements[3]));

                int[,] matrixAverage = AlgorithmGLCM.MatrixAveraging(GLCMList, intervals);
                float[,] normalizedGLCMMatrix = AlgorithmGLCM.NormalizeGLCM(matrixAverage, 0);

                energies.Add(AlgorithmGLCM.Energy(normalizedGLCMMatrix));
            }
        }

        private void nextImageButton_Click(object sender, EventArgs e)
        {
            imageIndex += 1;
            pictureBox1.Image = imagesBitmaps[imageIndex];

            if (imageIndex == imagePaths.Count - 1)
                nextImageButton.Enabled = false;

            previousImageButton.Enabled = true;
        }

        private void previousImageButton_Click(object sender, EventArgs e)
        {
            imageIndex -= 1;
            pictureBox1.Image = imagesBitmaps[imageIndex];

            if (imageIndex == 0)
                previousImageButton.Enabled = false;

            nextImageButton.Enabled = true;
        }
    }
}
