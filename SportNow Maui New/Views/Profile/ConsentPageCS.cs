using System;
using System.Collections.Generic;
using Microsoft.Maui;
using SportNow.Model;
using SportNow.Services.Data.JSON;
using System.Diagnostics;
using SportNow.CustomViews;
using System.Runtime.CompilerServices;


namespace SportNow.Views.Profile
{
    public class ConsentPageCS : DefaultPage
	{

		protected async override void OnAppearing()
		{
            base.OnAppearing();
            initLayout();
			initSpecificLayout();
		}


		protected async override void OnDisappearing()
		{
			if (absoluteLayout != null)
			{
				absoluteLayout = null;
				this.Content = null;
			}

		}

		//Image estadoQuotaImage;

		private CollectionView collectionViewMembers;
		List<Member> members_To_Approve;
		Label titleLabel;
		CheckBox checkboxConfirm;

        Button confirmConsentButton;

		CheckBox checkBoxAssembleiaGeral, checkBoxRegulamentoInterno, checkBoxTratamentoDados, checkBoxRegistoImagens, checkBoxFotografiaSocio, checkBoxWhatsApp;

        private ScrollView scrollView;

        public void initLayout()
		{
			Title = "CONSENTIMENTOS";

		}


		public async void initSpecificLayout()
		{
			if (absoluteLayout == null)
			{
				initBaseLayout();
            }

            scrollView = new ScrollView { Orientation = ScrollOrientation.Vertical };

            absoluteLayout.Add(scrollView);
            absoluteLayout.SetLayoutBounds(scrollView, new Rect(10 * App.screenWidthAdapter, 10 * App.screenHeightAdapter, App.screenWidth - 20 * App.screenWidthAdapter, App.screenHeight - 110 * App.screenHeightAdapter));


			Microsoft.Maui.Controls.Grid gridConsent = new Microsoft.Maui.Controls.Grid { Padding = 0, HorizontalOptions = LayoutOptions.FillAndExpand };
            scrollView.Content = gridConsent;
            gridConsent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            gridConsent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            gridConsent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //gridGeral.RowDefinitions.Add(new RowDefinition { Height = 1 });
            gridConsent.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); //GridLength.Auto 
            gridConsent.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); //GridLength.Auto 


            int y_index = (int)(20 * App.screenHeightAdapter);

			Label labelRegulamentoInterno = new Label { FontFamily = "futuracondensedmedium", BackgroundColor = Colors.Transparent, VerticalTextAlignment = TextAlignment.Start, HorizontalTextAlignment = TextAlignment.Start, FontSize = App.consentFontSize, TextColor = App.normalTextColor, LineBreakMode = LineBreakMode.WordWrap };
			labelRegulamentoInterno.Text = "Declaro que as informações e dados pessoais transmitidos são verdadeiros e atuais.\n\nAutorizo o tratamento dos meus dados pessoais e/ou do meu educando, por parte da Ippon Karate Portugal, para efeitos de processos associados a faturação, a atividades desportivas, em particular para filiação/refiliação em federações desportivas, incluindo inscrições em eventos desportivos nacionais ou internacionais, a contratação de seguros desportivos, e, bem assim, ao envio de mensagens sobre a atividade desportiva e corrente da Ippon Karate Portugal (SMS, MMS, APP e correio eletrónico).\n\nAutorizo igualmente o registo, gravação, captação de imagens e testemunhos dos treinos, competições e outros eventos de cariz desportivo, formativo e lúdico para utilização com finalidades pedagógicas e/ou promocionais. Neste âmbito, a Ippon Karate Portugal pode proceder à divulgação, total ou parcial, dessas atividades, imagens e testemunhos que lhe estão associadas através das suas páginas eletrónicas, portais ou redes sociais, incluindo plataformas e canais digitais pertencentes a órgãos de comunicação social. \n\nFace ao exposto, cedo, a título gratuito os direitos de imagem associados à minha participação e/ou do meu educando nas várias iniciativas desportivas, pedagógicas, formativas e lúdicas promovidas pela Ippon Karate Portugal. \n\nAutorizo a Ippon Karate Portugal a recolher a foto tipo passe do sócio para uso na ficha de sócio e para a emissão de credenciais de eventos. \n\nLi e concordo com o Regulamento Interno da Ippon Karate Portugal disponível em www.ippon.pt.\n";

            Label labelConfirm = new Label { FontFamily = "futuracondensedmedium", BackgroundColor = Colors.Transparent, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Start, FontSize = App.consentFontSize, TextColor = App.normalTextColor, LineBreakMode = LineBreakMode.WordWrap };
            labelConfirm.Text = "CONFIRMO QUE ACEITO OS CONSENTIMENTOS APRESENTADOS.";


            checkboxConfirm = new CheckBox { Color = App.topColor};

            gridConsent.Add(labelRegulamentoInterno, 0, 0);
            Microsoft.Maui.Controls.Grid.SetColumnSpan(labelRegulamentoInterno, 2);
            gridConsent.Add(checkboxConfirm, 0, 1);
			gridConsent.Add(labelConfirm, 1, 1);
            

            RoundButton confirmButton = new RoundButton("CONFIRMAR", App.screenWidth - 40 * App.screenWidthAdapter, 50);
			confirmButton.button.Clicked += confirmConsentButtonClicked;

			gridConsent.Add(confirmButton, 0, 2);
            Microsoft.Maui.Controls.Grid.SetColumnSpan(confirmButton, 2);

            /*absoluteLayout.Add(confirmButton);
            absoluteLayout.SetLayoutBounds(confirmButton, new Rect(10 * App.screenWidthAdapter, App.screenHeight - 160 * App.screenHeightAdapter, App.screenWidth - 20 * App.screenWidthAdapter, 50 * App.screenHeightAdapter));
			*/

        }

		public ConsentPageCS()
		{
            this.initLayout();
            this.initSpecificLayout();
        }

		async void confirmConsentButtonClicked(object sender, EventArgs e)
		{

            if (checkboxConfirm.IsChecked == false)
			{
                await DisplayAlert("Confirmação necessária", "Para prosseguir é necessário confirmar que aceitas as condições expostas.", "OK");
				return;
            }
            //SAVE CONSENTIMENTOS!!!!!
            showActivityIndicator();
			MemberManager memberManager = new MemberManager();

			App.member = new Member();
			App.member.consentimento_regulamento = "1";// Convert.ToInt32(checkBoxRegulamentoInterno.IsChecked).ToString();				

			//var result = await memberManager.Update_Member_Authorizations(App.member.id, App.member.consentimento_regulamento);
            hideActivityIndicator();
            Debug.Print("App.member.member_type = " + App.member.member_type);


            //await Navigation.PushAsync(new CompleteRegistration_Sports_PageCS());
            await Navigation.PushAsync(new NewMemberPageCS());

            /*if (App.member.member_type == "praticante")
			{
				await Navigation.PushAsync(new CompleteRegistration_Documents_PageCS());
			}
			else
			{
				await Navigation.PushAsync(new CompleteRegistration_Profile_PageCS());
			}*/


        }
	}

}