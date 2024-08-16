namespace DWViceSimpleInstaller
{
    partial class PatchConfirm
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
            this.fileName = new System.Windows.Forms.TextBox();
            this.directory = new System.Windows.Forms.TextBox();
            this.patch = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // fileName
            // 
            this.fileName.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(64)));
            this.fileName.Location = new System.Drawing.Point(90, 100);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(600, 32);
            this.fileName.TabIndex = 0;
            this.fileName.Text = "filename";
            // 
            // directory
            // 
            this.directory.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.directory.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(64)));
            this.directory.Location = new System.Drawing.Point(90, 30);
            this.directory.Name = "directory";
            this.directory.ReadOnly = true;
            this.directory.Size = new System.Drawing.Size(600, 32);
            this.directory.TabIndex = 1;
            this.directory.Text = "directory";
            this.directory.DoubleClick += new System.EventHandler(this.directory_DoubleClick);
            // 
            // patch
            // 
            this.patch.Font = new System.Drawing.Font("Arial Black", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(64)));
            this.patch.Location = new System.Drawing.Point(300, 159);
            this.patch.Name = "patch";
            this.patch.Size = new System.Drawing.Size(200, 40);
            this.patch.TabIndex = 2;
            this.patch.Text = "Start patcher";
            this.patch.UseVisualStyleBackColor = true;
            this.patch.Click += new System.EventHandler(this.patch_Click);
            // 
            // PatchConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 211);
            this.Controls.Add(this.patch);
            this.Controls.Add(this.directory);
            this.Controls.Add(this.fileName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatchConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PatchConfirm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.TextBox directory;
        private System.Windows.Forms.Button patch;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}