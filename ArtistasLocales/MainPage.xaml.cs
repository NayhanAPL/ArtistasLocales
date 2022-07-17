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

        private void ByDefault()
        {
            // esto es de prueba
            List<Artistas> list = new List<Artistas>();
            for (int i = 0; i < 6; i++)
            {
                list.Add(new Artistas()
                {
                    Nombre = "Nayhan",
                    ActividadProfecional = "Programador",
                    Correo = "nayhanprovedolabrada@gmail.com",
                    Curriculo = "no tiene nada por ahora está tratando de conseguir su primer empleo, trabaja con hxamarin forms en la creacion de aplicaciones android",
                    DireccionWeb = "nose/porahi/miraver/siencuentrasalgo/direc.www.com",
                    Fijo = "77974732",
                    Id = 1,
                    Manifestacion = EnumManifestaciones.Literatura,
                    Movil = "58747585",
                    Organizaciones = new List<EnumOrganizaciones>() { EnumOrganizaciones.ACAA, EnumOrganizaciones.ICRT, EnumOrganizaciones.ASCC },
                    Vinculaciones = new List<Proyectos>() { new Proyectos() { Id = 1, Descripcion = "es una app para manejar informacion de artistas", Lugar = "Guanabacoa", Nombre = "ArtistasLocales" } },
                    Foto = new Image() { Source = "imagen.jpeg"}
                });
                list.Add(new Artistas()
                {
                    Nombre = "Susana",
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
            labelCantArtistas.Text = list.Count.ToString();
            listArtistas.ItemsSource = list;
        }

        private void ButtonFavoritos_Clicked(object sender, EventArgs e)
        {

        }

        private void ButtonBuscar_Clicked(object sender, EventArgs e)
        {

        }

        private void ButtonOrdenar_Clicked(object sender, EventArgs e)
        {

        }

        private void listArtistas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

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
}
