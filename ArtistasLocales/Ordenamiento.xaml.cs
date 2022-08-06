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
    public partial class Ordenamiento : PopupPage
    {
        public Ordenamiento()
        {
            InitializeComponent();
            ByDefault();
        }

        private async void ByDefault()
        {
            await Task.Delay(20);
            OrderNombre.IsVisible = true;
            await Task.Delay(20);
            OrderEdad.IsVisible = true;
            await Task.Delay(20);
            OrderManifestacion.IsVisible = true;
            await Task.Delay(20);
            OrderOrganizaciones.IsVisible = true;
            await Task.Delay(20);
            OrderProfecion.IsVisible = true;
            
        }

        private async void OrderOrganizaciones_Clicked(object sender, EventArgs e)
        {
            await App.Database.SaveUpOpcionesOrdenar(new OpcionesOrdenar() { Id = 1, Tipo = "Organizacion" });
            MessagingCenter.Send<Ordenamiento, string>(this, "Ordenamiento", "Organizacion");
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OrderManifestacion_Clicked(object sender, EventArgs e)
        {
            await App.Database.SaveUpOpcionesOrdenar(new OpcionesOrdenar() { Id = 1, Tipo = "Manifestacion" });
            MessagingCenter.Send<Ordenamiento, string>(this, "Ordenamiento", "Manifestacion");
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OrderEdad_Clicked(object sender, EventArgs e)
        {
            await App.Database.SaveUpOpcionesOrdenar(new OpcionesOrdenar() { Id = 1, Tipo = "Edad" });
            MessagingCenter.Send<Ordenamiento, string>(this, "Ordenamiento", "Edad");
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OrderNombre_Clicked(object sender, EventArgs e)
        {
            await App.Database.SaveUpOpcionesOrdenar(new OpcionesOrdenar() { Id = 1, Tipo = "Nombre"});
            MessagingCenter.Send<Ordenamiento,string>(this, "Ordenamiento","Nombre");
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OrderProfecion_Clicked(object sender, EventArgs e)
        {
            await App.Database.SaveUpOpcionesOrdenar(new OpcionesOrdenar() { Id = 1, Tipo = "Profecion" });
            MessagingCenter.Send<Ordenamiento, string>(this, "Ordenamiento", "Profecion");
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}