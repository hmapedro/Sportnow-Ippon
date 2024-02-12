﻿using SportNow.Model;
using SportNow.Services.Data.JSON;
using System.Diagnostics;
using SportNow.CustomViews;
using System.Text.RegularExpressions;
using System.Net;

using SkiaSharp;
using Syncfusion.Maui.Core;

namespace SportNow.Views.Profile
{
    public class ProfileCS : DefaultPage
	{

		protected async override void OnAppearing()
		{
            base.OnAppearing();
            var result = await GetCurrentFees(App.member);

			if (quotaImage == null)
			{
                quotaImage = new Image
                {
                    Aspect = Aspect.AspectFit
                };
            }

			quotaImage.Source = "iconquotasinativas.png";

			if (App.member != null)
			{ 
				if (App.member.currentFee != null)
				{
					if ((App.member.currentFee.estado == "fechado") | (App.member.currentFee.estado == "recebido") | (App.member.currentFee.estado == "confirmado"))
					{
                        quotaImage.Source = "iconquotasativas.png";
                    }
				}
			}
		}


		protected async override void OnDisappearing()
		{
			if (changeMember == false)
            {
				await UpdateMemberInfo();
			} 
			
		}

		Image quotaImage, objectivesImage;

		private ScrollView scrollView;

		MenuButton geralButton;
		MenuButton identificacaoButton;
		MenuButton moradaButton;
		MenuButton encEducacaoButton;


		Microsoft.Maui.Controls.StackLayout stackButtons;
		private Microsoft.Maui.Controls.Grid gridGeral;
		private Microsoft.Maui.Controls.Grid gridIdentificacao;
		private Microsoft.Maui.Controls.Grid gridMorada;
		private Microsoft.Maui.Controls.Grid gridEncEducacao;
		private Microsoft.Maui.Controls.Grid gridButtons;

		FormValueEdit nameValue;
		FormValue emailValue;
		FormValueEdit phoneValue;
		FormValueEdit addressValue;
		FormValueEdit cityValue;
		FormValueEdit postalcodeValue;
		FormValueEdit EncEducacao1NomeValue;
		FormValueEdit EncEducacao1PhoneValue;
		FormValueEdit EncEducacao1MailValue;
		FormValueEdit EncEducacao2NomeValue;
		FormValueEdit EncEducacao2PhoneValue;
		FormValueEdit EncEducacao2MailValue;

		bool changeMember = false;

		bool enteringPage = true;

        RoundImage memberPhotoImage;
        Stream stream;


        int y_button_left = 0;
        int y_button_right = 0;


        public void initLayout()
		{
			Title = "PERFIL";


			var toolbarItem = new ToolbarItem
			{
                IconImageSource = "exit.png",
			};
			toolbarItem.Clicked += OnLogoutButtonClicked;
			ToolbarItems.Add(toolbarItem);

		}


		public async void initSpecificLayout()
		{
            LogManager logManager = new LogManager();
            await logManager.writeLog(App.original_member.id, App.member.id, "PROFILE VISIT", "Visit Profile Page");

			scrollView = new ScrollView { Orientation = ScrollOrientation.Vertical };

			absoluteLayout.Add(scrollView);
            absoluteLayout.SetLayoutBounds(scrollView, new Rect(0, 325 * App.screenHeightAdapter, App.screenWidth, (App.screenHeight) - 310 * App.screenHeightAdapter));

			int countStudents = App.original_member.students_count;

			CreatePhoto();			
			CreateGraduacao();
			CreateStackButtons();
			CreateGridGeral();
			CreateGridIdentificacao();
			CreateGridMorada();
			CreateGridEncEducacao();
			CreateGridButtons();

			/*gridIdentificacao.IsVisible = false;
			gridMorada.IsVisible = false;
			gridEncEducacao.IsVisible = false;*/
			OnGeralButtonClicked(null, null);
		}

		public void CreatePhoto()
		{
            //memberPhotoImage = new RoundImage();

            memberPhotoImage = new RoundImage();

            WebResponse response;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Constants.images_URL + App.member.id + "_photo");
			Debug.Print(Constants.images_URL + App.member.id + "_photo");
            request.Method = "HEAD";
            bool exists;
            try
            {
                response = request.GetResponse();
                Debug.Print("response.Headers.GetType()= " + response.Headers.GetType());
                exists = true;
            }
            catch (Exception ex)
            {
                exists = false;
            }

            Debug.Print("Photo exists? = " + exists);

            if (exists)
            {

				memberPhotoImage.Source = new UriImageSource
				{
					Uri = new Uri(Constants.images_URL + App.member.id + "_photo"),
					CachingEnabled = false,
					CacheValidity = new TimeSpan(0, 0, 0, 1)
				};
				//memberPhotoImage.Source = "https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png";// Constants.images_URL + App.member.id + "_photo";
            }
            else
            {
                memberPhotoImage.Source = "iconadicionarfoto.png";
            }

			//memberPhotoImage.Source = Constants.images_URL + App.member.id + "_photo";
            var memberPhotoImage_tap = new TapGestureRecognizer();
            memberPhotoImage_tap.Tapped += memberPhotoImageTappedAsync;
            memberPhotoImage.GestureRecognizers.Add(memberPhotoImage_tap);


