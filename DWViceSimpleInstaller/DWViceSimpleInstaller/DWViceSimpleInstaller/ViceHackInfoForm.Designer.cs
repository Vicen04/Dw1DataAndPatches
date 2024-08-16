namespace DWViceSimpleInstaller
{
    partial class ViceHackInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViceHackInfo));
            this.ViceHackInfoText = new System.Windows.Forms.TextBox();
            this.ViceHackInfoTitle = new System.Windows.Forms.Label();
            this.ViceHackInfoLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // ViceHackInfoText
            // 
            this.ViceHackInfoText.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ViceHackInfoText.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ViceHackInfoText.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ViceHackInfoText.HideSelection = false;
            this.ViceHackInfoText.Location = new System.Drawing.Point(100, 80);
            this.ViceHackInfoText.Multiline = true;
            this.ViceHackInfoText.Name = "ViceHackInfoText";
            this.ViceHackInfoText.ReadOnly = true;
            this.ViceHackInfoText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ViceHackInfoText.ShortcutsEnabled = false;
            this.ViceHackInfoText.Size = new System.Drawing.Size(600, 550);
            this.ViceHackInfoText.TabIndex = 0;
            this.ViceHackInfoText.Text = resources.GetString("ViceHackInfoText.Text");
            this.ViceHackInfoText.SelectionStart = 0;
            this.ViceHackInfoText.SelectionLength = 0;
            // 
            // ViceHackInfoTitle
            // 
            this.ViceHackInfoTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ViceHackInfoTitle.Font = new System.Drawing.Font("Arial", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViceHackInfoTitle.Location = new System.Drawing.Point(0, 15);
            this.ViceHackInfoTitle.Name = "ViceHackInfoTitle";
            this.ViceHackInfoTitle.Size = new System.Drawing.Size(785, 45);
            this.ViceHackInfoTitle.TabIndex = 1;
            this.ViceHackInfoTitle.Text = "Vice hack Info";
            this.ViceHackInfoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ViceHackInfoLink
            // 
            this.ViceHackInfoLink.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViceHackInfoLink.Location = new System.Drawing.Point(0, 660);
            this.ViceHackInfoLink.Name = "ViceHackInfoLink";
            this.ViceHackInfoLink.Size = new System.Drawing.Size(785, 26);
            this.ViceHackInfoLink.TabIndex = 2;
            this.ViceHackInfoLink.TabStop = true;
            this.ViceHackInfoLink.Text = "Spreadsheet with detailed information";
            this.ViceHackInfoLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ViceHackInfoLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ViceHackInfoLink_LinkClicked);
            // 
            // ViceHackInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 711);
            this.Controls.Add(this.ViceHackInfoLink);
            this.Controls.Add(this.ViceHackInfoTitle);
            this.Controls.Add(this.ViceHackInfoText);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViceHackInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vice hack help window";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ViceHackInfoText;
        private System.Windows.Forms.Label ViceHackInfoTitle;
        private System.Windows.Forms.LinkLabel ViceHackInfoLink;
    }
}