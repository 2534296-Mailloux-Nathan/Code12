namespace Code12Data
{
    public enum TypeCarte
    {
        Attaque,
        Defense,
        Soin,
        Magie,
        Score
    }
    // Classe représentant une carte spéciale dans le jeu
    public class CarteSpeciale
    {
        public string Nom { get; set; }
        public TypeCarte Type { get; set; }
        public int ManaUse { get; set; }
        public string Descrition { get; set; }
        public CarteSpeciale(string nom, TypeCarte type, int manaUse, string descrition)
        {
            Nom = nom;
            Type = type;
            ManaUse = manaUse;
            Descrition = descrition;
        }
    }
    public static partial class GameData // Partie de la classe GameData pour les variables et propriétés
    {
        

        //liste représentant les cartes spéciales dans l'inventaire du joueur
        private static List<CarteSpeciale> InventaireCartesSpeciales { get; set; } = new List<CarteSpeciale>();

        //listes representant les cartes de score restantes dans le desk pour le joueur et l'adversaire
        private static List<byte> DeskCartesScoreJoueur { get; set; } = new List<byte>();
        private static List<byte> DeskCartesScoreAdversaire { get; set; } = new List<byte>();

        //deck de référence pour les cartes de score un deck est une pile de carte mélangée au début de la partie et quant un deck est vide on le remplit avec une nouvelle copie mélangée du deck de référence
        private static readonly byte[] Deck = {1,1,1,1,2,2,2,2,3,3,3,3,4,4,4,4,5,5,5,5,6,6,6,6}; //Deck is a pile of 24 cards, 4 of each number 1 trough 6.


        private static byte scoreJoueur; //le maximum de score est 12 donc byte est suffisant
        private static byte manaJoueur; //le maximum de mana est 100 donc byte est suffisant
        private static readonly int PvMaxJoueur = 100;
        private static int pvJoueur = PvMaxJoueur;

        private static byte scoreAdversaire; 
        private static byte manaAdversaire;
        private static readonly int PvMaxAdversaire = 100;
        private static int pvAdversaire = PvMaxAdversaire;

    }
    public static partial class GameData // Partie de la classe GameData pour les méthodes 
    {
        #region CartesSpeciales
        // Méthode pour ajouter une carte spéciale à l'inventaire
        public static void AjouterCarteSpeciale(CarteSpeciale carte)
        {
            InventaireCartesSpeciales.Add(carte);
        }
        // Méthode pour obtenir toutes les cartes spéciales dans l'inventaire
        public static List<CarteSpeciale> ObtenirCartesSpeciales()
        {
            return InventaireCartesSpeciales;
        }
        public static void RetirerCarteSpeciale(CarteSpeciale carte)
        {
            InventaireCartesSpeciales.Remove(carte);
        }
        public static void RetirerCarteSpeciale(int index)
        {
            if (index >= 0 && index < InventaireCartesSpeciales.Count)
            {
                InventaireCartesSpeciales.RemoveAt(index);
            }
        }
        #endregion

        #region cartesScore
        public static void InitialiserDeskCartesScore()
        {
            
            DeskCartesScoreJoueur = Deck.ToList();
            DeskCartesScoreAdversaire = Deck.ToList();
            MelangerDeskCartesScore(DeskCartesScoreJoueur);
            MelangerDeskCartesScore(DeskCartesScoreAdversaire);
        }
        public static void MelangerDeskCartesScore(List<byte> desk)
        {
            Random rng = new Random();
            // Mélanger la liste en utilisant linq
            var melange = desk.OrderBy(a => rng.Next()).ToList();
            desk.Clear();
            desk.AddRange(melange);
            if (desk.Count != 24)
            {
                throw new Exception("Erreur lors du mélange du deck de cartes de score.");
            }
        }
        public static byte PiocherCarteScoreJoueur()
        {
            if (DeskCartesScoreJoueur.Count == 0)
            {
                //Reinitialiser le deck
                DeskCartesScoreJoueur = Deck.ToList();
                MelangerDeskCartesScore(DeskCartesScoreJoueur);
            }
            byte carte = DeskCartesScoreJoueur[0];
            DeskCartesScoreJoueur.RemoveAt(0);
            return carte;
        }
        public static byte PiocherCarteScoreAdversaire()
        {
            if (DeskCartesScoreAdversaire.Count == 0)
            {
                //Reinitialiser le deck
                DeskCartesScoreAdversaire = Deck.ToList();
                MelangerDeskCartesScore(DeskCartesScoreAdversaire);
            }
            byte carte = DeskCartesScoreAdversaire[0];
            DeskCartesScoreAdversaire.RemoveAt(0);
            return carte;
        }
        #endregion

        #region ScoreManagement
        public static byte ObtenirScoreJoueur()
        {
            return scoreJoueur;
        }
        public static byte ObtenirScoreAdversaire()
        {
            return scoreAdversaire;
        }
        public static void AjouterScoreJoueur(byte score)
        {
            scoreJoueur += score;
        }
        public static void AjouterScoreAdversaire(byte score)
        {
            scoreAdversaire += score;
        }
        public static void ReinitialiserScores()
        {
            scoreJoueur = 0;
            scoreAdversaire = 0;
        }
        #endregion

        #region ManaManagement
        public static byte ObtenirManaJoueur()
        {
            return manaJoueur;
        }
        public static byte ObtenirManaAdversaire()
        {
            return manaAdversaire;
        }
        public static void AjouterManaJoueur(byte mana)
        {
            if (manaJoueur + mana > 100)
            {
                manaJoueur = 100;
            }
            else
            {
                manaJoueur += mana;
            }
        }
        public static void AjouterManaAdversaire(byte mana)
        {
            if (manaAdversaire + mana > 100)
            {
                manaAdversaire = 100;
            }
            else
            {
                manaAdversaire += mana;
            }
        }
        public static void ReinitialiserMana()
        {
            manaJoueur = 0;
            manaAdversaire = 0;
        }
        #endregion

        #region PvManagement
        public static int ObtenirPvJoueur()
        {
            return pvJoueur;
        }
        public static int ObtenirPvAdversaire()
        {
            return pvAdversaire;
        }
        public static void AjouterPvJoueur(int pv)
        {
            pvJoueur += pv;
            if (pvJoueur > PvMaxJoueur)
            {
                pvJoueur = PvMaxJoueur;
            }
        }
        public static void AjouterPvAdversaire(int pv)
        {
            pvAdversaire += pv;
            if (pvAdversaire > PvMaxAdversaire)
            {
                pvAdversaire = PvMaxAdversaire;
            }
        }
        public static void RetirerPvJoueur(int pv)
        {
            pvJoueur -= pv;
            if (pvJoueur < 0)
            {
                pvJoueur = 0;
            }
        }
        public static void RetirerPvAdversaire(int pv)
        {
            pvAdversaire -= pv;
            if (pvAdversaire < 0)
            {
                pvAdversaire = 0;
            }
        }
        public static void ReinitialiserPv()
        {
            pvJoueur = PvMaxJoueur;
            pvAdversaire = PvMaxAdversaire;
        }
        #endregion

        #region DebugFunctions
        public static void initialiserCartesSpecialesDebug()
        {
            AjouterCarteSpeciale(new CarteSpeciale("Boule de feu", TypeCarte.Attaque, 5, "Inflige des dégâts de feu"));
            AjouterCarteSpeciale(new CarteSpeciale("Bouclier magique", TypeCarte.Defense, 3, "Réduit les dégâts magiques"));
            AjouterCarteSpeciale(new CarteSpeciale("Soin rapide", TypeCarte.Soin, 4, "Restaure rapidement des points de vie"));
            AjouterCarteSpeciale(new CarteSpeciale("Éclair", TypeCarte.Magie, 6, "Inflige des dégâts électriques"));
            AjouterCarteSpeciale(new CarteSpeciale("Lame de glace", TypeCarte.Attaque, 5, "Inflige des dégâts de glace"));
            AjouterCarteSpeciale(new CarteSpeciale("Boule de feu", TypeCarte.Attaque, 5, "Inflige des dégâts de feu"));
            AjouterCarteSpeciale(new CarteSpeciale("Bouclier magique", TypeCarte.Defense, 3, "Réduit les dégâts magiques"));
            AjouterCarteSpeciale(new CarteSpeciale("Soin rapide", TypeCarte.Soin, 4, "Restaure rapidement des points de vie"));
            AjouterCarteSpeciale(new CarteSpeciale("Éclair", TypeCarte.Magie, 6, "Inflige des dégâts électriques"));
            AjouterCarteSpeciale(new CarteSpeciale("Lame de glace", TypeCarte.Attaque, 5, "Inflige des dégâts de glace"));
            
            // Rafraîchir l'affichage après l'initialisation
            Code12Game.Affichange.RefreshDesk();
        }
        #endregion
    }
}
