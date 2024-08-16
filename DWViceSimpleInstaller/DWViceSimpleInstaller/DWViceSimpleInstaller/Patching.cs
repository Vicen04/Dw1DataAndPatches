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
    public partial class Patching : Form
    {
        public Patching()
        {
            InitializeComponent();
        }

        private void ClosePatcher_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
