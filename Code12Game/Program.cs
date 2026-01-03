using Spectre.Console;
using Code12Data;
using Code12Game.Input;
using Code12Game.Display;
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
            
            // Initialiser le contrôleur d'entrée
            var inputController = new InputController();
            inputController.LoadSettings("config/input_settings.json");
            
            var inputHandler = new InputHandler(inputController);
            
            // Lancer le HUD dans une tâche indépendante
            _ = Task.Run(() => Affichange.InitializeHUD());
            
            // Attendre que le HUD soit initialisé
            await Task.Delay(200);

            Console.ReadKey(false); // Attendre une touche pour continuer
            
            GameData.initialiserCartesSpecialesDebug();
            //Affichange.RefreshDesk(); // Rafraîchir l'affichage après modification


            //Exemple d'intégration dans une boucle de jeu
             while (true)
            {
                inputHandler.Update();
                await Task.Delay(16); // ~60 FPS
            }
        }


    }
}