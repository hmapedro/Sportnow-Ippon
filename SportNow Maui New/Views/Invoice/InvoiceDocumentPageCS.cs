
using SportNow.Model;
using Syncfusion.Maui.PdfViewer;
using System.Diagnostics;

namespace SportNow.Views
{
    public class InvoiceDocumentPageCS : DefaultPage
    {

        private Microsoft.Maui.Controls.StackLayout stackLayout;

        private Microsoft.Maui.Controls.Grid gridGrade;

        public List<MainMenuItem> MainMenuItems { get; set; }

        string invoiceid;

        public void initLayout()
        {
            Title = "Fatura";

            var toolbarItem = new Microsoft.Maui.Controls.ToolbarItem
            {
                //Text = "Logout",
                IconImageSource = "iconshare.png",

            };
            toolbarItem.Clicked += OnShareButtonClicked;
            ToolbarItems.Add(toolbarItem);

        }



        /* Unmerged change from project 'NK Sangalhos (net8.0-ios)'
        Before:
                public void initSpecificLayout()
        After:
                public void initSpecificLayoutAsync()
        */

        /* Unmerged change from project 'NK Sangalhos (net8.0-maccatalyst)'
        Before:
                public void initSpecificLayout()
        After:
                public void initSpecificLayoutAsync()
        */
        public async Task initSpecificLayoutAsync()
        {

            gridGrade = new Microsoft.Maui.Controls.Grid { Padding = 0, HorizontalOptions = LayoutOptions.FillAndExpand };
            gridGrade.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            gridGrade.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); //GridLength.Auto 

            var pdfUrl = Constants.RestUrl_Get_Invoice_byID + "?invoiceid=" + this.invoiceid;

            Debug.Print("pdfUrl = "+ pdfUrl);

            SfPdfViewer browser1 = new SfPdfViewer();

            HttpClient httpClient = new HttpClient();

            showActivityIndicator();
            HttpResponseMessage response = await httpClient.GetAsync(pdfUrl);
            Stream PdfDocumentStream = await response.Content.ReadAsStreamAsync();
            hideActivityIndicator();

            browser1.DocumentSource = PdfDocumentStream;
            browser1.WidthRequest = App.screenWidth;
            browser1.HeightRequest = App.screenHeight - 100 * App.screenHeightAdapter;

            gridGrade.Add(browser1, 0, 0);
            absoluteLayout.Add(gridGrade);
            absoluteLayout.SetLayoutBounds(gridGrade, new Rect(0, 0, App.screenWidth, App.screenHeight - 100 * App.screenHeightAdapter));
        }

        public InvoiceDocumentPageCS(Payment payment)
        {
            Debug.Print("payment.invoiceid = " + payment.invoiceid);
            this.invoiceid = payment.invoiceid;
            this.initLayout();
            this.initSpecificLayoutAsync();
            //CreateDiploma(member, examination);


        }

        public InvoiceDocumentPageCS(string invoiceid)
        {
            Debug.Print("invoiceid = " + invoiceid);
            this.invoiceid = invoiceid;
            this.initLayout();
            this.initSpecificLayoutAsync();
            //CreateDiploma(member, examination);


        }


        async void OnShareButtonClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("OnShareButtonClicked");
            await Share.RequestAsync(new ShareTextRequest
            {
                //Uri = "https://plataforma.nksl.org/diploma_1.jpg",
                Uri = Constants.RestUrl_Get_Invoice_byID + "?invoiceid=" + invoiceid,
                Title = "Partilha Fatura"
            });
        }

    }


}