			absoluteLayout.Add(memberPhotoImage);
            absoluteLayout.SetLayoutBounds(memberPhotoImage, new Rect((App.screenWidth/2) - (90 * App.screenHeightAdapter), 0, 180 * App.screenHeightAdapter, 180 * App.screenHeightAdapter));
        }

		public async void CreateQuotaButton()
		{

            quotaImage = new Image
            {
                Aspect = Aspect.AspectFit
            };

            var result = await GetCurrentFees(App.member);

			bool hasQuotaPayed = false;

			if (App.member.currentFee != null)
			{
				if ((App.member.currentFee.estado == "fechado") | (App.member.currentFee.estado == "recebido"))
				{
					hasQuotaPayed = true;
				}
			}

			if (hasQuotaPayed == true)
			{
                quotaImage.Source = "iconquotasativas.png";
            }
			else
			{
                quotaImage.Source = "iconquotasinativas.png";
            }

            TapGestureRecognizer quotasImage_tapEvent = new TapGestureRecognizer();
            quotasImage_tapEvent.Tapped += OnQuotaButtonClicked;
            quotaImage.GestureRecognizers.Add(quotasImage_tapEvent);

			absoluteLayout.Add(quotaImage);
            absoluteLayout.SetLayoutBounds(quotaImage, new Rect((App.screenWidth) - (47.5 * App.screenHeightAdapter), y_button_right * App.screenHeightAdapter, 35 * App.screenHeightAdapter, 35 * App.screenHeightAdapter));

            Label quotasLabel = new Label
            {
                FontFamily = "futuracondensedmedium",
                Text = "Quota",
                TextColor = App.normalTextColor,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
                FontSize = App.smallTextFontSize
            };

			absoluteLayout.Add(quotasLabel);
            absoluteLayout.SetLayoutBounds(quotasLabel, new Rect((App.screenWidth) - (60 * App.screenHeightAdapter), (y_button_right+37) * App.screenHeightAdapter, 60 * App.screenHeightAdapter, 15 * App.screenHeightAdapter));

            y_button_right = y_button_right + 60;


        }

        public async void CreateObjectivesButton()
        {


			objectivesImage = new Image
            {
                Aspect = Aspect.AspectFit
            };

            objectivesImage.Source = "iconexpectativas.png";

            TapGestureRecognizer objectivesImage_tapEvent = new TapGestureRecognizer();
            objectivesImage_tapEvent.Tapped += OnObjectivesButtonClicked;
            objectivesImage.GestureRecognizers.Add(objectivesImage_tapEvent);

			absoluteLayout.Add(objectivesImage);
            absoluteLayout.SetLayoutBounds(objectivesImage, new Rect((App.screenWidth) - (47.5 * App.screenHeightAdapter), y_button_right * App.screenHeightAdapter, 35 * App.screenHeightAdapter, 35 * App.screenHeightAdapter));


            Label objectivesLabel = new Label
            {
                FontFamily = "futuracondensedmedium",
                Text = "Expectativas",
                TextColor = App.normalTextColor,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
                FontSize = App.smallTextFontSize
            };

			absoluteLayout.Add(objectivesLabel);
            absoluteLayout.SetLayoutBounds(objectivesLabel, new Rect((App.screenWidth) - (60 * App.screenHeightAdapter), (y_button_right + 37) * App.screenHeightAdapter, 60 * App.screenHeightAdapter, 15 * App.screenHeightAdapter));
        
            y_button_right = y_button_right + 60;


        }


        public async void CreateGraduacao()
		{


			string gradeBeltFileName = "belt_" + App.member.grade.ToLower() + ".png";

			Image gradeBeltImage = new Image
			{
				Source = gradeBeltFileName
			};
            var tapGestureRecognizer_graduacaoFrame = new TapGestureRecognizer();
            tapGestureRecognizer_graduacaoFrame.Tapped += async (s, e) => {
                await Navigation.PushAsync(new myGradesPageCS("MinhasGraduaçoes"));
            };
            gradeBeltImage.GestureRecognizers.Add(tapGestureRecognizer_graduacaoFrame);


			absoluteLayout.Add(gradeBeltImage);
			absoluteLayout.SetLayoutBounds(gradeBeltImage, new Rect((App.screenWidth/2) - (50 * App.screenHeightAdapter), 185 * App.screenHeightAdapter, 100 * App.screenHeightAdapter, 40 * App.screenHeightAdapter));

            Label gradeLabel = new Label
			{
                FontFamily = "futuracondensedmedium",
                Text = Constants.grades[App.member.grade],
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Colors.White,
				LineBreakMode = LineBreakMode.NoWrap,
				FontSize = App.itemTitleFontSize
			};
			absoluteLayout.Add(gradeLabel);
            absoluteLayout.SetLayoutBounds(gradeLabel, new Rect((App.screenWidth / 2) - (50 * App.screenHeightAdapter), 230 * App.screenHeightAdapter, 100 * App.screenHeightAdapter, 30 * App.screenHeightAdapter));
		}
		

