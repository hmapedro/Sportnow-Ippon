using System;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Maui.Controls.Shapes;

namespace SportNow.CustomViews
{
    public class FormValue : Border
    {

        public Label label;
        //public string Text {get; set; }

        public FormValue(string text, double height)
        {
            createFormValue(text, height);
        }

        public FormValue(string text)
        {
            createFormValue(text, 45 * App.entryHeightAdapter);
        }

        public void createFormValue(string text, double height)
        {
            StrokeShape = new RoundRectangle
            {
                CornerRadius = 5 * (float)App.screenHeightAdapter,
            };
            Stroke = App.topColor;
            BackgroundColor = Colors.Transparent;
            Padding = new Thickness(2, 2, 2, 2);
            //this.MinimumHeightRequest = 50;
            this.HeightRequest = height;
            this.VerticalOptions = LayoutOptions.Center;

            label = new Label
            {
                Padding = new Thickness(5, 0, 5, 0),
                Text = text,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Colors.White,
                BackgroundColor = App.backgroundColor,
                FontSize = App.formValueFontSize,
                FontFamily = "futuracondensedmedium",
            };

            this.Content = label; // relativeLayout_Button;
        }
    }
}
