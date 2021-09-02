namespace TouchpadDatalogger
{
    partial class HOME_DataLogger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HOME_DataLogger));
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.lblComStatus = new System.Windows.Forms.Label();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnRunPCFilter = new System.Windows.Forms.Button();
            this.dgvGesturePlaylist = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnApplyFilterTeensy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGesturePlaylist)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.AutoSize = true;
            this.lblStatusTitle.Location = new System.Drawing.Point(12, 9);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(89, 13);
            this.lblStatusTitle.TabIndex = 3;
            this.lblStatusTitle.Text = "Hardware Status:";
            // 
            // lblComStatus
            // 
            this.lblComStatus.AutoSize = true;
            this.lblComStatus.Location = new System.Drawing.Point(98, 9);
            this.lblComStatus.Name = "lblComStatus";
            this.lblComStatus.Size = new System.Drawing.Size(73, 13);
            this.lblComStatus.TabIndex = 4;
            this.lblComStatus.Text = "Disconnected";
            // 
            // btnRecord
            // 
            this.btnRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnRecord.Location = new System.Drawing.Point(12, 67);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(118, 30);
            this.btnRecord.TabIndex = 0;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = false;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnRunPCFilter
            // 
            this.btnRunPCFilter.Enabled = false;
            this.btnRunPCFilter.Location = new System.Drawing.Point(15, 246);
            this.btnRunPCFilter.Name = "btnRunPCFilter";
            this.btnRunPCFilter.Size = new System.Drawing.Size(118, 30);
            this.btnRunPCFilter.TabIndex = 8;
            this.btnRunPCFilter.Text = "Run Filter (PC)";
            this.btnRunPCFilter.Click += new System.EventHandler(this.btnRunPCFilter_Click);
            // 
            // dgvGesturePlaylist
            // 
            this.dgvGesturePlaylist.AllowUserToAddRows = false;
            this.dgvGesturePlaylist.AllowUserToDeleteRows = false;
            this.dgvGesturePlaylist.AllowUserToOrderColumns = true;
            this.dgvGesturePlaylist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGesturePlaylist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colDate});
            this.dgvGesturePlaylist.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvGesturePlaylist.Location = new System.Drawing.Point(202, 9);
            this.dgvGesturePlaylist.MultiSelect = false;
            this.dgvGesturePlaylist.Name = "dgvGesturePlaylist";
            this.dgvGesturePlaylist.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvGesturePlaylist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGesturePlaylist.Size = new System.Drawing.Size(500, 267);
            this.dgvGesturePlaylist.TabIndex = 7;
            this.dgvGesturePlaylist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGesturePlaylist_CellContentClick);
            // 
            // colName
            // 
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.Width = 375;
            // 
            // colDate
            // 
            this.colDate.HeaderText = "Date";
            this.colDate.Name = "colDate";
            // 
            // btnApplyFilterTeensy
            // 
            this.btnApplyFilterTeensy.Enabled = false;
            this.btnApplyFilterTeensy.Location = new System.Drawing.Point(12, 32);
            this.btnApplyFilterTeensy.Name = "btnApplyFilterTeensy";
            this.btnApplyFilterTeensy.Size = new System.Drawing.Size(118, 30);
            this.btnApplyFilterTeensy.TabIndex = 9;
            this.btnApplyFilterTeensy.Text = "Enable Teensy Filter";
            this.btnApplyFilterTeensy.Click += new System.EventHandler(this.btnApplyFilterTeensy_Click);
            // 
            // HOME_DataLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 288);
            this.Controls.Add(this.btnApplyFilterTeensy);
            this.Controls.Add(this.dgvGesturePlaylist);
            this.Controls.Add(this.btnRunPCFilter);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.lblComStatus);
            this.Controls.Add(this.lblStatusTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HOME_DataLogger";
            this.Text = "Touchpad Data Logger";
            this.Load += new System.EventHandler(this.HOME_DataLogger_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGesturePlaylist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.Label lblComStatus;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnRunPCFilter;
        private System.Windows.Forms.DataGridView dgvGesturePlaylist;
        private System.Windows.Forms.Button btnApplyFilterTeensy;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
    }
}