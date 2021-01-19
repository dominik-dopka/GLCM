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
    public partial class Form1 : Form
    {

        private List<String> imagePaths;
        private List<Bitmap> imagesBitmaps;
        private int imageIndex;

        public Form1()
        {
            InitializeComponent();

            pictureBox1.BackColor = Color.Black;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            nextImageButton.Enabled = false;
            previousImageButton.Enabled = false;
        }

        private void chooseImagesButton_Click(object sender, EventArgs e)
        {
            string message = "";

            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imagePaths = new List<String>();
                imagesBitmaps = new List<Bitmap>();
                imageIndex = 0;

                foreach (string file in openFileDialog1.FileNames)
                {
                    //imagePaths.Add(Path.GetFileName(file));
                    imagePaths.Add(file);
                    //message += Path.GetFileName(file) + " - " + file + Environment.NewLine;
                }

                foreach(string path in imagePaths)
                {
                    message += path + Environment.NewLine;
                    Bitmap bitmap = ImageProcessing.ConvertToBitmap(path);
                    bitmap = ImageProcessing.MakeGrayscale(bitmap);
                    //ImageProcessing.MakeGrayscaleSlow(bitmap);
                    imagesBitmaps.Add(bitmap);
                }
                MessageBox.Show(message);
                //pictureBox1.ImageLocation = imagePaths.First();

                label1.Text = imagesBitmaps.Count.ToString() + " images chosen";
                pictureBox1.Image = imagesBitmaps.First();

                if (imagePaths.Count > 1)
                    nextImageButton.Enabled = true;
            }
        }

        private void nextImageButton_Click(object sender, EventArgs e)
        {
            imageIndex += 1;
            //pictureBox1.ImageLocation = imagePaths[imageIndex];
            pictureBox1.Image = imagesBitmaps[imageIndex];

            if (imageIndex == imagePaths.Count - 1)
                nextImageButton.Enabled = false;

            previousImageButton.Enabled = true;
        }

        private void previousImageButton_Click(object sender, EventArgs e)
        {
            imageIndex -= 1;
            //pictureBox1.ImageLocation = imagePaths[imageIndex];
            pictureBox1.Image = imagesBitmaps[imageIndex];

            if (imageIndex == 0)
                previousImageButton.Enabled = false;

            nextImageButton.Enabled = true;
        }
    }
}
