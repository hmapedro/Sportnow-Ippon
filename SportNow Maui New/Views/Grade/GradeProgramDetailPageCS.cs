using System;
using System.Collections.Generic;
using Microsoft.Maui;
using SportNow.Model;
using SportNow.Services.Data.JSON;
using System.Threading.Tasks;
using System.Diagnostics;
using SportNow.CustomViews;
using SportNow.ViewModel;
using Microsoft.Maui;


namespace SportNow.Views
{
	public class GradeProgramDetailPageCS : DefaultPage
	{

		protected override void OnDisappearing() {
			//collectionViewExaminations.SelectedItem = null;
		}

		Examination_Program examination_Program;


		public void initLayout()
		{
			Title = "PROGRAMA EXAME";
		}

		public void CleanProgramasExameCollectionView()
		{
		}

		public async void initSpecificLayout()
		{
			showActivityIndicator();

			CreateProgramasExameDetail();

            hideActivityIndicator();
		}

		public async void CreateProgramasExameDetail()
		{

			Microsoft.Maui.Controls.Grid grid = new Microsoft.Maui.Controls.Grid { Padding = 10 };
			grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 80 * App.screenWidthAdapter });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 200 * App.screenWidthAdapter });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

			Image image = new Image { Source = examination_Program.image, Aspect = Aspect.AspectFit, HeightRequest = 24, WidthRequest = 80 * App.screenWidthAdapter };

			Label gradeLabel = new Label { Text = examination_Program.examinationTo_string, FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = App.normalTextColor, TextDecorations = TextDecorations.Underline, LineBreakMode = LineBreakMode.NoWrap, FontSize = App.titleFontSize };


            /*Image youtubeImage = new Image { Aspect = Aspect.AspectFit, HeightRequest = 24, WidthRequest = 60, Source = "youtube.png" };
            youtubeImage.SetBinding(Image.AutomationIdProperty, "video");

            var youtubeImage_tap = new TapGestureRecognizer();
            youtubeImage_tap.Tapped += async (s, e) =>
            {
                try
                {
					Debug.Print("Open Youtube video "+ ((Image)s).AutomationId);
                    //await Browser.OpenAsync(((Image)s).AutomationId, BrowserLaunchMode.SystemPreferred);
                    await Browser.OpenAsync("https://www.ippon.pt", BrowserLaunchMode.SystemPreferred);
                }
                catch (Exception ex)
                {
                }
            };
            youtubeImage.GestureRecognizers.Add(youtubeImage_tap);*/

            grid.Add(image, 0, 0);
            grid.Add(gradeLabel, 1, 0);
            //grid.Add(youtubeImage, 2, 0);

            Microsoft.Maui.Controls.Grid gridDetail = new Microsoft.Maui.Controls.Grid { Padding = 10 };
			gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            
            //gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            gridDetail.ColumnDefinitions.Add(new ColumnDefinition { Width = WidthRequest = App.screenWidth });

			Label kihonHeaderLabel, kihonLabel, kataHeaderLabel, kataLabel, kumiteHeaderLabel, kumiteLabel, shiaikumiteHeaderLabel, shiaikumiteLabel, estacaokataHeaderLabel, youtubeLabel;//, estacaokataLabel;

			kihonHeaderLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = App.topColor, LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
			kihonHeaderLabel.Text = "KIHON";
			gridDetail.Add(kihonHeaderLabel, 0, 0);

            kihonLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = App.normalTextColor, LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
			kihonLabel.Text = examination_Program.kihonText;
			gridDetail.Add(kihonLabel, 0, 1);

			kataHeaderLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = App.topColor, LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
			kataHeaderLabel.Text = "KATA";
			gridDetail.Add(kataHeaderLabel, 0, 2);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(kataHeaderLabel, 3);

			kataLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = App.normalTextColor, LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
            kataLabel.Text = examination_Program.kataText;
			gridDetail.Add(kataLabel, 0, 3);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(kataLabel, 3);

			kumiteHeaderLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = App.topColor, LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
			kumiteHeaderLabel.Text = "KUMITE";
			gridDetail.Add(kumiteHeaderLabel, 0, 4);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(kumiteHeaderLabel, 3);

			kumiteLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = App.normalTextColor, LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
            kumiteLabel.Text = examination_Program.kumiteText;
			gridDetail.Add(kumiteLabel, 0, 5);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(kumiteLabel, 3);


			shiaikumiteHeaderLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = App.topColor, LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
			shiaikumiteHeaderLabel.Text = "SHIAI KUMITE";
			gridDetail.Add(shiaikumiteHeaderLabel, 0, 6);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(shiaikumiteHeaderLabel, 3);

			shiaikumiteLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = App.normalTextColor, LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
            shiaikumiteLabel.Text = examination_Program.shiaikumiteText;
			gridDetail.Add(shiaikumiteLabel, 0, 7);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(shiaikumiteLabel, 3);


            estacaokataHeaderLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = App.topColor, LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
            estacaokataHeaderLabel.Text = "ESTAÇÃO KATA";
            gridDetail.Add(estacaokataHeaderLabel, 0, 8);
            Microsoft.Maui.Controls.Grid.SetColumnSpan(estacaokataHeaderLabel, 3);

			int i = 9;
			foreach (Examination_Technique examination_Technique in examination_Program.examination_techniques)
			{
				if (examination_Technique.type == "estacao_kata")
				{
                    gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    Label estacaokataLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = App.normalTextColor, LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
                    estacaokataLabel.Text = examination_Technique.order + " - " + examination_Technique.name;
                    gridDetail.Add(estacaokataLabel, 0, i);
                    Microsoft.Maui.Controls.Grid.SetColumnSpan(estacaokataLabel, 2);

                    Image youtubeImage = new Image { Aspect = Aspect.AspectFit, HeightRequest = 24, WidthRequest = 60, Source = "youtube.png" };

                    var youtubeImage_tap = new TapGestureRecognizer();
                    youtubeImage_tap.Tapped += async (s, e) =>
                    {
                        try
                        {
                            Debug.Print("Open Youtube video " + examination_Technique.video);
                            await Browser.OpenAsync(examination_Technique.video, BrowserLaunchMode.SystemPreferred);
                            //await Browser.OpenAsync("https://www.ippon.pt", BrowserLaunchMode.SystemPreferred);
                        }
                        catch (Exception ex)
                        {
                        }
                    };
                    estacaokataLabel.GestureRecognizers.Add(youtubeImage_tap);

                    gridDetail.Add(youtubeImage, 2, i);
                    i++;
                }
            }

            /*estacaokataLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = App.normalTextColor, LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
            estacaokataLabel.Text = examination_Program.estacaoKataText;
            gridDetail.Add(estacaokataLabel, 0, 9);
            Microsoft.Maui.Controls.Grid.SetColumnSpan(estacaokataLabel, 3);*/

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			grid.Add(gridDetail, 0, 1);
			grid.SetColumnSpan(gridDetail, 2);

			absoluteLayout.Add(grid);
            absoluteLayout.SetLayoutBounds(grid, new Rect(0, 10 * App.screenHeightAdapter, App.screenWidth, App.screenHeight - 160 * App.screenHeightAdapter));
        }

			
		public GradeProgramDetailPageCS(Examination_Program examination_Program)
		{
			this.examination_Program = examination_Program;

            this.initLayout();
			this.initSpecificLayout();

			//Parent.

		}
	}
}

