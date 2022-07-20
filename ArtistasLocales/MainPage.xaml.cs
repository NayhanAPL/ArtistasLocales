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
        public void ByDefault()
        {
            Inicio();
            // esto es de prueba
            listArt.Clear();
            for (int i = 0; i < 6; i++)
            {
                listArt.Add(new Artistas()
                {
                    Nombre = "Nayhan",
                    FechaNacimiento = new DateTime(2001, 2, 26),
                    ActividadProfecional = "Programador",
                    Correo = "nayhanprovedolabrada@gmail.com",
                    Curriculo = "no tiene nada por ahora está tratando de conseguir su primer empleo, trabaja con hxamarin forms en la creacion de aplicaciones android alsnscjlkas aslkascas asclkas clksacas caslkcnskc; a;lvm d rvr[ vrwv rv orpvwe,vwe;lvcsdpvewv dvlc als casl;c k; csakc ec ec ek ckc asc as;ck ascas c askc pacac, ascasc asckas caksc alskc askc ac sack sack sack ecev  vkv l;v adv dsv;ds vd;slv sd;v, svk v;v ds;v sd;v sdv; sdvsdv ;v sd;lv dsvk sv;d vsd;v dsv;, ;sv sdkv sva;cas c;v sdkvs dvklds vds vs dvklsd vdslkv sd v dvlsd vls vlksd vkld vdksv dsklv dv  dv ewlkg ewgk d svdsv svk dsv sdv dkv skv sdvl dsvl dsvl dsv   dsvksdlfkdsafnakslfjasklfasf dslf dslfksdfaslfasf;ioawfhlkanvdascpiejgkdsn;vkNFI:OEANFVDSKLv;naiefvfnae,dsnkcads .",
                    DireccionWeb = "nose/porahi/miraver/siencuentrasalgo/direc.www.com",
                    Fijo = "77974732",
                    Id = 1,
                    Manifestacion = EnumManifestaciones.Literatura,
                    Movil = "58747585",
                    Organizaciones = new List<EnumOrganizaciones>() { EnumOrganizaciones.ACAA, EnumOrganizaciones.ICRT, EnumOrganizaciones.ASCC },
                    Vinculaciones = new List<Proyectos>() { new Proyectos() { Id = 1, Descripcion = "es una app para manejar informacion de artistas", Lugar = "Guanabacoa", Nombre = "ArtistasLocales" } },
                    Foto = new Image() { Source = "imagen.jpeg" }
                });
                listArt.Add(new Artistas()
                {
                    Nombre = "Susana",
                    FechaNacimiento = new DateTime(1992, 11, 21),
                    ActividadProfecional = "FrontEnd",
                    Correo = "susanabp1992@gmail.com",
                    Curriculo = "ha trabajado para el sitio oficial del museo de vellas artes",
                    DireccionWeb = "susy/porahi/miraver/siencuentrasalgo/direc.www.com",
                    Fijo = "78324543",
                    Id = 2,
                    Manifestacion = EnumManifestaciones.Disenno,
                    Movil = "58237413",
                    Organizaciones = new List<EnumOrganizaciones>() { EnumOrganizaciones.AHS, EnumOrganizaciones.UPEC },
                    Vinculaciones = new List<Proyectos>() {
                    new Proyectos() { Id = 1, Descripcion = "es una app para manejar informacion de artistas", Lugar = "Guanabacoa", Nombre = "ArtistasLocales" },
                    new Proyectos() { Id = 2, Descripcion = "Una aplicacion para el intercambio de bienes si uso de efectivo", Lugar = "Regla", Nombre = "Trueque" } },
                    Foto = new Image()
                });

            }
            //aqui deno pasarle a Ordenar la lista de la DB
            Ordenar(listArt);
            labelCantArtistas.Text = listArt.Count.ToString();
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
                var iList = list.OrderBy(x => x.Organizaciones.Count);
                foreach (var item in iList)
                {
                    string organizaciones = "";
                    foreach (var elem in item.Organizaciones)
                    {
                        organizaciones += elem.ToString() + ", ";
                    }
                    organizaciones = organizaciones.Substring(0, organizaciones.Length - 2);
                    listOrdered.Add(new ListViewUsers()
                    {
                        Principal = organizaciones,
                        Segundario = item.Nombre
                    });
                }
            }
            if (orden.Tipo == "Proyectos")
            {
                var iList = list.OrderBy(x => x.Vinculaciones[0].Nombre);
                foreach (var item in iList)
                {
                    string proyectos = "";
                    foreach (var elem in item.Vinculaciones)
                    {
                        proyectos += elem.Nombre + ", ";
                    }
                    proyectos = proyectos.Substring(0, proyectos.Length - 2);
                    listOrdered.Add(new ListViewUsers()
                    {
                        Principal = item.Nombre,
                        Segundario = proyectos
                    });
                }
            }
            if (orden.Tipo == "Manifestacion")
            {
                var iList = list.OrderBy(x => ((int)x.Manifestacion));
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
        }

        private void ButtonFavoritos_Clicked(object sender, EventArgs e)
        {

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
            //hay que cambiar esto por la lista de la base de datos
            var elem = (ListViewUsers)e.SelectedItem;
            ArtSelected = listArt.Find(x => x.Nombre == elem.Principal);
            if(ArtSelected == null) ArtSelected = listArt.Find(x => x.Nombre == elem.Segundario);
            await Navigation.PushModalAsync(new Perfil());
        }
    }

    public enum EnumManifestaciones
    {
        Danza,
        Musica,
        Teatro,
        Literatura,
        Visuales,
        Disenno,
        Fotografia
    }
    public enum EnumOrganizaciones
    {
        UNEAC,
        ACAA,
        UPEC,
        AHS,
        ASCC,
        ICRT
    }
    public class Artistas
    {
        //[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Image Foto { get; set; }
        public EnumManifestaciones Manifestacion { get; set; }
        public string ActividadProfecional { get; set; }
        public string Fijo { get; set; }
        public string Movil { get; set; }
        public string Correo { get; set; }
        public string DireccionWeb { get; set; }
        public string Curriculo { get; set; }
        public List<EnumOrganizaciones> Organizaciones { get; set; }
        public List<Proyectos> Vinculaciones { get; set; }
    }
    public class Proyectos
    {
        //[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
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
