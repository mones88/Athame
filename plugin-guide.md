Creating your own plugins
=========================

In Athame, new services can be added by creating plugins. A plugin is simply a .NET assembly that's loaded and activated at runtime.
Plugins can be created in any IDE, but in this tutorial we will be focusing on Visual Studio.
It would also be a good idea to download a copy of the Athame source code so it's easier to debug any problems. You should download
the source of the most recent release, which is available from the "Releases" tab.

Getting started
---------------
* First, we need to create a new project. Click `File > New > Project`, then in your language of choice choose the "Class Library"
  template. In the name field, make sure the name begins with "AthamePlugin." - for instance, "AthamePlugin.FooBar". This will
  ensure the plugin is recognised and loaded by Athame.

* After the project has been created, right-click the "References" item in the solution explorer window and click "Add Reference".
  Then click the "Browse..." button at the bottom of the window and browse to your copy of `Athame.PluginAPI.dll`, which is pre-built
  and in the same folder as the main Athame executable. You can also build it yourself from the source code copy of Athame.
  Make sure it is the most recent release.

* Select the "Athame.PluginAPI" reference and then click the properties tab. Set "Copy Local" to "False".

* We also need to add references to `System.Windows.Forms` and `System.Drawing`. In solution explorer, right-click References,
  click add, and under the "Assemblies" section to the left make sure both of those assemblies are checked, then click OK.

* Now we can add the main plugin class. Right-click the project, then choose `Add > New Item`. Name it the same as your plugin's name,
  but with "MusicService" at the end, for instance "FooBarMusicService". Also, delete the "Class1" class file if it exists, as it is no
  longer necessary.

* Once you have created the main plugin service class, make it inherit the `MusicService` class as found in `Athame.PluginAPI.Service` namespace.
  Visual Studio should prompt you to implement the missing members, so go ahead and do that.

Now that you have done that, your main class should look something like this:
	

	using System;
	using System.Threading.Tasks;
	using System.Windows.Forms;
	using Athame.PluginAPI;
	using Athame.PluginAPI.Downloader;
	using Athame.PluginAPI.Service;

	namespace AthamePlugin.FooBar
	{
	    public class FooBarMusicService : MusicService
	    {
	        public override Task<TrackFile> GetDownloadableTrackAsync(Track track)
	        {
	            throw new NotImplementedException();
	        }

	        public override Task<Playlist> GetPlaylistAsync(string playlistId)
	        {
	            throw new NotImplementedException();
	        }

	        public override UrlParseResult ParseUrl(Uri url)
	        {
	            throw new NotImplementedException();
	        }

	        public override Task<SearchResult> SearchAsync(string searchText, MediaType typesToRetrieve)
	        {
	            throw new NotImplementedException();
	        }

	        public override Task<Album> GetAlbumAsync(string albumId, bool withTracks)
	        {
	            throw new NotImplementedException();
	        }

	        public override Task<Track> GetTrackAsync(string trackId)
	        {
	            throw new NotImplementedException();
	        }

	        public override Control GetSettingsControl()
	        {
	            throw new NotImplementedException();
	        }

	        public override void Init(AthameApplication application, PluginContext pluginContext)
	        {
	            throw new NotImplementedException();
	        }

	        public override string Name { get; }
	        public override string Description { get; }
	        public override string Author { get; }
	        public override Uri Website { get; }
	        public override PluginVersion ApiVersion { get; }
	        public override object Settings { get; set; }
	        public override Uri[] BaseUri { get; }
	    }
	}

* There are a number of properties you should fill out with constant values: `Name`, `ApiVersion`, `BaseUri` and optionally `Description`, `Author`, and 
  `Website`. See the documentation for more details.

Authentication
--------------

* If your service requires username and password authentication, also implement the `IUsernamePasswordAuthenticationAsync` interface. This will enable Athame
  to manage the authentication process for you - you do not have to worry about creating a custom sign-in dialog, or managing the session.

* If your service requires custom authentication (for instance OAuth or some other kind), implement the `IAuthenticatableAsync` interface. This still enables
  Athame to manage the session, but allows you to show a custom authentication UI or window.

Settings
--------

* The `Settings` property is an object that you can use to store persistent values. At startup, the plugin's main class is instantiated. If there are no persistent
  settings, the property's value is retrieved and stored. This is because Athame expects the plugin to provide a default settings instance in this case.

  If there are persistent settings, the value is set. Athame will always pass an instance of what was saved, so you can directly cast to your plugin's settings type
  without worrying.

* If you are also implementing `IAuthenticatableAsync`, `IUsernamePasswordAuthenticationAsync` etc, you should set the `Account` property in the setter of `Settings`.

An example implementation:

### Class PlayMusicServiceSettings

    public class PlayMusicServiceSettings
    {
        public StreamQuality StreamQuality { get; set; }

        public string Email { get; set; }
        public string SessionToken { get; set; }

        public PlayMusicServiceSettings()
        {
            StreamQuality = StreamQuality.High;
        }
    }

### Class PlayMusicService
	private PlayMusicServiceSettings settings = new PlayMusicServiceSettings();

	// ...

    public override object Settings
    {
        get
        {
            return settings;
        }
        set
        {
            settings = (PlayMusicServiceSettings)value ?? new PlayMusicServiceSettings();
            Account = new AccountInfo
            {
                DisplayId = settings.Email
            };
        }
    }