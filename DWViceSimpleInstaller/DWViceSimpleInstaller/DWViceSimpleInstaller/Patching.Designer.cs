namespace DWViceSimpleInstaller
{
    partial class Patching
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
            this.ClosePatcher = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ClosePatcher
            // 
            this.ClosePatcher.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(64)));
            this.ClosePatcher.Location = new System.Drawing.Point(190, 110);
            this.ClosePatcher.Name = "ClosePatcher";
            this.ClosePatcher.Size = new System.Drawing.Size(100, 40);
            this.ClosePatcher.TabIndex = 0;
            this.ClosePatcher.Text = "Close";
            this.ClosePatcher.UseVisualStyleBackColor = true;
            this.ClosePatcher.Click += new System.EventHandler(this.ClosePatcher_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(117, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "The patch was succesful";
            // 
            // Patching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 161);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ClosePatcher);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Patching";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patching";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ClosePatcher;
        private System.Windows.Forms.Label label1;
    }
}