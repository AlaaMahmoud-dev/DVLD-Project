namespace DVLD_Project
{
    partial class frmManageDetainedLicenses
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
            this.components = new System.ComponentModel.Container();
            this.txtFilterBy = new System.Windows.Forms.TextBox();
            this.cbIsReleased = new System.Windows.Forms.ComboBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvDetainedLiscensesList = new System.Windows.Forms.DataGridView();
            this.DetainID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetainedLocalLicenseID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetainDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsReleased = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FineFees = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReleaseDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NationalNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReleaseAppID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnReleaseLicense = new System.Windows.Forms.Button();
            this.btnDetainLicense = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.showPersonDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonLicenseHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseDetainedLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLiscensesList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFilterBy
            // 
            this.txtFilterBy.Location = new System.Drawing.Point(268, 240);
            this.txtFilterBy.Name = "txtFilterBy";
            this.txtFilterBy.Size = new System.Drawing.Size(181, 20);
            this.txtFilterBy.TabIndex = 74;
            this.txtFilterBy.Visible = false;
            this.txtFilterBy.TextChanged += new System.EventHandler(this.txtFilterBy_TextChanged);
            this.txtFilterBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterBy_KeyPress);
            // 
            // cbIsReleased
            // 
            this.cbIsReleased.FormattingEnabled = true;
            this.cbIsReleased.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cbIsReleased.Location = new System.Drawing.Point(268, 240);
            this.cbIsReleased.Name = "cbIsReleased";
            this.cbIsReleased.Size = new System.Drawing.Size(151, 21);
            this.cbIsReleased.TabIndex = 76;
            this.cbIsReleased.Visible = false;
            this.cbIsReleased.SelectedIndexChanged += new System.EventHandler(this.cbIsReleased_SelectedIndexChanged_1);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "National No",
            "Full Name",
            "Detain.ID",
            "License.ID",
            "Release App.ID",
            "Is Released"});
            this.cbFilterBy.Location = new System.Drawing.Point(96, 240);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(151, 21);
            this.cbFilterBy.TabIndex = 73;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 72;
            this.label3.Text = "Filter By:";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.Location = new System.Drawing.Point(92, 673);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(59, 20);
            this.lblRecords.TabIndex = 70;
            this.lblRecords.Text = "[????]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 673);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 20);
            this.label5.TabIndex = 69;
            this.label5.Text = "Records:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.dgvDetainedLiscensesList);
            this.panel1.Location = new System.Drawing.Point(5, 276);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1316, 383);
            this.panel1.TabIndex = 67;
            // 
            // dgvDetainedLiscensesList
            // 
            this.dgvDetainedLiscensesList.AllowUserToAddRows = false;
            this.dgvDetainedLiscensesList.AllowUserToDeleteRows = false;
            this.dgvDetainedLiscensesList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDetainedLiscensesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetainedLiscensesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DetainID,
            this.DetainedLocalLicenseID,
            this.DetainDate,
            this.IsReleased,
            this.FineFees,
            this.ReleaseDate,
            this.NationalNO,
            this.FullName,
            this.ReleaseAppID});
            this.dgvDetainedLiscensesList.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvDetainedLiscensesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetainedLiscensesList.Location = new System.Drawing.Point(0, 0);
            this.dgvDetainedLiscensesList.Name = "dgvDetainedLiscensesList";
            this.dgvDetainedLiscensesList.ReadOnly = true;
            this.dgvDetainedLiscensesList.Size = new System.Drawing.Size(1312, 379);
            this.dgvDetainedLiscensesList.TabIndex = 0;
            // 
            // DetainID
            // 
            this.DetainID.HeaderText = "D.ID";
            this.DetainID.Name = "DetainID";
            this.DetainID.ReadOnly = true;
            // 
            // DetainedLocalLicenseID
            // 
            this.DetainedLocalLicenseID.HeaderText = "D.License ID";
            this.DetainedLocalLicenseID.Name = "DetainedLocalLicenseID";
            this.DetainedLocalLicenseID.ReadOnly = true;
            // 
            // DetainDate
            // 
            this.DetainDate.HeaderText = "D.Date";
            this.DetainDate.Name = "DetainDate";
            this.DetainDate.ReadOnly = true;
            this.DetainDate.Width = 170;
            // 
            // IsReleased
            // 
            this.IsReleased.HeaderText = "Is Released";
            this.IsReleased.Name = "IsReleased";
            this.IsReleased.ReadOnly = true;
            this.IsReleased.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsReleased.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // FineFees
            // 
            this.FineFees.HeaderText = "Fine Fees";
            this.FineFees.Name = "FineFees";
            this.FineFees.ReadOnly = true;
            this.FineFees.Width = 120;
            // 
            // ReleaseDate
            // 
            this.ReleaseDate.HeaderText = "Release Date";
            this.ReleaseDate.Name = "ReleaseDate";
            this.ReleaseDate.ReadOnly = true;
            this.ReleaseDate.Width = 170;
            // 
            // NationalNO
            // 
            this.NationalNO.HeaderText = "N.No";
            this.NationalNO.Name = "NationalNO";
            this.NationalNO.ReadOnly = true;
            // 
            // FullName
            // 
            this.FullName.HeaderText = "Full Name";
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            this.FullName.Width = 300;
            // 
            // ReleaseAppID
            // 
            this.ReleaseAppID.HeaderText = "Release App.ID";
            this.ReleaseAppID.Name = "ReleaseAppID";
            this.ReleaseAppID.ReadOnly = true;
            this.ReleaseAppID.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(514, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 31);
            this.label1.TabIndex = 66;
            this.label1.Text = "List Detained Licenses";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPersonDetailsToolStripMenuItem,
            this.showLicenseDetailsToolStripMenuItem,
            this.showPersonLicenseHistoryToolStripMenuItem,
            this.toolStripMenuItem1,
            this.releaseDetainedLicenseToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(242, 184);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(238, 6);
            // 
            // btnReleaseLicense
            // 
            this.btnReleaseLicense.BackColor = System.Drawing.Color.White;
            this.btnReleaseLicense.BackgroundImage = global::DVLD_Project.Properties.Resources.Release_Detained_License_64;
            this.btnReleaseLicense.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReleaseLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReleaseLicense.ForeColor = System.Drawing.Color.Firebrick;
            this.btnReleaseLicense.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReleaseLicense.Location = new System.Drawing.Point(1171, 204);
            this.btnReleaseLicense.Name = "btnReleaseLicense";
            this.btnReleaseLicense.Size = new System.Drawing.Size(71, 68);
            this.btnReleaseLicense.TabIndex = 77;
            this.btnReleaseLicense.UseVisualStyleBackColor = false;
            this.btnReleaseLicense.Click += new System.EventHandler(this.btnReleaseLicense_Click);
            // 
            // btnDetainLicense
            // 
            this.btnDetainLicense.BackColor = System.Drawing.Color.White;
            this.btnDetainLicense.BackgroundImage = global::DVLD_Project.Properties.Resources.Detain_64;
            this.btnDetainLicense.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDetainLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetainLicense.ForeColor = System.Drawing.Color.Firebrick;
            this.btnDetainLicense.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetainLicense.Location = new System.Drawing.Point(1250, 204);
            this.btnDetainLicense.Name = "btnDetainLicense";
            this.btnDetainLicense.Size = new System.Drawing.Size(71, 68);
            this.btnDetainLicense.TabIndex = 75;
            this.btnDetainLicense.UseVisualStyleBackColor = false;
            this.btnDetainLicense.Click += new System.EventHandler(this.btnDetainLicense_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Firebrick;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1211, 673);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 38);
            this.btnClose.TabIndex = 68;
            this.btnClose.Text = "       Close";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // showPersonDetailsToolStripMenuItem
            // 
            this.showPersonDetailsToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.PersonDetails_32;
            this.showPersonDetailsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showPersonDetailsToolStripMenuItem.Name = "showPersonDetailsToolStripMenuItem";
            this.showPersonDetailsToolStripMenuItem.Size = new System.Drawing.Size(241, 38);
            this.showPersonDetailsToolStripMenuItem.Text = "Show Person Details";
            this.showPersonDetailsToolStripMenuItem.Click += new System.EventHandler(this.showPersonDetailsToolStripMenuItem_Click);
            // 
            // showLicenseDetailsToolStripMenuItem
            // 
            this.showLicenseDetailsToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.LocalDriving_License;
            this.showLicenseDetailsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showLicenseDetailsToolStripMenuItem.Name = "showLicenseDetailsToolStripMenuItem";
            this.showLicenseDetailsToolStripMenuItem.Size = new System.Drawing.Size(241, 38);
            this.showLicenseDetailsToolStripMenuItem.Text = "Show License Details";
            this.showLicenseDetailsToolStripMenuItem.Click += new System.EventHandler(this.showLicenseDetailsToolStripMenuItem_Click);
            // 
            // showPersonLicenseHistoryToolStripMenuItem
            // 
            this.showPersonLicenseHistoryToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.PersonLicenseHistory_32;
            this.showPersonLicenseHistoryToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showPersonLicenseHistoryToolStripMenuItem.Name = "showPersonLicenseHistoryToolStripMenuItem";
            this.showPersonLicenseHistoryToolStripMenuItem.Size = new System.Drawing.Size(241, 38);
            this.showPersonLicenseHistoryToolStripMenuItem.Text = "Show Person License History";
            this.showPersonLicenseHistoryToolStripMenuItem.Click += new System.EventHandler(this.showPersonLicenseHistoryToolStripMenuItem_Click);
            // 
            // releaseDetainedLicenseToolStripMenuItem
            // 
            this.releaseDetainedLicenseToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.Release_Detained_License_32;
            this.releaseDetainedLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.releaseDetainedLicenseToolStripMenuItem.Name = "releaseDetainedLicenseToolStripMenuItem";
            this.releaseDetainedLicenseToolStripMenuItem.Size = new System.Drawing.Size(241, 38);
            this.releaseDetainedLicenseToolStripMenuItem.Text = "Release Detained License";
            this.releaseDetainedLicenseToolStripMenuItem.Click += new System.EventHandler(this.releaseDetainedLicenseToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::DVLD_Project.Properties.Resources.Detain_512;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(591, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(153, 159);
            this.pictureBox1.TabIndex = 65;
            this.pictureBox1.TabStop = false;
            // 
            // frmManageDetainedLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1325, 716);
            this.Controls.Add(this.btnReleaseLicense);
            this.Controls.Add(this.cbIsReleased);
            this.Controls.Add(this.btnDetainLicense);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtFilterBy);
            this.Name = "frmManageDetainedLicenses";
            this.Text = "frmManageDetainedLicenses";
            this.Load += new System.EventHandler(this.frmManageDetainedLicenses_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLiscensesList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilterBy;
        private System.Windows.Forms.ComboBox cbIsReleased;
        private System.Windows.Forms.Button btnDetainLicense;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvDetainedLiscensesList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetainID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetainedLocalLicenseID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetainDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsReleased;
        private System.Windows.Forms.DataGridViewTextBoxColumn FineFees;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReleaseDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn NationalNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReleaseAppID;
        private System.Windows.Forms.Button btnReleaseLicense;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showPersonDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem releaseDetainedLicenseToolStripMenuItem;
    }
}