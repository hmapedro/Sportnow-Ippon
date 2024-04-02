using System.Diagnostics;
using SportNow.Model;
using SportNow.Services.Data.JSON;
using SportNow.CustomViews;

using SportNow.Views.Profile;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;

namespace SportNow.Views
{
	public class LoginPageCS : DefaultPage
	{

		protected async override void OnAppearing()
		{

			base.OnAppearing();
			
            //CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);

            //			var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            //			App.screenWidth = mainDisplayInfo.Width;
            //			App.screenHeight = mainDisplayInfo.Height;
            //Debug.Print("AQUI Login - ScreenWidth = " + App.screenWidth + " ScreenHeight = " + App.screenHeight + "mainDisplayInfo.Density = " + mainDisplayInfo.Density);

			App.AdaptScreen();
			this.initSpecificLayout();	
        }

		Label welcomeLabel;
		Button loginButton;
		FormEntry usernameEntry;
		FormEntryPassword passwordEntry;
		Label messageLabel;

		string password = "";
		string username = "";
		string message = "";
	
		/*public void initBaseLayout() {


			//NavigationPage.SetHasNavigationBar(this, false);
			//Title = "Login";			

			NavigationPage.SetHasNavigationBar(this, false);
		}*/

		public void initSpecificLayout()
		{

			Microsoft.Maui.Controls.Grid gridLogin = new Microsoft.Maui.Controls.Grid { Padding = 0 };
			gridLogin.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridLogin.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridLogin.RowDefinitions.Add(new RowDefinition { Height = 50 * App.entryHeightAdapter });
			gridLogin.RowDefinitions.Add(new RowDefinition { Height = 50 * App.entryHeightAdapter });
            gridLogin.RowDefinitions.Add(new RowDefinition { Height = 50 * App.entryHeightAdapter });
            gridLogin.RowDefinitions.Add(new RowDefinition { Height = 60 * App.screenHeightAdapter });
            gridLogin.RowDefinitions.Add(new RowDefinition { Height = 60 * App.screenHeightAdapter });
            gridLogin.RowDefinitions.Add(new RowDefinition { Height = 60 * App.screenHeightAdapter });
            //gridLogin.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            gridLogin.ColumnDefinitions.Add(new ColumnDefinition { Width = App.screenWidth - 10 * App.screenWidthAdapter}); //GridLength.Auto 


			welcomeLabel = new Label
			{
				Text = "BEM VINDO",
				TextColor = Colors.White,
				FontSize = App.bigTitleFontSize,
				HorizontalOptions = LayoutOptions.Center,
				WidthRequest = (App.screenWidth / DeviceDisplay.MainDisplayInfo.Density)-20,
				HorizontalTextAlignment = TextAlignment.Center,
                FontFamily = "futuracondensedmedium",
                //BackgroundColor = Color.FromRgb(255, 0, 0)
            };

			
			Image logo_aksl = new Image
			{
				Source = "logo_aksl_round.png",
				HorizontalOptions = LayoutOptions.Center,
				HeightRequest = 224 * App.screenHeightAdapter
			};

            
            if (Preferences.Default.Get("EMAIL", "") != "")
			{
				username = Preferences.Default.Get("EMAIL", "");
            }


			//USERNAME ENTRY
			usernameEntry = new FormEntry(username, "EMAIL", Keyboard.Email, App.screenWidth - 20 * App.screenWidthAdapter );

            if (Preferences.Default.Get("PASSWORD", "") != "")
            {
                password = Preferences.Default.Get("PASSWORD", "");
            }


            //PASSWORD ENTRY
            passwordEntry = new FormEntryPassword(password, "PASSWORD", App.screenWidth - 20 * App.screenWidthAdapter);

			//LOGIN BUTTON
			GradientBrush gradient = new LinearGradientBrush
			{
				StartPoint = new Point(0, 0),
				EndPoint = new Point(1, 1),
			};

			gradient.GradientStops.Add(new GradientStop(Color.FromRgb(180,143,86), Convert.ToSingle(0)));
			gradient.GradientStops.Add(new GradientStop(Color.FromRgb(246,220,178), Convert.ToSingle(0.5)));

			loginButton = new Button
			{
				Text = "LOGIN",
				Background = gradient,
                TextColor = Colors.Black,
				HorizontalOptions = LayoutOptions.Center,
                WidthRequest = App.screenWidth - 20 * App.screenWidthAdapter,
                HeightRequest = 45 * App.entryHeightAdapter,
                FontFamily = "futuracondensedmedium",
				FontSize = App.titleFontSize
			};
			loginButton.Clicked += OnLoginButtonClicked;


			Border frame_loginButton = new Border
            {
				BackgroundColor = App.backgroundColor,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = 5 * (float)App.screenHeightAdapter,
                },
                Stroke = App.topColor,
                Padding = 0,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				WidthRequest = App.screenWidth - 20 * App.screenWidthAdapter,
                HeightRequest = 45 * App.entryHeightAdapter
            };
			
			frame_loginButton.Content = loginButton;

			messageLabel = new Label {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.End,
				TextColor = Colors.Red,
				FontSize = App.itemTitleFontSize,
                FontFamily = "futuracondensedmedium",
            };

			messageLabel.Text = this.message;

			//RECOVER PASSWORD LABEL
			Label recoverPasswordLabel = new Label
			{
				Text = "Recuperar palavra-passe",
				TextColor = Colors.White,
				FontSize = App.titleFontSize,
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center,
                FontFamily = "futuracondensedmedium"
            };
			var recoverPasswordLabel_tap = new TapGestureRecognizer();
			recoverPasswordLabel_tap.Tapped += (s, e) =>
			{
				/*Navigation.InsertPageBefore(new RecoverPasswordPageCS(), this);
				Navigation.PopAsync();*/

				 Navigation.PushAsync(new RecoverPasswordPageCS());

			};
			recoverPasswordLabel.GestureRecognizers.Add(recoverPasswordLabel_tap);


