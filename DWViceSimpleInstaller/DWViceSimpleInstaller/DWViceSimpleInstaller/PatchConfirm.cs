using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DWViceSimpleInstaller
{
    public partial class PatchConfirm : Form
    {
        MainApp mainForm;

        public PatchConfirm(MainApp form)
        {
            InitializeComponent();
            mainForm = form;
            directory.Text = System.IO.Directory.GetCurrentDirectory();
        }

        private void directory_DoubleClick(object sender, EventArgs e)
        {
            folderBrowserDialog1 = new FolderBrowserDialog()
            {
                Description = "Select a folder to save the new file"                
            };
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK);
                directory.Text = folderBrowserDialog1.SelectedPath;
        }

        private void patch_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            mainForm.CreatePatchedFile(directory.Text, fileName.Text);

        }

        public void ConfirmExit()
        {
            Form form = new Patching();
            form.FormClosed += new FormClosedEventHandler(CloseApp);
            form.ShowDialog();
        }

        private void CloseApp(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetFileName(string name)
        {
            fileName.Text = name;
        }
    }
}
