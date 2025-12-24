using Code12Data;
using Spectre.Console;

namespace Code12Game
{

    public static class ElementsGraphiques
    {
        public static Panel CarteSpeciale(CarteSpeciale carte)
        {


            var panel = new Panel($"Type: {carte.Type}\nMana: {carte.ManaUse}\nDescription: {carte.Descrition}")
            {
                Border = BoxBorder.Square,
                Header = new PanelHeader(carte.Nom, Justify.Center),

            };

            // donne une couleur au bord du panel selon le type de carte
            switch ((int)carte.Type)
            {
                case 0://attaque
                    {
                        panel.BorderColor(Color.Red);
                        break;
                    }
                case 1://defense
                    {
                        panel.BorderColor(Color.Green);
                        break;
                    }
                case 2://soin
                    {
                        panel.BorderColor(Color.Purple);
                        break;
                    }
                case 3://magie
                    {
                        panel.BorderColor(Color.Yellow);
                        break;
                    }
                default: // de base
                    {
                        panel.BorderColor(Color.White);
                        break;
                    }

            }
            return panel;
        }

        // carte de type score qui Aumente le score du joueur. la carte n'a pas de cout de mana et est seulemnet une carte avec un chiffre entre 0 et 6 qui represente l'augmentation du score qui sera appliqué au joueur elle sera contrus avec des car donc un quantre sera afficher en digi comme une horloge
        public static Panel CarteScore(int scoreValue)
        {
            string scoreDigi = scoreValue switch
            {
                0 => "\r\n\r\n  __  \r\n /  \\ \r\n| () |\r\n \\__/ \r\n\r\n",
                1 => "\r\n\r\n  _ \r\n / |\r\n | |\r\n |_|\r\n\r\n",
                2 => "\r\n\r\n ___ \r\n|_  )\r\n / / \r\n/___|\r\n\r\n",
                3 => "\r\n\r\n ____\r\n|__ /\r\n |_ \\\r\n|___/\r\n\r\n",
                4 => "\r\n\r\n _ _  \r\n| | | \r\n|_  _|\r\n  |_| \r\n\r\n",
                5 => "\r\n\r\n ___ \r\n| __|\r\n|__ \\\r\n|___/\r\n\r\n",
                6 => "\r\n\r\n  __ \r\n / / \r\n/ _ \\\r\n\\___/\r\n\r\n",
                _ => "?"
            };
            var panel = new Panel($"[bold yellow]{scoreDigi}[/]") // le chiffre dois etre centrer dans le panel
            {
                Border = BoxBorder.Square,
                Width = 14,
                Padding = new Padding(3, 3, 0, 0)

            };
            panel.BorderColor(Color.Gold1);
            return panel;
        }
    }
}
