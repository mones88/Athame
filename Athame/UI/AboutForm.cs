using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Athame.Properties;

namespace Athame.UI
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            pictureBox1.Image = new Icon(Resources.AthameIcon, 128, 128).ToBitmap();
            label2.Text = String.Format(label2.Text, Application.ProductVersion);
        }

        private void licensesTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
    }
}
