using ArtistasLocales;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Extensions;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ArtistasLocales
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Inicio();
            ByDefault();
        }
        public static List<Artistas> listArt = new List<Artistas>();
        public static Artistas ArtSelected = new Artistas();
        public static List<Proyectos> listVin = new List<Proyectos>();

        public async void ByDefault()
        {
            listArt.Clear();
            listVin.Clear();
            listArt = await App.Database.GetArtistas();
            listVin = await App.Database.GetProyectos();
            Ordenar(listArt);
        }
        public static SQLiteAsyncConnection databaseApolo;
        public async void Pick()
        {
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    if (result.FileName.EndsWith(".db3", StringComparison.OrdinalIgnoreCase))
                    {
                        var stream = await result.OpenReadAsync();
                        AccederCerrar(result);
                    }
                }
            }
            catch (Exception)
            {
                Mensajes.TextSubAlert = "Ha ocurrido un error. Intente otra vez";
                await Navigation.PushModalAsync(new Mensajes());
            }
        }
        private async void AccederCerrar(FileResult file)
        {
            if (file != null)
            {

                //--------------------------Acceder y llenar lista-------------------------------------------------
                string direrc = Path.Combine(file.FullPath,"");
                if (File.Exists(direrc))
                {
                    listArt.Clear();
                    listVin.Clear();

                    SQLite.SQLiteOpenFlags Flags =
                    SQLite.SQLiteOpenFlags.ReadOnly |
                    SQLite.SQLiteOpenFlags.SharedCache;

                    await Database._database.CloseAsync();

                    databaseApolo = new SQLiteAsyncConnection(direrc, Flags);
                    listArt = GetArtistas();
                    listVin = GetProyectos();
                    Ordenar(listArt);
                    ActualizarDB();
                    labelCantArtistas.Text = listArt.Count.ToString();
                    await databaseApolo.CloseAsync();
                    Database._database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "apoloApp.db3"));
                }
            }
        }
        private async void ActualizarDB()
        {
            var borrarArt = await App.Database.GetArtistas();
            borrarArt.ForEach(async x => await App.Database.DeleteArtistas(x));
            var borrarVin = await App.Database.GetProyectos();
            borrarVin.ForEach(async x => await App.Database.DeleteProyectos(x));
            listArt.ForEach(async x => await App.Database.SaveArtistas(x));
            listVin.ForEach(async x => await App.Database.SaveProyectos(x));
        }
        private List<Proyectos> GetProyectos()
        {
            var x = databaseApolo.Table<Proyectos>().ToListAsync();
            List<Proyectos> res = x.Result;
            return res;
        }
        private List<Artistas> GetArtistas()
        {
            var x = databaseApolo.Table<Artistas>().ToListAsync();
            List<Artistas> res = x.Result;
            return res;
        }
        public async void Ordenar(List<Artistas> list)
        {

            List<ListViewUsers> listOrdered = new List<ListViewUsers>();
            var orden = await App.Database.GetIdOpcionesOrdenar(1);
            if (orden.Tipo == "Nombre")
            {
                var iList = list.OrderBy(x => x.Nombre);
                foreach (var item in iList)
                {
                    listOrdered.Add(new ListViewUsers()
                    {
                        Principal = item.Nombre,
                        Segundario = item.Manifestacion.ToString(),
                        Foto = item.Foto != null ? ImageSource.FromStream(() => new MemoryStream(item.Foto)) : "perfil.jpeg"
                    });
                }
            }
            if (orden.Tipo == "Edad")
            {
                var iList = list.OrderBy(x => x.FechaNacimiento);
                foreach (var item in iList)
                {
                    listOrdered.Add(new ListViewUsers()
                    {
                        Principal = item.Nombre,
                        Segundario = item.FechaNacimiento.Day + "/" + item.FechaNacimiento.Month + "/" + item.FechaNacimiento.Year,
                        Foto = item.Foto != null ? ImageSource.FromStream(() => new MemoryStream(item.Foto)) : "perfil.jpeg"
                    });
                };

            }
            if (orden.Tipo == "Organizacion")
            {
                var iList = list.OrderBy(x => x.Organizaciones);
                foreach (var item in iList)
                {
                    string organizaciones = item.Organizaciones.Substring(0, item.Organizaciones.Length - 2);
                    listOrdered.Add(new ListViewUsers()
                    {
                        Principal = organizaciones,
                        Segundario = item.Nombre,
                        Foto = item.Foto != null ? ImageSource.FromStream(() => new MemoryStream(item.Foto)) : "perfil.jpeg"
                    });
                }
            }
            if (orden.Tipo == "Manifestacion")
            {
                var iList = list.OrderBy(x => x.Manifestacion);
                foreach (var item in iList)
                {
                    listOrdered.Add(new ListViewUsers()
                    {
                        Principal = item.Manifestacion.ToString(),
                        Segundario = item.Nombre,
                        Foto = item.Foto != null ? ImageSource.FromStream(() => new MemoryStream(item.Foto)) : "perfil.jpeg"
                    });
                }
            }
            if (orden.Tipo == "ActividadProfecional")
            {
                var iList = list.OrderBy(x => x.ActividadProfecional);
                foreach (var item in iList)
                {
                    listOrdered.Add(new ListViewUsers()
                    {
                        Principal = item.ActividadProfecional,
                        Segundario = item.Nombre,
                        Foto = item.Foto != null ? ImageSource.FromStream(() => new MemoryStream(item.Foto)) : "perfil.jpeg"
                    });
                }
            }


            listArtistas.ItemsSource = listOrdered;
        }
        private async void Inicio()
        {
            var ordenamiento = await App.Database.GetIdOpcionesOrdenar(1);
            if (ordenamiento == null)
            {
                await App.Database.SaveOpcionesOrdenar(new OpcionesOrdenar() { Tipo = "Nombre" });
            }
        }
        private async void ButtonFavoritos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PageFavoritos());
        }
        private async void ButtonBuscar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Buscar());
        }
        private async void ButtonOrdenar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new Ordenamiento());
            MessagingCenter.Subscribe<Ordenamiento, string>(this, "Ordenamiento", async (s, arg) =>
            {
                Ordenar(listArt);
            });
        }
        private async void listArtistas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var elem = (ListViewUsers)e.SelectedItem;
            ArtSelected = listArt.Find(x => x.Nombre == elem.Principal);
            if (ArtSelected == null) ArtSelected = listArt.Find(x => x.Nombre == elem.Segundario);
            await Navigation.PushModalAsync(new Perfil());
        }
        private async void Info_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Info());
        }
        private void NewDB_Clicked(object sender, EventArgs e)
        {
            Pick();
        }
    }
    public class ListViewUsers
    {
        public string Principal { get; set; }
        public string Segundario { get; set; }
        public ImageSource Foto { get; set; }
    }
}
