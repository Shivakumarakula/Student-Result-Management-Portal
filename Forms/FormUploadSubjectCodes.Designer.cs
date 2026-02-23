namespace project_RYS.Forms
{
    partial class FormUploadSubjectCodes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUploadSubjectCodes));
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSubjectFileName = new System.Windows.Forms.Label();
            this.btnUploadSubjects = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.backbtn11 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(461, 486);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 26);
            this.label2.TabIndex = 18;
            this.label2.Text = "Status:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblStatus.Location = new System.Drawing.Point(541, 492);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(131, 20);
            this.lblStatus.TabIndex = 17;
            this.lblStatus.Text = "No File Uploaded";
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // lblSubjectFileName
            // 
            this.lblSubjectFileName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSubjectFileName.AutoSize = true;
            this.lblSubjectFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubjectFileName.ForeColor = System.Drawing.Color.Blue;
            this.lblSubjectFileName.Location = new System.Drawing.Point(759, 365);
            this.lblSubjectFileName.Name = "lblSubjectFileName";
            this.lblSubjectFileName.Size = new System.Drawing.Size(42, 25);
            this.lblSubjectFileName.TabIndex = 16;
            this.lblSubjectFileName.Text = "null";
            this.lblSubjectFileName.Click += new System.EventHandler(this.lblSubjectFileName_Click);
            // 
            // btnUploadSubjects
            // 
            this.btnUploadSubjects.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUploadSubjects.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUploadSubjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadSubjects.Location = new System.Drawing.Point(441, 296);
            this.btnUploadSubjects.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUploadSubjects.Name = "btnUploadSubjects";
            this.btnUploadSubjects.Size = new System.Drawing.Size(312, 89);
            this.btnUploadSubjects.TabIndex = 15;
            this.btnUploadSubjects.Text = "Upload Subject Codes";
            this.btnUploadSubjects.UseVisualStyleBackColor = true;
            this.btnUploadSubjects.Click += new System.EventHandler(this.btnUploadSubjects_Click_1);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(372, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(492, 34);
            this.label1.TabIndex = 14;
            this.label1.Text = "Upload Subject-Codes Excel-Sheet";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // backbtn11
            // 
            this.backbtn11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.backbtn11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.backbtn11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backbtn11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backbtn11.ForeColor = System.Drawing.Color.Black;
            this.backbtn11.Location = new System.Drawing.Point(32, 28);
            this.backbtn11.Name = "backbtn11";
            this.backbtn11.Padding = new System.Windows.Forms.Padding(10);
            this.backbtn11.Size = new System.Drawing.Size(208, 55);
            this.backbtn11.TabIndex = 38;
            this.backbtn11.TabStop = false;
            this.backbtn11.Text = "Back to Dashboard";
            this.backbtn11.UseVisualStyleBackColor = false;
            this.backbtn11.Click += new System.EventHandler(this.backbtn_Click);
            // 
            // FormUploadSubjectCodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1209, 715);
            this.Controls.Add(this.backbtn11);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblSubjectFileName);
            this.Controls.Add(this.btnUploadSubjects);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Coral;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormUploadSubjectCodes";
            this.Text = "JNTUHUCEM PORTAL";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSubjectFileName;
        private System.Windows.Forms.Button btnUploadSubjects;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button backbtn11;
    }
}