using SportNow.Model;
using SportNow.Services.Data.JSON;
using System.Diagnostics;
using SportNow.Views.Profile;
using Microsoft.Maui.Controls.Shapes;
using System.Xml;

namespace SportNow.Views
{
	public class QuotasListPageCS : DefaultPage
	{

		protected override void OnAppearing()
		{
			initSpecificLayout();
		}

		protected override void OnDisappearing()
		{
			this.CleanScreen();
		}
		private AbsoluteLayout quotasabsoluteLayout;

		private Microsoft.Maui.Controls.StackLayout stackButtons;

		private CollectionView collectionViewPastQuotas;

		private List<Fee> pastQuotas;

		Image estadoQuotaImage;

		public void initLayout()
		{
			Title = "QUOTAS";

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
				absoluteLayout.Remove(quotasabsoluteLayout);

				stackButtons = null;
				collectionViewPastQuotas = null;
			}
		}

		public async void initSpecificLayout()
		{
			showActivityIndicator();

			CreateQuotas();

			hideActivityIndicator();
		}


		public void CreateQuotas() {
            quotasabsoluteLayout = new AbsoluteLayout
			{
				Margin = new Thickness(0)
			};

			CreateCurrentQuota();
			CreatePastQuotas();

			absoluteLayout.Add(quotasabsoluteLayout);
			absoluteLayout.SetLayoutBounds(quotasabsoluteLayout, new Rect(0, 10 * App.screenHeightAdapter, App.screenWidth, App.screenHeight - 80 * App.screenHeightAdapter));
		}

        public void CreateCurrentQuota()
        {
            CreateCurrentQuota(quotasabsoluteLayout);
        }

