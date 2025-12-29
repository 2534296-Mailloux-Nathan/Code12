using Code12Data;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code12Game
{
    public class DeckFactory
    {
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
    }
}
