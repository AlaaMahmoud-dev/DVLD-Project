namespace DVLD_Project
{
    partial class frmShowLDLApplicationDetails
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
            this.ctrlLDLAndBasicApplicationsDetails1 = new DVLD_Project.ctrlLDLAndBasicApplicationsDetails();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlLDLAndBasicApplicationsDetails1
            // 
            this.ctrlLDLAndBasicApplicationsDetails1.Location = new System.Drawing.Point(11, 12);
            this.ctrlLDLAndBasicApplicationsDetails1.Name = "ctrlLDLAndBasicApplicationsDetails1";
            this.ctrlLDLAndBasicApplicationsDetails1.Size = new System.Drawing.Size(1017, 383);
            this.ctrlLDLAndBasicApplicationsDetails1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Firebrick;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(918, 401);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 38);
            this.btnClose.TabIndex = 46;
            this.btnClose.Text = "       Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmShowLDLApplicationDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 443);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlLDLAndBasicApplicationsDetails1);
            this.Name = "frmShowLDLApplicationDetails";
            this.Text = "Local Driving License Application Details";
            this.Load += new System.EventHandler(this.frmShowLDLApplicationDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlLDLAndBasicApplicationsDetails ctrlLDLAndBasicApplicationsDetails1;
        private System.Windows.Forms.Button btnClose;
    }
}