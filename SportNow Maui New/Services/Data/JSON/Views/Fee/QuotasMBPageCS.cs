using System;
using System.Collections.Generic;
using Microsoft.Maui;
using SportNow.Model;
using SportNow.Services.Data.JSON;
using System.Threading.Tasks;
using System.Diagnostics;
using SportNow.Views.Profile;


namespace SportNow.Views
{
	public class QuotasMBPageCS : DefaultPage
	{
		private Member member;

		private Microsoft.Maui.Controls.Grid gridMBPayment;

		public void initLayout()
		{
			Title = "Quotas";
		}


		public async void initSpecificLayout()
		{

			member = App.member;

			var result = await GetFeePayment(member);

			
			createMBPaymentLayout();
		}

		public void createMBPaymentLayout() {
			gridMBPayment= new Microsoft.Maui.Controls.Grid { Padding = 10, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
			gridMBPayment.RowDefinitions.Add(new RowDefinition { Height = 100 });
			gridMBPayment.RowDefinitions.Add(new RowDefinition { Height = 20 });
			gridMBPayment.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridMBPayment.RowDefinitions.Add(new RowDefinition { Height = 20 });
			gridMBPayment.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridMBPayment.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); //GridLength.Auto
			gridMBPayment.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); //GridLength.Auto 

			Label feeYearLabel = new Label
			{
                FontFamily = "futuracondensedmedium",
                Text = "QUOTA "+DateTime.Now.ToString("yyyy"),
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Colors.White,
				LineBreakMode = LineBreakMode.NoWrap,
				FontSize = 50
			};

			Image MBLogoImage = new Image
			{
				Source = "logomultibanco.png",
				WidthRequest = 100,
				HeightRequest = 100
			};

			Label referenciaMBLabel = new Label
			{
                FontFamily = "futuracondensedmedium",
                Text = "Pagamento por\n Multibanco",
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Colors.White,
				//LineBreakMode = LineBreakMode.NoWrap,
				HeightRequest = 100,
				FontSize = 30
			};

			/*Label feeInactiveCommentLabel = new Label
			{
				Text = "Atenção: Com as quotas inativas o aluno não poderá participar em eventos e não terá seguro desportivo em caso de lesão.",
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Colors.White,
				FontSize = 20
			};

			Button activateButton = new Button
			{
				Text = "ATIVAR",
				BackgroundColor = Colors.Green,
				TextColor = Colors.White,
				WidthRequest = 100,
				HeightRequest = 50
			};
			//activateButton.Clicked += OnActivateButtonClicked;

			*/

			Microsoft.Maui.Controls.Grid gridMBDataPayment = new Microsoft.Maui.Controls.Grid { Padding = 10, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
			gridMBDataPayment.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridMBDataPayment.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridMBDataPayment.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridMBDataPayment.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); //GridLength.Auto
			gridMBDataPayment.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); //GridLength.Auto

			Label entityLabel = new Label
			{
                FontFamily = "futuracondensedmedium",
                Text = "Entidade:",
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Start,
				TextColor = Colors.White,
				FontSize = 20
			};
			Label referenceLabel = new Label
			{
                FontFamily = "futuracondensedmedium",
                Text = "Referência:",
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Start,
				TextColor = Colors.White,
				FontSize = 20
			};
			Label valueLabel = new Label
			{
                FontFamily = "futuracondensedmedium",
                Text = "Valor:",
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Start,
				TextColor = Colors.White,
				FontSize = 20
			};

			Label entityValue = new Label
			{
                FontFamily = "futuracondensedmedium",
                Text = App.member.currentFee.entidade,
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.End,
				TextColor = Colors.White,
				FontSize = 20
			};
			Label referenceValue = new Label
			{
                FontFamily = "futuracondensedmedium",
                Text = App.member.currentFee.referencia,
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.End,
				TextColor = Colors.White,
				FontSize = 20
			};
			Label valueValue = new Label
			{
                FontFamily = "futuracondensedmedium",
                Text = String.Format("{0:0.00}", App.member.currentFee.valor) + "€",
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.End,
				TextColor = Colors.White,
				FontSize = 20
			};

			Frame MBDataFrame= new Frame { BackgroundColor = App.backgroundColor, BorderColor = Colors.Yellow, CornerRadius = 10, IsClippedToBounds = true, Padding = 0 };
			MBDataFrame.Content = gridMBDataPayment;

			gridMBDataPayment.Add(entityLabel, 0, 0);
			gridMBDataPayment.Add(entityValue, 1, 0);
			gridMBDataPayment.Add(referenceLabel, 0, 1);
			gridMBDataPayment.Add(referenceValue, 1, 1);
			gridMBDataPayment.Add(valueLabel, 0, 2);
			gridMBDataPayment.Add(valueValue, 1, 2);

			gridMBDataPayment.Add(referenceLabel, 0, 1);
			gridMBDataPayment.Add(valueLabel, 0, 2);

			gridMBPayment.Add(feeYearLabel, 0, 0);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(feeYearLabel, 2);

			gridMBPayment.Add(MBLogoImage, 0, 2);
			gridMBPayment.Add(referenciaMBLabel, 1, 2);

			gridMBPayment.Add(MBDataFrame, 0, 4);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(MBDataFrame, 2);


			absoluteLayout.Add(gridMBPayment);
            absoluteLayout.SetLayoutBounds(gridMBPayment, new Rect(0, 10 * App.screenHeightAdapter, App.screenWidth, App.screenHeight - 10 * App.screenHeightAdapter));
		}

		public QuotasMBPageCS(Member member)
		{

			this.member = member;

			this.initLayout();
			this.initSpecificLayout();

		}

		async void OnPerfilButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProfileCS());
		}


		async Task<int> GetFeePayment(Member member)
		{
			Debug.WriteLine("GetFeePayment");
			MemberManager memberManager = new MemberManager();

			var result = await memberManager.GetCurrentFees(member);
			if (result == -1)
			{
								Application.Current.MainPage = new NavigationPage(new LoginPageCS("Verifique a sua ligação à Internet e tente novamente."))
				{
					BarBackgroundColor = Color.FromRgb(15, 15, 15),
					BarTextColor = Colors.White
				};
				return result;
			}
			return result;
		}

	}
}

