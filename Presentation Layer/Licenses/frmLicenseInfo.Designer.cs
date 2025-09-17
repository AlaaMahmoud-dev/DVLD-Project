namespace DVLD_Project
{
    partial class frmLicenseInfo
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
            this.label26 = new System.Windows.Forms.Label();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.ctrlLicenseInfo1 = new DVLD_Project.ctrlLicenseInfo();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            this.SuspendLayout();
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Firebrick;
            this.label26.Location = new System.Drawing.Point(386, 130);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(300, 37);
            this.label26.TabIndex = 47;
            this.label26.Text = "Driver License Info";
            // 
            // pictureBox15
            // 
            this.pictureBox15.BackgroundImage = global::DVLD_Project.Properties.Resources.LicenseView_400;
            this.pictureBox15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox15.Location = new System.Drawing.Point(495, 12);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(100, 115);
            this.pictureBox15.TabIndex = 46;
            this.pictureBox15.TabStop = false;
            // 
            // ctrlLicenseInfo1
            // 
            this.ctrlLicenseInfo1.Location = new System.Drawing.Point(12, 159);
            this.ctrlLicenseInfo1.Name = "ctrlLicenseInfo1";
            this.ctrlLicenseInfo1.Size = new System.Drawing.Size(1082, 355);
            this.ctrlLicenseInfo1.TabIndex = 48;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Firebrick;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(984, 520);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 38);
            this.btnClose.TabIndex = 49;
            this.btnClose.Text = "       Close";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // frmLicenseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 567);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlLicenseInfo1);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.pictureBox15);
            this.Name = "frmLicenseInfo";
            this.Text = "License Info";
            this.Load += new System.EventHandler(this.frmLicenseInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.PictureBox pictureBox15;
        private ctrlLicenseInfo ctrlLicenseInfo1;
        private System.Windows.Forms.Button btnClose;
    }
}