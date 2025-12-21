using Spectre.Console;
using Code12Data;
namespace Code12Game
{

    public class Program
    {

        public static void Main(string[] args)
        {
            Utiliteraire.ForcePleinEcran(true);
            AnsiConsole.Clear();

            AnsiConsole.Write(Affichange.GameHUD);
            Console.ReadKey();
            Console.Clear();

            GameData.initialiserCartesSpecialesDebug();
            Affichange.UpdateDesk(Affichange.CreateDeskLayout());
            AnsiConsole.Write(Affichange.GameHUD);
            Console.ReadKey();



        }


    }
}