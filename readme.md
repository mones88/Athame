[Athame](https://svbnet.co/athame)
======
Athame is a program for downloading music from music streaming and sharing services.
It is intended for educational and private use only, and **not** as a tool for pirating and distributing music.
Above all else, remember that the artists and studios put a lot of work into making music -- if you can, purchase
music directly from the artist as more money goes to them.

Since I am also caught up with other things I can't devote all my time to fixing and improving Athame. Right now it is
just a very buggy, basic tool which I hope will either be improved upon in the future, or be contributed to.

Download
--------
[Click the 'Releases' tab above at the top to download the latest, or just click here.](https://github.com/svbnet/Athame/releases)

Plugins
-------
| [Tidal](https://github.com/svbnet/AthamePlugin.Tidal/releases) | [Google Play Music](https://github.com/svbnet/AthamePlugin.GooglePlayMusic/releases) |
|-------|-------------------|

**At the moment, I am mostly working on the Athame core application, so I can't spend my time writing plugins for other services. However, services like Deezer and Spotify may become available in the future.**

Plugins are always distributed as Zip files - to install a plugin, simply extract the zip to the "Plugins" folder, which is in the same directory
as the Athame executable. A guide for creating your own plugins can be found on the wiki.

Then...
-------
Open the Athame.exe executable.

Keeping up to date
------------------
Follow me on [Twitter @svbnet](https://twitter.com/svbnet) and subscribe to [my blog](https://blog.svbnet.co) to stay up to date.

Usage
-----
Enter a URL in the "URL" textbox, then click "Add". It will show up in the download queue. Click "Start" to begin downloading.

If you haven't signed in, you can click the `Menu` button, then go to `Settings` and choose the tab of the music service
you want to sign into. You can also just enter a URL and click "Click here to sign in" on the error message below the URL
textbox.

Under `Settings > General`, you can change where music is downloaded to as well as the filename format used. There is an explanation
of the valid format specifiers on the General tab.

Build
-----
* .NET 4.6.2 or later
* Visual Studio 2015 (Express will work fine) or later with NuGet

Roadmap
-------
While Athame currently uses WinForms for its UI, this is a halfway solution to an ideal UI. I'm currently in the process of creating a
WPF UI to replace the WinForms UI, which will hopefully also fix many bugs in the process. I am also currently considering a cross-platform
interface (since WinForms is incredibly buggy on non-Windows platforms), but again this is just a consideration as writing a command-line
interface would take time away from porting it to WPF. A GTK# interface would also be possible, but would take a while since I am unfamiliar with GTK#.
There are currently no plans for a Cocoa (OS X) interface since I do not have a Mac.