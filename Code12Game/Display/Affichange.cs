using Code12Data;
using Spectre.Console;
using System.Threading.Tasks;
using Code12Game.Display;


namespace Code12Game.Display
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

        private static LiveDisplayContext? _liveContext;
        private static bool _needsRefresh = false;

        // fonction d'initialisation de l'affichange du jeu avec le live ansi console
        public static async Task InitializeHUD()
        {
            await AnsiConsole.Live(GameHUD)
                .AutoClear(true)
                .Overflow(VerticalOverflow.Ellipsis)
                .Cropping(VerticalOverflowCropping.Top)
                .StartAsync(async ctx =>
                {
                    _liveContext = ctx;
                    
                    // Initial rendering
                    RefreshDesk();
    
                    await Task.Delay(100);
                    
                    // Garder le Live actif - boucle infinie pour un jeu qui tourne en continu
                    while (true)
                    {
                        if (_needsRefresh)
                        {
                            ctx.Refresh();
                            _needsRefresh = false;
                        }
                        await Task.Delay(50);
                    }
                });
        }

        //fonction de desk(on vien update le layout desk) pour faire l'affichange des carte spécila/panel avec la selection en liste
        public static void RefreshDesk()
        {
            // Appel de l'autre classe pour récupérer le contenu
            var deckLa = DeckFactory.CreateDeskLayout();

            // Mise à jour du layout
            GameHUD["Left"]["desk"].Update(deckLa);

            // Marquer pour rafraîchissement lors du prochain cycle
            _needsRefresh = true;
        }
        public static void RefrshInfo()
        {

            _needsRefresh = true;
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

            // 2. Création de la carte (Popup)
            var scoreCardVisual = CardFactory.CreateCardLayout(scoreValue);

            // 4. Affichage de la carte
            viewLayout.Update(scoreCardVisual);
            _needsRefresh = true;

            // 5. Attente
            await Task.Delay(3000);

            // 6. Restauration
            var defaultView = new Panel(" ") { Expand = true };
            viewLayout.Update(defaultView);
            _needsRefresh = true;
        }

    }    
}