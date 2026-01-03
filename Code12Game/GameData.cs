using Code12Game.Input;
using Code12Game.Display;
namespace Code12Data
{
	#region Enums
	/// <summary>
	/// Énumération des types de cartes disponibles dans le jeu
	/// </summary>
	public enum TypeCarte
	{
		Attaque,
		Defense,
		Soin,
		Magie,
		Score
	}
	#endregion

	#region Classes
	/// <summary>
	/// Classe représentant une carte spéciale dans le jeu
	/// </summary>
	public class CarteSpeciale
	{
		/// <summary>
		/// Nom de la carte
		/// </summary>
		public string Nom { get; set; }
		
		/// <summary>
		/// Type de la carte (Attaque, Defense, Soin, Magie, Score)
		/// </summary>
		public TypeCarte Type { get; set; }
		
		/// <summary>
		/// Coût en mana pour utiliser cette carte
		/// </summary>
		public int ManaUse { get; set; }
		
		/// <summary>
		/// Description des effets de la carte
		/// </summary>
		public string Description { get; set; }
		
		/// <summary>
		/// Constructeur pour créer une nouvelle carte spéciale
		/// </summary>
		public CarteSpeciale(string nom, TypeCarte type, int manaUse, string description)
		{
			Nom = nom;
			Type = type;
			ManaUse = manaUse;
			Description = description;
		}
	}
	#endregion
	
	#region Variables et Propriétés
	/// <summary>
	/// Classe statique partielle contenant les données du jeu (variables et propriétés)
	/// </summary>
	public static partial class GameData
	{
		#region Utilitaires
		private static readonly Random _random = new Random();
		#endregion
		
		#region Cartes Spéciales
		private static List<CarteSpeciale> InventaireCartesSpeciales { get; set; } = new List<CarteSpeciale>();
		private static List<CarteSpeciale> InventaireCartesSpecialesActif { get; set; } = new List<CarteSpeciale>();
        #endregion

        #region Cartes de Score
        private static List<byte> DeskCartesScoreJoueur { get; set; } = new List<byte>();
		private static List<byte> DeskCartesScoreAdversaire { get; set; } = new List<byte>();
		// Deck de référence (24 cartes: 4 exemplaires de chaque chiffre de 1 à 6)
		private static readonly byte[] Deck = {1,1,1,1,2,2,2,2,3,3,3,3,4,4,4,4,5,5,5,5,6,6,6,6};
		#endregion
		
		#region Affichage
		#endregion
		
		#region Statistiques Joueur
		private static byte scoreJoueur;
		private static byte manaJoueur;
		private static readonly int PvMaxJoueur = 100;
		private static int pvJoueur = PvMaxJoueur;
		#endregion
		
		#region Statistiques Adversaire
		private static byte scoreAdversaire;
		private static byte manaAdversaire;
		private static readonly int PvMaxAdversaire = 100;
		private static int pvAdversaire = PvMaxAdversaire;
		#endregion
	}
	#endregion
	
	#region Méthodes
	/// <summary>
	/// Classe statique partielle contenant les méthodes de manipulation des données du jeu
	/// </summary>
	public static partial class GameData
	{
		#region Gestion des Cartes Spéciales
		/// <summary>
		/// Ajoute une carte spéciale à l'inventaire actif du joueur
		/// </summary>
		/// <param name="carte">La carte à ajouter</param>
		public static void AjouterCarteSpeciale(CarteSpeciale carte)
		{
			InventaireCartesSpecialesActif.Add(carte);
		}
		
		/// <summary>
		/// Ajoute une carte spéciale à l'inventaire non actif (possédé) du joueur
		/// </summary>
		/// <param name="carte">La carte à ajouter</param>
		public static void AjouterCarteSpecialePossedee(CarteSpeciale carte)
		{
			InventaireCartesSpeciales.Add(carte);
		}
		
		/// <summary>
		/// Obtient la liste de toutes les cartes spéciales dans l'inventaire actif
		/// </summary>
		/// <returns>Liste des cartes spéciales actives</returns>
		public static List<CarteSpeciale> ObtenirCartesSpeciales()
		{
			return InventaireCartesSpecialesActif;
		}
		
