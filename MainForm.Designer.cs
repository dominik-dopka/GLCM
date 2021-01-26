
namespace GLCM
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chooseImagesButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.nextImageButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.previousImageButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.energyLabel = new System.Windows.Forms.Label();
            this.energyValueLabel = new System.Windows.Forms.Label();
            this.entropyLabel = new System.Windows.Forms.Label();
            this.correlationLabel = new System.Windows.Forms.Label();
            this.inverseDifferenceMoment = new System.Windows.Forms.Label();
            this.inertiaLabel = new System.Windows.Forms.Label();
            this.entropyValueLabel = new System.Windows.Forms.Label();
            this.correlationValueLabel = new System.Windows.Forms.Label();
            this.inverseDifferenceMomentValueLabel = new System.Windows.Forms.Label();
            this.inertiaValueLabel = new System.Windows.Forms.Label();
            this.tableButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // chooseImagesButton
            // 
            this.chooseImagesButton.Location = new System.Drawing.Point(27, 48);
            this.chooseImagesButton.Name = "chooseImagesButton";
            this.chooseImagesButton.Size = new System.Drawing.Size(94, 23);
            this.chooseImagesButton.TabIndex = 0;
            this.chooseImagesButton.Text = "Choose Images";
            this.chooseImagesButton.UseVisualStyleBackColor = true;
            this.chooseImagesButton.Click += new System.EventHandler(this.chooseImagesButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(168, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(259, 253);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // nextImageButton
            // 
            this.nextImageButton.Location = new System.Drawing.Point(352, 327);
            this.nextImageButton.Name = "nextImageButton";
            this.nextImageButton.Size = new System.Drawing.Size(75, 23);
            this.nextImageButton.TabIndex = 2;
            this.nextImageButton.Text = "Next";
            this.nextImageButton.UseVisualStyleBackColor = true;
            this.nextImageButton.Click += new System.EventHandler(this.nextImageButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // previousImageButton
            // 
            this.previousImageButton.Location = new System.Drawing.Point(168, 327);
            this.previousImageButton.Name = "previousImageButton";
            this.previousImageButton.Size = new System.Drawing.Size(75, 23);
            this.previousImageButton.TabIndex = 3;
            this.previousImageButton.Text = "Previous";
            this.previousImageButton.UseVisualStyleBackColor = true;
            this.previousImageButton.Click += new System.EventHandler(this.previousImageButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "0 images chosen";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(168, 289);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(259, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // energyLabel
            // 
            this.energyLabel.AutoSize = true;
            this.energyLabel.Location = new System.Drawing.Point(511, 23);
            this.energyLabel.Name = "energyLabel";
            this.energyLabel.Size = new System.Drawing.Size(55, 13);
            this.energyLabel.TabIndex = 6;
            this.energyLabel.Text = "ENERGY:";
            // 
            // energyValueLabel
            // 
            this.energyValueLabel.AutoSize = true;
            this.energyValueLabel.Location = new System.Drawing.Point(511, 48);
            this.energyValueLabel.Name = "energyValueLabel";
            this.energyValueLabel.Size = new System.Drawing.Size(13, 13);
            this.energyValueLabel.TabIndex = 7;
            this.energyValueLabel.Text = "0";
            // 
            // entropyLabel
            // 
            this.entropyLabel.AutoSize = true;
            this.entropyLabel.Location = new System.Drawing.Point(511, 74);
            this.entropyLabel.Name = "entropyLabel";
            this.entropyLabel.Size = new System.Drawing.Size(62, 13);
            this.entropyLabel.TabIndex = 8;
            this.entropyLabel.Text = "ENTROPY:";
            // 
            // correlationLabel
            // 
            this.correlationLabel.AutoSize = true;
            this.correlationLabel.Location = new System.Drawing.Point(511, 126);
            this.correlationLabel.Name = "correlationLabel";
            this.correlationLabel.Size = new System.Drawing.Size(87, 13);
            this.correlationLabel.TabIndex = 9;
            this.correlationLabel.Text = "CORRELATION:";
            // 
            // inverseDifferenceMoment
            // 
            this.inverseDifferenceMoment.AutoSize = true;
            this.inverseDifferenceMoment.Location = new System.Drawing.Point(511, 179);
            this.inverseDifferenceMoment.Name = "inverseDifferenceMoment";
            this.inverseDifferenceMoment.Size = new System.Drawing.Size(178, 13);
            this.inverseDifferenceMoment.TabIndex = 10;
            this.inverseDifferenceMoment.Text = "INVERSE DIFFERENCE MOMENT:";
            // 
            // inertiaLabel
            // 
            this.inertiaLabel.AutoSize = true;
            this.inertiaLabel.Location = new System.Drawing.Point(511, 241);
            this.inertiaLabel.Name = "inertiaLabel";
            this.inertiaLabel.Size = new System.Drawing.Size(53, 13);
            this.inertiaLabel.TabIndex = 11;
            this.inertiaLabel.Text = "INERTIA:";
            // 
            // entropyValueLabel
            // 
            this.entropyValueLabel.AutoSize = true;
            this.entropyValueLabel.Location = new System.Drawing.Point(511, 98);
            this.entropyValueLabel.Name = "entropyValueLabel";
            this.entropyValueLabel.Size = new System.Drawing.Size(13, 13);
            this.entropyValueLabel.TabIndex = 12;
            this.entropyValueLabel.Text = "0";
            // 
            // correlationValueLabel
            // 
            this.correlationValueLabel.AutoSize = true;
            this.correlationValueLabel.Location = new System.Drawing.Point(511, 152);
            this.correlationValueLabel.Name = "correlationValueLabel";
            this.correlationValueLabel.Size = new System.Drawing.Size(13, 13);
            this.correlationValueLabel.TabIndex = 13;
            this.correlationValueLabel.Text = "0";
            // 
            // inverseDifferenceMomentValueLabel
            // 
            this.inverseDifferenceMomentValueLabel.AutoSize = true;
            this.inverseDifferenceMomentValueLabel.Location = new System.Drawing.Point(511, 210);
            this.inverseDifferenceMomentValueLabel.Name = "inverseDifferenceMomentValueLabel";
            this.inverseDifferenceMomentValueLabel.Size = new System.Drawing.Size(13, 13);
            this.inverseDifferenceMomentValueLabel.TabIndex = 14;
            this.inverseDifferenceMomentValueLabel.Text = "0";
            // 
            // inertiaValueLabel
            // 
            this.inertiaValueLabel.AutoSize = true;
            this.inertiaValueLabel.Location = new System.Drawing.Point(511, 272);
            this.inertiaValueLabel.Name = "inertiaValueLabel";
            this.inertiaValueLabel.Size = new System.Drawing.Size(13, 13);
            this.inertiaValueLabel.TabIndex = 15;
            this.inertiaValueLabel.Text = "0";
            // 
            // tableButton
            // 
            this.tableButton.Location = new System.Drawing.Point(514, 326);
            this.tableButton.Name = "tableButton";
            this.tableButton.Size = new System.Drawing.Size(75, 23);
            this.tableButton.TabIndex = 16;
            this.tableButton.Text = "Show Table";
            this.tableButton.UseVisualStyleBackColor = true;
            this.tableButton.Click += new System.EventHandler(this.tableButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableButton);
            this.Controls.Add(this.inertiaValueLabel);
            this.Controls.Add(this.inverseDifferenceMomentValueLabel);
            this.Controls.Add(this.correlationValueLabel);
            this.Controls.Add(this.entropyValueLabel);
            this.Controls.Add(this.inertiaLabel);
            this.Controls.Add(this.inverseDifferenceMoment);
            this.Controls.Add(this.correlationLabel);
            this.Controls.Add(this.entropyLabel);
            this.Controls.Add(this.energyValueLabel);
            this.Controls.Add(this.energyLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.previousImageButton);
            this.Controls.Add(this.nextImageButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.chooseImagesButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button chooseImagesButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button nextImageButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button previousImageButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label energyLabel;
        private System.Windows.Forms.Label energyValueLabel;
        private System.Windows.Forms.Label entropyLabel;
        private System.Windows.Forms.Label correlationLabel;
        private System.Windows.Forms.Label inverseDifferenceMoment;
        private System.Windows.Forms.Label inertiaLabel;
        private System.Windows.Forms.Label entropyValueLabel;
        private System.Windows.Forms.Label correlationValueLabel;
        private System.Windows.Forms.Label inverseDifferenceMomentValueLabel;
        private System.Windows.Forms.Label inertiaValueLabel;
        private System.Windows.Forms.Button tableButton;
    }
}

