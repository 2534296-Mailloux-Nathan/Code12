using Code12Data;
using Code12Game;
using Code12Game.Display;
using Code12Game.Input;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Code12Logic
{
    /// <summary>
    /// Contient la logique principale du jeu (cycle, score, combat).
    /// </summary>
    public static partial class Code12LogicGame
    {
        #region Cycle de jeu
        /// <summary>
        /// Initialise et démarre une partie.
        /// </summary>
        public static async Task StartGame()
        {
            Utiliteraire.ForcePleinEcran();
            AnsiConsole.Clear();

            // Initialisation de l'affichange du jeu
            AnsiConsole.MarkupLine("[bold green]Bienvenue dans Code12Game![/]");
            await Task.Delay(1000);
            AnsiConsole.Clear();

            // Initialiser le contrôleur d'entrée
            var inputController = new InputController();
            inputController.LoadSettings("config/input_settings.json");

            var inputHandler = new InputHandler(inputController);

            // Lancer le HUD dans une tâche indépendante
            _ = Task.Run(() => Affichange.InitializeHUD());

            // Attendre que le HUD soit initialisé
            await Task.Delay(200);

            Console.ReadKey(false); // Attendre une touche pour continuer

            GameData.initialiserCartesSpecialesDebug();
            //Affichange.RefreshDesk(); // Rafraîchir l'affichage après modification


            //Exemple d'intégration dans une boucle de jeu
            while (true)
            {
                inputHandler.Update();
                await Task.Delay(16); // ~60 FPS
            }

        }

        /// <summary>
        /// Exécute un tour de jeu (pioche, actions, résolution).
        /// </summary>
        public static void PlayTurn()
        {
            //verification 

            
        }

        /// <summary>
        /// Termine la partie et applique les états de fin de jeu.
        /// </summary>
        public static void EndGame()
        {
            // Logique de fin de jeu
        }
        #endregion

        #region Gestion du score
        /// <summary>
        /// Pioche une carte de score pour le joueur et applique le score.
        /// </summary>
        public static void PigerCarteScoreJoueur()
        {
            byte cartePigee = 0;
            cartePigee = GameData.PiocherCarteScoreJoueur();
            GameData.AjouterScoreJoueur(cartePigee);
            if(VerifierScoreJoueur())
            {
                //le joueur na pas depasser 12
            }
            else
            {
                //le joueur a depasser 12
            }
        }

        /// <summary>
        /// Pioche une carte de score pour l'adversaire et applique le score.
        /// </summary>
        private static void PigerCarteScoreAdversaire(out byte cartePigee)
        {
            cartePigee = GameData.PiocherCarteScoreAdversaire();
            GameData.AjouterScoreAdversaire(cartePigee);
        }

        /// <summary>
        /// Vérifie que le joueur n'a pas dépassé 12.
        /// </summary>
        private static bool VerifierScoreJoueur()
        {
            return GameData.ObtenirScoreJoueur() <= 12;
        }

        /// <summary>
        /// Vérifie que l'adversaire n'a pas dépassé 12.
        /// </summary>
        private static bool VerifierScoreAdversaire()
        {
            return GameData.ObtenirScoreAdversaire() <= 12;
        }

        /// <summary>
        /// Vérifie l'égalité des scores.
        /// </summary>
        private static bool VerifierEgaliteScores()
        {
            return GameData.ObtenirScoreJoueur() == GameData.ObtenirScoreAdversaire();
        }

        /// <summary>
        /// Détermine si le joueur a un score supérieur à l'adversaire.
        /// </summary>
        private static bool VerifierJoueurGagne()
        {
            return GameData.ObtenirScoreJoueur() > GameData.ObtenirScoreAdversaire();
        }
        #endregion

        #region Gestion du combat
        /// <summary>
        /// Calcule la puissance d'attaque et applique les effets (dégâts ou gain de mana).
        /// </summary>
        public static void PuissanceAttaque(out int puissanceAct)
        {
            if (VerifierEgaliteScores())
            {
                // Égalité : les deux gagnent du mana, aucun dégât
                GameData.AjouterManaJoueur(5);
                GameData.AjouterManaAdversaire(5);
                puissanceAct = 0;
            }
            else if (VerifierJoueurGagne())
            {
                puissanceAct = GameData.ObtenirScoreJoueur() - GameData.ObtenirScoreAdversaire();
                GameData.RetirerPvAdversaire(puissanceAct);

                if (GameData.ObtenirPvAdversaire() == 0)
                {
                    // Logique si l'adversaire est à 0 PV
                }
            }
            else
            {
                puissanceAct = GameData.ObtenirScoreAdversaire() - GameData.ObtenirScoreJoueur();
                GameData.RetirerPvJoueur(puissanceAct);
                if (GameData.ObtenirPvJoueur() == 0)
                {
                    // Logique si le joueur est à 0 PV
                }
            }
        }
        #endregion
    }
}
