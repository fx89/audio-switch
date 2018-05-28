AUTHOR: FX89

LICENSE: MIT

CREDITS:
   - EreTIk
   - http://www.daveamenta.com/2011-05/programmatically-or-command-line-change-the-default-sound-playback-device-in-windows-7/
   - https://stackoverflow.com/

ABOUT:
   Proof of concept app that changes the active audio device based on wether or not a window is focused

CONFIGURATION:
   Step 1) Install .NET Framework >= 4.6.x and .NET core >= 2.0.
   Step 2) Run the "list-devices.bat" script to get a list of the enabled audio devices pm your system
   Step 3) Edit "config.xml" to set the rules

CONFIG.XML PARAMETERS:
   PollingIntervalMillis = the amount of milliseconds to pass between two consecutive checks of the active window
   TimeoutMillis = the amount of milliseconds for which the currently active window has to be active before changing the active audio device
   AudioDeviceName = the name of the audio device to which to switch if the containing rule checks out
   ActiveWindowTitleContent = what to look for in the active window title to check out the rule
   Polarity = (true/false) - when set to true, the rule will check out if the active window contains the given ActiveWindowTitleContent
                           - when set to false, the rull will check out if the active window does not contain the given ActiveWindowTitleContent

STARTING THE APPLICATION:
   In background: run "start-in-background.vbs"
   In foreground: run "start.bat"
   !!! when running for the first time, run in foreground to spot any configuration errors

STOPPING THE APPLICATION:
   In background: either kill the dotnet.exe process or log out of your current desktop session
   In foreground: close the console

DELIVERABLES:
   AudioSwitch.dll                         - C++ DLL containing functionality related to querying and manipulating audio devices
   AudioSwitchWrapper.dll                  - .NET Standard library providing a high level object model to make it easier to call AudioSwitch.dll
   User32Wrapper.dll                       - .NET Standard library wrapping over user32.dll to get the active window title
   AudioDeviceSwitchApp.dll                - .NET Core application built upon AudioSwitchWrapper.dll and User32Wrapper.dll
   AudioDeviceSwitchApp.runtimeconfig.json - Configuration file for .NET
   config.xml                              - Configuration file for AudioDeviceSwitchApp.dll
   config.xml.bak                          - Backup of config.xml, in case something bad happens
   list-devices.bat                        - Script calling AudioDeviceSwitchApp.dll to list the audio devices currently enabled on the system
   start.bat                               - Script calling AudioDeviceSwitchApp.dll to start monitoring the active window
   start-in-background.vbs                 - VB script running start.bat in background

=============================================================================================================
This project was inteneded for personal use and will not be continued. Parts of it may be reused in bigger projects.
