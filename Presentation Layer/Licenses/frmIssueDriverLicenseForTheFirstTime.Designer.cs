namespace DVLD_Project
{
    partial class frmIssueDriverLicenseForTheFirstTime
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
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnIssue = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlLDLAndBasicApplicationsDetails1
            // 
            this.ctrlLDLAndBasicApplicationsDetails1.Location = new System.Drawing.Point(2, 3);
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
            this.btnClose.Location = new System.Drawing.Point(786, 567);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 38);
            this.btnClose.TabIndex = 83;
            this.btnClose.Text = "       Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(174, 407);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(845, 154);
            this.txtNotes.TabIndex = 82;
            // 
            // btnIssue
            // 
            this.btnIssue.BackColor = System.Drawing.Color.White;
            this.btnIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIssue.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnIssue.Image = global::DVLD_Project.Properties.Resources.IssueDrivingLicense_32;
            this.btnIssue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssue.Location = new System.Drawing.Point(902, 567);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(117, 38);
            this.btnIssue.TabIndex = 79;
            this.btnIssue.Text = "      Issue";
            this.btnIssue.UseVisualStyleBackColor = false;
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(44, 406);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 18);
            this.label5.TabIndex = 80;
            this.label5.Text = "Notes:";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DVLD_Project.Properties.Resources.Notes_32;
            this.pictureBox4.Location = new System.Drawing.Point(115, 407);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(32, 32);
            this.pictureBox4.TabIndex = 81;
            this.pictureBox4.TabStop = false;
            // 
            // frmIssueDriverLicenseForTheFirstTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 608);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.btnIssue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.ctrlLDLAndBasicApplicationsDetails1);
            this.Name = "frmIssueDriverLicenseForTheFirstTime";
            this.Text = "Issue Driver License For The First Time";
            this.Load += new System.EventHandler(this.frmIssueDriverLicenseForTheFirstTime_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlLDLAndBasicApplicationsDetails ctrlLDLAndBasicApplicationsDetails1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnIssue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox4;
    }
}