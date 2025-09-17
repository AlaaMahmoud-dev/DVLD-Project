namespace DVLD_Project
{
    partial class FindLocalLicenseInfoByLicenseID
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlLicenseInfo1 = new DVLD_Project.ctrlLicenseInfo();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnFindNow = new System.Windows.Forms.Button();
            this.txtLicenseIDValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlLicenseInfo1
            // 
            this.ctrlLicenseInfo1.Location = new System.Drawing.Point(0, 98);
            this.ctrlLicenseInfo1.Name = "ctrlLicenseInfo1";
            this.ctrlLicenseInfo1.Size = new System.Drawing.Size(1082, 355);
            this.ctrlLicenseInfo1.TabIndex = 0;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnFindNow);
            this.gbFilter.Controls.Add(this.txtLicenseIDValue);
            this.gbFilter.Controls.Add(this.label1);
            this.gbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(3, 3);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(573, 89);
            this.gbFilter.TabIndex = 1;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // btnFindNow
            // 
            this.btnFindNow.BackgroundImage = global::DVLD_Project.Properties.Resources.License_View_32;
            this.btnFindNow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFindNow.Enabled = false;
            this.btnFindNow.Location = new System.Drawing.Point(497, 21);
            this.btnFindNow.Name = "btnFindNow";
            this.btnFindNow.Size = new System.Drawing.Size(67, 54);
            this.btnFindNow.TabIndex = 2;
            this.btnFindNow.UseVisualStyleBackColor = true;
            this.btnFindNow.Click += new System.EventHandler(this.btnFindNow_Click);
            // 
            // txtLicenseIDValue
            // 
            this.txtLicenseIDValue.Location = new System.Drawing.Point(152, 35);
            this.txtLicenseIDValue.Name = "txtLicenseIDValue";
            this.txtLicenseIDValue.Size = new System.Drawing.Size(324, 26);
            this.txtLicenseIDValue.TabIndex = 1;
            this.txtLicenseIDValue.TextChanged += new System.EventHandler(this.txtLicenseIDValue_TextChanged);
            this.txtLicenseIDValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLicenseIDValue_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "LicenseID:";
            // 
            // FindLocalLicenseInfoByLicenseID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.ctrlLicenseInfo1);
            this.Name = "FindLocalLicenseInfoByLicenseID";
            this.Size = new System.Drawing.Size(1081, 448);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlLicenseInfo ctrlLicenseInfo1;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.TextBox txtLicenseIDValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFindNow;
    }
}
