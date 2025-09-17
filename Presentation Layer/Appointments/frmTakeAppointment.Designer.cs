namespace DVLD_Project
{
    partial class frmTakeAppointment
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
            this.lblAppointmentType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvAppointmentsList = new System.Windows.Forms.DataGridView();
            this.AppointmentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppointmentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaidFees = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isLocked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddNewAppointment = new System.Windows.Forms.Button();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbTestTypePic = new System.Windows.Forms.PictureBox();
            this.ctrlLDLAndBasicApplicationsDetails1 = new DVLD_Project.ctrlLDLAndBasicApplicationsDetails();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentsList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestTypePic)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAppointmentType
            // 
            this.lblAppointmentType.AutoSize = true;
            this.lblAppointmentType.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointmentType.ForeColor = System.Drawing.Color.Firebrick;
            this.lblAppointmentType.Location = new System.Drawing.Point(406, 136);
            this.lblAppointmentType.Name = "lblAppointmentType";
            this.lblAppointmentType.Size = new System.Drawing.Size(225, 29);
            this.lblAppointmentType.TabIndex = 45;
            this.lblAppointmentType.Text = "Appointment Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 575);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 18);
            this.label1.TabIndex = 47;
            this.label1.Text = "Appointments:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvAppointmentsList);
            this.panel1.Location = new System.Drawing.Point(12, 611);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1012, 222);
            this.panel1.TabIndex = 48;
            // 
            // dgvAppointmentsList
            // 
            this.dgvAppointmentsList.AllowUserToAddRows = false;
            this.dgvAppointmentsList.AllowUserToDeleteRows = false;
            this.dgvAppointmentsList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvAppointmentsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointmentsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AppointmentID,
            this.AppointmentDate,
            this.PaidFees,
            this.isLocked});
            this.dgvAppointmentsList.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvAppointmentsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAppointmentsList.Location = new System.Drawing.Point(0, 0);
            this.dgvAppointmentsList.Name = "dgvAppointmentsList";
            this.dgvAppointmentsList.ReadOnly = true;
            this.dgvAppointmentsList.Size = new System.Drawing.Size(1012, 222);
            this.dgvAppointmentsList.TabIndex = 0;
            // 
            // AppointmentID
            // 
            this.AppointmentID.HeaderText = "Appointment ID";
            this.AppointmentID.Name = "AppointmentID";
            this.AppointmentID.ReadOnly = true;
            this.AppointmentID.Width = 150;
            // 
            // AppointmentDate
            // 
            this.AppointmentDate.HeaderText = "Appointment Date";
            this.AppointmentDate.Name = "AppointmentDate";
            this.AppointmentDate.ReadOnly = true;
            this.AppointmentDate.Width = 150;
            // 
            // PaidFees
            // 
            this.PaidFees.HeaderText = "Paid Fees";
            this.PaidFees.Name = "PaidFees";
            this.PaidFees.ReadOnly = true;
            this.PaidFees.Width = 125;
            // 
            // isLocked
            // 
            this.isLocked.HeaderText = "is Locked";
            this.isLocked.Name = "isLocked";
            this.isLocked.ReadOnly = true;
            this.isLocked.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isLocked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.toolStripMenuItem1,
            this.takeTestToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 108);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(193, 6);
            // 
            // btnAddNewAppointment
            // 
            this.btnAddNewAppointment.BackgroundImage = global::DVLD_Project.Properties.Resources.AddAppointment_32;
            this.btnAddNewAppointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddNewAppointment.Location = new System.Drawing.Point(974, 567);
            this.btnAddNewAppointment.Name = "btnAddNewAppointment";
            this.btnAddNewAppointment.Size = new System.Drawing.Size(39, 37);
            this.btnAddNewAppointment.TabIndex = 49;
            this.btnAddNewAppointment.UseVisualStyleBackColor = true;
            this.btnAddNewAppointment.Click += new System.EventHandler(this.btnAddNewAppointment_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.edit_32;
            this.editToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // takeTestToolStripMenuItem
            // 
            this.takeTestToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.Test_32;
            this.takeTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.takeTestToolStripMenuItem.Name = "takeTestToolStripMenuItem";
            this.takeTestToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.takeTestToolStripMenuItem.Text = "Take Test";
            this.takeTestToolStripMenuItem.Click += new System.EventHandler(this.takeTestToolStripMenuItem_Click);
            // 
            // pbTestTypePic
            // 
            this.pbTestTypePic.BackgroundImage = global::DVLD_Project.Properties.Resources.Vision_512;
            this.pbTestTypePic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbTestTypePic.Location = new System.Drawing.Point(438, 1);
            this.pbTestTypePic.Name = "pbTestTypePic";
            this.pbTestTypePic.Size = new System.Drawing.Size(175, 132);
            this.pbTestTypePic.TabIndex = 44;
            this.pbTestTypePic.TabStop = false;
            // 
            // ctrlLDLAndBasicApplicationsDetails1
            // 
            this.ctrlLDLAndBasicApplicationsDetails1.Location = new System.Drawing.Point(12, 179);
            this.ctrlLDLAndBasicApplicationsDetails1.Name = "ctrlLDLAndBasicApplicationsDetails1";
            this.ctrlLDLAndBasicApplicationsDetails1.Size = new System.Drawing.Size(1017, 383);
            this.ctrlLDLAndBasicApplicationsDetails1.TabIndex = 46;
            // 
            // frmTakeAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 845);
            this.Controls.Add(this.btnAddNewAppointment);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlLDLAndBasicApplicationsDetails1);
            this.Controls.Add(this.lblAppointmentType);
            this.Controls.Add(this.pbTestTypePic);
            this.Name = "frmTakeAppointment";
            this.Text = "frmTakeAppointment";
            this.Load += new System.EventHandler(this.frmTakeAppointment_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentsList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbTestTypePic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAppointmentType;
        private System.Windows.Forms.PictureBox pbTestTypePic;
        private ctrlLDLAndBasicApplicationsDetails ctrlLDLAndBasicApplicationsDetails1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvAppointmentsList;
        private System.Windows.Forms.Button btnAddNewAppointment;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppointmentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppointmentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaidFees;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isLocked;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem takeTestToolStripMenuItem;
    }
}