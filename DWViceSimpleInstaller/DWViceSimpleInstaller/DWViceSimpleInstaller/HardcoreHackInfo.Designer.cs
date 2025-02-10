namespace DWViceSimpleInstaller
{
    partial class HardcoreHackInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HardcoreHackInfo));
            this.HardcoreHackInfoTitle = new System.Windows.Forms.Label();
            this.HardcoreHackInfoLink = new System.Windows.Forms.LinkLabel();
            this.HardcoreHackInfoText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // HardcoreHackInfoTitle
            // 
            this.HardcoreHackInfoTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.HardcoreHackInfoTitle.BackColor = System.Drawing.SystemColors.Control;
            this.HardcoreHackInfoTitle.Font = new System.Drawing.Font("Arial", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HardcoreHackInfoTitle.Location = new System.Drawing.Point(0, 15);
            this.HardcoreHackInfoTitle.Name = "HardcoreHackInfoTitle";
            this.HardcoreHackInfoTitle.Size = new System.Drawing.Size(785, 40);
            this.HardcoreHackInfoTitle.TabIndex = 2;
            this.HardcoreHackInfoTitle.Text = "Hardcore hack Info";
            this.HardcoreHackInfoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HardcoreHackInfoLink
            // 
            this.HardcoreHackInfoLink.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HardcoreHackInfoLink.Location = new System.Drawing.Point(0, 665);
            this.HardcoreHackInfoLink.Name = "HardcoreHackInfoLink";
            this.HardcoreHackInfoLink.Size = new System.Drawing.Size(785, 26);
            this.HardcoreHackInfoLink.TabIndex = 3;
            this.HardcoreHackInfoLink.TabStop = true;
            this.HardcoreHackInfoLink.Text = "Spreadsheet with detailed information";
            this.HardcoreHackInfoLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.HardcoreHackInfoLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HardcoreHackInfoLink_LinkClicked);
            // 
            // HardcoreHackInfoText
            // 
            this.HardcoreHackInfoText.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.HardcoreHackInfoText.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HardcoreHackInfoText.Location = new System.Drawing.Point(100, 70);
            this.HardcoreHackInfoText.Multiline = true;
            this.HardcoreHackInfoText.Name = "HardcoreHackInfoText";
            this.HardcoreHackInfoText.ReadOnly = true;
            this.HardcoreHackInfoText.Size = new System.Drawing.Size(600, 580);
            this.HardcoreHackInfoText.TabIndex = 4;
            this.HardcoreHackInfoText.Text = resources.GetString("HardcoreHackInfoText.Text");
            this.HardcoreHackInfoText.SelectionLength = 0;
            this.HardcoreHackInfoText.SelectionStart = 0;
            // 
            // HardcoreHackInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 711);
            this.Controls.Add(this.HardcoreHackInfoText);
            this.Controls.Add(this.HardcoreHackInfoLink);
            this.Controls.Add(this.HardcoreHackInfoTitle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HardcoreHackInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hardcore Hack Info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label HardcoreHackInfoTitle;
        private System.Windows.Forms.LinkLabel HardcoreHackInfoLink;
        private System.Windows.Forms.TextBox HardcoreHackInfoText;
    }
}