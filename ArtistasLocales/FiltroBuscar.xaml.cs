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
    public partial class FiltroBuscar : PopupPage
    {
        public FiltroBuscar()
        {
            InitializeComponent();
            ByDefault();
        }

        private async void ByDefault()
        {
            await Task.Delay(5);
            OrderNombre.IsVisible = true;
            await Task.Delay(5);
            OrderManifestacion.IsVisible = true;
            await Task.Delay(5);
            OrderOrganizaciones.IsVisible = true;
            await Task.Delay(5);
            OrderCurriculo.IsVisible = true;
            await Task.Delay(5);
            OrderProfecion.IsVisible = true;
            await Task.Delay(5);
            OrderMovil.IsVisible = true;
            await Task.Delay(5);
            OrderFijo.IsVisible = true;
            await Task.Delay(5);
            OrderCorreo.IsVisible = true;
            await Task.Delay(5);
            OrderWeb.IsVisible = true;
        }

        private async void OrderWeb_Clicked(object sender, EventArgs e)
        {
            Buscar.filtro = "Web";
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OrderCorreo_Clicked(object sender, EventArgs e)
        {
            Buscar.filtro = "Correo";
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OrderFijo_Clicked(object sender, EventArgs e)
        {
            Buscar.filtro = "Fijo";
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OrderMovil_Clicked(object sender, EventArgs e)
        {
            Buscar.filtro = "Movil";
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OrderProfecion_Clicked(object sender, EventArgs e)
        {
            Buscar.filtro = "Profecion";
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OrderCurriculo_Clicked(object sender, EventArgs e)
        {
            Buscar.filtro = "Curriculo";
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OrderOrganizaciones_Clicked(object sender, EventArgs e)
        {
            Buscar.filtro = "Organizacion";
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OrderManifestacion_Clicked(object sender, EventArgs e)
        {
            Buscar.filtro = "Manifestacion";
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OrderNombre_Clicked(object sender, EventArgs e)
        {
            Buscar.filtro = "Nombre";
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}