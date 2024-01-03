using System.Diagnostics;

namespace SportNow.CustomViews
{
    public class MenuButton: StackLayout
    {
        public Button button;
        BoxView line;

        public MenuButton(string text, double width, double height)
        {
            button = new Button
            {
                Text = text,
                BackgroundColor = Colors.Transparent, //FromRgb(25, 25, 25),
                TextColor = Color.FromRgb(200, 200, 200),
                FontSize = App.menuButtonFontSize,
                WidthRequest = width,
                HeightRequest = 50 * App.screenHeightAdapter,
                FontFamily = "futuracondensedmedium"
            };

            this.Spacing = 0;
            this.Orientation = StackOrientation.Vertical;
            this.MinimumHeightRequest = height * App.screenHeightAdapter;

            line = new BoxView
            {
                Color = Color.FromRgb(246, 220, 178),
                WidthRequest = width,
                HeightRequest = 2 * App.screenHeightAdapter,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            
            this.Add(button);
            this.Add(line);
        }

        public void activate() {

            this.button.TextColor = Color.FromRgb(246, 220, 178);
            this.line.Color = Color.FromRgb(246, 220, 178);
        }

        public void deactivate()
        {

            this.button.TextColor = Color.FromRgb(200, 200, 200);
            this.line.Color = Color.FromRgb(200, 200, 200);
        }
    }
}
