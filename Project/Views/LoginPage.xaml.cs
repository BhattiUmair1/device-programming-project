using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Loadicons();
        }

        private void Loadicons()
        {
            imgbackground.Source = ImageSource.FromResource("Project.Assets.Background.png");
        }

        private void TapGestureRecognizer_Continue(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entEmail.Text))
            {
                Navigation.PushAsync(new SearchPage(entEmail.Text));
            }
        }
    }
}