using System;
using System.Windows.Forms;
using Athame.Plugin;
using Athame.PluginAPI.Service;

namespace Athame.UI
{
    public partial class ServiceSettingsView : UserControl
    {

        private readonly SplitStringParser sspSignInStatus, sspSignInButton;
        private readonly MusicService service;
        private readonly IAuthenticatable authenticatable;

        public ServiceSettingsView(MusicService service)
        {
            this.service = service;
            authenticatable = service.AsAuthenticatable();
            if (authenticatable == null)
            {
                throw new ArgumentException("Service instance passed must implement IAuthenticatable", nameof(service));
            }
            InitializeComponent();
            sspSignInStatus = new SplitStringParser(signInStatusLabel);
            sspSignInButton = new SplitStringParser(signInButton);
            if (authenticatable.IsAuthenticated)
            {
                signInStatusLabel.Text = String.Format(sspSignInStatus.Get(authenticatable.IsAuthenticated),
                    LocalisableAccountNameFormat.GetFormattedName(authenticatable.Account));
            }
            else
            {
                sspSignInStatus.Update(false);
            }
            sspSignInButton.Update(authenticatable.IsAuthenticated);
            var control = service.GetSettingsControl();
            control.Dock = DockStyle.Fill;
            servicePanel.Controls.Add(control);
        }

        private async void signInButton_Click(object sender, EventArgs e)
        {
            if (!authenticatable.IsAuthenticated)
            {
                var authenticatableAsync = service.AsAuthenticatableAsync();
                if (authenticatableAsync == null)
                {
                    var dlg = new CredentialsForm(service);
                    var result = dlg.ShowDialog();
                    if (result != DialogResult.OK) return;
                }
                else
                {
                    await authenticatableAsync.AuthenticateAsync();
                }
                signInStatusLabel.Text = String.Format(sspSignInStatus.Get(authenticatable.IsAuthenticated),
                    LocalisableAccountNameFormat.GetFormattedName(authenticatable.Account));
                sspSignInButton.Update(authenticatable.IsAuthenticated);
            }
            else
            {
                authenticatable.Reset();
                sspSignInStatus.Update(false);
                sspSignInButton.Update(false);
            }

        }
    }
}
