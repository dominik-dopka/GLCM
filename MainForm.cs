using System;
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
        private List<String> fileNames;
        private List<String> imagePaths;
        private List<Bitmap> imagesBitmaps;
        private int imageIndex;
        private CSVData csv;

        private List<float[,]> averageNormalizedGLCMMatrix;
        private List<double> energyList;
        private List<double> entropyList;
        private List<double> correlationList;
        private List<double> inverseDifferenceMomentList;
        private List<double> inertiaList;

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
            showTableParametersButton.Enabled = false;
            showMatrixTableButton.Enabled = false;
            imageIndex = 0;

            fileNames = new List<String>();
            imagePaths = new List<String>();
            imagesBitmaps = new List<Bitmap>();
            csv = new CSVData();

            averageNormalizedGLCMMatrix = new List<float[,]>();
            energyList = new List<double>();
            entropyList = new List<double>();
            correlationList = new List<double>();
            inverseDifferenceMomentList = new List<double>();
            inertiaList = new List<double>();
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
                progressBarInit(openFileDialog1.FileNames.Length*2);

                for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
                {
                    imagePaths.Add(openFileDialog1.FileNames[i]);

                    //message += openFileDialog1.SafeFileNames[i] + Environment.NewLine;
                    Bitmap bitmap = ImageProcessing.ConvertToBitmap(openFileDialog1.FileNames[i]);
                    bitmap = ImageProcessing.MakeGrayscale(bitmap);
                    imagesBitmaps.Add(bitmap);

                    //csv.AddRow(openFileDialog1.SafeFileNames[i], openFileDialog1.FileNames[i]);
                    fileNames.Add(openFileDialog1.SafeFileNames[i]);

                    progressBar1.PerformStep();
                }

                label1.Text = imagesBitmaps.Count.ToString() + " images chosen";
                pictureBox1.BackColor = DefaultBackColor;
                pictureBox1.Image = imagesBitmaps.First();

                if (imagePaths.Count > 1)
                {
                    nextImageButton.Enabled = true;
                    showTableParametersButton.Enabled = true;
                    showMatrixTableButton.Enabled = true;
                }

                //MessageBox.Show(message);

                Calculations();

                energyValueLabel.Text = energyList.First().ToString();
                entropyValueLabel.Text = entropyList.First().ToString();
                correlationValueLabel.Text = correlationList.First().ToString();
                inverseDifferenceMomentValueLabel.Text = inverseDifferenceMomentList.First().ToString();
                inertiaValueLabel.Text = inertiaList.First().ToString();

                for (int i = 0; i < imagesBitmaps.Count; i++)
                {
                    csv.AddRow(fileNames[i], energyList[i], entropyList[i], correlationList[i],
                            inverseDifferenceMomentList[i], inertiaList[i]);
                }

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
                averageNormalizedGLCMMatrix.Add(normalizedGLCMMatrix);

                energyList.Add(AlgorithmGLCM.Energy(normalizedGLCMMatrix));
                entropyList.Add(AlgorithmGLCM.Entropy(normalizedGLCMMatrix));

                double mean = AlgorithmGLCM.MeanGLCM(normalizedGLCMMatrix);
                double variance2 = AlgorithmGLCM.Variance2(normalizedGLCMMatrix, mean);

                correlationList.Add(AlgorithmGLCM.Correlation(normalizedGLCMMatrix, mean, variance2));
                inverseDifferenceMomentList.Add(AlgorithmGLCM.InverseDifferenceMoment(normalizedGLCMMatrix));
                inertiaList.Add(AlgorithmGLCM.Inertia(normalizedGLCMMatrix));

                progressBar1.PerformStep();
            }
        }

        private void nextImageButton_Click(object sender, EventArgs e)
        {
            imageIndex += 1;
            pictureBox1.Image = imagesBitmaps[imageIndex];

            energyValueLabel.Text = energyList[imageIndex].ToString();
            entropyValueLabel.Text = entropyList[imageIndex].ToString();
            correlationValueLabel.Text = correlationList[imageIndex].ToString();
            inverseDifferenceMomentValueLabel.Text = inverseDifferenceMomentList[imageIndex].ToString();
            inertiaValueLabel.Text = inertiaList[imageIndex].ToString();

            if (imageIndex == imagePaths.Count - 1)
                nextImageButton.Enabled = false;

            previousImageButton.Enabled = true;
        }

        private void previousImageButton_Click(object sender, EventArgs e)
        {
            imageIndex -= 1;
            pictureBox1.Image = imagesBitmaps[imageIndex];

            energyValueLabel.Text = energyList[imageIndex].ToString();
            entropyValueLabel.Text = entropyList[imageIndex].ToString();
            correlationValueLabel.Text = correlationList[imageIndex].ToString();
            inverseDifferenceMomentValueLabel.Text = inverseDifferenceMomentList[imageIndex].ToString();
            inertiaValueLabel.Text = inertiaList[imageIndex].ToString();

            if (imageIndex == 0)
                previousImageButton.Enabled = false;

            nextImageButton.Enabled = true;
        }

        private void showTableParametersButton_Click(object sender, EventArgs e)
        {
            DataTableForm dataTableForm = new DataTableForm(csv, csv.getParametersDataTable(), "parameters.csv");
            dataTableForm.Show();
        }

        private void showMatrixTableButton_Click(object sender, EventArgs e)
        {
            csv.createMatrixTable(averageNormalizedGLCMMatrix[imageIndex]);
            DataTableForm dataTableForm = new DataTableForm(csv, csv.getMatrixDataTable(), "matrix.csv");
            dataTableForm.Show();
        }
    }
}