        public async void CreateCurrentQuota(AbsoluteLayout quotasabsoluteLayout)
		{
			if (App.member.currentFee == null)
			{
				var result = await GetCurrentFees(App.member);
			}
			

			bool hasQuotaPayed = false;

			if (App.member.currentFee != null)
			{
				if ((App.member.currentFee.estado == "fechado") | (App.member.currentFee.estado == "recebido") | (App.member.currentFee.estado == "confirmado"))
				{
					hasQuotaPayed = true;
				}
			}

			Border quotasFrame = new Border
            {
                BackgroundColor = Colors.Transparent,

                StrokeShape = new RoundRectangle
                {
                    CornerRadius = 5 * (float)App.screenHeightAdapter,
                },
				Stroke= App.topColor,
				Padding = new Thickness(2, 2, 2, 2),
				HeightRequest = 120*App.screenHeightAdapter,
				VerticalOptions = LayoutOptions.Center,
			};
			
			var tapGestureRecognizer_quotasFrame = new TapGestureRecognizer();
			tapGestureRecognizer_quotasFrame.Tapped += async (s, e) => {
				await Navigation.PushAsync(new QuotasPageCS());
			};
			quotasFrame.GestureRecognizers.Add(tapGestureRecognizer_quotasFrame);

			AbsoluteLayout currentQuotasabsoluteLayout = new AbsoluteLayout
			{
				Margin = new Thickness(0),
			};
			quotasFrame.Content = currentQuotasabsoluteLayout;

			string logoFeeFileName = "", estadoImageFileName = "";
			/*

                        if (hasQuotaPayed == true)
                        {
                            logoFeeFileName = "fnkpikp.png";
                            estadoImageFileName = "iconcheck.png";
                        }
                        else if (hasQuotaPayed == false)
                        {
                            logoFeeFileName = "fnkpikp.png";
                            estadoImageFileName = "iconinativo.png";
                        }
                        /*
                        Image LogoFee = new Image
                        {
                            Source = logoFeeFileName,
                            //WidthRequest = 100,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        quotasabsoluteLayout.Add(LogoFee);
                        quotasabsoluteLayout.SetLayoutBounds(LogoFee, new Rect((App.screenWidth / 2) - 70.3 * App.screenHeightAdapter, 40 * App.screenHeightAdapter, 140.6 * App.screenHeightAdapter, 60 * App.screenHeightAdapter));
                        */
			
            Image LogoFee1 = new Image
            {
                Source = "logo_fnkp.png",
                WidthRequest = 70 * App.screenHeightAdapter, 
                HeightRequest = 70 * App.screenHeightAdapter,
                HorizontalOptions = LayoutOptions.Center
            };

            Image LogoFee2 = new Image
            {
                Source = "company_logo_square.png",
                WidthRequest = 70 * App.screenHeightAdapter,
                HeightRequest = 70 * App.screenHeightAdapter, 
                HorizontalOptions = LayoutOptions.Center
            };

            /*Border logosFrame = new Border
            {
                BackgroundColor = Colors.Transparent,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = 5 * (float)App.screenHeightAdapter,
                },
                Stroke = App.topColor,
                Padding = new Thickness(10, 0, 0, 0)
            };

            logosFrame.Content = new StackLayout
            {
                Children = { LogoFee1, LogoFee2 },
                Orientation = StackOrientation.Horizontal // Alinha as imagens horizontalmente dentro do Frame
            };*/

            quotasabsoluteLayout.Add(LogoFee1);
            quotasabsoluteLayout.Add(LogoFee2);
            quotasabsoluteLayout.SetLayoutBounds(LogoFee1, new Rect((App.screenWidth / 2) - 70.3 - (70 * App.screenHeightAdapter), 40 * App.screenHeightAdapter, 140.6 * App.screenHeightAdapter, 70 * App.screenHeightAdapter));
            quotasabsoluteLayout.SetLayoutBounds(LogoFee2, new Rect((App.screenWidth / 2) + 70.3 - (70 * App.screenHeightAdapter), 40 * App.screenHeightAdapter, 140.6 * App.screenHeightAdapter, 70 * App.screenHeightAdapter));
           /* quotasabsoluteLayout.Add(logosFrame);
            //quotasabsoluteLayout.SetLayoutBounds(logosFrame, new Rect((App.screenWidth / 2) - 70.3, 40 * App.screenHeightAdapter, 2 * 70.3, 60 * App.screenHeightAdapter));
            quotasabsoluteLayout.SetLayoutBounds(logosFrame, new Rect((App.screenWidth / 2) - 70.3 * App.screenHeightAdapter
            - 90 * App.screenHeightAdapter, 60 * App.screenHeightAdapter, 2 * (70.3 * App.screenHeightAdapter + 90 * App.screenHeightAdapter), 70 * App.screenHeightAdapter));
		   */

            estadoQuotaImage = new Image
			{
				Source = estadoImageFileName,
				WidthRequest = 25
			};
            quotasabsoluteLayout.Add(estadoQuotaImage);
            quotasabsoluteLayout.SetLayoutBounds(estadoQuotaImage, new Rect((App.screenWidth) - 26 * App.screenHeightAdapter, 1 * App.screenHeightAdapter, 25 * App.screenHeightAdapter, 25 * App.screenHeightAdapter));
       
            Label feeLabel = new Label { FontFamily = "futuracondensedmedium", BackgroundColor = Colors.Transparent, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, FontSize = App.titleFontSize, TextColor = App.topColor, LineBreakMode = LineBreakMode.WordWrap };
            feeLabel.Text = "QUOTA";

            quotasabsoluteLayout.Add(feeLabel);
            quotasabsoluteLayout.SetLayoutBounds(feeLabel, new Rect(0, 5 * App.screenHeightAdapter, App.screenWidth, 30 * App.screenHeightAdapter));

            //quotasabsoluteLayout.Add(currentQuotasabsoluteLayout);
            //quotasabsoluteLayout.SetLayoutBounds(currentQuotasabsoluteLayout, new Rect(0, 10 * App.screenHeightAdapter, App.screenWidth, 120 * App.screenHeightAdapter));


			//quotasabsoluteLayout.Add(quotasFrame);
			// quotasabsoluteLayout.SetLayoutBounds(quotasFrame, new Rect(0, 10 * App.screenHeightAdapter, App.screenWidth, 100 * App.screenHeightAdapter));

		}

