
namespace YetiFTPClient
{
    partial class TransferScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferScreen));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SelectedBench = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TransferCount = new System.Windows.Forms.Label();
            this.CrossIcon = new System.Windows.Forms.PictureBox();
            this.TickIcon = new System.Windows.Forms.PictureBox();
            this.UploadingGIF = new System.Windows.Forms.PictureBox();
            this.UploadIcon = new System.Windows.Forms.PictureBox();
            this.TransferLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.IPLabel = new System.Windows.Forms.Label();
            this.HelpLabel = new System.Windows.Forms.Label();
            this.HelpButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CrossIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TickIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UploadingGIF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UploadIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(147, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(265, 135);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-6, -6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(559, 153);
            this.panel1.TabIndex = 3;
            // 
            // SelectedBench
            // 
            this.SelectedBench.AutoSize = true;
            this.SelectedBench.Font = new System.Drawing.Font("Raleway Medium", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SelectedBench.Location = new System.Drawing.Point(19, 170);
            this.SelectedBench.Name = "SelectedBench";
            this.SelectedBench.Size = new System.Drawing.Size(174, 29);
            this.SelectedBench.TabIndex = 0;
            this.SelectedBench.Text = "SmartBench: ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.TransferCount);
            this.panel2.Controls.Add(this.CrossIcon);
            this.panel2.Controls.Add(this.TickIcon);
            this.panel2.Controls.Add(this.UploadingGIF);
            this.panel2.Controls.Add(this.UploadIcon);
            this.panel2.Controls.Add(this.TransferLabel);
            this.panel2.Location = new System.Drawing.Point(19, 202);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(497, 299);
            this.panel2.TabIndex = 4;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // TransferCount
            // 
            this.TransferCount.Font = new System.Drawing.Font("Raleway", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TransferCount.Location = new System.Drawing.Point(47, 240);
            this.TransferCount.Name = "TransferCount";
            this.TransferCount.Size = new System.Drawing.Size(403, 47);
            this.TransferCount.TabIndex = 11;
            this.TransferCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CrossIcon
            // 
            this.CrossIcon.BackColor = System.Drawing.Color.Transparent;
            this.CrossIcon.Image = ((System.Drawing.Image)(resources.GetObject("CrossIcon.Image")));
            this.CrossIcon.Location = new System.Drawing.Point(213, 82);
            this.CrossIcon.Name = "CrossIcon";
            this.CrossIcon.Size = new System.Drawing.Size(64, 64);
            this.CrossIcon.TabIndex = 10;
            this.CrossIcon.TabStop = false;
            this.CrossIcon.Visible = false;
            // 
            // TickIcon
            // 
            this.TickIcon.BackColor = System.Drawing.Color.Transparent;
            this.TickIcon.Image = ((System.Drawing.Image)(resources.GetObject("TickIcon.Image")));
            this.TickIcon.Location = new System.Drawing.Point(213, 82);
            this.TickIcon.Name = "TickIcon";
            this.TickIcon.Size = new System.Drawing.Size(64, 64);
            this.TickIcon.TabIndex = 9;
            this.TickIcon.TabStop = false;
            this.TickIcon.Visible = false;
            // 
            // UploadingGIF
            // 
            this.UploadingGIF.BackColor = System.Drawing.Color.Transparent;
            this.UploadingGIF.Image = ((System.Drawing.Image)(resources.GetObject("UploadingGIF.Image")));
            this.UploadingGIF.Location = new System.Drawing.Point(213, 82);
            this.UploadingGIF.Name = "UploadingGIF";
            this.UploadingGIF.Size = new System.Drawing.Size(64, 64);
            this.UploadingGIF.TabIndex = 8;
            this.UploadingGIF.TabStop = false;
            this.UploadingGIF.Visible = false;
            // 
            // UploadIcon
            // 
            this.UploadIcon.Image = ((System.Drawing.Image)(resources.GetObject("UploadIcon.Image")));
            this.UploadIcon.Location = new System.Drawing.Point(183, 55);
            this.UploadIcon.Name = "UploadIcon";
            this.UploadIcon.Size = new System.Drawing.Size(130, 91);
            this.UploadIcon.TabIndex = 1;
            this.UploadIcon.TabStop = false;
            // 
            // TransferLabel
            // 
            this.TransferLabel.Font = new System.Drawing.Font("Raleway", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TransferLabel.Location = new System.Drawing.Point(47, 112);
            this.TransferLabel.Name = "TransferLabel";
            this.TransferLabel.Size = new System.Drawing.Size(403, 138);
            this.TransferLabel.TabIndex = 0;
            this.TransferLabel.Text = "Drag and drop your files here";
            this.TransferLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1000, 428);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "label2";
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Font = new System.Drawing.Font("Raleway", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IPLabel.Location = new System.Drawing.Point(343, 520);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(111, 25);
            this.IPLabel.TabIndex = 7;
            this.IPLabel.Text = "IP Address";
            // 
            // HelpLabel
            // 
            this.HelpLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.HelpLabel.AutoSize = true;
            this.HelpLabel.Font = new System.Drawing.Font("Raleway", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.HelpLabel.Location = new System.Drawing.Point(53, 520);
            this.HelpLabel.Name = "HelpLabel";
            this.HelpLabel.Size = new System.Drawing.Size(185, 25);
            this.HelpLabel.TabIndex = 9;
            this.HelpLabel.Text = "Click here for help";
            this.HelpLabel.Click += new System.EventHandler(this.HelpLabel_Click);
            // 
            // HelpButton
            // 
            this.HelpButton.BackColor = System.Drawing.Color.White;
            this.HelpButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("HelpButton.BackgroundImage")));
            this.HelpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.HelpButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HelpButton.FlatAppearance.BorderSize = 0;
            this.HelpButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.HelpButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.HelpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HelpButton.ForeColor = System.Drawing.Color.Transparent;
            this.HelpButton.Location = new System.Drawing.Point(12, 514);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(35, 35);
            this.HelpButton.TabIndex = 8;
            this.HelpButton.UseVisualStyleBackColor = true;
            this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // TransferScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(534, 561);
            this.Controls.Add(this.HelpLabel);
            this.Controls.Add(this.HelpButton);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.SelectedBench);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(550, 600);
            this.MinimumSize = new System.Drawing.Size(550, 600);
            this.Name = "TransferScreen";
            this.Text = "Yeti Tool SmartTransfer | Transfer Your Files";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CrossIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TickIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UploadingGIF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UploadIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label SelectedBench;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label TransferLabel;
        private System.Windows.Forms.PictureBox UploadIcon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Label HelpLabel;
        private System.Windows.Forms.Button HelpButton;
        private System.Windows.Forms.PictureBox UploadingGIF;
        private System.Windows.Forms.PictureBox TickIcon;
        private System.Windows.Forms.PictureBox CrossIcon;
        private System.Windows.Forms.Label TransferCount;
    }
}