            //RECOVER PASSWORD LABEL
            Label newMemberLabel = new Label
            {
                Text = "Novo Sócio",
                TextColor = Colors.White,
                FontSize = App.titleFontSize,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontFamily = "futuracondensedmedium"
            };
            var newMemberLabel_tap = new TapGestureRecognizer();
            newMemberLabel_tap.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new ConsentPageCS());
            };
            newMemberLabel.GestureRecognizers.Add(newMemberLabel_tap);

            gridLogin.Add(welcomeLabel, 0, 0);
			gridLogin.Add(logo_aksl, 0, 1);
			gridLogin.Add(messageLabel, 0, 2);
			gridLogin.Add(usernameEntry, 0, 3);
			gridLogin.Add(passwordEntry, 0, 4);
			gridLogin.Add(frame_loginButton, 0, 5);
			gridLogin.Add(recoverPasswordLabel, 0, 6);
            gridLogin.Add(newMemberLabel, 0, 7);

            absoluteLayout.Add(gridLogin);
            absoluteLayout.SetLayoutBounds(gridLogin, new Rect(0, 0, App.screenWidth, App.screenHeight));


            Label currentVersionLabel = new Label
            {
                Text = "Version " + App.VersionNumber + " " + App.BuildNumber,
                TextColor = App.normalTextColor,
                HorizontalTextAlignment = TextAlignment.End,
                VerticalTextAlignment = TextAlignment.End,
                FontSize = App.formValueSmallFontSize,
                FontFamily = "futuracondensedmedium",
            };

            absoluteLayout.Add(currentVersionLabel);
            absoluteLayout.SetLayoutBounds(currentVersionLabel, new Rect(0, App.screenHeight - 150 * App.screenHeightAdapter, App.screenWidth - 20 * App.screenWidthAdapter, 50 * App.screenHeightAdapter));

        }


		public LoginPageCS (string message)
		{
			if (message != "")
			{
				this.message = message;
				//UserDialogs.Instance.Alert(new AlertConfig() { Title = "Erro", message, OkText = "Ok" });
			}
			
			this.initBaseLayout();
			

		}

		async void OnSignUpButtonClicked (object sender, EventArgs e)
		{
			//await Navigation.PushAsync (new SignUpPageCS ());
		}

		async void OnLoginButtonClicked (object sender, EventArgs e)
		{
			Debug.WriteLine("OnLoginButtonClicked");

			loginButton.IsEnabled = false;

			var user = new User {
				Username = usernameEntry.entry.Text,
				Password = passwordEntry.entry.Text
			};


			MemberManager memberManager = new MemberManager();

			showActivityIndicator();

			var loginResult = await memberManager.Login(user);

			if (loginResult == "1")
			{
				Debug.WriteLine("login ok");

				App.members = await GetMembers(user);

				this.saveUserPassword(user.Username, user.Password);

				if (App.members.Count == 1)
                {
					App.original_member = App.members[0];
					App.member = App.original_member;

					//Navigation.InsertPageBefore(new MainTabbedPageCS("",""), this);
                    //await Navigation.PopAsync();

                    App.Current.MainPage = new NavigationPage(new MainTabbedPageCS("", ""))
                    {
                        BarBackgroundColor = Color.FromRgb(15, 15, 15),
                        BarTextColor = Colors.White//FromRgb(75, 75, 75)
                    };

                    
				}
				else if (App.members.Count > 1)
				{
					Navigation.InsertPageBefore(new SelectMemberPageCS(), this);
					await Navigation.PopAsync();
				}
			}
			else {
				Debug.WriteLine("login nok");
				loginButton.IsEnabled = true;
				passwordEntry.entry.Text = string.Empty;

				if (loginResult == "0")
				{
					Debug.WriteLine("Login falhou. O Utilizador não existe");
					messageLabel.Text = "Login falhou. O Utilizador não existe.";
				}
				else if (loginResult == "-1")
				{
					Debug.WriteLine("Login falhou. A Password está incorreta");
					messageLabel.Text = "Login falhou. A Password está incorreta.";
				}
				else if (loginResult == "-2")
				{
					Debug.WriteLine("Ocorreu um erro. Volte a tentar mais tarde.");
					messageLabel.Text = "Ocorreu um erro. Volte a tentar mais tarde.";
				}
				else
				{
					Debug.WriteLine("Ocorreu um erro. Volte a tentar mais tarde.");
					messageLabel.Text = "Ocorreu um erro. Volte a tentar mais tarde.";
					await DisplayAlert("Erro Login", loginResult, "Ok" );
				}

				this.saveUserPassword(user.Username, user.Password);
			}
			hideActivityIndicator();
		}


		async Task<List<Member>> GetMembers(User user)
		{
			Debug.WriteLine("GetMembers");
			MemberManager memberManager = new MemberManager();

			List<Member> members;

			members = await memberManager.GetMembers(user);

			return members;
			
		}

		protected void saveUserPassword(string email, string password)
		{
            Preferences.Default.Remove("EMAIL");
            Preferences.Default.Remove("PASSWORD");

            Preferences.Default.Set("EMAIL", email);
            Preferences.Default.Set("PASSWORD", password);

			//Application.Current.SavePropertiesAsync();

			username = Preferences.Default.Get("EMAIL", "");
        }
	}
}