		public ProfileCS()
		{
			Debug.WriteLine("ProfileCS");
			NavigationPage.SetBackButtonTitle(this, "");
			this.initLayout();
			this.initSpecificLayout();

		}

		public void CreateStackButtons() {
            var buttonWidth = (App.screenWidth - 15 * App.screenWidthAdapter) / 4;

            geralButton = new MenuButton("GERAL", buttonWidth, 60);
			geralButton.button.Clicked += OnGeralButtonClicked;
			identificacaoButton = new MenuButton("ID",buttonWidth, 60);
			identificacaoButton.button.Clicked += OnIdentificacaoButtonClicked;
			moradaButton = new MenuButton("CONTACTOS", buttonWidth, 60);
			moradaButton.button.Clicked += OnMoradaButtonClicked;
			encEducacaoButton = new MenuButton("E. EDUCAÇÃO",buttonWidth, 60);
			encEducacaoButton.button.Clicked += OnEncEducacaoButtonClicked;

			stackButtons = new Microsoft.Maui.Controls.StackLayout
			{
                Spacing = 5,
                Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children =
				{
					geralButton,
					identificacaoButton,
					moradaButton,
					encEducacaoButton
				}
			};

			absoluteLayout.Add(stackButtons);
            absoluteLayout.SetLayoutBounds(stackButtons, new Rect(0, 250 * App.screenHeightAdapter, App.screenWidth, 60 * App.screenHeightAdapter));

			geralButton.activate();
			identificacaoButton.deactivate();
			moradaButton.deactivate();
			encEducacaoButton.deactivate();
		}

		public void createChangePasswordButton()
		{
            Image changePasswordImage = new Image
            {
                Source = "iconpassword.png",
                Aspect = Aspect.AspectFit
            };

            TapGestureRecognizer changePasswordImage_tapEvent = new TapGestureRecognizer();
            changePasswordImage_tapEvent.Tapped += OnChangePasswordButtonClicked;
            changePasswordImage.GestureRecognizers.Add(changePasswordImage_tapEvent);

			absoluteLayout.Add(changePasswordImage);
            absoluteLayout.SetLayoutBounds(changePasswordImage, new Rect((App.screenWidth) - (47.5 * App.screenHeightAdapter), y_button_right * App.screenHeightAdapter, 35 * App.screenHeightAdapter, 35 * App.screenHeightAdapter));

            //Debug.Print("y_button_right 0 = " + y_button_right);

            Label changePasswordLabel = new Label
            {
                FontFamily = "futuracondensedmedium",
                Text = "Segurança",
                TextColor = App.normalTextColor,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
                FontSize = App.smallTextFontSize
            };

			absoluteLayout.Add(changePasswordLabel);
            absoluteLayout.SetLayoutBounds(changePasswordLabel, new Rect((App.screenWidth) - (60 * App.screenHeightAdapter), (y_button_right + 37) * App.screenHeightAdapter, 60 * App.screenHeightAdapter, 15 * App.screenHeightAdapter));

            y_button_right = y_button_right + 60;
        }


        public void createChangeMemberButton()
        {
            if (App.members.Count > 1)
            {

                Button changeMemberButton = new Button { HorizontalOptions = LayoutOptions.Center, BackgroundColor = Colors.Transparent, ImageSource = "botaoalmudarconta.png", HeightRequest = 30 };

                /*gridButtons.ColumnDefinitions.Add(new ColumnDefinition { Width = buttonWidth });
				//RoundButton changeMemberButton = new RoundButton("Login Outro Sócio", buttonWidth-5, 40);
				changeMemberButton.Clicked += OnChangeMemberButtonClicked;*/

                Image changeMemberImage = new Image
                {
                    Source = "iconescolherutilizador.png",
                    Aspect = Aspect.AspectFit
                };

                TapGestureRecognizer changeMemberImage_tapEvent = new TapGestureRecognizer();
                changeMemberImage_tapEvent.Tapped += OnChangeMemberButtonClicked;
                changeMemberImage.GestureRecognizers.Add(changeMemberImage_tapEvent);

				absoluteLayout.Add(changeMemberImage);
                absoluteLayout.SetLayoutBounds(changeMemberImage, new Rect((12.5 * App.screenHeightAdapter), y_button_left * App.screenHeightAdapter, 35 * App.screenHeightAdapter, 35 * App.screenHeightAdapter));

                Label changeMemberLabel = new Label
                {
                    FontFamily = "futuracondensedmedium",
                    Text = "Mudar Utilizador",
                    TextColor = App.normalTextColor,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Start,
                    FontSize = App.smallTextFontSize
                };

				absoluteLayout.Add(changeMemberLabel);
                absoluteLayout.SetLayoutBounds(changeMemberLabel, new Rect(0, (y_button_left + 37) * App.screenHeightAdapter, 60 * App.screenHeightAdapter, 15 * App.screenHeightAdapter));

                //Debug.Print("y_button_left 0 = " + y_button_left);

                y_button_left = y_button_left + 60;

                //Debug.Print("y_button_left 1 = " + y_button_left);
            }
        }

