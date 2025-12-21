using Spectre.Console;

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

        // Fonction de débogage pour afficher les ratios de tous les layouts
        public static void DebugLayout()
        {
            Console.WriteLine(GameHUD.GetLayout("Left").GetLayout("view").Ratio+" view");
            Console.WriteLine(GameHUD.GetLayout("Left").GetLayout("desk").Ratio+" desk");
            Console.WriteLine(GameHUD.GetLayout("info").Ratio+" info");

        }
        //fonction de desk(on vien update le layout desk) pour faire l'affichange des carte spécila/panel avec la selection en liste
        public static void UpdateDesk(Layout layout)
        {
            GameHUD.GetLayout("Left").Update(layout);
        }

    }
}