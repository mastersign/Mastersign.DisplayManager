# Display Manager

> A command line tool for recording and restoring Windows display configurations

## Use Case

* You have a Windows 7/8/10 PC with a couple of monitors / flatscreen TVs attanched
* You are not always using all of your monitors but you like to switch between different monitor configurations

## Usage

This tool is quite minimalistic.

1. Use the Windows display settings dialog to configure your monitors to your liking
2. Run `DisplayMan.exe record MyConfigurationA.xml` on the command line
3. Configure your monitors in another way
4. Run `DisplayMan.exe record MyConfigurationB.xml`

Now you have two recorded configurations and you can toggel between them

1. Run `DisplayMan.exe restore MyConfigurationB.xml`
2. Run `DisplayMan.exe restore MyConfigurationA.xml`

It is a good idea to create links for the different configurations, with nice icons, of course.
And if these links are placed on the desktop or in a toolbar inside the taskbar,
it is even possible to assign keyboard shortcuts to them.

## Technical Insight

This tool makes use of the [Windows API for Connecting and Configuring Displays](https://msdn.microsoft.com/en-us/library/windows/hardware/hh406259.aspx).
This API is available since Windows 7.
It only uses the methods
`QueryDisplayConfig`, `GetDisplayConfigBufferSize` and `SetDisplayConfig` in the `User32.dll`.
It does nothing more then to query the current configuration
and store it with the .NET XML serialization into a file.
For restore it just loads the configuration with .NET XML deserializaton and passes them to the Windows API.

## Acknowledgements

After spending quite some time with the older API (`EnumDisplayDevices`, `ChangeDisplaySettings`, ...)
I finally found the [question](https://stackoverflow.com/questions/16082330)
of [Erti-Chris Eelmaa](https://stackoverflow.com/users/1936622)
with the answer from [Stephen Martin](https://stackoverflow.com/users/12845)
and edits from [Lennart](https://stackoverflow.com/users/368354) and
[David Heffernan](https://stackoverflow.com/users/505088)
on StackOverflow.
The rest was easy. Thanks a lot guys, you saved my day!

## License

This project is released under the MIT license.

Copyright © by Tobias Kiertscher <dev@mastersign.de>.
