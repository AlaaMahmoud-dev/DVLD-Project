namespace DVLD_Project
{
    partial class frmShowLicensesHistory
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
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlFindPersonByFilter1 = new DVLD_Project.ctrlFindPersonByFilter();
            this.ctrlPersonLicensesHistory1 = new DVLD_Project.Licenses.ctrlPersonLicensesHistory();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            this.SuspendLayout();
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Firebrick;
            this.label26.Location = new System.Drawing.Point(396, 19);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(249, 37);
            this.label26.TabIndex = 49;
            this.label26.Text = "License History";
            // 
            // pictureBox15
            // 
            this.pictureBox15.BackgroundImage = global::DVLD_Project.Properties.Resources.PersonLicenseHistory_512;
            this.pictureBox15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox15.Location = new System.Drawing.Point(1, 167);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(214, 202);
            this.pictureBox15.TabIndex = 48;
            this.pictureBox15.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Firebrick;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(943, 771);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 38);
            this.btnClose.TabIndex = 54;
            this.btnClose.Text = "       Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlFindPersonByFilter1
            // 
            this.ctrlFindPersonByFilter1.cbFilterSelectedItem = "";
            this.ctrlFindPersonByFilter1.FilterEnabled = true;
            this.ctrlFindPersonByFilter1.Location = new System.Drawing.Point(221, 59);
            this.ctrlFindPersonByFilter1.Name = "ctrlFindPersonByFilter1";
            this.ctrlFindPersonByFilter1.Size = new System.Drawing.Size(833, 384);
            this.ctrlFindPersonByFilter1.TabIndex = 52;
            this.ctrlFindPersonByFilter1.txtFilterValue = "";
            this.ctrlFindPersonByFilter1.OnPersonSelected += new System.Action<int>(this.ctrlFindPersonByFilter1_OnPersonSelected);
            // 
            // ctrlPersonLicensesHistory1
            // 
            this.ctrlPersonLicensesHistory1.Location = new System.Drawing.Point(10, 449);
            this.ctrlPersonLicensesHistory1.Name = "ctrlPersonLicensesHistory1";
            this.ctrlPersonLicensesHistory1.Size = new System.Drawing.Size(1044, 316);
            this.ctrlPersonLicensesHistory1.TabIndex = 55;
            // 
            // frmShowLicensesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 821);
            this.Controls.Add(this.ctrlPersonLicensesHistory1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlFindPersonByFilter1);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.pictureBox15);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowLicensesHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmShowLicensesHistory";
            this.Load += new System.EventHandler(this.frmShowLicensesHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.PictureBox pictureBox15;
        private ctrlFindPersonByFilter ctrlFindPersonByFilter1;
        private System.Windows.Forms.Button btnClose;
        private Licenses.ctrlPersonLicensesHistory ctrlPersonLicensesHistory1;
    }
}