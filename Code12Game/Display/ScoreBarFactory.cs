using Spectre.Console;
using Code12Data;

namespace Code12Game.Display
{
    /// <summary>
    /// Factory pour créer l'affichage des barres de score du joueur et de l'adversaire
    /// </summary>
    public static class ScoreBarFactory
    {
        private const int MaxScore = 12; // Score maximum dans le jeu
        private const int BarBlocks = 12; // Nombre de blocs dans la barre
        private const int BarHeight = 3;

        /// <summary>
        /// Crée un layout avec les deux barres de score (joueur et adversaire)
        /// </summary>
        /// <returns>Panel contenant les barres de score</returns>
        public static Panel CreateScoreBarsLayout()
        {
            byte scoreJoueur = GameData.ObtenirScoreJoueur();
            byte scoreAdversaire = GameData.ObtenirScoreAdversaire();

            var table = new Table()
                .Border(TableBorder.None)
                .HideHeaders()
                .Centered()
                .AddColumn(new TableColumn(""));

            // Barre du joueur
            string playerBar = CreateScoreBar(scoreJoueur, Color.Green, "Joueur");
            table.AddRow(new Markup(playerBar));

            // Espacement
            table.AddRow(new Markup(" "));

            // Barre de l'adversaire
            string enemyBar = CreateScoreBar(scoreAdversaire, Color.Red, "Adversaire");
            table.AddRow(new Markup(enemyBar));

            return new Panel(table)
            {
                Header = new PanelHeader("[bold yellow]SCORE[/]", Justify.Center),
                Border = BoxBorder.Rounded,
                BorderStyle = new Style(Color.Yellow),
                Expand = true
            };
        }

        /// <summary>
        /// Crée une barre de score individuelle avec effet de remplissage
        /// </summary>
        /// <param name="score">Score actuel</param>
        /// <param name="color">Couleur de la barre</param>
        /// <param name="label">Label de la barre (Joueur/Adversaire)</param>
        /// <returns>String formatée avec markup Spectre.Console</returns>
        private static string CreateScoreBar(byte score, Color color, string label)
        {
            // Calculer le nombre de blocs remplis (max 12)
            int filledBlocks = score > MaxScore ? MaxScore : score;
            
            // Construire les lignes de la barre
            string topBorder = "X" + new string('X', (BarBlocks * 3) + (BarBlocks - 1)) + "X";
            string emptyBar = BuildBarLine(filledBlocks, color);
            string bottomBorder = topBorder;

            // Assembler avec le label et le score
            string scoreDisplay = score > MaxScore 
                ? $"[bold {color}]{score}[/] [red](!)[/]/[dim]{MaxScore}[/]" 
                : $"[bold {color}]{score}[/]/[dim]{MaxScore}[/]";
            
            // Centrer le label au-dessus de la barre
            string labelLine = $"[bold]{label}[/] - {scoreDisplay}";
                
            return $"{labelLine}\n" +
                   $"{topBorder}\n" +
                   $"{emptyBar}\n" +
                   $"{emptyBar}\n" +
                   $"{emptyBar}\n" +
                   $"{bottomBorder}";
        }

        /// <summary>
        /// Construit une ligne de la barre avec des blocs remplis et vides
        /// </summary>
        /// <param name="filledCount">Nombre de blocs à remplir (0-12)</param>
        /// <param name="color">Couleur des blocs remplis</param>
        /// <returns>Ligne formatée de la barre</returns>
        private static string BuildBarLine(int filledCount, Color color)
        {
            var line = "X";
            
            for (int i = 0; i < BarBlocks; i++)
            {
                // Ajouter un séparateur entre chaque bloc
                if (i > 0)
                {
                    line += "X";
                }
                
                if (i < filledCount)
                {
                    line += $"[{color}]███[/]"; // Bloc rempli (3 caractères pour plus de visibilité)
                }
                else
                {
                    line += "   "; // Espace vide (3 caractères)
                }
            }
            
            line += "X";
            return line;
        }

        /// <summary>
        /// Crée une version simplifiée avec des barres de progression Spectre.Console
        /// </summary>
        /// <returns>Panel avec barres de progression</returns>
        public static Panel CreateSimpleScoreBarsLayout()
        {
            byte scoreJoueur = GameData.ObtenirScoreJoueur();
            byte scoreAdversaire = GameData.ObtenirScoreAdversaire();

            var grid = new Grid()
                .AddColumn(new GridColumn().Width(20))
                .AddColumn(new GridColumn().Width(40));

            // Barre du joueur
            var playerBar = new BarChart()
                .Width(60)
                .Label("[bold green]Joueur[/]")
                .CenterLabel()
                .AddItem("Score", scoreJoueur, Color.Green);

            grid.AddRow(
                new Markup($"[bold]Joueur:[/]"),
                new Markup($"[green]{scoreJoueur}[/]/[dim]{MaxScore}[/]")
            );
            grid.AddRow(
                new Text(""),
                playerBar
            );

            // Barre de l'adversaire
            var enemyBar = new BarChart()
                .Width(60)
                .Label("[bold red]Adversaire[/]")
                .CenterLabel()
                .AddItem("Score", scoreAdversaire, Color.Red);

            grid.AddRow(
                new Markup($"[bold]Adversaire:[/]"),
                new Markup($"[red]{scoreAdversaire}[/]/[dim]{MaxScore}[/]")
            );
            grid.AddRow(
                new Text(""),
                enemyBar
            );

            return new Panel(grid)
            {
                Header = new PanelHeader("[bold yellow]SCORE[/]"),
                Border = BoxBorder.Rounded,
                BorderStyle = new Style(Color.Yellow),
                Expand = true
            };
        }
    }
}
