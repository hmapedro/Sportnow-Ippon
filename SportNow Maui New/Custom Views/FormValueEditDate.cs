//using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Behaviors;
using Microsoft.Maui.Controls.Shapes;
//using Syncfusion.Maui.Inputs;
using static System.Net.Mime.MediaTypeNames;

namespace SportNow.CustomViews
{
    /*public class FormValueEdit : Frame
    {

        public Entry entry;
        //public string Text {get; set; }

        public FormValueEdit(string text)
        {

            //this.BackgroundColor = Color.FromRgb(25, 25, 25);
            this.BackgroundColor = Color.FromRgb(25, 25, 25);
            this.BorderColor = Colors.LightGray;

            this.CornerRadius = 10;
            this.IsClippedToBounds = true;
            this.Padding = new Thickness(10, 2, 10, 2);
            //this.WidthRequest = width;
            this.HeightRequest = 30;

            //USERNAME ENTRY
            entry = new Entry
            {
                //Text = "tete@hotmail.com",
                Text = text,
                TextColor = Colors.White,
                BackgroundColor = App.backgroundColor,
                //Placeholder = placeholder,
                HorizontalOptions = LayoutOptions.Start,
                //WidthRequest = width,
                FontSize = 18,
            };
            this.Content = entry;

        }
    }*/

public class FormValueEditDate : Border
     {

         public Entry entry;
         //public string Text {get; set; }

         public FormValueEditDate(string Text) {

            StrokeShape = new RoundRectangle
            {
                CornerRadius = 5 * (float)App.screenHeightAdapter,
            };
            Stroke = App.topColor;
            BackgroundColor = Colors.Transparent;
            this.Padding = new Thickness(1, 2, 2, 2);
            //this.MinimumHeightRequest = 50;
            this.HeightRequest = 45 * App.entryHeightAdapter;
            this.VerticalOptions = LayoutOptions.Center;

            entry = new Entry
            {
                //Padding = new Thickness(5,0,5,0),
                Placeholder = "AAAA-MM-DD",
                Keyboard = Keyboard.Numeric,
                //Mask = "XXXX-XX-XX",
                //MaskType = MaskedEntryMaskType.Simple,
                Text = Text,
                HorizontalTextAlignment = TextAlignment.Start,
                TextColor = App.normalTextColor,
                BackgroundColor = App.backgroundColor,
                FontSize = App.formValueFontSize,
                //HeightRequest = 30
                FontFamily = "futuracondensedmedium",
            };

            var behavior = new MaskedBehavior
            {
                UnmaskedCharacter = 'X',
                Mask = "XXXX-XX-XX"
            };
#if ANDROID
            entry.TextChanged += OnTextChanged;
#endif
            entry.Behaviors.Add(behavior);
            this.Content = entry;


        }
#if ANDROID
        protected void OnTextChanged(object sender, EventArgs e)
        {
           (sender as Entry).CursorPosition = (sender as Entry).Text.Length;
        }
#endif
    }
}
