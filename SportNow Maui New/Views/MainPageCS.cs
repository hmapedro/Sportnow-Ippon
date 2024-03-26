﻿using System;
using System.Collections.Generic;
using Microsoft.Maui;
using SportNow.Model;
using SportNow.Services.Data.JSON;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using SportNow.ViewModel;
using Microsoft.Maui;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using SportNow.Views.Profile;
using SportNow.CustomViews;
using SportNow.Views.Personal;
using System.Xml;
using Microsoft.Maui.Controls.Shapes;
using System.Runtime.CompilerServices;

namespace SportNow.Views
{
	public class MainPageCS : DefaultPage
	{

		protected async override void OnAppearing()
		{
            base.OnAppearing();
            Debug.WriteLine("MainPageCS.OnAppearing");

            App.AdaptScreen();
            base.OnAppearing();

            //showActivityIndicator();
            initSpecificLayout();
            //hideActivityIndicator();

            //await Task.Delay(100);
            //loading.IsAnimationPlaying = false;
            //await Task.Delay(100);
            //loading.IsAnimationPlaying = true;

        }

		protected override void OnDisappearing()
		{
			this.CleanScreen();
		}

		public List<MainMenuItem> MainMenuItems { get; set; }

		private Member member;

		Label msg;
		Button btn;

		private ObservableCollection<Class_Schedule> cleanClass_Schedule, importantClass_Schedule;

		private List<Class_Schedule> teacherClass_Schedules;

		private CollectionView importantClassesCollectionView;
		private CollectionView importantEventsCollectionView;
		private CollectionView teacherClassesCollectionView;

		ScheduleCollection scheduleCollection;

		Label usernameLabel, attendanceLabel, eventsLabel, teacherClassesLabel;
		Label currentFeeLabel;
		Label famousQuoteLabel;
		Label currentVersionLabel;

		private List<Event> importantEvents;
		private List<Competition> importantCompetitions;
		private List<Examination_Session> importantExaminationSessions;


		int classesY = 0;
		int eventsY = 0;
		int teacherClassesY = 0;
		int eventsHeight = 0;
        int personalClassesY = 0;
        int feesOrQuoteY = 0;

		RoundButton personalClassesButton;
		RoundButton technicalButton;

        public void CleanScreen()
		{
			//Debug.Print("CleanScreen");
			//valida se os objetos já foram criados antes de os remover
			if (usernameLabel != null)
			{
				absoluteLayout.Remove(usernameLabel);
				usernameLabel = null;
			}

			if (importantClassesCollectionView != null)
			{
				//absoluteLayout.Clear();
				
				absoluteLayout.Children.Remove(importantClassesCollectionView);
				absoluteLayout.Children.Remove(attendanceLabel);
				absoluteLayout.Children.Remove(importantEventsCollectionView);
				absoluteLayout.Children.Remove(eventsLabel);

				importantClassesCollectionView = null;
				importantEventsCollectionView = null;
				attendanceLabel = null;
				eventsLabel = null;
			}
			if (teacherClassesCollectionView != null) {

				absoluteLayout.Children.Remove(teacherClassesCollectionView);
				absoluteLayout.Children.Remove(teacherClassesLabel);
				teacherClassesCollectionView = null;
				teacherClassesLabel = null;
			}
			if (currentFeeLabel != null)
			{
				absoluteLayout.Children.Remove(currentFeeLabel);
				currentFeeLabel = null;
			}
			if (currentVersionLabel != null)
			{
				absoluteLayout.Children.Remove(currentVersionLabel);
				currentVersionLabel = null;
			}

			if (famousQuoteLabel != null)
			{
				absoluteLayout.Children.Remove(famousQuoteLabel);
				famousQuoteLabel = null;
			}

		}

		public void initLayout()
		{

            Title = "PRINCIPAL";

			ToolbarItem toolbarItem = new ToolbarItem();

            if (App.member.members_to_approve.Count != 0)
			{
				toolbarItem.IconImageSource = "perfil.png";
            }
			else
            {
                toolbarItem.IconImageSource = "perfil.png";
            }

            toolbarItem.Clicked += OnPerfilButtonClicked;
			ToolbarItems.Add(toolbarItem);

		}


