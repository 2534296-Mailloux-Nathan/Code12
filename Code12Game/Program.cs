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

            // Initialisation de l'affichange du jeu
            AnsiConsole.MarkupLine("[bold green]Bienvenue dans Code12Game![/]");
            await Task.Delay(1000);
            AnsiConsole.Clear();
            
            // Lancer le HUD dans une tâche indépendante
            _ = Task.Run(() => Affichange.InitializeHUD());
            
            // Attendre que le HUD soit initialisé
            await Task.Delay(200);

            Console.ReadKey(false); // Attendre une touche pour continuer
            
            GameData.initialiserCartesSpecialesDebug();
            //Affichange.RefreshDesk(); // Rafraîchir l'affichage après modification
            await Affichange.ShowScoreCardTemporarily(3);
            
            Console.ReadKey(false); // Attendre une touche pour continuer



        }


    }
}