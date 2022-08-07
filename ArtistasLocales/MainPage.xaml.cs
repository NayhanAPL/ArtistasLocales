using ArtistasLocales;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Extensions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static List<Artist> listArt = new List<Artist>();
        public static Artist ArtSelected = new Artist();
        public static List<Proyecto> listVin = new List<Proyecto>();
        public static List<ListViewUsers> listOrdered = new List<ListViewUsers>();

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
                    var AlistArt = GetArtistas();
                    listVin = GetProyectos();
                    AlistArt.ForEach(x => listArt.Add(new Artist() {
                        ActividadProfecional = x.ActividadProfecional,
                        Correo = x.Correo,
                        Curriculo = x.Curriculo,
                        DireccionWeb = x.DireccionWeb,
                        FechaNacimiento = x.FechaNacimiento,
                        Fijo = x.Fijo,
                        Foto = x.Foto,
                        Manifestacion = x.Manifestacion,
                        Movil = x.Movil,
                        Nombre = x.Nombre,
                        Organizaciones = x.Organizaciones,
                        Id = x.Id
                    }));
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
            foreach (var item in borrarArt)
            {
                await App.Database.DeleteArtistas(item);
            }
            var borrarVin = await App.Database.GetProyectos();
            foreach (var item in borrarVin)
            {
                await App.Database.DeleteProyectos(item);
            }
            listArt.ForEach(async x => await App.Database.SaveArtistas(new Artist() { 
            ActividadProfecional = x.ActividadProfecional,
            Correo = x.Correo,
            Curriculo = x.Curriculo,
            DireccionWeb = x.DireccionWeb,
            FechaNacimiento = x.FechaNacimiento,
            Fijo = x.Fijo, 
            Foto = x.Foto,
            Manifestacion = x.Manifestacion,
            Movil = x.Movil,
            Nombre = x.Nombre,
            Organizaciones = x.Organizaciones
            }));
            listVin.ForEach(async x => await App.Database.SaveProyectos(new Proyecto() { 
            NameArt = x.NameArt,
            Descripcion = x.Descripcion,
            Lugar = x.Lugar,
            Nombre = x.Nombre
            }));
            var fav = await App.Database.GetFavoritos();
            foreach (var item in fav)
            {
                await App.Database.DeleteFavoritos(item);
            }
        }
        private List<Proyecto> GetProyectos()
        {
            var x = databaseApolo.Table<Proyecto>().ToListAsync();
            List<Proyecto> res = x.Result;
            return res;
        }
        private List<Artista> GetArtistas()
        {
            var x = databaseApolo.Table<Artista>().ToListAsync();
            List<Artista> res = x.Result;
            return res;
        }
        public async void Ordenar(List<Artist> list)
        {
            listOrdered.Clear();
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
                        Segundario = item.FechaNacimiento,
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
            if (orden.Tipo == "Profecion")
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

            labelCantArtistas.Text = listArt.Count.ToString();
            listArtistas.ItemsSource = null;
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
                await Task.Delay(1000);
                Ordenar(listArt);
                await Task.Delay(1000);
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
    public class Artista
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string FechaNacimiento { get; set; }
        public byte[] Foto { get; set; }
        public string Manifestacion { get; set; }
        public string ActividadProfecional { get; set; }
        public string Fijo { get; set; }
        public string Movil { get; set; }
        public string Correo { get; set; }
        public string DireccionWeb { get; set; }
        public string Curriculo { get; set; }
        public string Organizaciones { get; set; }
    }
}
