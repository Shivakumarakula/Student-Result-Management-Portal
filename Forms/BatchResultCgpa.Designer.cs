namespace project_RYS.Forms
{
    partial class BatchResultCgpa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchResultCgpa));
            this.label5 = new System.Windows.Forms.Label();
            this.submitbtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.acyearcombo = new System.Windows.Forms.ComboBox();
            this.branchcombo = new System.Windows.Forms.ComboBox();
            this.backbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(488, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 29);
            this.label5.TabIndex = 19;
            this.label5.Text = "RESULT";
            // 
            // submitbtn
            // 
            this.submitbtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.submitbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submitbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitbtn.Location = new System.Drawing.Point(469, 389);
            this.submitbtn.Name = "submitbtn";
            this.submitbtn.Size = new System.Drawing.Size(204, 61);
            this.submitbtn.TabIndex = 18;
            this.submitbtn.Text = "Submit";
            this.submitbtn.UseVisualStyleBackColor = true;
            this.submitbtn.Click += new System.EventHandler(this.submitbtn_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightBlue;
            this.label2.Location = new System.Drawing.Point(375, 298);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 22);
            this.label2.TabIndex = 15;
            this.label2.Text = "ACADEMIC YEAR";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(375, 195);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 22);
            this.label1.TabIndex = 14;
            this.label1.Text = "BRANCH";
            // 
            // acyearcombo
            // 
            this.acyearcombo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.acyearcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.acyearcombo.FormattingEnabled = true;
            this.acyearcombo.Location = new System.Drawing.Point(562, 295);
            this.acyearcombo.Name = "acyearcombo";
            this.acyearcombo.Size = new System.Drawing.Size(217, 28);
            this.acyearcombo.TabIndex = 11;
            // 
            // branchcombo
            // 
            this.branchcombo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.branchcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.branchcombo.FormattingEnabled = true;
            this.branchcombo.Location = new System.Drawing.Point(562, 192);
            this.branchcombo.Name = "branchcombo";
            this.branchcombo.Size = new System.Drawing.Size(217, 28);
            this.branchcombo.TabIndex = 10;
            this.branchcombo.SelectedIndexChanged += new System.EventHandler(this.branchcombo_SelectedIndexChanged);
            // 
            // backbtn
            // 
            this.backbtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.backbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.backbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backbtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backbtn.Location = new System.Drawing.Point(29, 29);
            this.backbtn.Name = "backbtn";
            this.backbtn.Padding = new System.Windows.Forms.Padding(10);
            this.backbtn.Size = new System.Drawing.Size(208, 55);
            this.backbtn.TabIndex = 38;
            this.backbtn.TabStop = false;
            this.backbtn.Text = "Back to Dashboard";
            this.backbtn.UseVisualStyleBackColor = false;
            this.backbtn.Click += new System.EventHandler(this.backbtn_Click);
            // 
            // BatchResultCgpa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1119, 596);
            this.Controls.Add(this.backbtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.submitbtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.acyearcombo);
            this.Controls.Add(this.branchcombo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BatchResultCgpa";
            this.Text = "JNTUHUCEM PORTAL";
            this.Load += new System.EventHandler(this.BatchResultCgpa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button submitbtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox acyearcombo;
        private System.Windows.Forms.ComboBox branchcombo;
        private System.Windows.Forms.Button backbtn;
    }
}