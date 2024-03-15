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
                // Obtener solo la descripción del juego
                string description = game.Description;

                // Eliminar etiquetas HTML de la descripción
                description = StripHtmlTags(description);

                // Eliminar el encabezado "Overview" si está presente
                description = RemoveOverviewHeader(description);

                return description;
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

        // Método para eliminar el encabezado "Overview" si está presente
        private static string RemoveOverviewHeader(string input)
        {
            // Buscar la posición donde comienza el texto de la descripción
            int startIndex = input.IndexOf("Overview");

            // Verificar si se encontró la palabra "Overview" y si está al principio de la cadena
            if (startIndex != -1 && startIndex == 0)
            {
                // Eliminar la parte inicial que contiene "Overview"
                input = input.Substring(startIndex + "Overview".Length).Trim();
            }

            return input;
        }
    }
}
