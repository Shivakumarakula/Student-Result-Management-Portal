namespace project_RYS
{
    partial class mainresult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainresult));
            this.lblTotalBacklogs = new System.Windows.Forms.Label();
            this.heading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTotalBacklogs
            // 
            this.lblTotalBacklogs.AutoSize = true;
            this.lblTotalBacklogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBacklogs.Location = new System.Drawing.Point(755, 667);
            this.lblTotalBacklogs.Name = "lblTotalBacklogs";
            this.lblTotalBacklogs.Size = new System.Drawing.Size(109, 39);
            this.lblTotalBacklogs.TabIndex = 1;
            this.lblTotalBacklogs.Text = "label1";
            // 
            // heading
            // 
            this.heading.AutoSize = true;
            this.heading.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heading.Location = new System.Drawing.Point(802, 31);
            this.heading.Name = "heading";
            this.heading.Size = new System.Drawing.Size(109, 39);
            this.heading.TabIndex = 2;
            this.heading.Text = "label1";
            // 
            // mainresult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1613, 820);
            this.Controls.Add(this.heading);
            this.Controls.Add(this.lblTotalBacklogs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainresult";
            this.Text = "JNTUHUCEM PORTAL";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTotalBacklogs;
        private System.Windows.Forms.Label heading;
    }
}