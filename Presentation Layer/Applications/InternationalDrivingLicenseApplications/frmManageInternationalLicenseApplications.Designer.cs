namespace DVLD_Project
{
    partial class frmManageInternationalLicenseApplications
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
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvInternationalLicenses = new System.Windows.Forms.DataGridView();
            this.InternationalLicenseID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApplicationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DriverID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocalLicenseID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssuedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpirationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.cbIsActive = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAddNewIntnationalLicense = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.showPersonDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonLicenseHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicenses)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFilterBy
            // 
            this.txtFilterBy.Location = new System.Drawing.Point(269, 203);
            this.txtFilterBy.Name = "txtFilterBy";
            this.txtFilterBy.Size = new System.Drawing.Size(181, 20);
            this.txtFilterBy.TabIndex = 62;
            this.txtFilterBy.Visible = false;
            this.txtFilterBy.TextChanged += new System.EventHandler(this.txtFilterBy_TextChanged);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Int.License ID",
            "L.D.L ID",
            "App ID",
            "Driver ID",
            "Is Active"});
            this.cbFilterBy.Location = new System.Drawing.Point(97, 202);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(151, 21);
            this.cbFilterBy.TabIndex = 61;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 60;
            this.label3.Text = "Filter By:";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.Location = new System.Drawing.Point(93, 635);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(59, 20);
            this.lblRecords.TabIndex = 58;
            this.lblRecords.Text = "[????]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 635);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 20);
            this.label5.TabIndex = 57;
            this.label5.Text = "Records:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.dgvInternationalLicenses);
            this.panel1.Location = new System.Drawing.Point(6, 238);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(988, 383);
            this.panel1.TabIndex = 55;
            // 
            // dgvInternationalLicenses
            // 
            this.dgvInternationalLicenses.AllowUserToAddRows = false;
            this.dgvInternationalLicenses.AllowUserToDeleteRows = false;
            this.dgvInternationalLicenses.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvInternationalLicenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternationalLicenses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InternationalLicenseID,
            this.ApplicationID,
            this.DriverID,
            this.LocalLicenseID,
            this.IssuedDate,
            this.ExpirationDate,
            this.isActive});
            this.dgvInternationalLicenses.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvInternationalLicenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInternationalLicenses.Location = new System.Drawing.Point(0, 0);
            this.dgvInternationalLicenses.Name = "dgvInternationalLicenses";
            this.dgvInternationalLicenses.ReadOnly = true;
            this.dgvInternationalLicenses.Size = new System.Drawing.Size(984, 379);
            this.dgvInternationalLicenses.TabIndex = 0;
            // 
            // InternationalLicenseID
            // 
            this.InternationalLicenseID.HeaderText = "Int.License ID";
            this.InternationalLicenseID.Name = "InternationalLicenseID";
            this.InternationalLicenseID.ReadOnly = true;
            this.InternationalLicenseID.Width = 120;
            // 
            // ApplicationID
            // 
            this.ApplicationID.HeaderText = "Application ID";
            this.ApplicationID.Name = "ApplicationID";
            this.ApplicationID.ReadOnly = true;
            this.ApplicationID.Width = 120;
            // 
            // DriverID
            // 
            this.DriverID.HeaderText = "Driver ID";
            this.DriverID.Name = "DriverID";
            this.DriverID.ReadOnly = true;
            this.DriverID.Width = 120;
            // 
            // LocalLicenseID
            // 
            this.LocalLicenseID.HeaderText = "L.License ID";
            this.LocalLicenseID.Name = "LocalLicenseID";
            this.LocalLicenseID.ReadOnly = true;
            this.LocalLicenseID.Width = 120;
            // 
            // IssuedDate
            // 
            this.IssuedDate.HeaderText = "Issue Date";
            this.IssuedDate.Name = "IssuedDate";
            this.IssuedDate.ReadOnly = true;
            this.IssuedDate.Width = 170;
            // 
            // ExpirationDate
            // 
            this.ExpirationDate.HeaderText = "Expiration Date";
            this.ExpirationDate.Name = "ExpirationDate";
            this.ExpirationDate.ReadOnly = true;
            this.ExpirationDate.Width = 170;
            // 
            // isActive
            // 
            this.isActive.HeaderText = "is Active";
            this.isActive.Name = "isActive";
            this.isActive.ReadOnly = true;
            this.isActive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.isActive.Width = 120;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(357, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 25);
            this.label1.TabIndex = 54;
            this.label1.Text = "International Licenses";
            // 
            // cbIsActive
            // 
            this.cbIsActive.FormattingEnabled = true;
            this.cbIsActive.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cbIsActive.Location = new System.Drawing.Point(269, 203);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(151, 21);
            this.cbIsActive.TabIndex = 64;
            this.cbIsActive.Visible = false;
            this.cbIsActive.SelectedIndexChanged += new System.EventHandler(this.cbIsActive_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPersonDetailsToolStripMenuItem,
            this.showLicenseDetailsToolStripMenuItem,
            this.showPersonLicenseHistoryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(242, 140);
            // 
            // btnAddNewIntnationalLicense
            // 
            this.btnAddNewIntnationalLicense.BackColor = System.Drawing.Color.White;
            this.btnAddNewIntnationalLicense.BackgroundImage = global::DVLD_Project.Properties.Resources.New_Application_64;
            this.btnAddNewIntnationalLicense.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddNewIntnationalLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewIntnationalLicense.ForeColor = System.Drawing.Color.Firebrick;
            this.btnAddNewIntnationalLicense.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewIntnationalLicense.Location = new System.Drawing.Point(921, 166);
            this.btnAddNewIntnationalLicense.Name = "btnAddNewIntnationalLicense";
            this.btnAddNewIntnationalLicense.Size = new System.Drawing.Size(71, 68);
            this.btnAddNewIntnationalLicense.TabIndex = 63;
            this.btnAddNewIntnationalLicense.UseVisualStyleBackColor = false;
            this.btnAddNewIntnationalLicense.Click += new System.EventHandler(this.btnAddNewIntnationalLicense_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::DVLD_Project.Properties.Resources.International_32;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(534, 59);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(49, 38);
            this.pictureBox2.TabIndex = 59;
            this.pictureBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Firebrick;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(884, 627);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 38);
            this.btnClose.TabIndex = 56;
            this.btnClose.Text = "       Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.showLicenseDetailsToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.License_View_32;
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
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::DVLD_Project.Properties.Resources.Applications;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(407, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(153, 159);
            this.pictureBox1.TabIndex = 53;
            this.pictureBox1.TabStop = false;
            // 
            // frmManageInternationalLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 667);
            this.Controls.Add(this.txtFilterBy);
            this.Controls.Add(this.cbIsActive);
            this.Controls.Add(this.btnAddNewIntnationalLicense);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmManageInternationalLicenseApplications";
            this.Text = "International Licenses ";
            this.Load += new System.EventHandler(this.frmManageInternationalLicenseApplications_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicenses)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddNewIntnationalLicense;
        private System.Windows.Forms.TextBox txtFilterBy;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvInternationalLicenses;
        private System.Windows.Forms.DataGridViewTextBoxColumn InternationalLicenseID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApplicationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriverID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocalLicenseID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssuedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpirationDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActive;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbIsActive;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showPersonDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHistoryToolStripMenuItem;
    }
}