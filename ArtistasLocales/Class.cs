using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistasLocales
{
    public class OpcionesOrdenar
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Tipo { get; set; }
    }
    public class Favoritos
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdArt { get; set; }
    }
    public class Artist
    {
        [PrimaryKey, AutoIncrement]
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
    public class Proyecto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NameArt { get; set; }
        public string Nombre { get; set; }
        public string Lugar { get; set; }
        public string Descripcion { get; set; }
    }
}
