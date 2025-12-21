using Code12Data;
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
            Console.WriteLine(GameHUD.GetLayout("Left").GetLayout("view").Ratio + " view");
            Console.WriteLine(GameHUD.GetLayout("Left").GetLayout("desk").Ratio + " desk");
            Console.WriteLine(GameHUD.GetLayout("info").Ratio + " info");

        }

        //fonction qui crée le layout desk avec les cartes spéciales en liste
        public static Layout CreateDeskLayout()
        {
            var deskLayout = new Layout("desk").Ratio(2);
            var cartesSpeciales = GameData.ObtenirCartesSpeciales();
            var panelList = new Panel("[bold underline]Cartes Spéciales[/]")
            {
                Border = BoxBorder.Rounded,
                Header = new PanelHeader("Inventaire", Justify.Center)
            };
            var table = new Table().NoBorder();
            table.AddColumn("Cartes Spéciales"); // optionnelle selon besoin
            foreach (var carte in cartesSpeciales)
            {
                table.AddColumn("");
            }
            // Construire une ligne contenant toutes les cartes
            var cellules = cartesSpeciales.Select(c => ElementsGraphiques.CarteSpeciale(c)).ToArray();
            table.AddRow(cellules);
            panelList = new Panel(table)
            {
                Border = BoxBorder.Double,
                Header = new PanelHeader("Inventaire", Justify.Center),
                Expand = true
            };
            deskLayout.Update(panelList);
            return deskLayout;
        }

        //fonction de desk(on vien update le layout desk) pour faire l'affichange des carte spécila/panel avec la selection en liste
        public static void UpdateDesk(Layout layout)
        {

            GameHUD.GetLayout("Left").GetLayout("desk").Update(layout);
        }

    }
    // class contenant les éléments graphiques comme les panneaux, les cartes spéciales, etc.
    public static class ElementsGraphiques
    {
        public static Panel CarteSpeciale(CarteSpeciale carte)
        {


            var panel = new Panel($"Type: {carte.Type}\nMana: {carte.ManaUse}\nDescription: {carte.Descrition}")
            {
                Border = BoxBorder.Square,
                Header = new PanelHeader(carte.Nom, Justify.Center),

            };
            switch ((int)carte.Type)
            {
                case 0:
                    {
                        panel.BorderColor(Color.Red);
                        break;
                    }
                case 1:
                    {
                        panel.BorderColor(Color.Green);
                        break;
                    }
                case 2:
                    {
                        panel.BorderColor(Color.Purple);
                        break;
                    }
                case 3:
                    {
                        panel.BorderColor(Color.Yellow);
                        break;
                    }

            }
            return panel;
        }
    }
}