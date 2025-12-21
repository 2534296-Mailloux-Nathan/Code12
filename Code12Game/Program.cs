using Code12Game;
using Spectre.Console;
using System;
using System.Text;
using System.Threading;

namespace Code12Game
{
   
    public class Program
    {
        
        public static void Main(string[] args)
        {
            Utiliteraire.ForcePleinEcran(true);
            AnsiConsole.Write(Affichange.GameHUD);
            


        }

      
        
    }
}