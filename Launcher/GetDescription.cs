using GiantBomb.Api;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Launcher
{
    internal static class GetDescription
    {
        private static GiantBombRestClient giantBomb = new GiantBombRestClient("9089ddc90570c04baae6a207074bf5bab4c78a57");

        public static async Task<string> GetGameOverview(string gameName)
        {
            // Aquí especificas que solo quieres el campo de descripción
            var games = await giantBomb.SearchForGamesAsync(gameName);
            var game = games.FirstOrDefault();

            if (game != null)
            {
                // Suponiendo que quieres remover las etiquetas HTML de la descripción
                return StripHtmlTags(game.Description);
            }
            else
            {
                return $"No se encontró el juego: {gameName}";
            }
        }

        // Método para quitar las etiquetas HTML de una cadena de texto
        private static string StripHtmlTags(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}