		public async void initSpecificLayout()
        {
			
			//await Task.Delay(1000);
           /* Image loading1 = new Image() { Source = "loading.gif", IsAnimationPlaying = true };
            absoluteLayout.Add(loading1);
            absoluteLayout.SetLayoutBounds(loading1, new Rect((App.screenWidth / 2) - 80 * App.screenWidthAdapter, (App.screenHeight / 2) - 140 * App.screenHeightAdapter, 160 * App.screenWidthAdapter, 80 * App.screenHeightAdapter));
		   */
            var textWelcome = "";

			textWelcome = "Olá " + App.member.nickname;

			//USERNAME LABEL
			usernameLabel = new Label
			{
				Text = textWelcome,
				TextColor = Colors.White,
				HorizontalTextAlignment = TextAlignment.End,
				FontSize = App.titleFontSize,
                FontFamily = "futuracondensedmedium",
            };
			absoluteLayout.Add(usernameLabel);
			absoluteLayout.SetLayoutBounds(usernameLabel, new Rect(App.screenWidth - 310 * App.screenWidthAdapter, 2 * App.screenHeightAdapter, 300 * App.screenWidthAdapter, 30 * App.screenHeightAdapter));

            showActivityIndicator();

            DateTime currentTime = DateTime.Now.Date;
            DateTime currentTime_add7 = DateTime.Now.AddDays(7).Date;

            string firstDay = currentTime.ToString("yyyy-MM-dd");
            string lastday = currentTime_add7.AddDays(6).ToString("yyyy-MM-dd");

            int result = await getClass_DetailData();

            teacherClass_Schedules = await GetAllClass_Schedules(firstDay, lastday);
            
            importantEvents = await GetImportantEvents();

            
            if (App.member.currentFee == null)
            {
                //Debug.Print("Current Fee NULL não devia acontecer!");
                var result1 = await GetCurrentFees(App.member);
            }

            hideActivityIndicator();
            if (App.member.students_count > 0)
            {
				teacherClassesY = (int) (40 * App.screenHeightAdapter);
				classesY = (int) ((teacherClassesY + App.ItemHeight ) + (50 * App.screenHeightAdapter));
				eventsY = (int) ((classesY + App.ItemHeight) + (50 * App.screenHeightAdapter));
				eventsHeight = (int)(App.ItemHeight  + 10);
				feesOrQuoteY = (int)((eventsY + eventsHeight) + (50 * App.screenHeightAdapter));
			}
			else if (App.member.students_count == 0)
			{
				classesY = (int) (40 * App.screenHeightAdapter);
				eventsY = (int) ((classesY + App.ItemHeight) + (50 * App.screenHeightAdapter));
				//eventsHeight = (int)(2 * (App.ItemHeight  + 10));
                eventsHeight = (int) (App.ItemHeight + 10);
                personalClassesY = (int)((eventsY + eventsHeight) + (50 * App.screenHeightAdapter));
                feesOrQuoteY = (int) ((personalClassesY + 150) + (50 * App.screenHeightAdapter));
			}


			_ = await createImportantClasses();

			_ = await createImportantEvents();

			Debug.Print("App.member.students_count = " + App.member.students_count);
			if (App.member.students_count > 0)
			{
				_ = await createImportantTeacherClasses();
			}

            if (App.member.students_count == 0)
            {
                createPersonalClasses();
            }

            _ = await createCurrentFee();

			//createVersion();

			//hideActivityIndicator();
		}

		public async void createPersonalClasses()
		{
			Label personalClassesLabel = new Label
			{
				Text = "SABIAS QUE AGORA JÁ PODES MARCAR AULAS INDIVIDUAIS COM OS TEUS TREINADORES FAVORITOS!",
				TextColor = Color.FromRgb(96, 182, 89),
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = App.titleFontSize,
                FontFamily = "futuracondensedmedium",
            };

            absoluteLayout.Add(personalClassesLabel);
            absoluteLayout.SetLayoutBounds(personalClassesLabel, new Rect(0, personalClassesY, App.screenWidth, 60 * App.screenHeightAdapter));

            personalClassesButton = new RoundButton("SABER MAIS!", App.screenWidth, 50 * App.screenHeightAdapter);
			personalClassesButton.button.BackgroundColor = App.topColor;
            personalClassesButton.button.Clicked += OnPersonalClassesButtonClicked;

            absoluteLayout.Add(personalClassesButton);
            absoluteLayout.SetLayoutBounds(personalClassesButton, new Rect(0, personalClassesY + (60 * App.screenWidthAdapter), App.screenWidth, 60 * App.screenHeightAdapter));
            
        }
            

        public async Task<int> createImportantTeacherClasses()
		{
			CompleteTeacherClass_Schedules();

			//AULAS LABEL
			teacherClassesLabel = new Label
			{
				Text = "PRÓXIMAS AULAS COMO INSTRUTOR/MONITOR",
				TextColor = Color.FromRgb(246, 220, 178),
				HorizontalTextAlignment = TextAlignment.Start,
				FontSize = App.titleFontSize,
                FontFamily = "futuracondensedmedium",
            };

            absoluteLayout.Add(teacherClassesLabel);
            absoluteLayout.SetLayoutBounds(teacherClassesLabel, new Rect(0, teacherClassesY, App.screenWidth, 30 * App.screenHeightAdapter));

			CreateTeacherClassesColletion();
			hideActivityIndicator();
			return 1;
		}

		public void CompleteTeacherClass_Schedules()
		{
			foreach (Class_Schedule class_schedule in teacherClass_Schedules)
			{
				DateTime class_schedule_date = DateTime.Parse(class_schedule.date).Date;

				class_schedule.datestring = Constants.daysofWeekPT[class_schedule_date.DayOfWeek.ToString()] + " - "
					+ class_schedule_date.Day + " "
					+ Constants.months[class_schedule_date.Month] + "\n"
					+ class_schedule.begintime + " às " + class_schedule.endtime;

				if (class_schedule.imagesource == null)
				{
					class_schedule.imagesourceObject = "logo_aksl.png";
				}
				else
				{
					class_schedule.imagesourceObject = new UriImageSource
					{
						Uri = new Uri(Constants.images_URL + class_schedule.classid + "_imagem_c"),
						CachingEnabled = true,
						CacheValidity = new TimeSpan(5, 0, 0, 0)
					};
				}
			}
		}


