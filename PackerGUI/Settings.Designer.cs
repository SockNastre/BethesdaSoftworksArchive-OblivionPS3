namespace PackerGUI
{
    partial class Settings
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
            this.groupBoxArchiveSettings = new System.Windows.Forms.GroupBox();
            this.checkBoxUsePS3FileFlags = new System.Windows.Forms.CheckBox();
            this.checkBoxCompressAssets = new System.Windows.Forms.CheckBox();
            this.buttonResetSettings = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxDirectDrawSurfaceSettings = new System.Windows.Forms.GroupBox();
            this.checkBoxConvertNormalMaps = new System.Windows.Forms.CheckBox();
            this.checkBoxExtendDDS = new System.Windows.Forms.CheckBox();
            this.groupBoxArchiveSettings.SuspendLayout();
            this.groupBoxDirectDrawSurfaceSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxArchiveSettings
            // 
            this.groupBoxArchiveSettings.Controls.Add(this.checkBoxUsePS3FileFlags);
            this.groupBoxArchiveSettings.Controls.Add(this.checkBoxCompressAssets);
            this.groupBoxArchiveSettings.Location = new System.Drawing.Point(14, 10);
            this.groupBoxArchiveSettings.Name = "groupBoxArchiveSettings";
            this.groupBoxArchiveSettings.Size = new System.Drawing.Size(378, 71);
            this.groupBoxArchiveSettings.TabIndex = 4;
            this.groupBoxArchiveSettings.TabStop = false;
            this.groupBoxArchiveSettings.Text = "Archive Settings";
            // 
            // checkBoxUsePS3FileFlags
            // 
            this.checkBoxUsePS3FileFlags.AutoSize = true;
            this.checkBoxUsePS3FileFlags.Checked = true;
            this.checkBoxUsePS3FileFlags.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUsePS3FileFlags.Location = new System.Drawing.Point(10, 42);
            this.checkBoxUsePS3FileFlags.Name = "checkBoxUsePS3FileFlags";
            this.checkBoxUsePS3FileFlags.Size = new System.Drawing.Size(159, 17);
            this.checkBoxUsePS3FileFlags.TabIndex = 3;
            this.checkBoxUsePS3FileFlags.Text = "Use PS3 file flags (0xCDCD)";
            this.checkBoxUsePS3FileFlags.UseVisualStyleBackColor = true;
            // 
            // checkBoxCompressAssets
            // 
            this.checkBoxCompressAssets.AutoSize = true;
            this.checkBoxCompressAssets.Location = new System.Drawing.Point(10, 19);
            this.checkBoxCompressAssets.Name = "checkBoxCompressAssets";
            this.checkBoxCompressAssets.Size = new System.Drawing.Size(105, 17);
            this.checkBoxCompressAssets.TabIndex = 1;
            this.checkBoxCompressAssets.Text = "Compress assets";
            this.checkBoxCompressAssets.UseVisualStyleBackColor = true;
            // 
            // buttonResetSettings
            // 
            this.buttonResetSettings.Location = new System.Drawing.Point(12, 171);
            this.buttonResetSettings.Name = "buttonResetSettings";
            this.buttonResetSettings.Size = new System.Drawing.Size(105, 23);
            this.buttonResetSettings.TabIndex = 6;
            this.buttonResetSettings.Text = "Reset to Default";
            this.buttonResetSettings.UseVisualStyleBackColor = true;
            this.buttonResetSettings.Click += new System.EventHandler(this.buttonResetSettings_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(317, 171);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBoxDirectDrawSurfaceSettings
            // 
            this.groupBoxDirectDrawSurfaceSettings.Controls.Add(this.checkBoxConvertNormalMaps);
            this.groupBoxDirectDrawSurfaceSettings.Controls.Add(this.checkBoxExtendDDS);
            this.groupBoxDirectDrawSurfaceSettings.Location = new System.Drawing.Point(14, 87);
            this.groupBoxDirectDrawSurfaceSettings.Name = "groupBoxDirectDrawSurfaceSettings";
            this.groupBoxDirectDrawSurfaceSettings.Size = new System.Drawing.Size(378, 71);
            this.groupBoxDirectDrawSurfaceSettings.TabIndex = 7;
            this.groupBoxDirectDrawSurfaceSettings.TabStop = false;
            this.groupBoxDirectDrawSurfaceSettings.Text = "DirectDraw Surface (.DDS) Settings";
            // 
            // checkBoxConvertNormalMaps
            // 
            this.checkBoxConvertNormalMaps.AutoSize = true;
            this.checkBoxConvertNormalMaps.Checked = true;
            this.checkBoxConvertNormalMaps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxConvertNormalMaps.Location = new System.Drawing.Point(10, 42);
            this.checkBoxConvertNormalMaps.Name = "checkBoxConvertNormalMaps";
            this.checkBoxConvertNormalMaps.Size = new System.Drawing.Size(183, 17);
            this.checkBoxConvertNormalMaps.TabIndex = 2;
            this.checkBoxConvertNormalMaps.Text = "Convert normal maps (PC -> PS3)";
            this.checkBoxConvertNormalMaps.UseVisualStyleBackColor = true;
            // 
            // checkBoxExtendDDS
            // 
            this.checkBoxExtendDDS.AutoSize = true;
            this.checkBoxExtendDDS.Checked = true;
            this.checkBoxExtendDDS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxExtendDDS.Location = new System.Drawing.Point(10, 19);
            this.checkBoxExtendDDS.Name = "checkBoxExtendDDS";
            this.checkBoxExtendDDS.Size = new System.Drawing.Size(83, 17);
            this.checkBoxExtendDDS.TabIndex = 1;
            this.checkBoxExtendDDS.Text = "Extend data";
            this.checkBoxExtendDDS.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 202);
            this.Controls.Add(this.groupBoxDirectDrawSurfaceSettings);
            this.Controls.Add(this.groupBoxArchiveSettings);
            this.Controls.Add(this.buttonResetSettings);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Settings_HelpButtonClicked);
            this.groupBoxArchiveSettings.ResumeLayout(false);
            this.groupBoxArchiveSettings.PerformLayout();
            this.groupBoxDirectDrawSurfaceSettings.ResumeLayout(false);
            this.groupBoxDirectDrawSurfaceSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxArchiveSettings;
        private System.Windows.Forms.CheckBox checkBoxCompressAssets;
        private System.Windows.Forms.Button buttonResetSettings;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBoxDirectDrawSurfaceSettings;
        private System.Windows.Forms.CheckBox checkBoxExtendDDS;
        private System.Windows.Forms.CheckBox checkBoxConvertNormalMaps;
        private System.Windows.Forms.CheckBox checkBoxUsePS3FileFlags;
    }
}