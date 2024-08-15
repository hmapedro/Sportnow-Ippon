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
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = App.screenWidth - 80 * App.screenWidthAdapter });
			//grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

			Image image = new Image { Source = examination_Program.image, Aspect = Aspect.AspectFit, HeightRequest = 24, WidthRequest = 80 * App.screenWidthAdapter };

			Label gradeLabel = new Label { Text = examination_Program.examinationTo_string, FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = Colors.White, TextDecorations = TextDecorations.Underline, LineBreakMode = LineBreakMode.NoWrap, FontSize = App.titleFontSize };

			grid.Add(image, 0, 0);
			grid.Add(gradeLabel, 1, 0);

            Microsoft.Maui.Controls.Grid gridDetail = new Microsoft.Maui.Controls.Grid { Padding = 10 };
			gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //gridDetail.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            gridDetail.ColumnDefinitions.Add(new ColumnDefinition { Width = WidthRequest = App.screenWidth - 20 * App.screenWidthAdapter});

			Label kihonHeaderLabel, kihonLabel, kataHeaderLabel, kataLabel, kumiteHeaderLabel, kumiteLabel, shiaikumiteHeaderLabel, shiaikumiteLabel, youtubeLabel;

			kihonHeaderLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = Color.FromRgb(246, 220, 178), LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
			kihonHeaderLabel.Text = "KIHON";
			gridDetail.Add(kihonHeaderLabel, 0, 0);

			kihonLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = Colors.White, LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
			kihonLabel.Text = examination_Program.kihonText;
			gridDetail.Add(kihonLabel, 0, 1);

			kataHeaderLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = Color.FromRgb(246, 220, 178), LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
			kataHeaderLabel.Text = "KATA";
			gridDetail.Add(kataHeaderLabel, 0, 2);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(kataHeaderLabel, 3);

			kataLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = Colors.White, LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
            kataLabel.Text = examination_Program.kataText;
			gridDetail.Add(kataLabel, 0, 3);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(kataLabel, 3);

			kumiteHeaderLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = Color.FromRgb(246, 220, 178), LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
			kumiteHeaderLabel.Text = "KUMITE";
			gridDetail.Add(kumiteHeaderLabel, 0, 4);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(kumiteHeaderLabel, 3);

			kumiteLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = Colors.White, LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
            kumiteLabel.Text = examination_Program.kumiteText;
			gridDetail.Add(kumiteLabel, 0, 5);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(kumiteLabel, 3);


			shiaikumiteHeaderLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = Color.FromRgb(246, 220, 178), LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
			shiaikumiteHeaderLabel.Text = "SHIAI KUMITE";
			gridDetail.Add(shiaikumiteHeaderLabel, 0, 6);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(shiaikumiteHeaderLabel, 3);

			shiaikumiteLabel = new Label { FontFamily = "futuracondensedmedium", HorizontalTextAlignment = TextAlignment.Start, TextColor = Colors.White, LineBreakMode = LineBreakMode.WordWrap, FontSize = App.itemTitleFontSize };
            shiaikumiteLabel.Text = examination_Program.shiaikumiteText;
			gridDetail.Add(shiaikumiteLabel, 0, 7);
			Microsoft.Maui.Controls.Grid.SetColumnSpan(shiaikumiteLabel, 3);

			grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			grid.Add(gridDetail, 0, 1);
			grid.SetColumnSpan(gridDetail, 2);

			absoluteLayout.Add(grid);
            absoluteLayout.SetLayoutBounds(grid, new Rect(0, 10 * App.screenHeightAdapter, App.screenWidth - 20 * App.screenWidthAdapter, App.screenHeight - 160 * App.screenHeightAdapter));
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

