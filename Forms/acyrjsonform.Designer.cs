namespace project_RYS.Forms
{
    partial class acyrjsonform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(acyrjsonform));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblacyrFileName = new System.Windows.Forms.Label();
            this.btnUploadacyr = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(649, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 37);
            this.label3.TabIndex = 36;
            this.label3.Text = "JSON FILE";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(235, 452);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 26);
            this.label2.TabIndex = 35;
            this.label2.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblStatus.Location = new System.Drawing.Point(315, 458);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(131, 20);
            this.lblStatus.TabIndex = 34;
            this.lblStatus.Text = "No File Uploaded";
            // 
            // lblacyrFileName
            // 
            this.lblacyrFileName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblacyrFileName.AutoSize = true;
            this.lblacyrFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblacyrFileName.ForeColor = System.Drawing.Color.Blue;
            this.lblacyrFileName.Location = new System.Drawing.Point(533, 331);
            this.lblacyrFileName.Name = "lblacyrFileName";
            this.lblacyrFileName.Size = new System.Drawing.Size(42, 25);
            this.lblacyrFileName.TabIndex = 33;
            this.lblacyrFileName.Text = "null";
            // 
            // btnUploadacyr
            // 
            this.btnUploadacyr.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUploadacyr.AutoEllipsis = true;
            this.btnUploadacyr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUploadacyr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadacyr.Location = new System.Drawing.Point(215, 262);
            this.btnUploadacyr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUploadacyr.Name = "btnUploadacyr";
            this.btnUploadacyr.Size = new System.Drawing.Size(312, 89);
            this.btnUploadacyr.TabIndex = 32;
            this.btnUploadacyr.Text = "Upload JSON file";
            this.btnUploadacyr.UseVisualStyleBackColor = true;
            this.btnUploadacyr.Click += new System.EventHandler(this.btnUploadacyr_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(146, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(470, 34);
            this.label1.TabIndex = 31;
            this.label1.Text = "Upload Acedamic Yeae BackUp ";
            // 
            // bbtn
            // 
            this.bbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bbtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bbtn.Location = new System.Drawing.Point(50, 47);
            this.bbtn.Name = "bbtn";
            this.bbtn.Padding = new System.Windows.Forms.Padding(10);
            this.bbtn.Size = new System.Drawing.Size(208, 55);
            this.bbtn.TabIndex = 37;
            this.bbtn.TabStop = false;
            this.bbtn.Text = "Back to Dashboard";
            this.bbtn.UseVisualStyleBackColor = false;
            this.bbtn.Click += new System.EventHandler(this.backbtn_Click);
            // 
            // acyrjsonform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(915, 594);
            this.Controls.Add(this.bbtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblacyrFileName);
            this.Controls.Add(this.btnUploadacyr);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "acyrjsonform";
            this.Text = "JNTUHUCEM PORTAL";
            this.Load += new System.EventHandler(this.acyrjsonform_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblacyrFileName;
        private System.Windows.Forms.Button btnUploadacyr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bbtn;
    }
}