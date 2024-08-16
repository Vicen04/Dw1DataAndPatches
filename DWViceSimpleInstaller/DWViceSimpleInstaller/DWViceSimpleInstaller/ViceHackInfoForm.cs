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
    public partial class ViceHackInfo : Form
    {
        string originalLink = "https://docs.google.com/spreadsheets/d/17-Xsd4rshCH_MenzfCeV3TdKIFyfvxLPL4Q1eebMezQ/edit?usp=sharing";

        public ViceHackInfo()
        {
            InitializeComponent();            
        }

        private void ViceHackInfoLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.ViceHackInfoLink.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start(originalLink);
        }

        public void ChangeTitle(string newTitle)
        {
            this.ViceHackInfoTitle.Text = newTitle;
        }

        public void ChangeText(string newText)
        {
            this.ViceHackInfoText.Text = newText;
            this.ViceHackInfoText.SelectionStart = 0;
            this.ViceHackInfoText.SelectionLength = 0;
        }

        public void ChangeLink(string newDescription, string newLink)
        {
            this.ViceHackInfoLink.Text = newDescription;
            originalLink = newLink;
        }

        public void DisableLink()
        {
            this.ViceHackInfoLink.Text = "";
            this.ViceHackInfoLink.Enabled = false;
            this.ViceHackInfoLink.Visible = false;
        }
    }
}
