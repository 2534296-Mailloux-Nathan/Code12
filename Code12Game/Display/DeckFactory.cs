using Code12Data;
using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code12Game.Display
{
    public class DeckFactory
    {
        //fonction qui crée le layout desk avec les cartes spéciales en liste
        public static Layout CreateDeskLayout()
        {
            var deskLayout = new Layout("desk").Ratio(2);
            var cartesSpeciales = GameData.ObtenirCartesSpeciales();
            
            var cartesAffichees = cartesSpeciales
                .Take(4)
                .ToList();
            
            var table = new Table().NoBorder();

            for (int i = 0; i < 4; i++)
            {
                table.AddColumn(new TableColumn($"{i+1}") { Alignment = Justify.Center });
            }
            
            var cellules = new List<IRenderable>();
            for (int i = 0; i < 4; i++)
            {
                if (i < cartesAffichees.Count)
                {
                    cellules.Add(ElementsGraphiques.CarteSpeciale(cartesAffichees[i]));
                }
                else
                {
                    cellules.Add(new Text(""));
                }
            }
            table.AddRow(cellules.ToArray());
            
            var panelList = new Panel(table)
            {
                Border = BoxBorder.Double,
                Header = new PanelHeader($"Carte Spécial", Justify.Center),
                Expand = true
            };
            deskLayout.Update(panelList);
            return deskLayout;
        }
    }
}