        public void createChangeStudentButton()
        {

			Debug.Print("createChangeStudentButton App.original_member.students_countt = " + App.original_member.students_count);
            if (App.original_member.students_count > 1)
            {

                Button changeStudentButton = new Button { HorizontalOptions = LayoutOptions.Center, BackgroundColor = Colors.Transparent, ImageSource = "botaoalmudarconta.png", HeightRequest = 30 };

                /*gridButtons.ColumnDefinitions.Add(new ColumnDefinition { Width = buttonWidth });
				//RoundButton changeMemberButton = new RoundButton("Login Outro Sócio", buttonWidth-5, 40);
				changeMemberButton.Clicked += OnChangeMemberButtonClicked;*/

                Image changeStudentImage = new Image
                {
                    Source = "iconescolheraluno.png",
                    Aspect = Aspect.AspectFit
                };

                TapGestureRecognizer changeStudentImage_tapEvent = new TapGestureRecognizer();
                changeStudentImage_tapEvent.Tapped += OnChangeStudentButtonClicked;
                changeStudentImage.GestureRecognizers.Add(changeStudentImage_tapEvent);

				absoluteLayout.Add(changeStudentImage);
                absoluteLayout.SetLayoutBounds(changeStudentImage, new Rect((12.5 * App.screenHeightAdapter), y_button_left * App.screenHeightAdapter, 35 * App.screenHeightAdapter, 35 * App.screenHeightAdapter));

                Label changeStudentLabel = new Label
                {
                    FontFamily = "futuracondensedmedium",
                    Text = "Alunos",
                    TextColor = App.normalTextColor,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Start,
                    FontSize = App.smallTextFontSize
                };

				absoluteLayout.Add(changeStudentLabel);
                absoluteLayout.SetLayoutBounds(changeStudentLabel, new Rect(0, (y_button_left + 37) * App.screenHeightAdapter, 60 * App.screenHeightAdapter, 15 * App.screenHeightAdapter));

                y_button_left = y_button_left + 60;
            }
        }

		public void createApproveStudentButton()
		{
            if ((App.member.isInstrutorResponsavel == "1") | (App.member.isResponsavelAdministrativo == "1"))
            {
                Image membersToApproveImage = new Image
                {
                    Source = "iconaprovarinscricoes.png",
                    Aspect = Aspect.AspectFit
                };

                TapGestureRecognizer membersToApproveImage_tapEvent = new TapGestureRecognizer();
                membersToApproveImage_tapEvent.Tapped += membersToApproveImage_Clicked;
                membersToApproveImage.GestureRecognizers.Add(membersToApproveImage_tapEvent);


				absoluteLayout.Add(membersToApproveImage);
                absoluteLayout.SetLayoutBounds(membersToApproveImage, new Rect((12.5 * App.screenHeightAdapter), y_button_left * App.screenHeightAdapter, 35 * App.screenHeightAdapter, 35 * App.screenHeightAdapter));

                Label membersToApproveLabel = new Label
                {
                    FontFamily = "futuracondensedmedium",
                    Text = "Aprovar Inscrições",
                    TextColor = App.normalTextColor,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Start,
                    FontSize = App.smallTextFontSize
                };

				absoluteLayout.Add(membersToApproveLabel);
                absoluteLayout.SetLayoutBounds(membersToApproveLabel, new Rect(0, (y_button_left + 37) * App.screenHeightAdapter, 60 * App.screenHeightAdapter, 15 * App.screenHeightAdapter));

                y_button_left = y_button_left + 60;
            }
        }
        public void CreateGridButtons()
		{
			createChangePasswordButton();
            createChangeMemberButton();
            createChangeStudentButton();
			createApproveStudentButton();
            CreateObjectivesButton();
			CreateQuotaButton();
        }