		/// <summary>
		/// Obtient la liste de toutes les cartes spéciales dans l'inventaire possédé (non actif)
		/// </summary>
		/// <returns>Liste des cartes spéciales possédées</returns>
		public static List<CarteSpeciale> ObtenirCartesSpecialesPossedees()
		{
			return InventaireCartesSpeciales;
		}
		
		/// <summary>
		/// Retire une carte spéciale spécifique de l'inventaire actif
		/// </summary>
		/// <param name="carte">La carte à retirer</param>
		public static void RetirerCarteSpeciale(CarteSpeciale carte)
		{
			InventaireCartesSpecialesActif.Remove(carte);
		}
		
		/// <summary>
		/// Retire une carte spéciale spécifique de l'inventaire possédé (non actif)
		/// </summary>
		/// <param name="carte">La carte à retirer</param>
		public static void RetirerCarteSpecialePossedee(CarteSpeciale carte)
		{
			InventaireCartesSpeciales.Remove(carte);
		}
		
		/// <summary>
		/// Retire une carte spéciale de l'inventaire actif par son index
		/// </summary>
		/// <param name="index">L'index de la carte à retirer</param>
		public static void RetirerCarteSpeciale(int index)
		{
			if (index >= 0 && index < InventaireCartesSpecialesActif.Count)
			{
				InventaireCartesSpecialesActif.RemoveAt(index);
			}
		}
		
		/// <summary>
		/// Retire une carte spéciale de l'inventaire possédé (non actif) par son index
		/// </summary>
		/// <param name="index">L'index de la carte à retirer</param>
		public static void RetirerCarteSpecialePossedee(int index)
		{
			if (index >= 0 && index < InventaireCartesSpeciales.Count)
			{
				InventaireCartesSpeciales.RemoveAt(index);
			}
		}
		
