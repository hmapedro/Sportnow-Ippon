using SportNow.Model;


namespace SportNow.Views
{
	public class IntroPageCS : DefaultPage
	{

		public List<MainMenuItem> MainMenuItems { get; set; }

		protected override void OnAppearing()
		{
			App.screenWidth = Application.Current.MainPage.Width;//DeviceDisplay.MainDisplayInfo.Width;
			App.screenHeight = Application.Current.MainPage.Height; //DeviceDisplay.MainDisplayInfo.Height;
			//Debug.Print("ScreenWidth = "+ App.screenWidth + " ScreenHeight = " + App.screenHeight);
		}

		public void initLayout()
		{
			Title = "Home";
		}



		public IntroPageCS ()
		{
			this.initLayout();
			
		}
	}
}
