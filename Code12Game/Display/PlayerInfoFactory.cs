using Spectre.Console;
using System;

namespace Code12Game.Display
{
    public static class PlayerInfoFactory
    {

        public static Panel CreatePlayerInfoLayoutWithProgressBars(int currentHealth, int maxHealth, int currentMana, int maxMana)
        {
            // Calcul des pourcentages
            double healthPercent = maxHealth > 0 ? (double)currentHealth / maxHealth * 100 : 0;
            double manaPercent = maxMana > 0 ? (double)currentMana / maxMana * 100 : 0;

            // Barre de santé
            var healthBar = new BreakdownChart()
            {
                Width = 40,
                ShowTags = false
            };
            healthBar.Data.Add(new BreakdownChartItem("Pv", currentHealth, Color.FromHex(GetHealthColorHex(healthPercent))));
            
            
            

            // Barre de mana
            var manaBar = new BreakdownChart()
            {
                Width = 40,
                ShowTags = false
            };
            manaBar.Data.Add(new BreakdownChartItem("Mana", currentMana, Color.Blue));
            
            
            // Création du contenu
            var content = new Rows(

                new Markup($"[red bold]❤ Points de Vie[/]"),
                healthBar,
                new Markup($"  [{GetHealthColor(healthPercent)}]{currentHealth}[/] / [grey]{maxHealth}[/]"),
               
                new Markup($"[blue bold]✦ Mana[/]"),
                manaBar,
                new Markup($"  [blue]{currentMana}[/] / [grey]{maxMana}[/]")
            );
            
            var panel = new Panel(content)
            {
                Header = new PanelHeader("═══ Statut ═══"),
                Border = BoxBorder.Double,
                Expand = true
            };
            
            return panel;
        }
        
        // Méthode helper pour déterminer la couleur selon le pourcentage de santé
        private static string GetHealthColor(double healthPercent)
        {
            if (healthPercent >= 70) return "green";
            if (healthPercent >= 40) return "yellow";
            if (healthPercent >= 20) return "orange1";
            return "red";
        }

        private static string GetHealthColorHex(double healthPercent)
        {
            if (healthPercent >= 70) return "#00FF00"; // vert
            if (healthPercent >= 40) return "#FFFF00"; // jaune
            if (healthPercent >= 20) return "#FFA500"; // orange
            return "#FF0000"; // rouge
        }
    }
}