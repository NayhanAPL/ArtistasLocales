using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArtistasLocales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Buscar : ContentPage
    {
        public Buscar()
        {
            InitializeComponent();
        }
        public static string filtro = "Nombre";
        private void EntryBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<ListViewUsers> listaBusqueda = new List<ListViewUsers>();
            //la busqueda debe ser en la base de datos
            string text = (string)e.NewTextValue;
            if (Buscar.filtro == "Web") 
            {
                var busqueda = MainPage.listArt.FindAll(x => x.DireccionWeb.Contains(text));
                foreach (var item in busqueda)
                {
                    listaBusqueda.Add(new ListViewUsers() { Principal = item.Nombre, Segundario = item.DireccionWeb,
                    Foto = item.Foto != null ? ImageSource.FromStream(() => new MemoryStream(item.Foto)) : "perfil.jpeg"
                    });
                }
            }
            else if (Buscar.filtro == "Correo") 
            {
                var busqueda = MainPage.listArt.FindAll(x => x.Correo.Contains(text));
                foreach (var item in busqueda)
                {
                    listaBusqueda.Add(new ListViewUsers() { Principal = item.Nombre, Segundario = item.Correo,
                        Foto = item.Foto != null ? ImageSource.FromStream(() => new MemoryStream(item.Foto)) : "perfil.jpeg"
                    });
                }
            }
            else if (Buscar.filtro == "Movil") 
            {
                var busqueda = MainPage.listArt.FindAll(x => x.Movil.Contains(text));
                foreach (var item in busqueda)
                {
                    listaBusqueda.Add(new ListViewUsers() { Principal = item.Nombre, Segundario = item.Movil,
                        Foto = item.Foto != null ? ImageSource.FromStream(() => new MemoryStream(item.Foto)) : "perfil.jpeg"
                    });
                }
            }
            else if (Buscar.filtro == "Fijo")
            {
                var busqueda = MainPage.listArt.FindAll(x => x.Fijo.Contains(text));
                foreach (var item in busqueda)
                {
                    listaBusqueda.Add(new ListViewUsers() { Principal = item.Nombre, Segundario = item.Fijo,
                        Foto = item.Foto != null ? ImageSource.FromStream(() => new MemoryStream(item.Foto)) : "perfil.jpeg"
                    });
                }
            }
            else if (Buscar.filtro == "Nombre") 
            {
                var busqueda = MainPage.listArt.FindAll(x => x.Nombre.Contains(text));
                foreach (var item in busqueda)
                {
                    listaBusqueda.Add(new ListViewUsers() { Principal = item.Nombre, Segundario = item.Manifestacion.ToString(),
                        Foto = item.Foto != null ? ImageSource.FromStream(() => new MemoryStream(item.Foto)) : "perfil.jpeg"
                    });
                }
            }
            else if (Buscar.filtro == "Profecion") 
            {
                var busqueda = MainPage.listArt.FindAll(x => x.ActividadProfecional.Contains(text));
                foreach (var item in busqueda)
                {
                    listaBusqueda.Add(new ListViewUsers() { Principal = item.ActividadProfecional, Segundario = item.Nombre,
                        Foto = item.Foto != null ? ImageSource.FromStream(() => new MemoryStream(item.Foto)) : "perfil.jpeg"
                    });
                }
            }
            else if (Buscar.filtro == "Curriculo") 
            {
                var busqueda = MainPage.listArt.FindAll(x => x.Curriculo.Contains(text));
                foreach (var item in busqueda)
                {
                    string curriculo = item.Curriculo.Substring(0, item.Curriculo.Length > 0 && item.Curriculo.Length > 60 ? 60 : item.Curriculo.Length);
                    curriculo = curriculo.Length == 60 ? curriculo + "..." : item.Curriculo.Length == 0 ? "..." : item.Curriculo;
                    listaBusqueda.Add(new ListViewUsers() { Principal = item.Nombre, Segundario = curriculo ,
                        Foto = item.Foto != null ? ImageSource.FromStream(() => new MemoryStream(item.Foto)) : "perfil.jpeg"
                    });
                }
            }
            else if (Buscar.filtro == "Organizacion") 
            {
                var busqueda = MainPage.listArt.FindAll(x => x.Organizaciones.Contains(text));
                foreach (var item in busqueda)
                {
                    listaBusqueda.Add(new ListViewUsers() { Principal = item.Organizaciones, Segundario = item.Nombre ,
                        Foto = item.Foto != null ? ImageSource.FromStream(() => new MemoryStream(item.Foto)) : "perfil.jpeg"
                    });
                }
            }
            else if (Buscar.filtro == "Manifestacion") 
            {
                var busqueda = MainPage.listArt.FindAll(x => x.Manifestacion.Contains(text));
                foreach (var item in busqueda)
                {
                    listaBusqueda.Add(new ListViewUsers() { Principal = item.Manifestacion, Segundario = item.Nombre,
                        Foto = item.Foto != null ? ImageSource.FromStream(() => new MemoryStream(item.Foto)) : "perfil.jpeg"
                    });
                }
            }

            listArtistasBusqueda.ItemsSource = listaBusqueda;
        }

        private async void ButtonFiltroBusqueda_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new FiltroBuscar());
        }

        private async void listArtistasBusqueda_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //hay que cambiar esto por la lista de la base de datos
            var elem = (ListViewUsers)e.SelectedItem;
            MainPage.ArtSelected = MainPage.listArt.Find(x => x.Nombre == elem.Principal);
            if (MainPage.ArtSelected == null) MainPage.ArtSelected = MainPage.listArt.Find(x => x.Nombre == elem.Segundario);
            await Navigation.PushModalAsync(new Perfil());
        }
    }
}