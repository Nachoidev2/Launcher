using craftersmine.SteamGridDBNet;
using craftersmine.SteamGridDBNet.Exceptions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Launcher
{
    public static class SearchGameManager
    {
        private static readonly SteamGridDb sgdb = new SteamGridDb("b31e5f6ab66df37f2d0c8613d70413f9");

        public static async Task<(SteamGridDbGrid[], SteamGridDbHero[])> SearchGameAsync(string searchTerm)
        {
            SteamGridDbGrid[] grids = null;
            SteamGridDbHero[] heroes = null;

            try
            {
                SteamGridDbGame[] games = await sgdb.SearchForGamesAsync(searchTerm);

                if (games != null && games.Length > 0)
                {
                    // Manejar los juegos encontrados, por ejemplo, seleccionar el primero o permitir que el usuario elija
                    var firstGame = games[0]; // Solo un ejemplo, ajusta según sea necesario
                    var ID = firstGame.Id;

                    grids = await sgdb.GetGridsByGameIdAsync(ID);
                    heroes = await sgdb.GetHeroesByGameIdAsync(ID);

                    if (grids != null && grids.Length > 0)
                    {
                        return (grids, heroes);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron grids para el juego seleccionado.");
                    }
                }
                else
                {
                    // No se encontraron juegos
                    MessageBox.Show("No se encontraron juegos para el término de búsqueda proporcionado.");
                }
            }
            catch (SteamGridDbBadRequestException ex)
            {
                // Manejar una solicitud mal formada, potencialmente un error de API o un bug en la biblioteca
            }
            catch (SteamGridDbUnauthorizedException ex)
            {
                // Manejar error de autenticación, como una clave de API faltante, inválida o expirada
            }
            catch (SteamGridDbNotFoundException ex)
            {
                // Manejar el caso en que no se encuentran juegos con los parámetros especificados
            }
            catch (SteamGridDbForbiddenException ex)
            {
                // Manejar el acceso prohibido, por ejemplo, intentar eliminar un objeto que no posees
            }
            catch (SteamGridDbImageException ex)
            {
                // Manejar errores relacionados con el acceso a imágenes
            }
            catch (SteamGridDbException ex)
            {
                // Manejar cualquier otro error genérico al acceder a la API de SteamGridDB
            }

            return (new SteamGridDbGrid[0], new SteamGridDbHero[0]);
        }
    }
}
