namespace project_RYS.Forms
{
    partial class uploadnamesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uploadnamesForm));
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStudentFileName = new System.Windows.Forms.Label();
            this.btnUploadnames = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.backbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(457, 504);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 26);
            this.label2.TabIndex = 23;
            this.label2.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblStatus.Location = new System.Drawing.Point(537, 510);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(131, 20);
            this.lblStatus.TabIndex = 22;
            this.lblStatus.Text = "No File Uploaded";
            // 
            // lblStudentFileName
            // 
            this.lblStudentFileName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStudentFileName.AutoSize = true;
            this.lblStudentFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentFileName.ForeColor = System.Drawing.Color.Blue;
            this.lblStudentFileName.Location = new System.Drawing.Point(755, 383);
            this.lblStudentFileName.Name = "lblStudentFileName";
            this.lblStudentFileName.Size = new System.Drawing.Size(42, 25);
            this.lblStudentFileName.TabIndex = 21;
            this.lblStudentFileName.Text = "null";
            // 
            // btnUploadnames
            // 
            this.btnUploadnames.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUploadnames.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUploadnames.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadnames.Location = new System.Drawing.Point(437, 314);
            this.btnUploadnames.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUploadnames.Name = "btnUploadnames";
            this.btnUploadnames.Size = new System.Drawing.Size(312, 89);
            this.btnUploadnames.TabIndex = 20;
            this.btnUploadnames.Text = "Upload Names";
            this.btnUploadnames.UseVisualStyleBackColor = true;
            this.btnUploadnames.Click += new System.EventHandler(this.btnUploadnames_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(368, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(529, 34);
            this.label1.TabIndex = 19;
            this.label1.Text = "Upload Students_names Excel-Sheet";
            // 
            // backbtn
            // 
            this.backbtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.backbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.backbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backbtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backbtn.Location = new System.Drawing.Point(29, 31);
            this.backbtn.Name = "backbtn";
            this.backbtn.Padding = new System.Windows.Forms.Padding(10);
            this.backbtn.Size = new System.Drawing.Size(208, 55);
            this.backbtn.TabIndex = 38;
            this.backbtn.TabStop = false;
            this.backbtn.Text = "Back to Dashboard";
            this.backbtn.UseVisualStyleBackColor = false;
            this.backbtn.Click += new System.EventHandler(this.backbtn_Click);
            // 
            // uploadnamesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1228, 698);
            this.Controls.Add(this.backbtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblStudentFileName);
            this.Controls.Add(this.btnUploadnames);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "uploadnamesForm";
            this.Text = "JNTUHUCEM PORTAL";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStudentFileName;
        private System.Windows.Forms.Button btnUploadnames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button backbtn;
    }
}