using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code12Data;


namespace Code12Logic
{
    public partial class Code12LogicGame
    {
        public void StartGame()
        {
            // Logique de démarrage du jeu
        }

        public void PlayTurn()
        {
            // Logique pour jouer un tour
            // logique temporaire pour piger les cartes de score et gérer le score Test
        }

        public void EndGame()
        {
            // Logique de fin de jeu
        }

        #region score handling
        // logique pour la pige des cartes de score
        private void PigerCarteScoreJoueur(out byte cartePigee)
        {
            cartePigee = GameData.PiocherCarteScoreJoueur();
            GameData.AjouterScoreJoueur(cartePigee);

        }
        private void PigerCarteScoreAdversaire(out byte cartePigee)
        {
            cartePigee = GameData.PiocherCarteScoreAdversaire();
            GameData.AjouterScoreAdversaire(cartePigee);
        }

        // logique pour gérer le score du joueur et de l'adversaire
        // si il a bust ou autre cas de figure

        /*
         The Attack Gauge is the main way that both you and your opponent will do combat.

By drawing from the Dealer Deck, you increase its number by the number listed on the card, and if at the end of the round your number is higher than your opponents, you attack once for every point of difference. In case of a draw, both fighters get bonus EP (usually 5).

Attack Gauges have, by default, have a maximum of 12. Going over this amount will cause the unlucky party to Overcharge, prematurely forcing damage calculation, and returning their number to the previous one before they Overcharged.

If a character that is able to perform Criticals lands exactly 12, the Attack Gauge will glow bright yellow, and force them to stand. If they win this round, their last strike is a Critical, dealing bonus damage. In addition, if their opponent Overcharges, they instead do a Super Attack, which also ignores armor.

Your opponent's Attack Gauge usually has some red bars going from the right to left. Those are the positions where your opponent can (and will) stand; otherwise they must keep dealing from their Dealer Deck. This can be used to your advantage - if you are able to inflict Unbalanced on the enemy (with a card such as Kick) they will Overcharge if they draw anything above a 2.

The Dealer Deck is your main method of manipulating the Attack Gauge, although both sides of the combat play Modifiers to attempt to win the round. 
         */
        private bool VerifierScoreJoueur()
        {
            return GameData.ObtenirScoreJoueur() <= 12;
        }
        private bool VerifierScoreAdversaire()
        {
            return GameData.ObtenirScoreAdversaire() <= 12;
        }
        private bool VerifierEgaliteScores()
        {
            return GameData.ObtenirScoreJoueur() == GameData.ObtenirScoreAdversaire();
        }
        private bool VerifierJoueurGagne()
        {
            return GameData.ObtenirScoreJoueur() > GameData.ObtenirScoreAdversaire();
        }
        #endregion

        #region Cobat handling
        // logique pour gérer le combat : le qui gagne, les degats, si c une null, le mana regagnier si null ect
        public void PuissanceAttaque(out int puissanceAct)
        {
            // Logique de calcul de la puissance d'attaque
            if (VerifierEgaliteScores())
            {
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
