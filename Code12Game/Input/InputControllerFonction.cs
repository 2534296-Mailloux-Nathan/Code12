using Code12Data;
using Code12Game.Display;
using Code12Logic;

using System;

namespace Code12Game.Input
{
    /// <summary>
    /// Classe partielle contenant les fonctions métier du contrôleur d'entrée
    /// </summary>
    public partial class InputController
    {
        #region Desk State Handlers
        public void DefilerCartesVersLaDroite(int nombreDeCartes = 1)
        {
            // Fonction désactivée : l'inventaire est limité à 4 cartes sans défilement
            Affichange.RefreshDesk();
        }

        public void DefilerCartesVersLaGauche(int nombreDeCartes = 1)
        {
            // Fonction désactivée : l'inventaire est limité à 4 cartes sans défilement
            Affichange.RefreshDesk();
        }

        public void AllerAuDebutDesCartes()
        {
            // Fonction désactivée : l'inventaire est limité à 4 cartes sans défilement
            Affichange.RefreshDesk();
        }

        public void AllerALaFinDesCartes()
        {
            // Fonction désactivée : l'inventaire est limité à 4 cartes sans défilement
            Affichange.RefreshDesk();
        }

        private void HandleConfirmInDesk()
        {
            // TODO: Implémenter la logique de confirmation pour le Desk
            //elle actionne la carte selectionner* précision la selection de la carte est pour l'instant non implementer suit a son implementation future nouspourrons activer la carte selectionner

        }

        private void HandleBackInDesk()
        {
            //retour a info
            GameData.DefinirEtatActuel(InputState.Info);
        }
        #endregion

        #region Info State Handlers
        private void PicherUneCarteScore()
        {
            Code12LogicGame.PigerCarteScoreJoueur();



        }
        #endregion


        private void HandleBackInView()
        {
            GameData.DefinirEtatActuel(InputState.Desk);
        }
    }
}