		public void CreateGridGeral() {

			gridGeral = new Microsoft.Maui.Controls.Grid { Padding = 0, HorizontalOptions = LayoutOptions.FillAndExpand, RowSpacing = 5 * App.screenHeightAdapter };
			gridGeral.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridGeral.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridGeral.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridGeral.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridGeral.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridGeral.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			//gridGeral.RowDefinitions.Add(new RowDefinition { Height = 1 });
			gridGeral.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); //GridLength.Auto
			gridGeral.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); //GridLength.Auto 

			Label number_memberLabel = new FormLabel { Text = "Nº SÓCIO" };
			FormValue number_memberValue = new FormValue(App.member.number_member);

			FormLabel nameLabel = new FormLabel { Text = "NOME", HorizontalTextAlignment = TextAlignment.Start };
			nameValue = new FormValueEdit(App.member.name);

			FormLabel dojoLabel = new FormLabel { Text = "DOJO"};
			//FormValue dojoValue = new FormValue (Constants.dojos[member.dojo]);
			FormValue dojoValue = new FormValue(App.member.dojo);

			FormLabel birthdateLabel = new FormLabel { Text = "NASCIMENTO"};
            //FormValue birthdateValue = new FormValue (member.birthdate?.ToString("yyyy-MM-dd"));
            FormValue birthdateValue = new FormValue(App.member.birthdate);

            FormLabel registrationdateLabel = new FormLabel { Text = "INSCRIÇÃO"};
			FormValue registrationdateValue = new FormValue (App.member.registrationdate?.ToString("yyyy-MM-dd"));

			gridGeral.Add(number_memberLabel, 0, 0);
			gridGeral.Add(number_memberValue, 1, 0);

			gridGeral.Add(nameLabel, 0, 1);
			gridGeral.Add(nameValue, 1, 1);

			gridGeral.Add(dojoLabel, 0, 2);
			gridGeral.Add(dojoValue, 1, 2);

			gridGeral.Add(birthdateLabel, 0, 3);
			gridGeral.Add(birthdateValue, 1, 3);

			gridGeral.Add(registrationdateLabel, 0, 4);
			gridGeral.Add(registrationdateValue, 1, 4);
			
			/*absoluteLayout.Add(gridGeral,
				xConstraint: )0),
				yConstraint: )240),
				widthConstraint: Constraint.RelativeToParent((parent) =>
				{
					return (parent.Width); // center of image (which is 40 wide)
				}),
				heightConstraint: Constraint.RelativeToParent((parent) =>
				{
					return (parent.Height) - 230; // center of image (which is 40 wide)
				})
			);*/
		}

		public void CreateGridIdentificacao()
		{

			gridIdentificacao = new Microsoft.Maui.Controls.Grid { Padding = 10, RowSpacing = 5 * App.screenHeightAdapter };
			gridIdentificacao.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridIdentificacao.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridIdentificacao.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridIdentificacao.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); //GridLength.Auto
			gridIdentificacao.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); //GridLength.Auto 

			FormLabel cc_numberLabel = new FormLabel { Text = "CC" };
			FormValue cc_numberValue = new FormValue (App.member.cc_number);

			FormLabel nifLabel = new FormLabel { Text = "NIF"};
			FormValue nifValue = new FormValue (App.member.nif);

			FormLabel fnkpLabel = new FormLabel { Text = "FNKP" };
			FormValue fnkpValue = new FormValue (App.member.number_fnkp);

			gridIdentificacao.Add(cc_numberLabel, 0, 0);
			gridIdentificacao.Add(cc_numberValue, 1, 0);

			gridIdentificacao.Add(nifLabel, 0, 1);
			gridIdentificacao.Add(nifValue, 1, 1);

			gridIdentificacao.Add(fnkpLabel, 0, 2);
			gridIdentificacao.Add(fnkpValue, 1, 2);

			/*absoluteLayout.Add(gridIdentificacao,
				xConstraint: )0),
				yConstraint: )230),
				widthConstraint: Constraint.RelativeToParent((parent) =>
				{
					return (parent.Width); // center of image (which is 40 wide)
				}),
				heightConstraint: Constraint.RelativeToParent((parent) =>
				{
					return (parent.Height) - 230; // center of image (which is 40 wide)
				})
			);*/
		}

		public void CreateGridMorada()
		{

			gridMorada = new Microsoft.Maui.Controls.Grid { Padding = 10, RowSpacing = 5 * App.screenHeightAdapter };
			gridMorada.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridMorada.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridMorada.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridMorada.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridMorada.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridMorada.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); //GridLength.Auto
			gridMorada.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); //GridLength.Auto 


			FormLabel emailLabel = new FormLabel { Text = "EMAIL" };
			emailValue = new FormValue(App.member.email);

			FormLabel phoneLabel = new FormLabel { Text = "TELEFONE" };
			phoneValue = new FormValueEdit(App.member.phone);

			FormLabel addressLabel = new FormLabel { Text = "MORADA" };
			addressValue = new FormValueEdit(App.member.address);

			FormLabel cityLabel = new FormLabel { Text = "CIDADE" };
			cityValue = new FormValueEdit(App.member.city);

			FormLabel postalcodeLabel = new FormLabel { Text = "CÓDIGO POSTAL" };
			postalcodeValue = new FormValueEdit(App.member.postalcode);


			gridMorada.Add(emailLabel, 0, 0);
			gridMorada.Add(emailValue, 1, 0);

			gridMorada.Add(phoneLabel, 0, 1);
			gridMorada.Add(phoneValue, 1, 1);

			gridMorada.Add(addressLabel, 0, 2);
			gridMorada.Add(addressValue, 1, 2);

			gridMorada.Add(cityLabel, 0, 3);
			gridMorada.Add(cityValue, 1, 3);

			gridMorada.Add(postalcodeLabel, 0, 4);
			gridMorada.Add(postalcodeValue, 1, 4);

			/*absoluteLayout.Add(gridMorada,
				xConstraint: )0),
				yConstraint: )230),
				widthConstraint: Constraint.RelativeToParent((parent) =>
				{
					return (parent.Width); // center of image (which is 40 wide)
				}),
				heightConstraint: Constraint.RelativeToParent((parent) =>
				{
					return (parent.Height) - 230; // center of image (which is 40 wide)
				})
			);*/
		}

		public void CreateGridEncEducacao()
		{

			gridEncEducacao = new Microsoft.Maui.Controls.Grid { Padding = 10, RowSpacing = 5 * App.screenHeightAdapter };
			gridEncEducacao.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridEncEducacao.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridEncEducacao.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridEncEducacao.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridEncEducacao.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridEncEducacao.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridEncEducacao.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridEncEducacao.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridEncEducacao.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); 
			gridEncEducacao.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });


			FormLabel EncEducacao1Label = new FormLabel { Text = "ENCARREGADO DE EDUCACAO 1", FontSize = App.itemTitleFontSize };

			FormLabel EncEducacao1NomeLabel = new FormLabel { Text = "NOME" };
			EncEducacao1NomeValue = new FormValueEdit(App.member.name_enc1);

			FormLabel EncEducacao1PhoneLabel = new FormLabel { Text = "TELEFONE" };
			EncEducacao1PhoneValue = new FormValueEdit(App.member.phone_enc1);

			FormLabel EncEducacao1MailLabel = new FormLabel { Text = "MAIL" };
			EncEducacao1MailValue = new FormValueEdit(App.member.mail_enc1);

			FormLabel EncEducacao2Label = new FormLabel { Text = "ENCARREGADO DE EDUCACAO 2", FontSize = App.itemTitleFontSize };

			FormLabel EncEducacao2NomeLabel = new FormLabel { Text = "NOME" };
			EncEducacao2NomeValue = new FormValueEdit(App.member.name_enc2);

			FormLabel EncEducacao2PhoneLabel = new FormLabel { Text = "TELEFONE" };
			EncEducacao2PhoneValue = new FormValueEdit(App.member.phone_enc2);

			FormLabel EncEducacao2MailLabel = new FormLabel { Text = "MAIL" };
			EncEducacao2MailValue = new FormValueEdit(App.member.mail_enc2);


			gridEncEducacao.Add(EncEducacao1Label, 0, 0);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(EncEducacao1Label, 2);

			gridEncEducacao.Add(EncEducacao1NomeLabel, 0, 1);
			gridEncEducacao.Add(EncEducacao1NomeValue, 1, 1);

			gridEncEducacao.Add(EncEducacao1PhoneLabel, 0, 2);
			gridEncEducacao.Add(EncEducacao1PhoneValue, 1, 2);

			gridEncEducacao.Add(EncEducacao1MailLabel, 0, 3);
			gridEncEducacao.Add(EncEducacao1MailValue, 1, 3);

			gridEncEducacao.Add(EncEducacao2Label, 0, 4);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(EncEducacao2Label, 2);

			gridEncEducacao.Add(EncEducacao2NomeLabel, 0, 5);
			gridEncEducacao.Add(EncEducacao2NomeValue, 1, 5);

			gridEncEducacao.Add(EncEducacao2PhoneLabel, 0, 6);
			gridEncEducacao.Add(EncEducacao2PhoneValue, 1, 6);

			gridEncEducacao.Add(EncEducacao2MailLabel, 0, 7);
			gridEncEducacao.Add(EncEducacao2MailValue, 1, 7);

			/*absoluteLayout.Add(gridEncEducacao,
				xConstraint: )0),
				yConstraint: )230),
				widthConstraint: Constraint.RelativeToParent((parent) =>
				{
					return (parent.Width); // center of image (which is 40 wide)
				}),
				heightConstraint: Constraint.RelativeToParent((parent) =>
				{
					return (parent.Height) - 230; // center of image (which is 40 wide)
				})
			);*/
		}


		async void OnGeralButtonClicked(object sender, EventArgs e)
		{
			Debug.WriteLine("OnLoginButtonClicked");
			geralButton.activate();
			identificacaoButton.deactivate();
			moradaButton.deactivate();
			encEducacaoButton.deactivate();

			scrollView.Content = gridGeral;

			/*gridGeral.IsVisible = true;
			gridIdentificacao.IsVisible = false;
			gridMorada.IsVisible = false;
			gridEncEducacao.IsVisible = false;*/

			Debug.Print("AQUIIII1");
			if (enteringPage == false) {
				await UpdateMemberInfo();
				enteringPage = false;
			}
			

		}

		async void OnIdentificacaoButtonClicked(object sender, EventArgs e)
		{
			Debug.WriteLine("OnIdentificacaoButtonClicked");
			geralButton.deactivate();
			identificacaoButton.activate();
			moradaButton.deactivate();
			encEducacaoButton.deactivate();

			scrollView.Content = gridIdentificacao;

			/*gridGeral.IsVisible = false;
			gridIdentificacao.IsVisible = true;
			gridMorada.IsVisible = false;
			gridEncEducacao.IsVisible = false;*/

			await UpdateMemberInfo();
		}


		async void OnMoradaButtonClicked(object sender, EventArgs e)
		{
			Debug.WriteLine("OnMoradaButtonClicked");

			geralButton.deactivate();
			identificacaoButton.deactivate();
			moradaButton.activate();
			encEducacaoButton.deactivate();

			scrollView.Content = gridMorada;

			/*gridGeral.IsVisible = false;
			gridIdentificacao.IsVisible = false;
			gridMorada.IsVisible = true;
			gridEncEducacao.IsVisible = false;*/

			await UpdateMemberInfo();
		}

		async void OnEncEducacaoButtonClicked(object sender, EventArgs e)
		{
			Debug.WriteLine("OnEncEducacaoButtonClicked");

			geralButton.deactivate();
			identificacaoButton.deactivate();
			moradaButton.deactivate();
			encEducacaoButton.activate();

            scrollView.Content = gridEncEducacao;

			/*gridGeral.IsVisible = false;
			gridIdentificacao.IsVisible = false;
			gridMorada.IsVisible = false;
			gridEncEducacao.IsVisible = true;*/

			await UpdateMemberInfo();
		}

		async void OnLogoutButtonClicked(object sender, EventArgs e)
		{
			Debug.WriteLine("OnLogoutButtonClicked");

			Preferences.Default.Remove("EMAIL");
            Preferences.Default.Remove("PASSWORD");
            Preferences.Default.Remove("SELECTEDUSER");
			App.member = null;
			App.members = null;



			Application.Current.MainPage = new NavigationPage(new LoginPageCS(""))
			{
				BarBackgroundColor = Color.FromRgb(15, 15, 15),
				BarTextColor = Colors.White//FromRgb(75, 75, 75)
			};
		}

		async void OnChangePasswordButtonClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ChangePasswordPageCS(App.member));
		}

		async void OnChangeMemberButtonClicked(object sender, EventArgs e)
		{
			changeMember = true;

			//Navigation.PushAsync(new SelectMemberPageCS());
			Navigation.InsertPageBefore(new SelectMemberPageCS(), this);
			await Navigation.PopAsync();
			await Navigation.PopAsync();
		}

		async void OnChangeStudentButtonClicked(object sender, EventArgs e)
		{
            changeMember = true;
			Navigation.InsertPageBefore(new SelectStudentPageCS(), this);
			await Navigation.PopAsync();
			await Navigation.PopAsync();

			//Navigation.PushAsync(new SelectStudentPageCS());
		}

		async void OnBackOriginalButtonClicked(object sender, EventArgs e)
		{
			changeMember = true;
			App.member = App.original_member;
            //Navigation.PushAsync(new MainTabbedPageCS(""));
            App.Current.MainPage = new NavigationPage(new MainTabbedPageCS("", ""))
            {
                BarBackgroundColor = Color.FromRgb(15, 15, 15),
                BarTextColor = Colors.White//FromRgb(75, 75, 75)
            };

		}


        async void membersToApproveImage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ApproveRegistrationPageCS());
        }

        async void OnQuotaButtonClicked(object sender, EventArgs e)
		{

			//activateButton.IsEnabled = false;

			MemberManager memberManager = new MemberManager();

			if (App.member.currentFee is null)
			{
				var result_create = await memberManager.CreateFee(App.member, DateTime.Now.ToString("yyyy"));
				if (result_create == -1)
				{
					Application.Current.MainPage = new NavigationPage(new LoginPageCS("Verifique a sua ligação à Internet e tente novamente."))
					{
						BarBackgroundColor = Color.FromRgb(15, 15, 15),
						BarTextColor = Colors.White
					};
					return;
				}
				var result_get = await GetCurrentFees(App.member);
				if (result_get == -1)
				{
					Application.Current.MainPage = new NavigationPage(new LoginPageCS("Verifique a sua ligação à Internet e tente novamente."))
					{
						BarBackgroundColor = Color.FromRgb(15, 15, 15),
						BarTextColor = Colors.White
					};
					return;
				}
                await Navigation.PushAsync(new QuotasPaymentPageCS(App.member));
            }
			else
			{
                await Navigation.PushAsync(new QuotasListPageCS());
			}

		}

        async void OnObjectivesButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ObjectivesPageCS());   
        }


        async Task<int> GetCurrentFees(Member member)
		{
			Debug.WriteLine("GetCurrentFees");
			MemberManager memberManager = new MemberManager();

			var result = await memberManager.GetCurrentFees(App.member);
			if (result == -1)
			{
				Application.Current.MainPage = new NavigationPage(new LoginPageCS("Verifique a sua ligação à Internet e tente novamente."))
				{
					BarBackgroundColor = Color.FromRgb(15, 15, 15),
					BarTextColor = Colors.White
				};
				return -1;
			}
			return result;
		}


		async Task<string> UpdateMemberInfo()
		{
			Debug.Print("AQUIIII UpdateMemberInfo");

			if (App.member != null)
			{
                if (string.IsNullOrEmpty(postalcodeValue.entry.Text)) {
					postalcodeValue.entry.Text = "";
				}

				Debug.WriteLine("UpdateMemberInfo " + nameValue.entry.Text);
				if (nameValue.entry.Text == "")
				{
					nameValue.entry.Text = App.member.name;
                    await DisplayAlert("DADOS INVÁLIDOS", "O nome introduzido não é válido.", "Ok" );
					return "-1";
				}
				else if (phoneValue.entry.Text == null)
                {
                    phoneValue.entry.Text = App.member.phone;
                    await DisplayAlert("DADOS INVÁLIDOS", "Tem de introduzir o telefone.", "Ok" );
                    return "-1";
                }
                else if (!Regex.IsMatch(phoneValue.entry.Text, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$"))
				{
					phoneValue.entry.Text = App.member.phone;
                    await DisplayAlert("DADOS INVÁLIDOS", "O telefone introduzido não é válido.", "Ok" );
					return "-1";
				}
				else if (!Regex.IsMatch((postalcodeValue.entry.Text), @"^\d{4}-\d{3}$"))
				{
					postalcodeValue.entry.Text = App.member.postalcode;
                    await DisplayAlert("DADOS INVÁLIDOS", "O código postal introduzido não é válido.", "Ok" );
					return "-1";
				}
				
				Debug.WriteLine("UpdateMemberInfo "+ App.member.name);
				App.member.name = nameValue.entry.Text;
				App.member.email = emailValue.label.Text;
				App.member.phone = phoneValue.entry.Text;
				App.member.address = addressValue.entry.Text;
				App.member.city = cityValue.entry.Text;
				App.member.postalcode = postalcodeValue.entry.Text;
				App.member.name_enc1 = EncEducacao1NomeValue.entry.Text;
				App.member.phone_enc1 = EncEducacao1PhoneValue.entry.Text;
				App.member.mail_enc1 = EncEducacao1MailValue.entry.Text;
				App.member.name_enc2 = EncEducacao2NomeValue.entry.Text;
				App.member.phone_enc2 = EncEducacao2PhoneValue.entry.Text;
				App.member.mail_enc2 = EncEducacao2MailValue.entry.Text;


				MemberManager memberManager = new MemberManager();

				var result = await memberManager.UpdateMemberInfo(App.member);
				if (result == "-1")
				{
					Application.Current.MainPage = new NavigationPage(new LoginPageCS("Verifique a sua ligação à Internet e tente novamente."))
					{
						BarBackgroundColor = Color.FromRgb(15, 15, 15),
						BarTextColor = Colors.White
					};
					return "-1";
				}
				return result;
			}
			return "";
		}

        void memberPhotoImageTappedAsync(object sender, System.EventArgs e)
        {
            displayMemberPhotoImageActionSheet();
        }

        async Task<string> displayMemberPhotoImageActionSheet()
        {
            var actionSheet = await DisplayActionSheet("Fotografia Sócio", "Cancel", null, "Tirar Foto", "Galeria de Imagens");
            MemberManager memberManager = new MemberManager();
            string result = "";
            switch (actionSheet)
            {
                case "Cancel":
                    break;
                case "Tirar Foto":
                    TakeAPhotoTapped();
                    break;
                case "Galeria de Imagens":
                    OpenGalleryTapped();
                    break;
            }
            /*Device.BeginInvokeOnMainThread(() =>
			{
				var fileName = SetImageFileName();
				DependencyService.Get<CameraInterface>().LaunchCamera(FileFormatEnum.JPEG, fileName);
			});*/

            return "";
        }

        async void OpenGalleryTapped()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Por favor escolha uma foto"
            });

            if (result != null)
            {
                Stream stream_aux = await result.OpenReadAsync();
                Stream localstream = await result.OpenReadAsync();

                memberPhotoImage.Source = ImageSource.FromStream(() => localstream);
                if (DeviceInfo.Platform != DevicePlatform.iOS)
                {
                    memberPhotoImage.Rotation = 90;
                    stream = RotateBitmap(stream_aux, 90);
                }
                else
                {
                    memberPhotoImage.Rotation = 0;
                    stream = RotateBitmap(stream_aux, 90);
                }

                MemberManager memberManager = new MemberManager();
                memberManager.Upload_Member_Photo(stream);
            }

        }

        async void TakeAPhotoTapped()
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                Stream stream_aux = await result.OpenReadAsync();
                Stream localstream = await result.OpenReadAsync();

                memberPhotoImage.Source = ImageSource.FromStream(() => localstream);
                memberPhotoImage.Rotation = 90;
                stream = RotateBitmap(stream_aux, 90);
                
                MemberManager memberManager = new MemberManager();
                memberManager.Upload_Member_Photo(stream);
            }

        }

        public Stream RotateBitmap(Stream _stream, int angle)
        {
            Stream streamlocal = null;
            SKBitmap bitmap = SKBitmap.Decode(_stream);
            SKBitmap rotatedBitmap = new SKBitmap(bitmap.Height, bitmap.Width);
            if (angle != 0)
            {
                using (var surface = new SKCanvas(rotatedBitmap))
                {
                    surface.Translate(rotatedBitmap.Width, 0);
                    surface.RotateDegrees(angle);
                    surface.DrawBitmap(bitmap, 0, 0);
                }
            }
            else
            {
                rotatedBitmap = bitmap;
            }

            using (MemoryStream memStream = new MemoryStream())
            using (SKManagedWStream wstream = new SKManagedWStream(memStream))
            {
                rotatedBitmap.Encode(wstream, SKEncodedImageFormat.Jpeg, 40);
                byte[] data = memStream.ToArray();
                streamlocal = new MemoryStream(data);
            }
            return streamlocal;

        }
    }
}