		public void CreateTeacherClassesColletion()
		{
			//COLLECTION TEACHER CLASSES
			teacherClassesCollectionView = new CollectionView
			{
				SelectionMode = SelectionMode.Single,
				ItemsSource = teacherClass_Schedules,
				ItemsLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Horizontal) { VerticalItemSpacing = 5 * App.screenHeightAdapter, HorizontalItemSpacing = 5 * App.screenWidthAdapter, },
				EmptyView = new ContentView
				{
					Content = new Microsoft.Maui.Controls.StackLayout
					{
						Children =
							{
								new Label { Text = "Não existem aulas agendadas esta semana.", HorizontalTextAlignment = TextAlignment.Start, TextColor = Colors.White, FontFamily = "futuracondensedmedium", FontSize = App.itemTitleFontSize },
							}
					}
				}
			};

			teacherClassesCollectionView.SelectionChanged += OnTeacherClassesCollectionViewSelectionChanged;

			teacherClassesCollectionView.ItemTemplate = new DataTemplate(() =>
			{

				AbsoluteLayout itemabsoluteLayout = new AbsoluteLayout
				{
					HeightRequest = App.ItemHeight ,
					WidthRequest = App.ItemWidth
				};

                Border itemFrame = new Border
                {
                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = 5 * (float)App.screenHeightAdapter,
                    },
                    Stroke = App.topColor,
                    BackgroundColor = Colors.Transparent,
                    Padding = new Thickness(0, 0, 0, 0),
                    HeightRequest = (App.ItemHeight),
                    VerticalOptions = LayoutOptions.Center,
                };

                Image eventoImage = new Image { Aspect = Aspect.AspectFill, Opacity = 0.5 }; //, HeightRequest = 60, WidthRequest = 60
				eventoImage.SetBinding(Image.SourceProperty, "imagesourceObject");

				itemFrame.Content = eventoImage;

                /*var itemFrame_tap = new TapGestureRecognizer();
				itemFrame_tap.Tapped += (s, e) =>
				{
					Navigation.PushAsync(new EquipamentsPageCS("protecoescintos"));
				};
				itemFrame.GestureRecognizers.Add(itemFrame_tap);*/

                itemabsoluteLayout.Add(itemFrame);
                itemabsoluteLayout.SetLayoutBounds(itemFrame, new Rect(0, 0, App.ItemWidth, App.ItemHeight));

				Label dateLabel = new Label { VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, FontSize = App.itemTextFontSize, TextColor = Colors.White, FontFamily = "futuracondensedmedium", LineBreakMode = LineBreakMode.WordWrap };
				dateLabel.SetBinding(Label.TextProperty, "datestring");

                itemabsoluteLayout.Add(dateLabel);
                itemabsoluteLayout.SetLayoutBounds(dateLabel, new Rect(25 * App.screenWidthAdapter, App.ItemHeight - (45 * App.screenHeightAdapter), App.ItemWidth - (50 * App.screenWidthAdapter), 40 * App.screenHeightAdapter));

				Label nameLabel = new Label { BackgroundColor = Colors.Transparent, VerticalTextAlignment = TextAlignment.Start, HorizontalTextAlignment = TextAlignment.Center, FontSize = App.itemTitleFontSize, TextColor = Colors.White, FontFamily = "futuracondensedmedium", LineBreakMode = LineBreakMode.WordWrap };
				nameLabel.SetBinding(Label.TextProperty, "name");


                itemabsoluteLayout.Add(nameLabel);
                itemabsoluteLayout.SetLayoutBounds(nameLabel, new Rect(3 * App.screenWidthAdapter, 25 * App.screenHeightAdapter, App.ItemWidth - (6 * App.screenWidthAdapter), 50 * App.screenHeightAdapter));

				Image participationImagem = new Image { Aspect = Aspect.AspectFill }; //, HeightRequest = 60, WidthRequest = 60
				participationImagem.SetBinding(Image.SourceProperty, "participationimage");


                itemabsoluteLayout.Add(participationImagem);
                itemabsoluteLayout.SetLayoutBounds(participationImagem, new Rect((App.ItemWidth - 25 * App.screenWidthAdapter), 5 * App.screenWidthAdapter, 20 * App.screenWidthAdapter, 20 * App.screenWidthAdapter));

				return itemabsoluteLayout;
			});

			//teacherClassesCollectionView.ScrollTo(5);
            
