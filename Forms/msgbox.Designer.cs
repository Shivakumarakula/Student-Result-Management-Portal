//namespace project_RYS
//{
//    partial class msgbox
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(msgbox));
//            this.panel1 = new System.Windows.Forms.Panel();
//            this.button1 = new System.Windows.Forms.Button();
//            this.msg = new System.Windows.Forms.Label();
//            this.SuspendLayout();
//            // 
//            // panel1
//            // 
//            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
//            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
//            this.panel1.Location = new System.Drawing.Point(0, 265);
//            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
//            this.panel1.Name = "panel1";
//            this.panel1.Size = new System.Drawing.Size(770, 29);
//            this.panel1.TabIndex = 0;
//            // 
//            // button1
//            // 
//            this.button1.BackColor = System.Drawing.Color.SkyBlue;
//            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.button1.FlatAppearance.BorderSize = 0;
//            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.button1.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.button1.Location = new System.Drawing.Point(537, 176);
//            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
//            this.button1.Name = "button1";
//            this.button1.Size = new System.Drawing.Size(140, 56);
//            this.button1.TabIndex = 1;
//            this.button1.Text = "Okay";
//            this.button1.UseVisualStyleBackColor = false;
//            this.button1.Click += new System.EventHandler(this.button1_Click);
//            // 
//            // msg
//            // 
//            this.msg.AutoSize = true;
//            this.msg.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.msg.Location = new System.Drawing.Point(94, 86);
//            this.msg.Name = "msg";
//            this.msg.Size = new System.Drawing.Size(0, 27);
//            this.msg.TabIndex = 2;
//            // 
//            // msgbox
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.AutoScroll = true;
//            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
//            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
//            this.ClientSize = new System.Drawing.Size(770, 294);
//            this.Controls.Add(this.msg);
//            this.Controls.Add(this.button1);
//            this.Controls.Add(this.panel1);
//            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
//            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
//            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
//            this.Name = "msgbox";
//            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
//            this.Text = "JNTUHUCEM PORTAL";
//            this.Load += new System.EventHandler(this.msgbox_Load);
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        #endregion

//        private System.Windows.Forms.Panel panel1;
//        private System.Windows.Forms.Button button1;
//        private System.Windows.Forms.Label msg;
//    }
//}

using System.Windows.Forms;

namespace project_RYS
{
    partial class msgbox
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox msg;
        private Button buttonClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.msg = new System.Windows.Forms.TextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // msg
            // 
            this.msg.BackColor = this.BackColor;
            this.msg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.msg.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.msg.Location = new System.Drawing.Point(22, 20);
            this.msg.Multiline = true;
            this.msg.Name = "msg";
            this.msg.ReadOnly = true;
            this.msg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.msg.Size = new System.Drawing.Size(791, 304);
            this.msg.TabIndex = 0;
            this.msg.TabStop = false;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(354, 330);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(101, 30);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // msgbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 372);
            this.Controls.Add(this.msg);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "msgbox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message";
            this.Load += new System.EventHandler(this.msgbox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
