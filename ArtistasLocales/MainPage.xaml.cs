using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArtistasLocales
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ByDefault();
        }
        public static List<Artistas> listArt = new List<Artistas>();
        public static Artistas ArtSelected = new Artistas();
        public static List<Proyectos> listVin = new List<Proyectos>();
        public void ByDefault()
        {
            Inicio();
            
            BusquedaDB();
            for (int i = 0; i < 6; i++)
                {
                    listArt.Add(new Artistas()
                    {
                        Nombre = "Nayhan" + i,
                        FechaNacimiento = new DateTime(2001, 2, 26),
                        ActividadProfecional = "Programador",
                        Correo = "nayhanprovedolabrada@gmail.com",
                        Curriculo = "no tiene nada por ahora está tratando de conseguir su primer empleo, trabaja con hxamarin forms en la creacion de aplicaciones android alsnscjlkas aslkascas asclkas clksacas caslkcnskc; a;lvm d rvr[ vrwv rv orpvwe,vwe;lvcsdpvewv dvlc als casl;c k; csakc ec ec ek ckc asc as;ck ascas c askc pacac, ascasc asckas caksc alskc askc ac sack sack sack ecev  vkv l;v adv dsv;ds vd;slv sd;v, svk v;v ds;v sd;v sdv; sdvsdv ;v sd;lv dsvk sv;d vsd;v dsv;, ;sv sdkv sva;cas c;v sdkvs dvklds vds vs dvklsd vdslkv sd v dvlsd vls vlksd vkld vdksv dsklv dv  dv ewlkg ewgk d svdsv svk dsv sdv dkv skv sdvl dsvl dsvl dsv   dsvksdlfkdsafnakslfjasklfasf dslf dslfksdfaslfasf;ioawfhlkanvdascpiejgkdsn;vkNFI:OEANFVDSKLv;naiefvfnae,dsnkcads .",
                        DireccionWeb = "https://www.google.ru/search",
                        Fijo = "77974732",
                        Id = i,
                        Manifestacion = "Literatura",
                        Movil = "58747585",
                        Organizaciones = "ACAA, ICRT, ASCC, "
                    });
                    listVin.Add(new Proyectos()
                    {
                        Id = 1,
                        Descripcion = "es una app para manejar informacion de artistas",
                        Lugar = "Guanabacoa",
                        Nombre = "ArtistasLocales",
                        IdArt = i
                    }
                    );

                    listArt.Add(new Artistas()
                    {
                        Nombre = "Susana" + (i + 6),
                        FechaNacimiento = new DateTime(1992, 11, 21),
                        ActividadProfecional = "FrontEnd",
                        Correo = "susanabp1992@gmail.com",
                        Curriculo = "ha trabajado para el sitio oficial del museo de vellas artes",
                        DireccionWeb = "susy/porahi/miraver/siencuentrasalgo/direc.www.com",
                        Fijo = "78324543",
                        Id = i + 6,
                        Manifestacion = "Disenno",
                        Movil = "58237413",
                        Organizaciones = "AHS, UPEC, "
                    });
                    listVin.AddRange(new List<Proyectos>()
                {
                    new Proyectos() { Id = 2, IdArt = (i + 6), Descripcion = "es una app para manejar informacion de artistas", Lugar = "Guanabacoa", Nombre = "ArtistasLocales" },
                    new Proyectos() { Id = 3, IdArt = (i + 6), Descripcion = "Una aplicacion para el intercambio de bienes si uso de efectivo", Lugar = "Regla", Nombre = "Trueque" }
                });
                } // esto hay que sustituirlo por la consulta a la base de datos.
            Ordenar(listArt);              // quitar de aqui
            labelCantArtistas.Text = listArt.Count.ToString(); // quitar de aqui
        }
        private async void BusquedaDB()
        {
            int res = 0;
            string sqlite = "apolo";
            VersionActual version = await App.Database.GetIdVersionActual(1);
            if (version.Version == "0")
            {
                res = BuscarEnRegistros("apolo * .db");// expresion regular                
            }
            else
            {
                int limit = Convert.ToInt32(version.Version);
                for (int i = limit + 10; i >= limit; i--)
                {
                    res = BuscarEnRegistros($"apolo{i}.db");
                    if (res != 0) break;                    
                }
                if (res == 0)
                {
                    Mensajes.TextSubAlert = "Base de datos fuera de rango. Puede que esté obsolela o existen muchas versiones de por medio respecto a la que usted tenía.";
                    res = BuscarEnRegistros("apolo * .db");// expresion regular
                    if (res == 0)
                    {
                        Mensajes.TextSubAlert = "Base de datos no encontrada. asegurese de descargarla y tenerla en sus ficheros.";
                    }
                }
            }
            await App.Database.SaveUpVersionActual(new VersionActual() { Id = 1, Version = res.ToString() });
        }
        private int BuscarEnRegistros(string nombre)
        {
            int version = 0;
            listArt.Clear();

            // operaciones para buscar en los ficheros
            // hacer la consulta a la DB para pedir artistas y proyectos
            // darle a listArt y listPro sus valores
            // sacar la version del encontrado

            Ordenar(listArt);
            labelCantArtistas.Text = listArt.Count.ToString();
            return version;
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
                        Segundario = item.Manifestacion.ToString()
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
                        Segundario = item.FechaNacimiento.Day + "/" + item.FechaNacimiento.Month + "/" + item.FechaNacimiento.Year
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
                        Segundario = item.Nombre
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
                        Segundario = item.Nombre
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
                        Segundario = item.Nombre
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
            var version = await App.Database.GetIdVersionActual(1);
            if (ordenamiento == null)
            {
                await App.Database.SaveVersionActual(new VersionActual() { Version = "0" });
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
                ByDefault();
            });
        }
        private async void listArtistas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var elem = (ListViewUsers)e.SelectedItem;
            ArtSelected = listArt.Find(x => x.Nombre == elem.Principal);
            if(ArtSelected == null) ArtSelected = listArt.Find(x => x.Nombre == elem.Segundario);
            await Navigation.PushModalAsync(new Perfil());
        }
        private async void Info_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Info());
        }
    }


    public class Artistas
    {
        //[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
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
    public class Proyectos
    {
        //[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdArt { get; set; }
        public string Nombre { get; set; }
        public string Lugar { get; set; }
        public string Descripcion { get; set; }
    }
    public class ListViewUsers
    {
        public string Principal { get; set; }
        public string Segundario { get; set; }
    }
}
