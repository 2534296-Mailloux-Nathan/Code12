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
            new Layout("info")
            .Ratio(1)
            .SplitRows(
                new Layout("playerInfo").Size(8),
                new Layout("stateInfo").Ratio(6))
            );

        private static LiveDisplayContext? _liveContext;

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
                    RefrshInfo();
                    RefreshScoreBars();

                    // Garder le Live actif - boucle infinie pour un jeu qui tourne en continu
                    while (true)
                    {
                        ctx.Refresh();
                        await Task.Delay(100); // Rafraîchir toutes les 100ms
                    }
                });
        }

        //fonction de desk(on vien update le layout desk) pour faire l'affichange des carte spécila/panel avec la selection en tableau
        public static void RefreshDesk()
        {
            // Appel de l'autre classe pour récupérer le contenu
            var deckLa = DeckFactory.CreateDeskLayout();

            // Mise à jour du layout
            GameHUD["Left"]["desk"].Update(deckLa);
            ForceRefresh();
        }
        
        public static void RefrshInfo()
        {
            // Exemple avec des valeurs - remplacez par vos vraies données de joueur
            int currentHealth = GameData.ObtenirPvJoueur();
            int maxHealth = GameData.ObtenirPvMaxJoueur();
            int currentMana = GameData.ObtenirManaJoueur();
            
            
            // Utilisation de la factory pour créer l'affichange des infos joueur
            var playerInfoPanel = PlayerInfoFactory.CreatePlayerInfoLayoutWithProgressBars(
                currentHealth, 
                maxHealth, 
                currentMana,
                100

            );
            
            // Mise à jour du layout
            GameHUD["info"]["playerInfo"].Update(playerInfoPanel);
            ForceRefresh();
        }
        
        /// <summary>
        /// Actualise l'affichage des barres de score
        /// </summary>
        public static void RefreshScoreBars()
        {
            var scoreBarsPanel = ScoreBarFactory.CreateScoreBarsLayout();
            GameHUD["Left"]["view"].Update(scoreBarsPanel);
            ForceRefresh();
        }
        
        /// <summary>
        /// Force un rafraîchissement immédiat de l'affichage
        /// </summary>
        public static void ForceRefresh()
        {
            _liveContext?.Refresh();
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

            // 5. Attente
            await Task.Delay(3000);

            // 6. Restauration
            var defaultView = new Panel(" ") { Expand = true };
            viewLayout.Update(defaultView);
        }

    }    
}