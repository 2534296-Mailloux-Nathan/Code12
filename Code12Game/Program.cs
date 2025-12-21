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
            AnsiConsole.Clear();
            while (true)
            {
                AnsiConsole.Write(Affichange.GameHUD);
                Console.ReadKey();
                Console.Clear();
                Affichange.DebugLayout();


            }
        }
      
        
    }
}