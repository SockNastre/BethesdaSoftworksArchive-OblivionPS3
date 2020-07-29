namespace PackerGUI
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.richTextBoxCredits = new System.Windows.Forms.RichTextBox();
            this.labelAboutInformation = new System.Windows.Forms.Label();
            this.pictureBoxApplicationIcon = new System.Windows.Forms.PictureBox();
            this.buttonOkay = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxApplicationIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxCredits
            // 
            this.richTextBoxCredits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxCredits.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxCredits.Location = new System.Drawing.Point(11, 93);
            this.richTextBoxCredits.Name = "richTextBoxCredits";
            this.richTextBoxCredits.ReadOnly = true;
            this.richTextBoxCredits.Size = new System.Drawing.Size(400, 228);
            this.richTextBoxCredits.TabIndex = 35;
            this.richTextBoxCredits.Text = resources.GetString("richTextBoxCredits.Text");
            // 
            // labelAboutInformation
            // 
            this.labelAboutInformation.AutoSize = true;
            this.labelAboutInformation.Location = new System.Drawing.Point(81, 12);
            this.labelAboutInformation.Name = "labelAboutInformation";
            this.labelAboutInformation.Size = new System.Drawing.Size(233, 39);
            this.labelAboutInformation.TabIndex = 34;
            this.labelAboutInformation.Text = "BethesdaSoftworksArchive OblivionPS3 Packer\r\nVersion: 1.0.0.0\r\nCopyright©  2020  " +
    "SockNastre";
            // 
            // pictureBoxApplicationIcon
            // 
            this.pictureBoxApplicationIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxApplicationIcon.Location = new System.Drawing.Point(11, 12);
            this.pictureBoxApplicationIcon.Name = "pictureBoxApplicationIcon";
            this.pictureBoxApplicationIcon.Size = new System.Drawing.Size(64, 68);
            this.pictureBoxApplicationIcon.TabIndex = 33;
            this.pictureBoxApplicationIcon.TabStop = false;
            // 
            // buttonOkay
            // 
            this.buttonOkay.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOkay.Location = new System.Drawing.Point(337, 327);
            this.buttonOkay.Name = "buttonOkay";
            this.buttonOkay.Size = new System.Drawing.Size(75, 23);
            this.buttonOkay.TabIndex = 32;
            this.buttonOkay.Text = "OK";
            this.buttonOkay.UseVisualStyleBackColor = true;
            this.buttonOkay.Click += new System.EventHandler(this.buttonOkay_Click);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 358);
            this.Controls.Add(this.richTextBoxCredits);
            this.Controls.Add(this.labelAboutInformation);
            this.Controls.Add(this.pictureBoxApplicationIcon);
            this.Controls.Add(this.buttonOkay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About BethesdaSoftworksArchive OblivionPS3 Packer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxApplicationIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxCredits;
        private System.Windows.Forms.Label labelAboutInformation;
        private System.Windows.Forms.PictureBox pictureBoxApplicationIcon;
        private System.Windows.Forms.Button buttonOkay;
    }
}