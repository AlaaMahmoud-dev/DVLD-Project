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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label26 = new System.Windows.Forms.Label();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tcLicensesHistory = new System.Windows.Forms.TabControl();
            this.localLicensesHistory = new System.Windows.Forms.TabPage();
            this.dgvLocalLicensesHistory = new System.Windows.Forms.DataGridView();
            this.InternationalLicensesHistory = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.LicenseID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApplicationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpirationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvInternationalLicensesHistory = new System.Windows.Forms.DataGridView();
            this.InternationalLicenseID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BasicLocalLicenseID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IntApplicationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlFindPersonByFilter1 = new DVLD_Project.ctrlFindPersonByFilter();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tcLicensesHistory.SuspendLayout();
            this.localLicensesHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicensesHistory)).BeginInit();
            this.InternationalLicensesHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicensesHistory)).BeginInit();
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblRecordsCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tcLicensesHistory);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 449);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1042, 311);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver Licenses";
            // 
            // tcLicensesHistory
            // 
            this.tcLicensesHistory.Controls.Add(this.localLicensesHistory);
            this.tcLicensesHistory.Controls.Add(this.InternationalLicensesHistory);
            this.tcLicensesHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcLicensesHistory.Location = new System.Drawing.Point(12, 34);
            this.tcLicensesHistory.Name = "tcLicensesHistory";
            this.tcLicensesHistory.SelectedIndex = 0;
            this.tcLicensesHistory.Size = new System.Drawing.Size(1024, 235);
            this.tcLicensesHistory.TabIndex = 52;
            this.tcLicensesHistory.SelectedIndexChanged += new System.EventHandler(this.tcLicensesHistory_SelectedIndexChanged);
            // 
            // localLicensesHistory
            // 
            this.localLicensesHistory.Controls.Add(this.label3);
            this.localLicensesHistory.Controls.Add(this.dgvLocalLicensesHistory);
            this.localLicensesHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.localLicensesHistory.Location = new System.Drawing.Point(4, 29);
            this.localLicensesHistory.Name = "localLicensesHistory";
            this.localLicensesHistory.Padding = new System.Windows.Forms.Padding(3);
            this.localLicensesHistory.Size = new System.Drawing.Size(1016, 202);
            this.localLicensesHistory.TabIndex = 0;
            this.localLicensesHistory.Text = "Local";
            this.localLicensesHistory.UseVisualStyleBackColor = true;
            // 
            // dgvLocalLicensesHistory
            // 
            this.dgvLocalLicensesHistory.AllowUserToAddRows = false;
            this.dgvLocalLicensesHistory.AllowUserToDeleteRows = false;
            this.dgvLocalLicensesHistory.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLocalLicensesHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLocalLicensesHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalLicensesHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LicenseID,
            this.ApplicationID,
            this.ClassName,
            this.IssueDate,
            this.ExpirationDate,
            this.IsActive});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLocalLicensesHistory.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLocalLicensesHistory.Location = new System.Drawing.Point(17, 42);
            this.dgvLocalLicensesHistory.Name = "dgvLocalLicensesHistory";
            this.dgvLocalLicensesHistory.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLocalLicensesHistory.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLocalLicensesHistory.Size = new System.Drawing.Size(988, 142);
            this.dgvLocalLicensesHistory.TabIndex = 0;
            // 
            // InternationalLicensesHistory
            // 
            this.InternationalLicensesHistory.Controls.Add(this.label2);
            this.InternationalLicensesHistory.Controls.Add(this.dgvInternationalLicensesHistory);
            this.InternationalLicensesHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InternationalLicensesHistory.Location = new System.Drawing.Point(4, 29);
            this.InternationalLicensesHistory.Name = "InternationalLicensesHistory";
            this.InternationalLicensesHistory.Padding = new System.Windows.Forms.Padding(3);
            this.InternationalLicensesHistory.Size = new System.Drawing.Size(1016, 202);
            this.InternationalLicensesHistory.TabIndex = 1;
            this.InternationalLicensesHistory.Text = "International";
            this.InternationalLicensesHistory.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(25, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 18);
            this.label3.TabIndex = 36;
            this.label3.Text = "Local Licenses History";
            // 
            // LicenseID
            // 
            this.LicenseID.HeaderText = "Lic.ID";
            this.LicenseID.Name = "LicenseID";
            this.LicenseID.ReadOnly = true;
            // 
            // ApplicationID
            // 
            this.ApplicationID.HeaderText = "App.ID";
            this.ApplicationID.Name = "ApplicationID";
            this.ApplicationID.ReadOnly = true;
            // 
            // ClassName
            // 
            this.ClassName.HeaderText = "Class Name";
            this.ClassName.Name = "ClassName";
            this.ClassName.ReadOnly = true;
            this.ClassName.Width = 250;
            // 
            // IssueDate
            // 
            this.IssueDate.HeaderText = "Issue Date";
            this.IssueDate.Name = "IssueDate";
            this.IssueDate.ReadOnly = true;
            this.IssueDate.Width = 200;
            // 
            // ExpirationDate
            // 
            this.ExpirationDate.HeaderText = "Expiration Date";
            this.ExpirationDate.Name = "ExpirationDate";
            this.ExpirationDate.ReadOnly = true;
            this.ExpirationDate.Width = 200;
            // 
            // IsActive
            // 
            this.IsActive.HeaderText = "Is Active";
            this.IsActive.Name = "IsActive";
            this.IsActive.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(30, 272);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 18);
            this.label1.TabIndex = 53;
            this.label1.Text = "#Records:";
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRecordsCount.Location = new System.Drawing.Point(113, 272);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(54, 18);
            this.lblRecordsCount.TabIndex = 54;
            this.lblRecordsCount.Text = "[????]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(22, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 18);
            this.label2.TabIndex = 38;
            this.label2.Text = "Local Licenses History";
            // 
            // dgvInternationalLicensesHistory
            // 
            this.dgvInternationalLicensesHistory.AllowUserToAddRows = false;
            this.dgvInternationalLicensesHistory.AllowUserToDeleteRows = false;
            this.dgvInternationalLicensesHistory.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvInternationalLicensesHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternationalLicensesHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InternationalLicenseID,
            this.BasicLocalLicenseID,
            this.IntApplicationID,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewCheckBoxColumn1});
            this.dgvInternationalLicensesHistory.Location = new System.Drawing.Point(14, 41);
            this.dgvInternationalLicensesHistory.Name = "dgvInternationalLicensesHistory";
            this.dgvInternationalLicensesHistory.ReadOnly = true;
            this.dgvInternationalLicensesHistory.Size = new System.Drawing.Size(986, 142);
            this.dgvInternationalLicensesHistory.TabIndex = 37;
            // 
            // InternationalLicenseID
            // 
            this.InternationalLicenseID.HeaderText = "Int.Lic.ID";
            this.InternationalLicenseID.Name = "InternationalLicenseID";
            this.InternationalLicenseID.ReadOnly = true;
            // 
            // BasicLocalLicenseID
            // 
            this.BasicLocalLicenseID.HeaderText = "Lic.ID";
            this.BasicLocalLicenseID.Name = "BasicLocalLicenseID";
            this.BasicLocalLicenseID.ReadOnly = true;
            // 
            // IntApplicationID
            // 
            this.IntApplicationID.HeaderText = "App.ID";
            this.IntApplicationID.Name = "IntApplicationID";
            this.IntApplicationID.ReadOnly = true;
            this.IntApplicationID.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Issue Date";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Expiration Date";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 200;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "Is Active";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Firebrick;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(944, 766);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 38);
            this.btnClose.TabIndex = 54;
            this.btnClose.Text = "       Close";
            this.btnClose.UseVisualStyleBackColor = false;
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
            // 
            // frmShowLicensesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 807);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ctrlFindPersonByFilter1);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.pictureBox15);
            this.Name = "frmShowLicensesHistory";
            this.Text = "frmShowLicensesHistory";
            this.Load += new System.EventHandler(this.frmShowLicensesHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tcLicensesHistory.ResumeLayout(false);
            this.localLicensesHistory.ResumeLayout(false);
            this.localLicensesHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicensesHistory)).EndInit();
            this.InternationalLicensesHistory.ResumeLayout(false);
            this.InternationalLicensesHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicensesHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.PictureBox pictureBox15;
        private ctrlFindPersonByFilter ctrlFindPersonByFilter1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tcLicensesHistory;
        private System.Windows.Forms.TabPage localLicensesHistory;
        private System.Windows.Forms.DataGridView dgvLocalLicensesHistory;
        private System.Windows.Forms.TabPage InternationalLicensesHistory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn LicenseID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApplicationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpirationDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvInternationalLicensesHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn InternationalLicenseID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BasicLocalLicenseID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IntApplicationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.Button btnClose;
    }
}