using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArtistasLocales
{
    public class Database
    {
        public static SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<OpcionesOrdenar>().Wait();
            _database.CreateTableAsync<Favoritos>().Wait();
            _database.CreateTableAsync<Artist>().Wait();
            _database.CreateTableAsync<Proyecto>().Wait();
        }

        //---------------------------------OpcionesOrdenar-------------------------------------------


        //consulta por id de OpcionesOrdenar---------------------------------------------------------
        public Task<OpcionesOrdenar> GetIdOpcionesOrdenar(int Id)
        {
            return _database.Table<OpcionesOrdenar>().Where(a => a.Id == Id).FirstOrDefaultAsync();
        }
        //annadir el OpcionesOrdenar en la db--------------------------------------------------------
        public Task<int> SaveOpcionesOrdenar(OpcionesOrdenar U)
        {
            if (U.Id == 0)
                return _database.InsertAsync(U);
            else return null;
        }
        //guardar la actualizacion de el OpcionesOrdenar en la db------------------------------------
        public Task<int> SaveUpOpcionesOrdenar(OpcionesOrdenar U)
        {
            if (U.Id != 0)
                return _database.UpdateAsync(U);
            else return _database.InsertAsync(U);
        }


        //---------------------------------Favoritos-------------------------------------------


        //consulta completa Favoritos----------------------------------------------------------
        public Task<List<Favoritos>> GetFavoritos()
        {
            return _database.Table<Favoritos>().ToListAsync();
        }
        //consulta por id de Favoritos---------------------------------------------------------
        public Task<Favoritos> GetIdFavoritos(int Id)
        {
            return _database.Table<Favoritos>().Where(a => a.Id == Id).FirstOrDefaultAsync();
        }
        //annadir el Favoritos en la db--------------------------------------------------------
        public Task<int> SaveFavoritos(Favoritos U)
        {
            if (U.Id == 0)
                return _database.InsertAsync(U);
            else return null;
        }
        //guardar la actualizacion de el Favoritos en la db------------------------------------
        public Task<int> SaveUpFavoritos(Favoritos U)
        {
            if (U.Id != 0)
                return _database.UpdateAsync(U);
            else return _database.InsertAsync(U);
        }
        // borrar una fila de la tabla Favoritos-----------------------------------------------
        public Task<int> DeleteFavoritos(Favoritos Favoritos)
        {
            var x = _database.DeleteAsync(Favoritos);
            return x;
        }
        // devuelve por IdArt en Favoritos------------------------------------------------------
        public async Task<List<Favoritos>> GetByIdArtFavoritos(int id)
        {
            return await _database.QueryAsync<Favoritos>($"Select * from Favoritos where IdArt = '{id}'");
        }


        //---------------------------------Artista-------------------------------------------


        //consulta completa Artistas----------------------------------------------------------
        public Task<List<Artist>> GetArtistas()
        {
            return _database.Table<Artist>().ToListAsync();
        }
        //consulta por id de Artistas---------------------------------------------------------
        public Task<Artist> GetIdArtistas(int Id)
        {
            return _database.Table<Artist>().Where(a => a.Id == Id).FirstOrDefaultAsync();
        }
        //annadir el Artistas en la db--------------------------------------------------------
        public Task<int> SaveArtistas(Artist U)
        {
            return _database.InsertAsync(U);
        }
        //guardar la actualizacion de el Artistas en la db------------------------------------
        public Task<int> SaveUpArtistas(Artist U)
        {
            if (U.Id != 0)
                return _database.UpdateAsync(U);
            else return _database.InsertAsync(U);
        }
        // borrar una fila de la tabla Artistas-----------------------------------------------
        public Task<int> DeleteArtistas(Artist Artistas)
        {
            var x = _database.DeleteAsync(Artistas);
            return x;
        }


        //---------------------------------Proyectos-------------------------------------------


        //consulta completa Proyectos----------------------------------------------------------
        public Task<List<Proyecto>> GetProyectos()
        {
            return _database.Table<Proyecto>().ToListAsync();
        }
        //consulta por id de Favoritos---------------------------------------------------------
        public Task<Proyecto> GetIdProyectos(int Id)
        {
            return _database.Table<Proyecto>().Where(a => a.Id == Id).FirstOrDefaultAsync();
        }
        //annadir el Proyectos en la db--------------------------------------------------------
        public Task<int> SaveProyectos(Proyecto U)
        {
            if (U.Id == 0)
                return _database.InsertAsync(U);
            else return null;
        }
        //guardar la actualizacion de el Proyectos en la db------------------------------------
        public Task<int> SaveUpProyectos(Proyecto U)
        {
            if (U.Id != 0)
                return _database.UpdateAsync(U);
            else return _database.InsertAsync(U);
        }
        // borrar una fila de la tabla Proyectos-----------------------------------------------
        public Task<int> DeleteProyectos(Proyecto Proyectos)
        {
            var x = _database.DeleteAsync(Proyectos);
            return x;
        }
        // devuelve por IdArt en Proyectos------------------------------------------------------
        public async Task<List<Proyecto>> GetByNameArtProyectos(string name)
        {
            return await _database.QueryAsync<Proyecto>($"Select * from Proyecto where NameArt = '{name}'");
        }
    }
}