		public async void CreatePastQuotas()
		{			
			var result = await GetPastFees(App.member);

			Label historicoQuotasLabel = new Label { FontFamily = "futuracondensedmedium", BackgroundColor = Colors.Transparent, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, FontSize = App.titleFontSize, TextColor = App.topColor, LineBreakMode = LineBreakMode.WordWrap };
			historicoQuotasLabel.Text = "HISTÓRICO QUOTAS";

			quotasabsoluteLayout.Add(historicoQuotasLabel);
            quotasabsoluteLayout.SetLayoutBounds(historicoQuotasLabel, new Rect(0, 130 * App.screenHeightAdapter, App.screenWidth, 50 * App.screenHeightAdapter));

			//COLLECTION GRADUACOES
			collectionViewPastQuotas = new CollectionView
			{
				SelectionMode = SelectionMode.Single,
				ItemsSource = App.member.pastFees,
				ItemsLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Vertical) { VerticalItemSpacing = 5, HorizontalItemSpacing = 5,  },
				EmptyView = new ContentView
				{
					Content = new Microsoft.Maui.Controls.StackLayout
					{
						Children =
							{
								new Label { FontFamily = "futuracondensedmedium", Text = "Não existem quotas anteriores.", HorizontalTextAlignment = TextAlignment.Center, TextColor = App.normalTextColor, FontSize = App.itemTitleFontSize },
							}
					}
				}
			};

			GradientBrush gradient = new LinearGradientBrush
			{
				StartPoint = new Point(0, 0),
				EndPoint = new Point(1, 1),
			};

			gradient.GradientStops.Add(new GradientStop(Color.FromRgb(180, 143, 86), Convert.ToSingle(0)));
			gradient.GradientStops.Add(new GradientStop(Color.FromRgb(246, 220, 178), Convert.ToSingle(0.5)));

			collectionViewPastQuotas.SelectionChanged += OncollectionViewFeeSelectionChangedAsync;

