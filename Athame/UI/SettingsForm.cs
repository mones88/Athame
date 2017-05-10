using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Athame.Plugin;
using Athame.PluginAPI.Service;
using Athame.Properties;
using Athame.Settings;

namespace Athame.UI
{
    public partial class SettingsForm : Form
    {
        private readonly AthameSettings defaults = Program.DefaultSettings.Settings;
        private readonly List<MusicService> services;

        public SettingsForm()
        {
            InitializeComponent();
            // General save
            saveToRadioButton.Checked = !defaults.GeneralSavePreference.AskForLocation;
            askWhereToSaveRadioButton.Checked = defaults.GeneralSavePreference.AskForLocation;
            saveLocLabel.Text = defaults.GeneralSavePreference.SaveDirectory;
            pathFormatTextBox.Text = defaults.GeneralSavePreference.SaveFormat;

            // Inherit checkbox
            pldSameAsAlbumTrack.Checked = defaults.PlaylistSavePreferenceUsesGeneral;

            // Playlist save
            pldSaveToRadioButton.Checked = !defaults.PlaylistSavePreference.AskForLocation;
            pldAskWhereToSaveRadioButton.Checked = defaults.PlaylistSavePreference.AskForLocation;
            pldSaveLocLabel.Text = defaults.PlaylistSavePreference.SaveDirectory;
            pldPathFormatTextBox.Text = defaults.PlaylistSavePreference.SaveFormat;

            // Album artwork save setting
            switch (defaults.AlbumArtworkSaveFormat)
            {
                case AlbumArtworkSaveFormat.DontSave:
                    artworkDontSaveRadioButton.Checked = true;
                    break;
                case AlbumArtworkSaveFormat.AsCover:
                    artworkSaveAsFileRadioButton.Checked = true;
                    break;
                case AlbumArtworkSaveFormat.AsArtistAlbum:
                    artworkSaveAsFormattedFileRadioButton.Checked = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Populate services
            services = new List<MusicService>(Program.DefaultPluginManager.ServicesEnumerable());
            foreach (var service in services)
            {
                servicesListBox.Items.Add(service.Info.Name);
            }
            if (servicesListBox.Items.Count > 0) servicesListBox.SelectedIndex = 0;
        }


        #region ' General save event handlers 

        private void saveLocBrowseButton_Click(object sender, EventArgs e)
        {
            if (mFolderBrowserDialog.ShowDialog() != DialogResult.OK) return;
            defaults.GeneralSavePreference.SaveDirectory = mFolderBrowserDialog.SelectedPath;
            saveLocLabel.Text = defaults.GeneralSavePreference.SaveDirectory;
        }

        private void pathFormatTextBox_TextChanged(object sender, EventArgs e)
        {
            defaults.GeneralSavePreference.SaveFormat = pathFormatTextBox.Text;
        }

        private void saveToRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            defaults.GeneralSavePreference.AskForLocation = !saveToRadioButton.Checked;
        }

        private void askWhereToSaveRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            defaults.GeneralSavePreference.AskForLocation = askWhereToSaveRadioButton.Checked;
        }
        #endregion

        #region ' Playlist save event handlers 

        private void pldSaveToRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            defaults.PlaylistSavePreference.AskForLocation = !pldSaveToRadioButton.Checked;
        }

        private void pldAskWhereToSaveRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            defaults.PlaylistSavePreference.AskForLocation = pldAskWhereToSaveRadioButton.Checked;
        }

        private void pldPathFormatTextBox_TextChanged(object sender, EventArgs e)
        {
            defaults.PlaylistSavePreference.SaveFormat = pldPathFormatTextBox.Text;
        }

        private void pldSaveLocBrowseButton_Click(object sender, EventArgs e)
        {
            if (mFolderBrowserDialog.ShowDialog() != DialogResult.OK) return;
            defaults.PlaylistSavePreference.SaveDirectory = mFolderBrowserDialog.SelectedPath;
            saveLocLabel.Text = defaults.PlaylistSavePreference.SaveDirectory;
        }

        #endregion


        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Program.DefaultSettings.Save();
            DialogResult = DialogResult.OK;
        }

        private MusicService ss = null;

        private void servicesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ss = services[servicesListBox.SelectedIndex];
            serviceUiPanel.Controls.Clear();
            if (ss.AsAuthenticatable() != null)
            {
                var ssv = new ServiceSettingsView(ss) {Dock = DockStyle.Fill};
                serviceUiPanel.Controls.Add(ssv);
            }
            else
            {
                serviceUiPanel.Controls.Add(ss.GetSettingsControl());
            }
            serviceNameLabel.Text = ss.Info.Name;
            serviceDescriptionLabel.Text = ss.Info.Description;
            serviceAuthorLabel.Text = ss.Info.Author;
            serviceWebsiteLabel.Text = ss.Info.Website.ToString();
        }

        private void formatHelpButton_Click(object sender, EventArgs e)
        {
            var formatForm = new PathFormatHelpForm();
            var rightTopCorner = new Point(Location.X + Width, Location.Y);
            formatForm.StartPosition = FormStartPosition.Manual;
            formatForm.Location = rightTopCorner;
            formatForm.Show(this);
            
        }
        private void pldSameAsAlbumTrack_CheckedChanged(object sender, EventArgs e)
        {
            defaults.PlaylistSavePreferenceUsesGeneral = pldSameAsAlbumTrack.Checked;
            pldOptionsGroupBox.Enabled = !pldSameAsAlbumTrack.Checked;
        }

        #region ' Album artwork radio buttons 
        private void artworkSaveAsFileRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (artworkSaveAsFileRadioButton.Checked) defaults.AlbumArtworkSaveFormat = AlbumArtworkSaveFormat.AsCover;
        }

        private void artworkSaveAsFormattedFileRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (artworkSaveAsFormattedFileRadioButton.Checked) defaults.AlbumArtworkSaveFormat = AlbumArtworkSaveFormat.AsArtistAlbum;
        }

        private void artworkDontSaveRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (artworkDontSaveRadioButton.Checked) defaults.AlbumArtworkSaveFormat = AlbumArtworkSaveFormat.DontSave;
        }
        #endregion

        private void openPluginDirButton_Click(object sender, EventArgs e)
        {
            Process.Start("\"" + Program.DefaultPluginManager.PluginDirectory + "\"");
        }

        private void serviceWebsiteLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ss == null) return;
            Process.Start("\"" + ss.Info.Website.ToString() + "\"");
        }
    }
}
