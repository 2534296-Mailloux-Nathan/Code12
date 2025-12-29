using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace Code12Game
{
    internal static class CardFactory
    {
       

        public static Panel CreateCardLayout(int scoreCard)
        {
            // Logique pour créer le layout de la carte de score
            var cardLayout = new Panel(new Text($"Score: {scoreCard}"));

            return cardLayout;
        }
    }
}
