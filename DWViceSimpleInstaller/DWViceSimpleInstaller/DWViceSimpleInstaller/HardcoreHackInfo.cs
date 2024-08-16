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
    public partial class HardcoreHackInfo : Form
    {

        string defaultLink = "https://docs.google.com/spreadsheets/d/13hKiq2UGXikMRRLJfKpkudDk9RZ4BRvlCYma1Sof11A/edit?usp=sharing";

        public HardcoreHackInfo()
        {
            InitializeComponent();
        }

        private void HardcoreHackInfoLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.HardcoreHackInfoLink.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start(defaultLink);
        }

        public void ChangeLink(string newLink, string newDescription)
        {
            defaultLink = newLink;
            this.HardcoreHackInfoLink.Text = newDescription;
        }


        public void ChangeTitle(string newTitle)
        {
            this.HardcoreHackInfoTitle.Text = newTitle;
        }

        public void ChangeText(string newText)
        {
            this.HardcoreHackInfoText.Text = newText;
            this.HardcoreHackInfoText.SelectionStart = 0;
            this.HardcoreHackInfoText.SelectionLength= 0;
        }
    }
}
