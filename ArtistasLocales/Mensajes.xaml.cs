using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArtistasLocales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mensajes : PopupPage
    {
        public Mensajes()
        {
            InitializeComponent();
            ByDefault();
        }
        public static string TextSubAlert = "";
        private async void ByDefault()
        {
            textSubAlert.Text = TextSubAlert;
            await Task.Delay(3000);
            if (!salio)
            {
                await PopupNavigation.Instance.PopAsync(true);
            }
        }
        public static bool salio = false;
        private async void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            salio = true;
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}