Athame
======
Athame is a program for downloading music from music streaming and sharing services.
It is intended for educational and private use only, and **not** as a tool for pirating and distributing music.
Above all else, remember that the artists and studios put a lot of work into making music -- if you can, purchase
music directly from the artist as more money goes to them.

Since I am also caught up with other things I can't devote all my time to fixing and improving Athame. Right now it is
just a very buggy, basic tool which I hope will either be improved upon in the future, or be contributed to.

Notes about testing services and development
--------------------------------------------
Unfortunately I can't pay for both Tidal and Google Play Music, so I am unable to test Google Play Music functionality.

Latest release
--------------
The code in the current repo probably won't compile, so check out the releases tab to download a pre-built binary.

Requirements
------------
* .NET 4.6.2 - if you do not have this installed, you should be prompted to download.

Getting Started
---------------
* Click the 'Releases' tab above at the top to download a pre-built zip, optionally with plugins. Extract it and launch the Athame.exe executable.

Plugins
-------
Plugins are always distributed as Zip files - to install a plugin, simply extract the zip to the "Plugins" folder, which is in the same directory
as the Athame executable. A guide for creating your own plugins can be found [here](plugin-guide.md).

Usage
-----
Enter a URL in the "URL" textbox, then click "Add". It will show up in the download queue. Click "Start" to begin downloading.

Currently, Athame only supports Tidal and Google Play Music URLs. This will eventually be upgraded to a generic plugin
interface, and the Google Play Music and Tidal services will be in different repositories.

If you haven't signed in, you can click the `Menu` button, then go to `Settings` and choose the tab of the music service
you want to sign into. You can also just enter a URL and click "Click here to sign in" on the error message below the URL
textbox.

Under `Settings > General`, you can change where music is downloaded to as well as the filename format used. There is an explanation
of the valid format specifiers on the General tab.

Build
-----
* Visual Studio 2015 (Express will work fine) with NuGet

Roadmap
-------
While Athame currently uses WinForms for its UI, this is a halfway solution to an ideal UI. I'm currently in the process of creating a
WPF UI to replace the WinForms UI, which will hopefully also fix many bugs in the process. I am also currently considering a cross-platform
interface (since WinForms is incredibly buggy on non-Windows platforms), but again this is just a consideration as writing a command-line
interface would take time away from porting it to WPF. A GTK# interface would also be possible, but would take a while since I am unfamiliar with GTK#.
There are currently no plans for a Cocoa (OS X) interface since I do not have a Mac.