﻿using SportNow.Model;
using SportNow.Services.Data.JSON;
using System.Diagnostics;
using SportNow.CustomViews;


using SportNow.Views.Profile;

namespace SportNow.Views
{
	public class EquipamentTypePageCS : DefaultPage
	{

		protected override void OnAppearing()
		{
			initSpecificLayout();
		}

		protected override void OnDisappearing()
		{
			this.CleanScreen();
		}

		private AbsoluteLayout equipamentosabsoluteLayout;

		private Microsoft.Maui.Controls.StackLayout stackButtons;

		private OptionButton karategiButton, protecoescintosButton, merchandisingButton;
	

		public void initLayout()
		{
			Title = "EQUIPAMENTOS";

			var toolbarItem = new ToolbarItem
			{
				//Text = "Logout",
				IconImageSource = "perfil.png",

			};
			toolbarItem.Clicked += OnPerfilButtonClicked;
			ToolbarItems.Add(toolbarItem);

		}


		public void CleanScreen()
		{
			Debug.Print("CleanScreen");
			//valida se os objetos já foram criados antes de os remover
			if (stackButtons != null)
			{
				absoluteLayout.Remove(stackButtons);
				absoluteLayout.Remove(equipamentosabsoluteLayout);

				stackButtons = null;
                equipamentosabsoluteLayout = null;
			}

		}

		public async void initSpecificLayout()
		{
			CreateEquipamentos();
		}


		public void CreateEquipamentos()
		{
            equipamentosabsoluteLayout = new AbsoluteLayout
			{
				Margin = new Thickness(5)
			};

			CreateEquipamentosOptionButtons();

			absoluteLayout.Add(equipamentosabsoluteLayout);
            absoluteLayout.SetLayoutBounds(equipamentosabsoluteLayout, new Rect(0, 20 * App.screenHeightAdapter, App.screenWidth, App.screenHeight - 20 * App.screenHeightAdapter));
		}

		public void CreateEquipamentosOptionButtons()
		{
			var width = Constants.ScreenWidth;
			var buttonWidth = (width) / 2;


			karategiButton = new OptionButton("KARATE GIs", "fotokarategis.png", buttonWidth, 100 * App.screenHeightAdapter);
			//minhasGraduacoesButton.button.Clicked += OnMinhasGraduacoesButtonClicked;
			var karategiButton_tap = new TapGestureRecognizer();
			karategiButton_tap.Tapped += (s, e) =>
			{
				Navigation.PushAsync(new EquipamentsPageCS("karategis"));
			};
			karategiButton.GestureRecognizers.Add(karategiButton_tap);

			protecoescintosButton = new OptionButton("PROTEÇÕES E CINTOS", "fotoprotecoescintos.png", buttonWidth, 100 * App.screenHeightAdapter);
			var protecoescintosButton_tap = new TapGestureRecognizer();
			protecoescintosButton_tap.Tapped += (s, e) =>
			{
				Navigation.PushAsync(new EquipamentsPageCS("protecoescintos"));
			};
			protecoescintosButton.GestureRecognizers.Add(protecoescintosButton_tap);

			merchandisingButton = new OptionButton("MERCHANDISING", "fotomerchandisingaksl.png", buttonWidth, 100 * App.screenHeightAdapter);
			var merchandisingButton_tap = new TapGestureRecognizer();
			merchandisingButton_tap.Tapped += (s, e) =>
			{
				Navigation.PushAsync(new EquipamentsPageCS("merchandising"));
			};
			merchandisingButton.GestureRecognizers.Add(merchandisingButton_tap);


			Microsoft.Maui.Controls.StackLayout stackEquipamentosButtons = new Microsoft.Maui.Controls.StackLayout
			{
				//WidthRequest = 370,
				Margin = new Thickness(0),
				Spacing = 50,
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HeightRequest = 350,
				Children =
				{
					karategiButton,
					protecoescintosButton,
					merchandisingButton,
				}
			};

			equipamentosabsoluteLayout.Add(stackEquipamentosButtons);
			equipamentosabsoluteLayout.SetLayoutBounds(stackEquipamentosButtons, new Rect(App.screenWidth / 4, 0, App.screenWidth / 2, 400 * App.screenHeightAdapter));

		}



		public EquipamentTypePageCS()
		{

			this.initLayout();
			//this.initSpecificLayout();

		}

		async void OnPerfilButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProfileCS());
		}

		async Task<List<Event_Participation>> GetPastEventParticipations()
		{
			Debug.WriteLine("GetPastCompetitionParticipations");
			EventManager eventManager = new EventManager();

			List<Event_Participation> pastEventParticipations = await eventManager.GetPastEventParticipations(App.member.id);
			if (pastEventParticipations == null)
			{
								Application.Current.MainPage = new NavigationPage(new LoginPageCS("Verifique a sua ligação à Internet e tente novamente."))
				{
					BarBackgroundColor = Color.FromRgb(15, 15, 15),
					BarTextColor = Colors.White
				};
				return null;
			}
			return pastEventParticipations;
		}

	}
}
