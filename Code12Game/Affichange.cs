using Code12Data;
using Spectre.Console;
using System.Threading.Tasks;


namespace Code12Game
{
    public static class Affichange
    {
        public static Layout GameHUD = new Layout()
            .Visible()
            .SplitColumns(
            new Layout("Left")
            .Ratio(3)
            .SplitRows(new Layout("view").Ratio(3), new Layout("desk").Ratio(2)),
            new Layout("info").Ratio(1)
            );

        //fonction de desk(on vien update le layout desk) pour faire l'affichange des carte spécila/panel avec la selection en liste
        public static void RefreshDesk()
        {
            // Appel de l'autre classe pour récupérer le contenu
            var deckLa = DeckFactory.CreateDeskLayout();

            // Mise à jour du layout
            GameHUD["Left"]["desk"].Update(deckLa);
        }
        public static void RenderHUD()
        {
            AnsiConsole.Write(GameHUD);
        }

        //Fonction de view pour faire l'affichange du carte de score pigée durant un temps temporaire au pardessus le jeu
        public static async Task ShowScoreCardTemporarily(int scoreValue)
        {
            // 1. Accès sécurisé au layout de la vue
            var viewLayout = GameHUD.GetLayout("Left").GetLayout("view");
            //if (viewLayout == null) return;

            // 2. Création de la carte (Popup)
            var scoreCardVisual = CardFactory.CreateCardLayout(scoreValue);



            // 3. Centrage (L'effet "Popup")


            // 4. Affichage de la carte
            viewLayout.Update(scoreCardVisual);

            RenderHUD();
            // 5. Attente
            await Task.Delay(3000);

            // 6. Restauration
            // Comme Layout n'expose pas le contenu actuel, on restaure un contenu par défaut.
            // Si vous avez une factory pour le contenu de la view, remplacez la ligne ci‑dessous par l'appel approprié.
            var defaultView = new Panel(" ") { Expand = true };
            viewLayout.Update(defaultView);
        }

        // Fonction de débogage pour afficher les ratios de tous les layouts
        public static void DebugLayout()
        {
            Console.WriteLine(GameHUD.GetLayout("Left").GetLayout("view").Ratio + " view");
            Console.WriteLine(GameHUD.GetLayout("Left").GetLayout("desk").Ratio + " desk");
            Console.WriteLine(GameHUD.GetLayout("info").Ratio + " info");

        }

    }    
}