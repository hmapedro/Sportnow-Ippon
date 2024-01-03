using System;
using System.Diagnostics;
using System.Windows.Input;
using Microsoft.Maui;

namespace SportNow.Views
{
    public class DefaultPage : ContentPage
	{
        Microsoft.Maui.Controls.StackLayout stack;
        ActivityIndicator indicator;
        Image loading;

        private DeviceOrientationService _deviceOrientationService;
        private string _orientationLockState = "Unlocked";

        public ICommand LockPortraitCommand { get; }
        public ICommand LockLandscapeCommand { get; }
        public ICommand UnlockOrientationCommand { get; }

        public string OrientationLockState;/*
        {
            get => _orientationLockState;
            set => SetField(ref _orientationLockState, value);
        }*/

        private void LockOrientation(DisplayOrientation? orientation)
        {
            if (_deviceOrientationService == null)
            {
                //Debug.Print("_deviceOrientationService null");
                return;
            }


            switch (orientation)
            {
                case DisplayOrientation.Portrait:
                    //Debug.Print("Locked Portrait");
                    this.OrientationLockState = "Locked Portrait";
                    _deviceOrientationService.LockPortraitInterface();
                    break;
                case DisplayOrientation.Landscape:
                    //Debug.Print("Locked Landscape");
                    _deviceOrientationService.LockPortraitInterface();
                    this.OrientationLockState = "Locked Landscape";
                    break;
                case null:
                case DisplayOrientation.Unknown:
                default:
                   // Debug.Print("Unlocked");
                    _deviceOrientationService.LockPortraitInterface();
                    this.OrientationLockState = "Unlocked";
                    break;
            }
        }


        public DefaultPage()
        {
			this.initBaseLayout();

            DeviceOrientationService deviceOrientationService = new DeviceOrientationService();

            deviceOrientationService.LockPortraitInterface();

#if ANDROID
            var currentActivity = ActivityStateManager.Default.GetCurrentActivity();
            if (currentActivity is not null)
            {
                currentActivity.RequestedOrientation = (Android.Content.PM.ScreenOrientation)DisplayOrientation.Portrait;

            }
#elif IOS
            this.LockOrientation(DisplayOrientation.Portrait);
#endif
        }

        public AbsoluteLayout absoluteLayout;

		public void initBaseLayout()
		{
            
			this.BackgroundColor = App.backgroundColor;

            absoluteLayout = new AbsoluteLayout
            {
				Margin = new Thickness(5 * App.screenWidthAdapter)
			};
			Content = absoluteLayout;

			NavigationPage.SetBackButtonTitle(this, "");


            if (Application.Current.MainPage != null)
            {
                var navigationPage = Application.Current.MainPage as NavigationPage;
                navigationPage.BarBackgroundColor = App.backgroundColor;
                navigationPage.BarTextColor = App.normalTextColor;
            }

            stack = new Microsoft.Maui.Controls.StackLayout() { BackgroundColor = App.backgroundColor, Opacity = 0.6 };
            loading = new Image() { Source = "loading.gif", IsAnimationPlaying = true }; 

            //indicator = new ActivityIndicator() { Color = App.topColor, HeightRequest = 100, WidthRequest = 100, MinimumHeightRequest = 100, MinimumWidthRequest = 100};
        }

        public void showActivityIndicator()
        {
            //indicator.IsRunning = true;

            /*if (absoluteLayout == null)
            {
                initBaseLayout();
            }*/

            absoluteLayout.Add(stack);
            absoluteLayout.SetLayoutBounds(stack, new Rect(0, 0, App.screenWidth, App.screenHeight));

            absoluteLayout.Add(loading);
            absoluteLayout.SetLayoutBounds(loading, new Rect((App.screenWidth / 2) - 50, (App.screenHeight / 2) - 100 - 50 * App.screenHeightAdapter, 100 * App.screenHeightAdapter, 100 * App.screenHeightAdapter));
        }

        public void hideActivityIndicator()
        {
            absoluteLayout.Remove(stack);
            absoluteLayout.Remove(loading);
            //indicator.IsRunning = false;
        }
    }
}
