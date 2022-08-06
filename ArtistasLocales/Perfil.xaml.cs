using Plugin.Messaging;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArtistasLocales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Perfil : ContentPage
    {
        public Perfil()
        {
            InitializeComponent();
            ByDefault();
        }

        private async void ByDefault()
        {
            string nombre = "";
            foreach (var item in MainPage.ArtSelected.Nombre) { nombre += item;if (item == ' ') break; }
            LabelNombre.Text = nombre;
            LabelCorreo.Text = MainPage.ArtSelected.Correo;
            LabelMovil.Text = MainPage.ArtSelected.Movil;
            LabelWeb.Text = MainPage.ArtSelected.DireccionWeb;
            LabelFijo.Text = MainPage.ArtSelected.Fijo;
            LabelEdad.Text = MainPage.ArtSelected.FechaNacimiento;
            LabelManifestacion.Text = MainPage.ArtSelected.Manifestacion.ToString();
            LabelProfesion.Text = MainPage.ArtSelected.ActividadProfecional; 
            LeerMasCurriculo.IsVisible = false;
            ImagenPerfil.Source = MainPage.ArtSelected.Foto != null ? ImageSource.FromStream(() => new MemoryStream(MainPage.ArtSelected.Foto)) : "perfil.jpeg";
            if (MainPage.ArtSelected.Curriculo != null && MainPage.ArtSelected.Curriculo.Length <= 250)
                LabelCurriculo.Text = MainPage.ArtSelected.Curriculo;
            else
            { 
                LeerMasCurriculo.IsVisible = true;
                LabelCurriculo.Text = MainPage.ArtSelected.Curriculo.Substring(0,250);
            }


            string organizaciones = MainPage.ArtSelected.Organizaciones.Substring(0, MainPage.ArtSelected.Organizaciones.Length -2);
            LabelOrganizaciones.Text = organizaciones;

            LeerMasVinculaciones.IsVisible = false;
            string vinculos = "";
            var vinculaciones = MainPage.listVin.FindAll(x => x.NameArt == MainPage.ArtSelected.Nombre);
            if (vinculaciones.Count > 0)
            {
                vinculos += "\n" + vinculaciones[0].Nombre + " \n" + "Lugar: " + vinculaciones[0].Lugar + "\n" + vinculaciones[0].Descripcion;
                if (vinculaciones.Count > 1) LeerMasVinculaciones.IsVisible = true;
                LabelVinculaciones.Text = vinculos;
            }
            var obj = await App.Database.GetByIdArtFavoritos(MainPage.ArtSelected.Id);
            if (obj.Count > 0) Favorito.TextColor = Color.Gold;
        }

        private async void ButtonLlamar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LlamadaMovFijo());
        }
        public static string tipoMensaje = "";
        private void ButtonMensaje_Clicked(object sender, EventArgs e)
        {
            string text = "";
            var sms = CrossMessaging.Current.SmsMessenger;
            if (sms.CanSendSms)
            { sms.SendSms(MainPage.ArtSelected.Movil, text); }
        }
        private void ButtonCorreo_Clicked(object sender, EventArgs e)
        {
            string email = "";
            string title = "";
            var Email = CrossMessaging.Current.EmailMessenger;
            if (Email.CanSendEmail)
            {
                Email.SendEmail(MainPage.ArtSelected.Correo, title, email);
            }
        }
        private async void ButtonWeb_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Browser.OpenAsync(MainPage.ArtSelected.DireccionWeb, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception)
            {
                await DisplayAlert("Ha ocurrido un error", "La dirección web del perfil no es correcta.","OK");
            }
            
        }
        private async void LeerMasCurriculo_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Currículo", MainPage.ArtSelected.Curriculo, "OK");
        }
        private async void LeerMasVinculaciones_Clicked(object sender, EventArgs e)
        {
            string vinculos = "";
            var vinculaciones = MainPage.listVin.FindAll(x => x.NameArt == MainPage.ArtSelected.Nombre);
            foreach (var item in vinculaciones)
            {
                vinculos += "\n" + item.Nombre + " \n" + "Lugar: " + item.Lugar + "\n" + item.Descripcion + "\n ";
            }
            await DisplayAlert("Vinculaciones", vinculos, "OK");
        }
        private async void Favorito_Clicked(object sender, EventArgs e)
        {
            var existe = await App.Database.GetByIdArtFavoritos(MainPage.ArtSelected.Id);
            if (existe.Count > 0)
            {
                Favorito.TextColor = Color.Gray;
                await App.Database.DeleteFavoritos(existe[0]);
            }
            else
            {
                Favorito.TextColor = Color.Gold;
                await App.Database.SaveFavoritos(new Favoritos() { IdArt = MainPage.ArtSelected.Id });
            }
               
        }
    }
}