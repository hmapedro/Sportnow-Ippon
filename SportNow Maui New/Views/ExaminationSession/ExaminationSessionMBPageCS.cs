using SportNow.Model;
using SportNow.Services.Data.JSON;
using System.Diagnostics;


namespace SportNow.Views
{
	public class ExaminationSessionMBPageCS : DefaultPage
	{

		protected override void OnDisappearing()
		{
			//App.competition_participation = competition_participation;

			//registerButton.IsEnabled = true;
			//estadoValue.entry.Text = App.competition_participation.estado;
		}

		private Examination_Session examination_session;

		private List<Payment> payments;

		private Microsoft.Maui.Controls.Grid gridMBPayment;

		public void initLayout()
		{
			Title = "INSCRIÇÃO";
		}


		public async void initSpecificLayout()
		{

			payments = await GetExaminationSession_Payment(examination_session);

			if ((payments == null) | (payments.Count == 0))
			{
				createRegistrationConfirmed();
			}
			else {
				createMBPaymentLayout();
			}
		}

		public async void createRegistrationConfirmed()
		{
			Label inscricaoOKLabel = new Label
			{
				Text = "A tua Inscrição na " + examination_session.name + " está Confirmada. \n Boa sorte e nunca te esqueças de te divertir!",
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = App.normalTextColor,
				//LineBreakMode = LineBreakMode.NoWrap,
				HeightRequest = 400,
				FontSize = 30
			};

			absoluteLayout.Add(inscricaoOKLabel);
            absoluteLayout.SetLayoutBounds(inscricaoOKLabel, new Rect(0, 10 * App.screenHeightAdapter, App.screenWidth, 80 * App.screenHeightAdapter));

			CompetitionManager competitionManager = new CompetitionManager();

			await competitionManager.Update_Competition_Participation_Status(examination_session.participationid, "confirmado");
			examination_session.participationconfirmed = "confirmado";

		}

		public void createMBPaymentLayout() {
            gridMBPayment = new Microsoft.Maui.Controls.Grid { Padding = 10, ColumnSpacing = 20 * App.screenHeightAdapter, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
            gridMBPayment.RowDefinitions.Add(new RowDefinition { Height = 150 * App.screenHeightAdapter });
            gridMBPayment.RowDefinitions.Add(new RowDefinition { Height = 20 * App.screenHeightAdapter });
            gridMBPayment.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            gridMBPayment.RowDefinitions.Add(new RowDefinition { Height = 20 * App.screenHeightAdapter });
            gridMBPayment.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            gridMBPayment.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridMBPayment.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); //GridLength.Auto
			gridMBPayment.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); //GridLength.Auto 

			Label competitionParticipationNameLabel = new Label
			{
				Text = "Para confirmares a tua presença na\n " + examination_session.name + "\n efetua o pagamento no MB com os dados apresentados em baixo",
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = App.normalTextColor,
				//LineBreakMode = LineBreakMode.NoWrap,
				FontSize = 20
			};

			Image MBLogoImage = new Image
			{
				Source = "logomultibanco.png",
                WidthRequest = 164 * App.screenHeightAdapter,
                HeightRequest = 142 * App.screenHeightAdapter
            };

			Label referenciaMBLabel = new Label
			{
				Text = "Pagamento por\n Multibanco",
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = App.normalTextColor,
                //LineBreakMode = LineBreakMode.NoWrap,
                HeightRequest = 142 * App.screenHeightAdapter,
                FontSize = App.bigTitleFontSize
            };

			Microsoft.Maui.Controls.Grid gridMBDataPayment = new Microsoft.Maui.Controls.Grid { Padding = 10 * App.screenWidthAdapter, ColumnSpacing = 5 * App.screenHeightAdapter, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
			gridMBDataPayment.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridMBDataPayment.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridMBDataPayment.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridMBDataPayment.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); //GridLength.Auto
			gridMBDataPayment.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); //GridLength.Auto

			Label entityLabel = new Label
			{
				Text = "Entidade:",
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Start,
				TextColor = App.normalTextColor,
                FontSize = App.titleFontSize
            };
			Label referenceLabel = new Label
			{
				Text = "Referência:",
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Start,
				TextColor = App.normalTextColor,
                FontSize = App.titleFontSize
            };
			Label valueLabel = new Label
			{
				Text = "Valor:",
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Start,
				TextColor = App.normalTextColor,
                FontSize = App.titleFontSize
            };

			Label entityValue = new Label
			{
				Text = payments[0].entity,
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.End,
				TextColor = App.normalTextColor,
                FontSize = App.titleFontSize
            };
			Label referenceValue = new Label
			{
				Text = payments[0].reference,
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.End,
				TextColor = App.normalTextColor,
                FontSize = App.titleFontSize
            };
			Label valueValue = new Label
			{
                Text = String.Format("{0:0.00}", payments[0].value) + "€",
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.End,
				TextColor = App.normalTextColor,
                FontSize = App.titleFontSize
            };

			Frame MBDataFrame= new Frame { BackgroundColor = App.backgroundColor, BorderColor = Colors.Yellow, CornerRadius = 10, IsClippedToBounds = true, Padding = 0 };
			MBDataFrame.Content = gridMBDataPayment;

			gridMBDataPayment.Add(entityLabel, 0, 0);
			gridMBDataPayment.Add(entityValue, 1, 0);
			gridMBDataPayment.Add(referenceLabel, 0, 1);
			gridMBDataPayment.Add(referenceValue, 1, 1);
			gridMBDataPayment.Add(valueLabel, 0, 2);
			gridMBDataPayment.Add(valueValue, 1, 2);

			gridMBPayment.Add(competitionParticipationNameLabel, 0, 0);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(competitionParticipationNameLabel, 2);

			gridMBPayment.Add(MBLogoImage, 0, 2);
			gridMBPayment.Add(referenciaMBLabel, 1, 2);

			gridMBPayment.Add(MBDataFrame, 0, 4);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(MBDataFrame, 2);


			absoluteLayout.Add(gridMBPayment);
            absoluteLayout.SetLayoutBounds(gridMBPayment, new Rect(0, 10 * App.screenWidthAdapter, App.screenWidth, App.screenHeight - 10 * App.screenHeightAdapter));
		}

		public ExaminationSessionMBPageCS(Examination_Session examination_session)
		{

			this.examination_session = examination_session;
			//App.competition_participation = competition_participation;

			this.initLayout();
			this.initSpecificLayout();

		}

		async Task<List<Payment>> GetExaminationSession_Payment(Examination_Session examination_session)
		{
			Debug.WriteLine("GetExaminationSession_Payment");
			ExaminationSessionManager examination_sessionManager = new ExaminationSessionManager();

			Debug.Print("examination_session.participationid = " + examination_session.participationid);
			List<Payment> payments = await examination_sessionManager.GetExamination_Payment(examination_session.participationid);
			if (payments == null)
			{
				Application.Current.MainPage = new NavigationPage(new LoginPageCS("Verifique a sua ligação à Internet e tente novamente."))
			{
				BarBackgroundColor = App.backgroundColor,
				BarTextColor = App.normalTextColor
			};
			return null;
			}
			return payments;
		}

	}
}

