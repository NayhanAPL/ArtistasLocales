using Plugin.Messaging;
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
    public partial class LlamadaMovFijo : PopupPage
    {
        public LlamadaMovFijo()
        {
            InitializeComponent();
        }

        private async void Fijo_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MainPage.ArtSelected.Fijo))
            {
                var call = CrossMessaging.Current.PhoneDialer;
                if (call.CanMakePhoneCall)
                { call.MakePhoneCall(MainPage.ArtSelected.Fijo); }
            }
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void Movil_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MainPage.ArtSelected.Movil))
            {
                var call = CrossMessaging.Current.PhoneDialer;
                if (call.CanMakePhoneCall)
                { call.MakePhoneCall(MainPage.ArtSelected.Movil); }
            }
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}