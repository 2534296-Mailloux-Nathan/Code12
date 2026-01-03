using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace Code12Game.Display
{
    internal static class CardFactory
    {
       

        public static Panel CreateCardLayout(int scoreCard)
        {
            Panel panel = ElementsGraphiques.CarteScore(scoreCard);
            return panel;
        }
    }
}
