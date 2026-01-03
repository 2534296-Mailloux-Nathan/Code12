using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code12Data;


namespace Code12Logic
{
    /// <summary>
    /// Contient la logique principale du jeu (cycle, score, combat).
    /// </summary>
    public partial class Code12LogicGame
    {
        #region Cycle de jeu
        /// <summary>
        /// Initialise et démarre une partie.
        /// </summary>
        public void StartGame()
        {
            // Logique de démarrage du jeu
        }

        /// <summary>
        /// Exécute un tour de jeu (pioche, actions, résolution).
        /// </summary>
        public void PlayTurn()
        {
            // Logique pour jouer un tour
            // logique temporaire pour piger les cartes de score et gérer le score Test
        }

        /// <summary>
        /// Termine la partie et applique les états de fin de jeu.
        /// </summary>
        public void EndGame()
        {
            // Logique de fin de jeu
        }
        #endregion

        #region Gestion du score
        /// <summary>
        /// Pioche une carte de score pour le joueur et applique le score.
        /// </summary>
        private void PigerCarteScoreJoueur(out byte cartePigee)
        {
            cartePigee = GameData.PiocherCarteScoreJoueur();
            GameData.AjouterScoreJoueur(cartePigee);
        }

        /// <summary>
        /// Pioche une carte de score pour l'adversaire et applique le score.
        /// </summary>
        private void PigerCarteScoreAdversaire(out byte cartePigee)
        {
            cartePigee = GameData.PiocherCarteScoreAdversaire();
            GameData.AjouterScoreAdversaire(cartePigee);
        }

        /// <summary>
        /// Vérifie que le joueur n'a pas dépassé 12.
        /// </summary>
        private bool VerifierScoreJoueur()
        {
            return GameData.ObtenirScoreJoueur() <= 12;
        }

        /// <summary>
        /// Vérifie que l'adversaire n'a pas dépassé 12.
        /// </summary>
        private bool VerifierScoreAdversaire()
        {
            return GameData.ObtenirScoreAdversaire() <= 12;
        }

        /// <summary>
        /// Vérifie l'égalité des scores.
        /// </summary>
        private bool VerifierEgaliteScores()
        {
            return GameData.ObtenirScoreJoueur() == GameData.ObtenirScoreAdversaire();
        }

        /// <summary>
        /// Détermine si le joueur a un score supérieur à l'adversaire.
        /// </summary>
        private bool VerifierJoueurGagne()
        {
            return GameData.ObtenirScoreJoueur() > GameData.ObtenirScoreAdversaire();
        }
        #endregion

        #region Gestion du combat
        /// <summary>
        /// Calcule la puissance d'attaque et applique les effets (dégâts ou gain de mana).
        /// </summary>
        public void PuissanceAttaque(out int puissanceAct)
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
