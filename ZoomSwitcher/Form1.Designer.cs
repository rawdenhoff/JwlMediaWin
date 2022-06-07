namespace ZoomSwitcher
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.chkJWL = new System.Windows.Forms.CheckBox();
            this.chkZoom = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Visible = true;
            // 
            // chkJWL
            // 
            this.chkJWL.AutoSize = true;
            this.chkJWL.Checked = true;
            this.chkJWL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkJWL.Enabled = false;
            this.chkJWL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkJWL.Location = new System.Drawing.Point(48, 13);
            this.chkJWL.Name = "chkJWL";
            this.chkJWL.Size = new System.Drawing.Size(66, 28);
            this.chkJWL.TabIndex = 0;
            this.chkJWL.Text = "JWL";
            this.chkJWL.UseVisualStyleBackColor = true;
            this.chkJWL.CheckedChanged += new System.EventHandler(this.chkJWL_CheckedChanged);
            // 
            // chkZoom
            // 
            this.chkZoom.AutoSize = true;
            this.chkZoom.Checked = true;
            this.chkZoom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkZoom.Enabled = false;
            this.chkZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkZoom.Location = new System.Drawing.Point(48, 39);
            this.chkZoom.Name = "chkZoom";
            this.chkZoom.Size = new System.Drawing.Size(79, 28);
            this.chkZoom.TabIndex = 1;
            this.chkZoom.Text = "Zoom";
            this.chkZoom.UseVisualStyleBackColor = true;
            this.chkZoom.CheckedChanged += new System.EventHandler(this.chkZoom_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 79);
            this.Controls.Add(this.chkZoom);
            this.Controls.Add(this.chkJWL);
            this.Name = "Form1";
            this.Text = "Switcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Unload);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.CheckBox chkJWL;
        private System.Windows.Forms.CheckBox chkZoom;
    }
}

