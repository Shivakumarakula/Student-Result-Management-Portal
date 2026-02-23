namespace project_RYS.Forms
{
    partial class extra_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(extra_form));
            this.button6 = new System.Windows.Forms.Button();
            this.swgpands = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.downloadbtn = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button6
            // 
            this.button6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button6.BackColor = System.Drawing.SystemColors.Control;
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.Black;
            this.button6.Image = global::project_RYS.Properties.Resources.icons8_upload_30;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(481, 533);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(278, 93);
            this.button6.TabIndex = 26;
            this.button6.Text = "Upload StudentsAttempts JSON-File";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // swgpands
            // 
            this.swgpands.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.swgpands.Cursor = System.Windows.Forms.Cursors.Hand;
            this.swgpands.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.swgpands.Image = global::project_RYS.Properties.Resources.icons8_result_30;
            this.swgpands.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.swgpands.Location = new System.Drawing.Point(437, 357);
            this.swgpands.Name = "swgpands";
            this.swgpands.Size = new System.Drawing.Size(328, 89);
            this.swgpands.TabIndex = 25;
            this.swgpands.Text = "  Subject wise GradePoints & SGPA";
            this.swgpands.UseVisualStyleBackColor = true;
            this.swgpands.Click += new System.EventHandler(this.swgpands_Click);
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Image = global::project_RYS.Properties.Resources.icons8_result_30;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(51, 349);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(302, 97);
            this.button5.TabIndex = 24;
            this.button5.Text = " Semister wise SGPA, Credits & CGPA";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // downloadbtn
            // 
            this.downloadbtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.downloadbtn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.downloadbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.downloadbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadbtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.downloadbtn.Location = new System.Drawing.Point(481, 121);
            this.downloadbtn.Name = "downloadbtn";
            this.downloadbtn.Size = new System.Drawing.Size(301, 85);
            this.downloadbtn.TabIndex = 23;
            this.downloadbtn.Text = "Download Excel File Structure";
            this.downloadbtn.UseVisualStyleBackColor = false;
            this.downloadbtn.Click += new System.EventHandler(this.downloadbtn_Click);
            // 
            // button7
            // 
            this.button7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button7.BackColor = System.Drawing.SystemColors.Control;
            this.button7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.Black;
            this.button7.Image = global::project_RYS.Properties.Resources.icons8_result_30;
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.Location = new System.Drawing.Point(869, 367);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(318, 92);
            this.button7.TabIndex = 27;
            this.button7.Text = "Batch Wise Students      Backlogs Report";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // extra_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1257, 718);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.swgpands);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.downloadbtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "extra_form";
            this.Text = "JNTUHUCEM PORTAL";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button swgpands;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button downloadbtn;
        private System.Windows.Forms.Button button7;
    }
}