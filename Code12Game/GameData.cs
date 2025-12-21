using System.Globalization;

namespace Code12Data
{
    public enum TypeCarte
    {
        Attaque,
        Defense,
        Soin,
        Magie
    }
    // Classe représentant une carte spéciale dans le jeu
    public class CarteSpeciale
    {
        public string Nom { get; set; }
        public TypeCarte Type { get; set; }
        public int ManaUse { get; set; }
        public string Descrition { get; set; }
        public CarteSpeciale(string nom, TypeCarte type, int manaUse,string descrition)
        {
            Nom = nom;
            Type = type;
            ManaUse = manaUse;
            Descrition = descrition;
        }
    }
    public static class GameData
    {
        // Classe pour stocker les données du jeu comme les cartes spéciales, les joueurs, etc.

        //liste représentant les cartes spéciales dans l'inventaire du joueur
        private static List<CarteSpeciale> InventaireCartesSpeciales { get; set; } = new List<CarteSpeciale>();

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
        }
    }
}
