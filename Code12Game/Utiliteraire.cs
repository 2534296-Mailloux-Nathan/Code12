namespace Code12Game
{
    public static class Utiliteraire
    {
        public static string ChargerTexture(string cheminFichier)
        {
            // Logique de chargement de texture ici
            return "Texture chargée à partir de " + cheminFichier;
        }
        public static void ForcePleinEcran(bool debug)
        {
            while (Console.WindowHeight < 37 || Console.WindowWidth < 133)
            {
                Console.WriteLine("Appuyez sur \x1b[1;35mF11\x1b[0m pour mettre le terminal en plein écran.");
                if (debug)
                {
                    Console.WriteLine($"Hauteur de la fenêtre : {Console.WindowHeight} , Largeur de la fenêtre : {Console.WindowWidth}");
                }
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
            }

            if (debug)
            {
                Console.SetCursorPosition(0, 0); for (int i = 0; i < 133; i++) Console.Write("\x1b[1;35m*\x1b[0m");
                Console.SetCursorPosition(1, 2); Console.WriteLine($"Le terminal est maintenant en plein écran.\n\"Hauteur de la fenêtre : {Console.WindowHeight} , Largeur de la fenêtre : {Console.WindowWidth}");
                Console.SetCursorPosition(0, 36); for (int i = 0; i < 133; i++) Console.Write("\x1b[1;35m*\x1b[0m");
                for (int i = 1; i < 36; i++)
                {
                    Console.SetCursorPosition(0, i); Console.Write("\x1b[1;35m*\x1b[0m");
                    Console.SetCursorPosition(132, i); Console.Write("\x1b[1;35m*\x1b[0m");
                }
                Console.ReadKey();
            }
        }
        public static void ForcePleinEcran()
        {
            while (Console.WindowHeight < 37 || Console.WindowWidth < 133)
            {
                Console.WriteLine("Appuyez sur \x1b[1;35mF11\x1b[0m pour mettre le terminal en plein écran.");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
            }
        }
    }
}