			collectionViewPastQuotas.ItemTemplate = new DataTemplate(() =>
			{
				AbsoluteLayout itemabsoluteLayout = new AbsoluteLayout
				{
					HeightRequest= 30 * App.screenHeightAdapter,
                    WidthRequest = App.screenWidth
                };

				Border itemFrame = new Border
                {
                    BackgroundColor = Colors.Transparent,

                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = 5 * (float)App.screenHeightAdapter,
                    },
                    Stroke = App.topColor,
					Padding = new Thickness(2, 2, 2, 2),
					HeightRequest = 30 * App.screenHeightAdapter,
					VerticalOptions = LayoutOptions.Center,
				};

				//itemFrame.Content = itemabsoluteLayout;

                itemabsoluteLayout.Add(itemFrame);
                itemabsoluteLayout.SetLayoutBounds(itemFrame, new Rect(0, 0, App.screenWidth, 30 * App.screenHeightAdapter));

                Label periodLabel = new Label { FontFamily = "futuracondensedmedium", BackgroundColor = Colors.Transparent, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Start, FontSize = App.itemTextFontSize, TextColor = App.normalTextColor, LineBreakMode = LineBreakMode.WordWrap };
				periodLabel.SetBinding(Label.TextProperty, "periodo");

				itemabsoluteLayout.Add(periodLabel);
				itemabsoluteLayout.SetLayoutBounds(periodLabel, new Rect(5 * App.screenWidthAdapter, 0, 30 * App.screenWidthAdapter, 30 * App.screenHeightAdapter));

				Label nameLabel = new Label { FontFamily = "futuracondensedmedium", BackgroundColor = Colors.Transparent, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Start, FontSize = App.itemTextFontSize, TextColor = App.normalTextColor, LineBreakMode = LineBreakMode.WordWrap };
				nameLabel.SetBinding(Label.TextProperty, "tipo_desc");

				itemabsoluteLayout.Add(nameLabel);
				itemabsoluteLayout.SetLayoutBounds(nameLabel, new Rect(35 * App.screenWidthAdapter, 0, App.screenWidth - 40 * App.screenWidthAdapter, 30 * App.screenHeightAdapter));

				Image participationImagem = new Image { Aspect = Aspect.AspectFill }; //, HeightRequest = 60, WidthRequest = 60
				participationImagem.Source = "iconcheck.png";

				itemabsoluteLayout.Add(participationImagem);
				itemabsoluteLayout.SetLayoutBounds(participationImagem, new Rect(App.screenWidth - 29 * App.screenWidthAdapter, 1 * App.screenHeightAdapter, 28 * App.screenWidthAdapter, 28 * App.screenHeightAdapter));
				
				return itemabsoluteLayout;
			});

			quotasabsoluteLayout.Add(collectionViewPastQuotas);
            quotasabsoluteLayout.SetLayoutBounds(collectionViewPastQuotas, new Rect(0, 170 * App.screenHeightAdapter, App.screenWidth, App.screenHeight - 170 * App.screenHeightAdapter));
		}


		public QuotasListPageCS()
		{

			this.initLayout();
			//this.initSpecificLayout();

		}

		async void OnPerfilButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProfileCS());
		}

		async Task<int> GetCurrentFees(Member member)
		{
			Debug.WriteLine("GetCurrentFees");
			MemberManager memberManager = new MemberManager();

			var result = await memberManager.GetCurrentFees(member);
			if (result == -1)
			{
				Application.Current.MainPage = new NavigationPage(new LoginPageCS("Verifique a sua ligação à Internet e tente novamente."))
				{
					BarBackgroundColor = App.backgroundColor,
					BarTextColor = App.normalTextColor
				};
				return -1;
			}
			return result;
		}

		async Task<int> GetPastFees(Member member)
		{
			Debug.WriteLine("GetPastFees");
			MemberManager memberManager = new MemberManager();

			var result = await memberManager.GetPastFees(member);
			if (result == -1)
			{
				Application.Current.MainPage = new NavigationPage(new LoginPageCS("Verifique a sua ligação à Internet e tente novamente."))
				{
					BarBackgroundColor = App.backgroundColor,
					BarTextColor = App.normalTextColor
				};
				return -1;
			}
			return result;
		}

		void OncollectionViewFeeSelectionChangedAsync(object sender, SelectionChangedEventArgs e)
		{
			Debug.Print("OncollectionViewFeeSelectionChangedAsync");

			if ((sender as CollectionView).SelectedItem != null)
			{
				Fee selectedFee = (sender as CollectionView).SelectedItem as Fee;

				InvoiceDocument(selectedFee);
				
				(sender as CollectionView).SelectedItem = null;
			}
			else
			{
				Debug.WriteLine("OncollectionViewMonthFeesSelectionChanged selected item = nulll");
			}
		}

		public async void InvoiceDocument(Fee fee)
		{
			Payment payment = await GetFeePaymentAsync(fee);
			if (payment.invoiceid != null)
			{
				await Navigation.PushAsync(new InvoiceDocumentPageCS(payment));
			}
		}
		

		public async Task<Payment> GetFeePaymentAsync(Fee fee)
		{
			Debug.WriteLine("GetFeePayment");
			MemberManager memberManager = new MemberManager();

			List<Payment> result = await memberManager.GetFeePayment(fee.id);
			if (result == null)
			{
				Application.Current.MainPage = new NavigationPage(new LoginPageCS("Verifique a sua ligação à Internet e tente novamente."))
				{
					BarBackgroundColor = App.backgroundColor,
					BarTextColor = App.normalTextColor
				};
				return result[0];
			}
			return result[0];
		}

	}
}
