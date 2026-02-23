namespace project_RYS.Forms
{
    partial class BatchResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchResult));
            this.branchcombo = new System.Windows.Forms.ComboBox();
            this.acyearcombo = new System.Windows.Forms.ComboBox();
            this.semcombo = new System.Windows.Forms.ComboBox();
            this.yearcombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.subbtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.backbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // branchcombo
            // 
            this.branchcombo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.branchcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.branchcombo.FormattingEnabled = true;
            this.branchcombo.Location = new System.Drawing.Point(531, 217);
            this.branchcombo.Name = "branchcombo";
            this.branchcombo.Size = new System.Drawing.Size(181, 28);
            this.branchcombo.TabIndex = 0;
            this.branchcombo.SelectedIndexChanged += new System.EventHandler(this.branchcombo_SelectedIndexChanged);
            // 
            // acyearcombo
            // 
            this.acyearcombo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.acyearcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.acyearcombo.FormattingEnabled = true;
            this.acyearcombo.Location = new System.Drawing.Point(531, 330);
            this.acyearcombo.Name = "acyearcombo";
            this.acyearcombo.Size = new System.Drawing.Size(181, 28);
            this.acyearcombo.TabIndex = 1;
            // 
            // semcombo
            // 
            this.semcombo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.semcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.semcombo.FormattingEnabled = true;
            this.semcombo.Items.AddRange(new object[] {
            "1",
            "2"});
            this.semcombo.Location = new System.Drawing.Point(531, 535);
            this.semcombo.Name = "semcombo";
            this.semcombo.Size = new System.Drawing.Size(181, 28);
            this.semcombo.TabIndex = 2;
            // 
            // yearcombo
            // 
            this.yearcombo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.yearcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.yearcombo.FormattingEnabled = true;
            this.yearcombo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.yearcombo.Location = new System.Drawing.Point(531, 444);
            this.yearcombo.Name = "yearcombo";
            this.yearcombo.Size = new System.Drawing.Size(181, 28);
            this.yearcombo.TabIndex = 3;
            this.yearcombo.SelectedIndexChanged += new System.EventHandler(this.yearcombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(344, 225);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "BRANCH";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightBlue;
            this.label2.Location = new System.Drawing.Point(344, 333);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 22);
            this.label2.TabIndex = 5;
            this.label2.Text = "ACADEMIC YEAR";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightBlue;
            this.label3.Location = new System.Drawing.Point(344, 447);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 22);
            this.label3.TabIndex = 6;
            this.label3.Text = "YEAR";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.LightBlue;
            this.label4.Location = new System.Drawing.Point(344, 543);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 22);
            this.label4.TabIndex = 7;
            this.label4.Text = "SEM";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // subbtn
            // 
            this.subbtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.subbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subbtn.Location = new System.Drawing.Point(433, 636);
            this.subbtn.Name = "subbtn";
            this.subbtn.Size = new System.Drawing.Size(170, 58);
            this.subbtn.TabIndex = 8;
            this.subbtn.Text = "Submit";
            this.subbtn.UseVisualStyleBackColor = true;
            this.subbtn.Click += new System.EventHandler(this.subbtn_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(457, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 29);
            this.label5.TabIndex = 9;
            this.label5.Text = "RESULT";
            // 
            // backbtn
            // 
            this.backbtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.backbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.backbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backbtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backbtn.Location = new System.Drawing.Point(32, 28);
            this.backbtn.Name = "backbtn";
            this.backbtn.Padding = new System.Windows.Forms.Padding(10);
            this.backbtn.Size = new System.Drawing.Size(208, 55);
            this.backbtn.TabIndex = 38;
            this.backbtn.TabStop = false;
            this.backbtn.Text = "Back to Dashboard";
            this.backbtn.UseVisualStyleBackColor = false;
            this.backbtn.Click += new System.EventHandler(this.backbtn_Click);
            // 
            // BatchResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(975, 737);
            this.Controls.Add(this.backbtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.subbtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.yearcombo);
            this.Controls.Add(this.semcombo);
            this.Controls.Add(this.acyearcombo);
            this.Controls.Add(this.branchcombo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BatchResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "";
            this.Text = "JNTUHUCEM PORTAL";
            this.Load += new System.EventHandler(this.BatchResult_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox acyearcombo;
        private System.Windows.Forms.ComboBox semcombo;
        private System.Windows.Forms.ComboBox yearcombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button subbtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox branchcombo;
        private System.Windows.Forms.Button backbtn;
    }
}