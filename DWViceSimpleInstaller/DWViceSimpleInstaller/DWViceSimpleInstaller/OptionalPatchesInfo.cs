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
    public partial class OptionalPatchesInfo : Form
    {
        public OptionalPatchesInfo()
        {
            InitializeComponent();
        }

        public void ChangeTitle(string newTitle)
        {
            this.OptionalPatchesInfoTitle.Text = newTitle;
        }

        public void ChangeText(string newText)
        {
            this.OptionalPatchesInfoText.Text = newText;
            this.OptionalPatchesInfoText.SelectionStart = 0;
            this.OptionalPatchesInfoText.SelectionLength = 0;
        }
    }
}
