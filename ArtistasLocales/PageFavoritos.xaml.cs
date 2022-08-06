using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArtistasLocales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageFavoritos : ContentPage
    {
        public PageFavoritos()
        {
            InitializeComponent();
            ByDefault();
        }

        private async void ByDefault()
        {
            var listaFav = await App.Database.GetFavoritos();
            List<Artist> lista = new List<Artist>();
            foreach (var item in listaFav)
            {
                var elem = MainPage.listArt.Find(x => x.Id == item.IdArt);
                if(elem != null) lista.Add(elem);
            }
            ObservableCollection<ListViewUsers> view = new ObservableCollection<ListViewUsers>() { };
            if (lista.Count > 0)
            {
                if (lista[0] != null)
                {
                    lista.ForEach(x => view.Add(new ListViewUsers()
                    {
                        Principal = x.Nombre,
                        Segundario = x.Manifestacion,
                        Foto = x.Foto != null ? ImageSource.FromStream(() => new MemoryStream(x.Foto)) : "perfil.jpeg"
                    }));
                    listFavoritos.ItemsSource = view;
                }
            }        
        }

        private async void listFavoritos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var elem = (Artist)e.SelectedItem;
            MainPage.ArtSelected = MainPage.listArt.Find(x => x.Nombre == elem.Nombre);
            await Navigation.PushModalAsync(new Perfil());
        }
    }
}