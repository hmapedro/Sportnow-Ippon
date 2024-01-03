using System.Diagnostics;
using CarPlay;
using Foundation;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using SportNow.Views;
using UIKit;

namespace SportNow;

[Register("AppDelegate")]
public class AppDelegate : AppDelegateEx
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

