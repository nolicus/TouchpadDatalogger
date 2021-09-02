namespace TouchpadDatalogger
{
    partial class FileNamePrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileNamePrompt));
            this.tbRecordName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartRec = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbRecordName
            // 
            this.tbRecordName.Location = new System.Drawing.Point(12, 30);
            this.tbRecordName.Name = "tbRecordName";
            this.tbRecordName.Size = new System.Drawing.Size(313, 20);
            this.tbRecordName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Record Name:";
            // 
            // btnStartRec
            // 
            this.btnStartRec.Location = new System.Drawing.Point(12, 56);
            this.btnStartRec.Name = "btnStartRec";
            this.btnStartRec.Size = new System.Drawing.Size(313, 23);
            this.btnStartRec.TabIndex = 2;
            this.btnStartRec.Text = "Start Recording";
            this.btnStartRec.UseVisualStyleBackColor = true;
            this.btnStartRec.Click += new System.EventHandler(this.btnStartRec_Click);
            // 
            // FileNamePrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 88);
            this.Controls.Add(this.btnStartRec);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbRecordName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileNamePrompt";
            this.Text = "Record SaveAs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbRecordName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartRec;
    }
}