            absoluteLayout.Add(teacherClassesCollectionView);
            absoluteLayout.SetLayoutBounds(teacherClassesCollectionView, new Rect(0, teacherClassesY + (30 * App.screenHeightAdapter), App.screenWidth, App.ItemHeight + (10 * App.screenHeightAdapter)));
		}

		public async Task<int> createImportantClasses()
		{
			//AULAS LABEL
			attendanceLabel = new Label
			{
				Text = "PRÓXIMAS AULAS COMO ALUNO(A)",
				TextColor = Color.FromRgb(246, 220, 178),
				HorizontalTextAlignment = TextAlignment.Start,
                FontSize = App.titleFontSize,
                FontFamily = "futuracondensedmedium",
            };
			absoluteLayout.Add(attendanceLabel);
			absoluteLayout.SetLayoutBounds(attendanceLabel, new Rect(0, classesY, App.screenWidth, 30 * App.screenHeightAdapter));

			scheduleCollection = new ScheduleCollection();
			scheduleCollection.Items = importantClass_Schedule;
			createClassesCollection();
			hideActivityIndicator();
			return 1;
		}

		public async Task<int> getClass_DetailData()
		{
			DateTime currentTime = DateTime.Now.Date;
			DateTime firstDayWeek = currentTime.AddDays(-Constants.daysofWeekInt[currentTime.DayOfWeek.ToString()]);

			importantClass_Schedule = await GetStudentClass_Schedules(currentTime.ToString("yyyy-MM-dd"), currentTime.AddDays(7).ToString("yyyy-MM-dd"));//  new List<Class_Schedule>();
			cleanClass_Schedule = new ObservableCollection<Class_Schedule>();

			CompleteClass_Schedules();

			return 1;
		}


		public void CompleteClass_Schedules()
		{
			foreach (Class_Schedule class_schedule in importantClass_Schedule)
			{
				/*if (class_schedule.classattendancestatus == "confirmada")
				{
					class_schedule.participationimage = "iconcheck.png";
				}*/
				DateTime class_schedule_date = DateTime.Parse(class_schedule.date).Date;

				class_schedule.datestring = Constants.daysofWeekPT[class_schedule_date.DayOfWeek.ToString()] + " - "
					+ class_schedule_date.Day + " "
					+ Constants.months[class_schedule_date.Month] + "\n"
					+ class_schedule.begintime + " às " + class_schedule.endtime;
				if (class_schedule.imagesource == null)
				{
					class_schedule.imagesourceObject = "logo_aksl.png";
				}
				else
				{
					class_schedule.imagesourceObject = new UriImageSource
					{
							Uri = new Uri(Constants.images_URL + class_schedule.classid + "_imagem_c"),
							CachingEnabled = true,
							CacheValidity = new TimeSpan(5, 0, 0, 0)
					};
				}
				

				if ((class_schedule.classattendancestatus == "confirmada") | (class_schedule.classattendancestatus == "fechada"))
				{
					class_schedule.participationimage = "iconcheck.png";
				}
				else
				{
					class_schedule.participationimage = "iconinativo.png";
				}

			}

		}

		public void createClassesCollection()
		{
			importantClassesCollectionView = new CollectionView
			{
				SelectionMode = SelectionMode.Multiple,
				//ItemsSource = importantClass_Schedule,
				ItemsLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Horizontal) { VerticalItemSpacing = 5 * App.screenHeightAdapter, HorizontalItemSpacing = 5 * App.screenWidthAdapter, },
				EmptyView = new ContentView
				{
					Content = new Microsoft.Maui.Controls.StackLayout
					{
						Children =
							{
								new Label { Text = "Não existem aulas agendadas esta semana.", HorizontalTextAlignment = TextAlignment.Start, TextColor = Colors.White, FontFamily = "futuracondensedmedium",FontSize = App.itemTitleFontSize },
							}
					}
				}
			};
			this.BindingContext = scheduleCollection;
			importantClassesCollectionView.SetBinding(ItemsView.ItemsSourceProperty, "Items");


			importantClassesCollectionView.SelectionChanged += OnClassScheduleCollectionViewSelectionChanged;

			importantClassesCollectionView.ItemTemplate = new DataTemplate(() =>
			{
				AbsoluteLayout itemabsoluteLayout = new AbsoluteLayout
				{
					HeightRequest = App.ItemHeight,
					WidthRequest = App.ItemWidth,
				};

                Border itemFrame = new Border
                {
                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = 5 * (float)App.screenHeightAdapter,
                    },
                    Stroke = App.topColor,
                    BackgroundColor = Colors.Transparent,
                    Padding = new Thickness(0, 0, 0, 0),
                    HeightRequest = (App.ItemHeight),
                    VerticalOptions = LayoutOptions.Center,
                };

                Image eventoImage = new Image {
					Aspect = Aspect.AspectFill,
					Opacity = 0.5,
				};
				eventoImage.SetBinding(Image.SourceProperty, "imagesourceObject");

				itemFrame.Content = eventoImage;

				itemabsoluteLayout.Add(itemFrame);
				itemabsoluteLayout.SetLayoutBounds(itemFrame, new Rect(0, 0, App.ItemWidth, App.ItemHeight));

				Label nameLabel = new Label { BackgroundColor = Colors.Transparent, VerticalTextAlignment = TextAlignment.Start, HorizontalTextAlignment = TextAlignment.Center, FontSize = App.itemTitleFontSize, TextColor = Colors.White, FontFamily = "futuracondensedmedium", LineBreakMode = LineBreakMode.WordWrap };
				nameLabel.SetBinding(Label.TextProperty, "name");

                itemabsoluteLayout.Add(nameLabel);
                itemabsoluteLayout.SetLayoutBounds(nameLabel, new Rect(3 * App.screenWidthAdapter, 25 * App.screenHeightAdapter, App.ItemWidth - (6 * App.screenWidthAdapter), 50 * App.screenHeightAdapter));

                Label dateLabel = new Label { VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, FontSize = App.itemTextFontSize, TextColor = Colors.White, FontFamily = "futuracondensedmedium", LineBreakMode = LineBreakMode.WordWrap };
                dateLabel.SetBinding(Label.TextProperty, "datestring");

                itemabsoluteLayout.Add(dateLabel);
                itemabsoluteLayout.SetLayoutBounds(dateLabel, new Rect(25 * App.screenWidthAdapter, App.ItemHeight - (45 * App.screenHeightAdapter), App.ItemWidth - (50 * App.screenWidthAdapter), 40 * App.screenHeightAdapter));


                Image participationImagem = new Image { Aspect = Aspect.AspectFill }; //, HeightRequest = 60, WidthRequest = 60
                participationImagem.SetBinding(Image.SourceProperty, "participationimage");

                itemabsoluteLayout.Add(participationImagem);
                itemabsoluteLayout.SetLayoutBounds(participationImagem, new Rect((App.ItemWidth - 25 * App.screenWidthAdapter), 5 * App.screenWidthAdapter, 20 * App.screenWidthAdapter, 20 * App.screenWidthAdapter));

				return itemabsoluteLayout;
			});

            absoluteLayout.Add(importantClassesCollectionView);
            absoluteLayout.SetLayoutBounds(importantClassesCollectionView, new Rect(0, classesY + (30 * App.screenHeightAdapter), App.screenWidth, App.ItemHeight + (10 * App.screenHeightAdapter)));
		}

		public async Task<int> createImportantEvents()
		{
			foreach (Event event_i in importantEvents)
			{
				if ((event_i.imagemNome == "") | (event_i.imagemNome is null))
				{
					event_i.imagemSource = "logo_aksl.png";
				}
				else
				{
					event_i.imagemSource = Constants.images_URL + event_i.id + "_imagem_c";

				}

				if ((event_i.participationconfirmed == "inscrito") | (event_i.participationconfirmed == "confirmado"))
				{
					event_i.participationimage = "iconcheck.png";
				}
                else if (event_i.participationconfirmed == "cancelado")
                {
                    event_i.participationimage = "iconinativo.png";
                }
            }

			//AULAS LABEL
			eventsLabel = new Label
			{
				Text = "PRÓXIMOS EVENTOS",
				TextColor = Color.FromRgb(246, 220, 178),
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalTextAlignment = TextAlignment.End,
				FontSize = App.titleFontSize,
                FontFamily = "futuracondensedmedium",
            };

            absoluteLayout.Add(eventsLabel);
            absoluteLayout.SetLayoutBounds(eventsLabel, new Rect(0, eventsY, App.screenWidth, 30 * App.screenHeightAdapter));

			CreateProximosEventosColletion();
			hideActivityIndicator();
			return 1;
		}

		public void CreateProximosEventosColletion()
		{
			int gridLines = 1; //estava 2


            //COLLECTION EVENTOS IMPORTANTES
            importantEventsCollectionView = new CollectionView
			{
				SelectionMode = SelectionMode.Single,
				ItemsSource = importantEvents,
				ItemsLayout = new GridItemsLayout(gridLines, ItemsLayoutOrientation.Horizontal) { VerticalItemSpacing = 5 * App.screenHeightAdapter, HorizontalItemSpacing = 5 * App.screenWidthAdapter, },
				EmptyView = new ContentView
				{
					Content = new Microsoft.Maui.Controls.StackLayout
					{
						Children =
							{
								new Label { Text = "Não existem Eventos agendados.", HorizontalTextAlignment = TextAlignment.Start, TextColor = Colors.White, FontFamily = "futuracondensedmedium", FontSize = App.itemTitleFontSize },
							}
					}
				}
			};

			importantEventsCollectionView.SelectionChanged += OnProximosEventosCollectionViewSelectionChanged;

			importantEventsCollectionView.ItemTemplate = new DataTemplate(() =>
			{

                /*Image eventoImage = new Image { Aspect = Aspect.AspectFill, Opacity = 0.25 }; //, HeightRequest = 60, WidthRequest = 60
                eventoImage.SetBinding(Image.SourceProperty, "imagemSource");

                Label nameLabel = new Label { BackgroundColor = Colors.Transparent, VerticalTextAlignment = TextAlignment.Start, HorizontalTextAlignment = TextAlignment.Center, FontSize = App.itemTitleFontSize, TextColor = Colors.White, FontFamily = "futuracondensedmedium", LineBreakMode = LineBreakMode.WordWrap };
                nameLabel.SetBinding(Label.TextProperty, "name");

                Label categoryLabel = new Label { VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, FontSize = App.itemTextFontSize, TextColor = Colors.White, FontFamily = "futuracondensedmedium", LineBreakMode = LineBreakMode.WordWrap };
                categoryLabel.SetBinding(Label.TextProperty, "category");


                Microsoft.Maui.Controls.StackLayout itemStackLayout = new Microsoft.Maui.Controls.StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Spacing = 20,

                    Children =
                {
                    nameLabel,
                    categoryLabel
                }
                };

				return itemStackLayout;*/
                

				AbsoluteLayout itemabsoluteLayout = new AbsoluteLayout
				{
					HeightRequest = App.ItemHeight,
					WidthRequest = App.ItemWidth
				};

                Border itemFrame = new Border
				{
                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = 5 * (float)App.screenHeightAdapter,
                    },
					Stroke = App.topColor,
                    BackgroundColor = Colors.Transparent,
					Padding = new Thickness(0, 0, 0, 0),
					HeightRequest = (App.ItemHeight),
					VerticalOptions = LayoutOptions.Center,
				};

				Image eventoImage = new Image { Aspect = Aspect.AspectFill, Opacity = 0.25 }; //, HeightRequest = 60, WidthRequest = 60
				eventoImage.SetBinding(Image.SourceProperty, "imagemSource");

				itemFrame.Content = eventoImage;

                itemabsoluteLayout.Add(itemFrame);
                itemabsoluteLayout.SetLayoutBounds(itemFrame, new Rect(0, 0, App.ItemWidth, App.ItemHeight));

                Label nameLabel = new Label { BackgroundColor = Colors.Transparent, VerticalTextAlignment = TextAlignment.Start, HorizontalTextAlignment = TextAlignment.Center, FontSize = App.itemTitleFontSize, TextColor = Colors.White, FontFamily = "futuracondensedmedium", LineBreakMode = LineBreakMode.WordWrap };
                nameLabel.SetBinding(Label.TextProperty, "name");

                itemabsoluteLayout.Add(nameLabel);
                itemabsoluteLayout.SetLayoutBounds(nameLabel, new Rect(3 * App.screenWidthAdapter, 25 * App.screenHeightAdapter, App.ItemWidth - (6 * App.screenWidthAdapter), 50 * App.screenHeightAdapter));

                Label categoryLabel = new Label { VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, FontSize = App.itemTextFontSize, TextColor = Colors.White, FontFamily = "futuracondensedmedium", LineBreakMode = LineBreakMode.WordWrap };
                categoryLabel.SetBinding(Label.TextProperty, "category");

                itemabsoluteLayout.Add(categoryLabel);
                itemabsoluteLayout.SetLayoutBounds(categoryLabel, new Rect(3 * App.screenWidthAdapter, ((App.ItemHeight - (15 * App.screenHeightAdapter)) / 2), App.ItemWidth - (6 * App.screenWidthAdapter), (App.ItemHeight - (15 * App.screenHeightAdapter)) / 4));

                Label dateLabel = new Label { VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, FontSize = App.itemTextFontSize, TextColor = Colors.White, FontFamily = "futuracondensedmedium", LineBreakMode = LineBreakMode.WordWrap };
                dateLabel.SetBinding(Label.TextProperty, "detailed_date");

                itemabsoluteLayout.Add(dateLabel);
                itemabsoluteLayout.SetLayoutBounds(dateLabel, new Rect(3 * App.screenWidthAdapter, (App.ItemHeight - 15) - ((App.ItemHeight - 15) / 4), App.ItemWidth, (App.ItemHeight - (15 * App.screenHeightAdapter)) / 4));

				Image participationImagem = new Image { Aspect = Aspect.AspectFill }; //, HeightRequest = 60, WidthRequest = 60
				participationImagem.SetBinding(Image.SourceProperty, "participationimage");

                itemabsoluteLayout.Add(participationImagem);
                itemabsoluteLayout.SetLayoutBounds(participationImagem, new Rect((App.ItemWidth - 25 * App.screenWidthAdapter), 5 * App.screenWidthAdapter, 20 * App.screenWidthAdapter, 20 * App.screenWidthAdapter));


                return itemabsoluteLayout;
            });

            absoluteLayout.Add(importantEventsCollectionView);
            absoluteLayout.SetLayoutBounds(importantEventsCollectionView, new Rect(0, eventsY + (35 * App.screenHeightAdapter), App.screenWidth, eventsHeight));
		}




		public void createFamousQuote()
		{
			Random random = new Random();
			int random_number = random.Next(Constants.famousQuotes.Count);

			famousQuoteLabel = new Label
			{
				Text = Constants.famousQuotes[random_number],
				TextColor = Colors.White,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = App.titleFontSize,
				FontAttributes = FontAttributes.Italic,
                FontFamily = "futuracondensedmedium",
            };

			absoluteLayout.Add(famousQuoteLabel);
            absoluteLayout.SetLayoutBounds(famousQuoteLabel, new Rect(0, feesOrQuoteY, App.screenWidth, 90 * App.screenHeightAdapter));
		}

		public async Task<int> createCurrentFee()
		{
			bool hasQuotaPayed = false;

			if (App.member.currentFee != null)
			{
				if ((App.member.currentFee.estado == "fechado") | (App.member.currentFee.estado == "recebido") | (App.member.currentFee.estado == "confirmado"))
				{
					hasQuotaPayed = true;
					createFamousQuote();
					hideActivityIndicator();
					return 1;
				}
			}

			if (hasQuotaPayed == false)
            {

                bool answer = await DisplayAlert("A TUA QUOTA NÃO ESTÁ ATIVA.", "A tua quota para este ano não está ativa. Queres efetuar o pagamento?", "Sim", "Não");
                Debug.WriteLine("Answer: " + answer);
				if (answer == true)
				{
                    await Navigation.PushAsync(new QuotasPageCS());
                }

                currentFeeLabel = new Label
				{
					Text = "A TUA QUOTA PARA ESTE ANO NÃO ESTÁ ATIVA. \n DESTA FORMA NÃO PODERÁS PARTICIPAR NOS NOSSOS EVENTOS :(. \n ATIVA AQUI A TUA QUOTA.",
					TextColor = Colors.Red,
					HorizontalTextAlignment = TextAlignment.Center,
					FontSize = App.itemTextFontSize,
                    FontFamily = "futuracondensedmedium",
                };

				var currentFeeLabel_tap = new TapGestureRecognizer();
				currentFeeLabel_tap.Tapped += async (s, e) =>
				{
					await Navigation.PushAsync(new QuotasPageCS());
				};
				currentFeeLabel.GestureRecognizers.Add(currentFeeLabel_tap);


				absoluteLayout.Add(currentFeeLabel);
                absoluteLayout.SetLayoutBounds(currentFeeLabel, new Rect(0, feesOrQuoteY, App.screenWidth, 50 * App.screenHeightAdapter));
			}
			hideActivityIndicator();
			return 1;
		}

        public async void createDelayedMonthFee()
        {


            bool answer = await DisplayAlert("A TUA QUOTA NÃO ESTÁ ATIVA.", "A tua quota para este ano não está ativa. Queres efetuar o pagamento?", "Sim", "Não");
            Debug.WriteLine("Answer: " + answer);
                
        }

        public async void createVersion()
		{
			currentVersionLabel = new Label
			{
				Text = "Version 1.2(23)",
				TextColor = Colors.White,
				HorizontalTextAlignment = TextAlignment.End,
				FontSize = 10
			};

			absoluteLayout.Add(currentVersionLabel);
			absoluteLayout.SetLayoutBounds(currentVersionLabel, new Rect(0, feesOrQuoteY + 90 * App.screenHeightAdapter, App.screenWidth, 30 * App.screenHeightAdapter));
		}
		

		public MainPageCS ()
		{

			this.initLayout();
			//this.initSpecificLayout(App.members);

		}

		void OnSendClick(object sender, EventArgs e)
		{
			/*notificationNumber++;
			string title = $"Local Notification #{notificationNumber}";
			string $"You have now received {notificationNumber} notifications!";
			notificationManager.SendNotification(title, message);*/
		}

		void OnScheduleClick(object sender, EventArgs e)
		{
			/*notificationNumber++;
			string title = $"Local Notification #{notificationNumber}";
			string $"You have now received {notificationNumber} notifications!";
			notificationManager.SendNotification(title, message, DateTime.Now.AddSeconds(10));*/
		}

		void ShowNotification(string title, string message)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				msg.Text = $"Notification Received:\nTitle: {title}\nMessage: {message}";
			});
		}

		async void OnPerfilButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProfileCS());
		}

		async Task<ObservableCollection<Class_Schedule>> GetStudentClass_Schedules(string begindate, string enddate)
		{
			Debug.WriteLine("GetStudentClass_Schedules");
			ClassManager classManager = new ClassManager();
			ObservableCollection<Class_Schedule> class_schedules_i = await classManager.GetStudentClass_Schedules_obs(App.member.id, begindate, enddate);
			if (class_schedules_i == null)
			{
				Application.Current.MainPage = new NavigationPage(new LoginPageCS("Verifique a sua ligação à Internet e tente novamente."))
				{
					BarBackgroundColor = Color.FromRgb(15, 15, 15),
					BarTextColor = Colors.White
				};
				return null;
			}
			return class_schedules_i;
		}

		async Task<List<Event>> GetImportantEvents()
		{
			Debug.WriteLine("GetImportantEvents");
			EventManager eventManager = new EventManager();
			List<Event> events = await eventManager.GetImportantEvents(App.member.id);
			if (events == null)
			{
				Application.Current.MainPage = new NavigationPage(new LoginPageCS("Verifique a sua ligação à Internet e tente novamente."))
				{
					BarBackgroundColor = Color.FromRgb(15, 15, 15),
					BarTextColor = Colors.White
				};
				return null;
			}
			return events;
		}

		async Task<int> GetCurrentFees(Member member)
		{
			Debug.WriteLine("MainTabbedPageCS.GetCurrentFees");
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

		async Task<List<Class_Schedule>> GetAllClass_Schedules(string begindate, string enddate)
		{
			ClassManager classManager = new ClassManager();
			List<Class_Schedule> class_schedules_i = await classManager.GetAllClass_Schedules(App.member.id, begindate, enddate);

			if (class_schedules_i == null)
			{
				Application.Current.MainPage = new NavigationPage(new LoginPageCS("Verifique a sua ligação à Internet e tente novamente."))
				{
					BarBackgroundColor = Color.FromRgb(15, 15, 15),
					BarTextColor = Colors.White
				};
				return null;
			}
			return class_schedules_i;
		}

		async void OnClassScheduleCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			showActivityIndicator();
			Debug.WriteLine("MainPageCS.OnClassScheduleCollectionViewSelectionChanged");

            if ((sender as CollectionView).SelectedItems.Count != 0)
			{
                Debug.WriteLine("MainPageCS.OnClassScheduleCollectionViewSelectionChanged 1");
                ClassManager classmanager = new ClassManager();

				Class_Schedule class_schedule = (sender as CollectionView).SelectedItems[0] as Class_Schedule;
				if (class_schedule.classattendanceid == null)
				{
                    Task.Run(async () =>
                    {
                        string class_attendance_id = await classmanager.CreateClass_Attendance(App.member.id, class_schedule.classid, "confirmada", class_schedule.date);
                        class_schedule.classattendanceid = class_attendance_id;
                        return true;
                    });
                    //string class_attendance_id = await classmanager.CreateClass_Attendance(App.member.id, class_schedule.classid, "confirmada", class_schedule.date);
                    /*                    string class_attendance_id =  classmanager.CreateClass_Attendance_sync(App.member.id, class_schedule.classid, "confirmada", class_schedule.date);
                                        */
                    class_schedule.classattendancestatus = "confirmada";
                    class_schedule.participationimage = "iconcheck.png";


                }
                else
				{
                    Debug.WriteLine("MainPageCS.OnClassScheduleCollectionViewSelectionChanged 2");
                    if (class_schedule.classattendancestatus == "anulada")
					{
						class_schedule.classattendancestatus = "confirmada";
						class_schedule.participationimage = "iconcheck.png";
						int result = await classmanager.UpdateClass_Attendance(class_schedule.classattendanceid, class_schedule.classattendancestatus);
					}
					else if (class_schedule.classattendancestatus == "confirmada")
					{
						class_schedule.classattendancestatus = "anulada";
						class_schedule.participationimage = "iconinativo.png";
						int result = await classmanager.UpdateClass_Attendance(class_schedule.classattendanceid, class_schedule.classattendancestatus);
					}
					else if (class_schedule.classattendancestatus == "fechada")
					{
						await DisplayAlert("PRESENÇA EM AULA", "A tua presença nesta aula já foi validada pelo instrutor pelo que não é possível alterar o seu estado.", "Ok" );
					}
					//int result = await classmanager.UpdateClass_Attendance(class_schedule.classattendanceid, class_schedule.classattendancestatus);
				}
                Debug.WriteLine("MainPageCS.OnClassScheduleCollectionViewSelectionChanged 3");
                ((CollectionView)sender).SelectedItems.Clear();
                Debug.WriteLine("MainPageCS.OnClassScheduleCollectionViewSelectionChanged 4");
                /*importantClassesCollectionView.ItemsSource = cleanClass_Schedule;
				importantClassesCollectionView.ItemsSource = importantClass_Schedule;*/

                hideActivityIndicator();
                Debug.WriteLine("MainPageCS.OnClassScheduleCollectionViewSelectionChanged 5");
            }
		}

		async void OnProximosEventosCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Debug.WriteLine("OnCollectionViewProximosEstagiosSelectionChanged " + (sender as CollectionView).SelectedItem.GetType().ToString());



            if ((sender as CollectionView).SelectedItem != null)
			{
				Event event_v = (sender as CollectionView).SelectedItem as Event;

				if (event_v.type == "estagio")
				{
					await Navigation.PushAsync(new DetailEventPageCS(event_v));
				}
				else if (event_v.type == "competicao")
				{

					if (event_v.participationid == null)
					{
                        await Navigation.PushAsync(new DetailCompetitionPageCS(event_v.id, event_v.name));
                    }
					else
					{
                        await Navigation.PushAsync(new DetailCompetitionPageCS(event_v.id, event_v.name, event_v.participationid));
                    }
					
				}
				else if (event_v.type == "sessaoexame")
				{
					await Navigation.PushAsync(new ExaminationSessionPageCS(event_v.id));
				}

			}
		}

		async void OnTeacherClassesCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Debug.WriteLine("MainPageCS.OnClassAttendanceCollectionViewSelectionChanged");

			if ((sender as CollectionView).SelectedItem != null)
			{
				Class_Schedule class_schedule = (sender as CollectionView).SelectedItem as Class_Schedule;
				(sender as CollectionView).SelectedItem = null;
				await Navigation.PushAsync(new AttendanceClassPageCS(class_schedule));

			}
		}

        async void OnPersonalClassesButtonClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("MainPageCS.OnPersonalClassesButtonClicked");
            await Navigation.PushAsync(new PersonalInfoPageCS());
        }

        async void OnTechnicalButtonClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("MainPageCS.OnTechnicalButtonClicked");
            await Navigation.PushAsync(new AttendanteEvaluationPageCS("6c36406a-7ab5-fcb0-b186-64d243523b11"));
        }
    }
}
