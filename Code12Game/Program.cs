using Spectre.Console;
using Code12Data;
using System.Threading.Tasks;

namespace Code12Game
{

    public class Program
    {

        public static async Task Main(string[] args)
        {
            Utiliteraire.ForcePleinEcran();
            AnsiConsole.Clear();

            // Affichage initial du HUD
            AnsiConsole.Write(Affichange.GameHUD);
            Console.ReadKey();
            Console.Clear();

            // Initialisation des données et rafraîchissement du desk
            GameData.initialiserCartesSpecialesDebug();
            Affichange.RefreshDesk();
            AnsiConsole.Write(Affichange.GameHUD);

            // --- TEST: afficher temporairement une carte de score ---
            // Appelle la méthode asynchrone qui affiche une popup durant quelques secondes
            await Affichange.ShowScoreCardTemporarily(100);

            // Réafficher le HUD après le test
            AnsiConsole.Write(Affichange.GameHUD);
            Console.ReadKey();

        }


    }
}