		/// <summary>
		/// Pioche une carte de l'inventaire possédé vers l'inventaire actif
		/// </summary>
		/// <param name="index">L'index de la carte à piocher</param>
		/// <returns>True si la carte a été piochée, False sinon</returns>
		public static bool PiocherCarteSpeciale(int index)
		{
			if (index >= 0 && index < InventaireCartesSpeciales.Count)
			{
				CarteSpeciale carte = InventaireCartesSpeciales[index];
				InventaireCartesSpeciales.RemoveAt(index);
				InventaireCartesSpecialesActif.Add(carte);
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Pioche une carte de l'inventaire possédé vers l'inventaire actif
		/// </summary>
		/// <param name="carte">La carte à piocher</param>
		/// <returns>True si la carte a été piochée, False sinon</returns>
		public static bool PiocherCarteSpeciale(CarteSpeciale carte)
		{
			if (InventaireCartesSpeciales.Contains(carte))
			{
				InventaireCartesSpeciales.Remove(carte);
				InventaireCartesSpecialesActif.Add(carte);
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Remet une carte de l'inventaire actif vers l'inventaire possédé
		/// </summary>
		/// <param name="index">L'index de la carte à remettre</param>
		/// <returns>True si la carte a été remise, False sinon</returns>
		public static bool RemettreCarteSpeciale(int index)
		{
			if (index >= 0 && index < InventaireCartesSpecialesActif.Count)
			{
				CarteSpeciale carte = InventaireCartesSpecialesActif[index];
				InventaireCartesSpecialesActif.RemoveAt(index);
				InventaireCartesSpeciales.Add(carte);
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Remet une carte de l'inventaire actif vers l'inventaire possédé
		/// </summary>
		/// <param name="carte">La carte à remettre</param>
		/// <returns>True si la carte a été remise, False sinon</returns>
		public static bool RemettreCarteSpeciale(CarteSpeciale carte)
		{
			if (InventaireCartesSpecialesActif.Contains(carte))
			{
				InventaireCartesSpecialesActif.Remove(carte);
				InventaireCartesSpeciales.Add(carte);
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Échange une carte de l'inventaire possédé avec une carte de l'inventaire actif
		/// </summary>
		/// <param name="indexPossedee">L'index de la carte possédée à échanger</param>
		/// <param name="indexActif">L'index de la carte active à échanger</param>
		/// <returns>True si l'échange a été effectué, False sinon</returns>
		public static bool EchangerCarteSpeciale(int indexPossedee, int indexActif)
		{
			if (indexPossedee >= 0 && indexPossedee < InventaireCartesSpeciales.Count &&
			    indexActif >= 0 && indexActif < InventaireCartesSpecialesActif.Count)
			{
				CarteSpeciale cartePossedee = InventaireCartesSpeciales[indexPossedee];
				CarteSpeciale carteActive = InventaireCartesSpecialesActif[indexActif];
				
				InventaireCartesSpeciales[indexPossedee] = carteActive;
				InventaireCartesSpecialesActif[indexActif] = cartePossedee;
				
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Échange une carte de l'inventaire possédé avec une carte de l'inventaire actif
		/// </summary>
		/// <param name="cartePossedee">La carte possédée à échanger</param>
		/// <param name="carteActive">La carte active à échanger</param>
		/// <returns>True si l'échange a été effectué, False sinon</returns>
		public static bool EchangerCarteSpeciale(CarteSpeciale cartePossedee, CarteSpeciale carteActive)
		{
			if (InventaireCartesSpeciales.Contains(cartePossedee) && 
			    InventaireCartesSpecialesActif.Contains(carteActive))
			{
				InventaireCartesSpeciales.Remove(cartePossedee);
				InventaireCartesSpecialesActif.Remove(carteActive);
				
				InventaireCartesSpeciales.Add(carteActive);
				InventaireCartesSpecialesActif.Add(cartePossedee);
				
				return true;
			}
			return false;
		}
		#endregion
		
		#region Gestion des Cartes de Score
		/// <summary>
		/// Initialise les decks de cartes de score pour le joueur et l'adversaire
		/// </summary>
		public static void InitialiserDeskCartesScore()
		{
			DeskCartesScoreJoueur = Deck.ToList();
			DeskCartesScoreAdversaire = Deck.ToList();
			MelangerDeskCartesScore(DeskCartesScoreJoueur);
			MelangerDeskCartesScore(DeskCartesScoreAdversaire);
		}
		
		/// <summary>
		/// Mélange un deck de cartes de score en utilisant l'algorithme de Fisher-Yates
		/// </summary>
		/// <param name="desk">Le deck à mélanger</param>
		/// <exception cref="Exception">Lancée si le deck ne contient pas exactement 24 cartes après le mélange</exception>
		public static void MelangerDeskCartesScore(List<byte> desk)
		{
			int n = desk.Count;
			// Algorithme de Fisher-Yates pour un mélange optimal O(n)
			for (int i = n - 1; i > 0; i--)
			{
				int j = _random.Next(i + 1);
				// Échange les éléments à l'index i et j
				(desk[i], desk[j]) = (desk[j], desk[i]);
			}
			
			// Validation de l'intégrité du deck
			if (desk.Count != 24)
			{
				throw new Exception("Erreur lors du mélange du deck de cartes de score.");
			}
		}
		
		/// <summary>
		/// Pioche une carte de score du deck du joueur
		/// Si le deck est vide, il est réinitialisé et mélangé
		/// </summary>
		/// <returns>La valeur de la carte piochée (1-6)</returns>
		public static byte PiocherCarteScoreJoueur()
		{
			// Réinitialiser le deck s'il est vide
			if (DeskCartesScoreJoueur.Count == 0)
			{
				DeskCartesScoreJoueur = Deck.ToList();
				MelangerDeskCartesScore(DeskCartesScoreJoueur);
			}
			
			// Piocher la première carte
			byte carte = DeskCartesScoreJoueur[0];
			DeskCartesScoreJoueur.RemoveAt(0);
			return carte;
		}
		
		/// <summary>
		/// Pioche une carte de score du deck de l'adversaire
		/// Si le deck est vide, il est réinitialisé et mélangé
		/// </summary>
		/// <returns>La valeur de la carte piochée (1-6)</returns>
		public static byte PiocherCarteScoreAdversaire()
		{
			// Réinitialiser le deck s'il est vide
			if (DeskCartesScoreAdversaire.Count == 0)
			{
				DeskCartesScoreAdversaire = Deck.ToList();
				MelangerDeskCartesScore(DeskCartesScoreAdversaire);
			}
			
			// Piocher la première carte
			byte carte = DeskCartesScoreAdversaire[0];
			DeskCartesScoreAdversaire.RemoveAt(0);
			return carte;
		}
		#endregion
		
		#region Gestion du Score
		/// <summary>
		/// Obtient le score actuel du joueur
		/// </summary>
		/// <returns>Le score du joueur</returns>
		public static byte ObtenirScoreJoueur()
		{
			return scoreJoueur;
		}
		
		/// <summary>
		/// Obtient le score actuel de l'adversaire
		/// </summary>
		/// <returns>Le score de l'adversaire</returns>
		public static byte ObtenirScoreAdversaire()
		{
			return scoreAdversaire;
		}
		
		/// <summary>
		/// Ajoute des points au score du joueur
		/// Le score est plafonné à byte.MaxValue (255)
		/// </summary>
		/// <param name="score">Le nombre de points à ajouter</param>
		public static void AjouterScoreJoueur(byte score)
		{
			if (scoreJoueur + score > byte.MaxValue)
			{
				scoreJoueur = byte.MaxValue;
			}
			else
			{
				scoreJoueur += score;
			}
		}
		
		/// <summary>
		/// Ajoute des points au score de l'adversaire
		/// Le score est plafonné à byte.MaxValue (255)
		/// </summary>
		/// <param name="score">Le nombre de points à ajouter</param>
		public static void AjouterScoreAdversaire(byte score)
		{
			if (scoreAdversaire + score > byte.MaxValue)
			{
				scoreAdversaire = byte.MaxValue;
			}
			else
			{
				scoreAdversaire += score;
			}
		}
		
		/// <summary>
		/// Réinitialise les scores du joueur et de l'adversaire à 0
		/// </summary>
		public static void ReinitialiserScores()
		{
			scoreJoueur = 0;
			scoreAdversaire = 0;
		}
		#endregion
		
		#region Gestion du Mana
		/// <summary>
		/// Obtient le mana actuel du joueur
		/// </summary>
		/// <returns>Le mana du joueur</returns>
		public static byte ObtenirManaJoueur()
		{
			return manaJoueur;
		}
		
		/// <summary>
		/// Obtient le mana actuel de l'adversaire
		/// </summary>
		/// <returns>Le mana de l'adversaire</returns>
		public static byte ObtenirManaAdversaire()
		{
			return manaAdversaire;
		}
		
		/// <summary>
		/// Méthode utilitaire pour ajouter du mana avec un plafond de 100
		/// </summary>
		/// <param name="manaActuel">Le mana actuel</param>
		/// <param name="manaAjoute">Le mana à ajouter</param>
		/// <returns>La nouvelle valeur de mana (plafonnée à 100)</returns>
		private static byte AjouterManaAvecPlafond(byte manaActuel, byte manaAjoute)
		{
			if (manaActuel + manaAjoute > 100)
			{
				return 100;
			}
			return (byte)(manaActuel + manaAjoute);
		}
		
		/// <summary>
		/// Ajoute du mana au joueur
		/// Le mana est plafonné à 100
		/// </summary>
		/// <param name="mana">La quantité de mana à ajouter</param>
		public static void AjouterManaJoueur(byte mana)
		{
			manaJoueur = AjouterManaAvecPlafond(manaJoueur, mana);
		}
		
		/// <summary>
		/// Ajoute du mana à l'adversaire
		/// Le mana est plafonné à 100
		/// </summary>
		/// <param name="mana">La quantité de mana à ajouter</param>
		public static void AjouterManaAdversaire(byte mana)
		{
			manaAdversaire = AjouterManaAvecPlafond(manaAdversaire, mana);
		}
		
		/// <summary>
		/// Retire du mana au joueur
		/// </summary>
		/// <param name="mana">La quantité de mana à retirer</param>
		/// <returns>True si le mana a pu être retiré, False si le joueur n'a pas assez de mana</returns>
		public static bool RetirerManaJoueur(byte mana)
		{
			if (manaJoueur >= mana)
			{
				manaJoueur -= mana;
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Retire du mana à l'adversaire
		/// </summary>
		/// <param name="mana">La quantité de mana à retirer</param>
		/// <returns>True si le mana a pu être retiré, False si l'adversaire n'a pas assez de mana</returns>
		public static bool RetirerManaAdversaire(byte mana)
		{
			if (manaAdversaire >= mana)
			{
				manaAdversaire -= mana;
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Réinitialise le mana du joueur et de l'adversaire à 0
		/// </summary>
		public static void ReinitialiserMana()
		{
			manaJoueur = 0;
			manaAdversaire = 0;
		}
		#endregion
		
		#region Gestion des Points de Vie
		/// <summary>
		/// Obtient les points de vie actuels du joueur
		/// </summary>
		/// <returns>Les PV du joueur</returns>
		public static int ObtenirPvJoueur()
		{
			return pvJoueur;
		}
		
		/// <summary>
		/// Obtient les points de vie actuels de l'adversaire
		/// </summary>
		/// <returns>Les PV de l'adversaire</returns>
		public static int ObtenirPvAdversaire()
		{
			return pvAdversaire;
		}
		
		/// <summary>
		/// Ajoute des points de vie au joueur
		/// Les PV sont plafonnés au maximum (100)
		/// </summary>
		/// <param name="pv">La quantité de PV à ajouter</param>
		public static void AjouterPvJoueur(int pv)
		{
			pvJoueur += pv;
			if (pvJoueur > PvMaxJoueur)
			{
				pvJoueur = PvMaxJoueur;
			}
		}
		
		/// <summary>
		/// Ajoute des points de vie à l'adversaire
		/// Les PV sont plafonnés au maximum (100)
		/// </summary>
		/// <param name="pv">La quantité de PV à ajouter</param>
		public static void AjouterPvAdversaire(int pv)
		{
			pvAdversaire += pv;
			if (pvAdversaire > PvMaxAdversaire)
			{
				pvAdversaire = PvMaxAdversaire;
			}
		}
		
		/// <summary>
		/// Retire des points de vie au joueur
		/// Les PV ne peuvent pas descendre en dessous de 0
		/// </summary>
		/// <param name="pv">La quantité de PV à retirer</param>
		public static void RetirerPvJoueur(int pv)
		{
			pvJoueur -= pv;
			if (pvJoueur < 0)
			{
				pvJoueur = 0;
			}
		}
		
		/// <summary>
		/// Retire des points de vie à l'adversaire
		/// Les PV ne peuvent pas descendre en dessous de 0
		/// </summary>
		/// <param name="pv">La quantité de PV à retirer</param>
		public static void RetirerPvAdversaire(int pv)
		{
			pvAdversaire -= pv;
			if (pvAdversaire < 0)
			{
				pvAdversaire = 0;
			}
		}
		
		/// <summary>
		/// Réinitialise les points de vie du joueur et de l'adversaire à leur maximum
		/// </summary>
		public static void ReinitialiserPv()
		{
			pvJoueur = PvMaxJoueur;
			pvAdversaire = PvMaxAdversaire;
		}
		#endregion
		
		#region Fonctions de Debug
		/// <summary>
		/// Initialise l'inventaire avec des cartes spéciales pour le debug
		/// </summary>
		public static void initialiserCartesSpecialesDebug()
		{
			// Ajout de cartes de test pour le debug
			AjouterCarteSpeciale(new CarteSpeciale("1Boule de feu", TypeCarte.Attaque, 5, "Inflige des dégâts de feu"));
			AjouterCarteSpeciale(new CarteSpeciale("2Bouclier magique", TypeCarte.Defense, 3, "Réduit les dégâts magiques"));
			AjouterCarteSpeciale(new CarteSpeciale("3Soin rapide", TypeCarte.Soin, 4, "Restaure rapidement des points de vie"));
			AjouterCarteSpeciale(new CarteSpeciale("4Éclair", TypeCarte.Magie, 6, "Inflige des dégâts électriques"));
			AjouterCarteSpeciale(new CarteSpeciale("5Lame de glace", TypeCarte.Attaque, 5, "Inflige des dégâts de glace"));
			AjouterCarteSpeciale(new CarteSpeciale("6Boule de feu", TypeCarte.Attaque, 5, "Inflige des dégâts de feu"));
			AjouterCarteSpeciale(new CarteSpeciale("7Bouclier magique", TypeCarte.Defense, 3, "Réduit les dégâts magiques"));
			AjouterCarteSpeciale(new CarteSpeciale("8Soin rapide", TypeCarte.Soin, 4, "Restaure rapidement des points de vie"));
			AjouterCarteSpeciale(new CarteSpeciale("9Éclair", TypeCarte.Magie, 6, "Inflige des dégâts électriques"));
			AjouterCarteSpeciale(new CarteSpeciale("10Lame de glace", TypeCarte.Attaque, 5, "Inflige des dégâts de glace"));
			
			// Rafraîchir l'affichage après l'initialisation
			Affichange.RefreshDesk();
		}
		#endregion
	}
	#endregion
}
