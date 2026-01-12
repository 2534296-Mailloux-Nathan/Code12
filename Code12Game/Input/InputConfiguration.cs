namespace Code12Game.Input
{
    /// <summary>
    /// Énumération des actions disponibles dans le jeu
    /// </summary>
    public enum GameAction
    {
        Up,
        Down,
        Left,
        Right,
        Confirm,
        Back,
        PiocherCarte,
        Exit
    }

    /// <summary>
    /// Énumération des états d'affichage possibles du jeu
    /// basée sur les layouts de Affichange
    /// </summary>
    public enum InputState
    {
        View,
        Desk,
        Info,
        MenuPrincipal//Écran d'accueil non implémenté TODO
    }
}
