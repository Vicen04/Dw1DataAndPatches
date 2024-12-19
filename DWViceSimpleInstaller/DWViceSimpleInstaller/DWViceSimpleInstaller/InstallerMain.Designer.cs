namespace DWViceSimpleInstaller
{
    partial class MainApp
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
            this.Title1 = new System.Windows.Forms.Label();
            this.VicePatchButton = new System.Windows.Forms.Button();
            this.HardcorePatcher = new System.Windows.Forms.Button();
            this.OptionalPatcher = new System.Windows.Forms.Button();
            this.ViceHackInfo = new System.Windows.Forms.Button();
            this.HardcoreInfo = new System.Windows.Forms.Button();
            this.OptionalPatchesInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Title1
            // 
            this.Title1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Title1.AutoSize = true;
            this.Title1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Title1.Font = new System.Drawing.Font("Arial Black", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Title1.Location = new System.Drawing.Point(29, 9);
            this.Title1.Name = "Title1";
            this.Title1.Size = new System.Drawing.Size(1082, 68);
            this.Title1.TabIndex = 0;
            this.Title1.Text = "Digimon World Patch Installer by Vice04";
            this.Title1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VicePatchButton
            // 
            this.VicePatchButton.BackColor = System.Drawing.Color.Aqua;
            this.VicePatchButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.VicePatchButton.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VicePatchButton.Location = new System.Drawing.Point(314, 186);
            this.VicePatchButton.Name = "VicePatchButton";
            this.VicePatchButton.Size = new System.Drawing.Size(400, 75);
            this.VicePatchButton.TabIndex = 1;
            this.VicePatchButton.Text = "Vice hack 2.0.2 patcher";
            this.VicePatchButton.UseVisualStyleBackColor = false;
            this.VicePatchButton.Click += new System.EventHandler(this.VicePatchButton_Click);
            // 
            // HardcorePatcher
            // 
            this.HardcorePatcher.BackColor = System.Drawing.Color.Firebrick;
            this.HardcorePatcher.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HardcorePatcher.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.HardcorePatcher.Location = new System.Drawing.Point(314, 352);
            this.HardcorePatcher.Name = "HardcorePatcher";
            this.HardcorePatcher.Size = new System.Drawing.Size(400, 75);
            this.HardcorePatcher.TabIndex = 2;
            this.HardcorePatcher.Text = "Hardcore hack patcher";
            this.HardcorePatcher.UseVisualStyleBackColor = false;
            this.HardcorePatcher.Click += new System.EventHandler(this.HardcorePatcher_Click);
            // 
            // OptionalPatcher
            // 
            this.OptionalPatcher.BackColor = System.Drawing.Color.Goldenrod;
            this.OptionalPatcher.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptionalPatcher.Location = new System.Drawing.Point(314, 514);
            this.OptionalPatcher.Name = "OptionalPatcher";
            this.OptionalPatcher.Size = new System.Drawing.Size(400, 75);
            this.OptionalPatcher.TabIndex = 3;
            this.OptionalPatcher.Text = "Optional patches only";
            this.OptionalPatcher.UseVisualStyleBackColor = false;
            this.OptionalPatcher.Click += new System.EventHandler(this.OptionalPatcher_Click);
            // 
            // ViceHackInfo
            // 
            this.ViceHackInfo.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViceHackInfo.Location = new System.Drawing.Point(759, 186);
            this.ViceHackInfo.Name = "ViceHackInfo";
            this.ViceHackInfo.Size = new System.Drawing.Size(75, 75);
            this.ViceHackInfo.TabIndex = 4;
            this.ViceHackInfo.Text = "?";
            this.ViceHackInfo.UseVisualStyleBackColor = true;
            this.ViceHackInfo.Click += new System.EventHandler(this.ViceHackInfo_Click);
            // 
            // HardcoreInfo
            // 
            this.HardcoreInfo.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HardcoreInfo.Location = new System.Drawing.Point(759, 352);
            this.HardcoreInfo.Name = "HardcoreInfo";
            this.HardcoreInfo.Size = new System.Drawing.Size(75, 75);
            this.HardcoreInfo.TabIndex = 5;
            this.HardcoreInfo.Text = "?";
            this.HardcoreInfo.UseVisualStyleBackColor = true;
            this.HardcoreInfo.Click += new System.EventHandler(this.HardcoreInfo_Click);
            // 
            // OptionalPatchesInfo
            // 
            this.OptionalPatchesInfo.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptionalPatchesInfo.Location = new System.Drawing.Point(759, 514);
            this.OptionalPatchesInfo.Name = "OptionalPatchesInfo";
            this.OptionalPatchesInfo.Size = new System.Drawing.Size(75, 75);
            this.OptionalPatchesInfo.TabIndex = 6;
            this.OptionalPatchesInfo.Text = "?";
            this.OptionalPatchesInfo.UseVisualStyleBackColor = true;
            this.OptionalPatchesInfo.Click += new System.EventHandler(this.OptionalPatchesInfo_Click);
            // 
            // MainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 693);
            this.Controls.Add(this.OptionalPatchesInfo);
            this.Controls.Add(this.HardcoreInfo);
            this.Controls.Add(this.ViceHackInfo);
            this.Controls.Add(this.OptionalPatcher);
            this.Controls.Add(this.HardcorePatcher);
            this.Controls.Add(this.VicePatchButton);
            this.Controls.Add(this.Title1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "MainApp";
            this.Text = "Digimon patcher by Vice04";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title1;
        private System.Windows.Forms.Button VicePatchButton;
        private System.Windows.Forms.Button HardcorePatcher;
        private System.Windows.Forms.Button OptionalPatcher;
        private System.Windows.Forms.Button ViceHackInfo;
        private System.Windows.Forms.Button HardcoreInfo;
        private System.Windows.Forms.Button OptionalPatchesInfo;
    }
}

