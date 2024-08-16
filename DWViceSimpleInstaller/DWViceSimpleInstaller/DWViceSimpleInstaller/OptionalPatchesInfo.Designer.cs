namespace DWViceSimpleInstaller
{
    partial class OptionalPatchesInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionalPatchesInfo));
            this.OptionalPatchesInfoTitle = new System.Windows.Forms.Label();
            this.OptionalPatchesInfoText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // OptionalPatchesInfoTitle
            // 
            this.OptionalPatchesInfoTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.OptionalPatchesInfoTitle.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptionalPatchesInfoTitle.Location = new System.Drawing.Point(0, 15);
            this.OptionalPatchesInfoTitle.Name = "OptionalPatchesInfoTitle";
            this.OptionalPatchesInfoTitle.Size = new System.Drawing.Size(785, 44);
            this.OptionalPatchesInfoTitle.TabIndex = 0;
            this.OptionalPatchesInfoTitle.Text = "Optional patches info";
            this.OptionalPatchesInfoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OptionalPatchesInfoText
            // 
            this.OptionalPatchesInfoText.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.OptionalPatchesInfoText.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OptionalPatchesInfoText.Location = new System.Drawing.Point(100, 80);
            this.OptionalPatchesInfoText.Multiline = true;
            this.OptionalPatchesInfoText.Name = "OptionalPatchesInfoText";
            this.OptionalPatchesInfoText.ReadOnly = true;
            this.OptionalPatchesInfoText.Size = new System.Drawing.Size(600, 450);
            this.OptionalPatchesInfoText.TabIndex = 1;
            this.OptionalPatchesInfoText.Text = resources.GetString("OptionalPatchesInfoText.Text");
            this.OptionalPatchesInfoText.SelectionLength = 0;
            this.OptionalPatchesInfoText.SelectionStart = 0;
            // 
            // OptionalPatchesInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.OptionalPatchesInfoText);
            this.Controls.Add(this.OptionalPatchesInfoTitle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionalPatchesInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Optional patches info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label OptionalPatchesInfoTitle;
        private System.Windows.Forms.TextBox OptionalPatchesInfoText;
    }
}