# Issues trouvés dans Code12Game

Ce document liste les problèmes identifiés lors de la vérification du projet Code12Game.

## 🔴 Problèmes critiques (empêchent la compilation)

### Issue #1: Classe `CardFactory` manquante
**Priorité:** CRITIQUE  
**Fichier:** `Code12Game/Affichange.cs` (ligne 76)  
**Description:**  
La classe `CardFactory` est référencée dans la méthode `ShowScoreCardTemporarily()` mais n'existe pas dans le projet.

```csharp
// Ligne 76 dans Affichange.cs
var scoreCardVisual = CardFactory.CreateCardLayout(scoreValue);
```

**Impact:** Le projet ne compile pas.

**Solution suggérée:**  
Créer la classe `CardFactory` avec une méthode statique `CreateCardLayout(int scoreValue)` qui retourne un `Panel` représentant une carte de score, ou utiliser directement `ElementsGraphiques.CarteScore()` qui semble déjà implémenter cette fonctionnalité.

---

### Issue #2: Classe `DeckFactory` manquante
**Priorité:** CRITIQUE  
**Fichier:** `Code12Game/Affichange.cs` (ligne 55)  
**Description:**  
La classe `DeckFactory` est référencée dans la méthode `RefreshDesk()` mais n'existe pas dans le projet.

```csharp
// Ligne 55 dans Affichange.cs
var deckLa = DeckFactory.CreateDeskLayout();
```

**Impact:** Le projet ne compile pas.

**Solution suggérée:**  
Créer la classe `DeckFactory` avec une méthode statique `CreateDeskLayout()` qui retourne un `IRenderable` représentant l'affichage des cartes dans le deck.

---

## 🟡 Problèmes moyens (bugs potentiels)

### Issue #3: Faute de frappe dans le nom de propriété
**Priorité:** MOYENNE  
**Fichier:** `Code12Game/GameData.cs` (lignes 17, 23)  
**Description:**  
La propriété est nommée `Descrition` au lieu de `Description` dans la classe `CarteSpeciale`.

```csharp
// Ligne 17
public string Descrition { get; set; }

// Ligne 23
Descrition = descrition;
```

**Impact:** Incohérence de nommage, confusion possible pour les développeurs.

**Solution suggérée:**  
Renommer la propriété `Descrition` en `Description` et mettre à jour toutes les références (ligne 13 dans `TypeElementAffichange.cs` utilise déjà "Description" dans l'affichage, ce qui crée de la confusion).

---

## 🟢 Problèmes mineurs (améliorations suggérées)

### Issue #4: Nom de fichier et classe incohérent
**Priorité:** FAIBLE  
**Fichier:** `Code12Game/Affichange.cs`  
**Description:**  
Le nom du fichier et de la classe est `Affichange` au lieu de `Affichage` (orthographe correcte en français).

**Impact:** Faible - problème cosmétique mais peut causer de la confusion.

**Solution suggérée:**  
Renommer le fichier en `Affichage.cs` et la classe en `Affichage`, ou garder le nom actuel si c'est un choix délibéré.

---

### Issue #5: Code dupliqué dans la fonction debug
**Priorité:** FAIBLE  
**Fichier:** `Code12Game/GameData.cs` (lignes 238-247)  
**Description:**  
Dans `initialiserCartesSpecialesDebug()`, les mêmes 5 cartes sont ajoutées deux fois, ce qui semble être une duplication accidentelle.

```csharp
// Les mêmes cartes sont ajoutées deux fois
AjouterCarteSpeciale(new CarteSpeciale("Boule de feu", TypeCarte.Attaque, 5, "Inflige des dégâts de feu"));
AjouterCarteSpeciale(new CarteSpeciale("Bouclier magique", TypeCarte.Defense, 3, "Réduit les dégâts magiques"));
// ... (répété deux fois)
```

**Impact:** Cartes en double dans l'inventaire lors du debug.

**Solution suggérée:**  
Supprimer les lignes dupliquées (243-247) ou documenter si c'est intentionnel.

---

### Issue #6: Méthode `ForcePleinEcran` avec paramètre non utilisé
**Priorité:** FAIBLE  
**Fichier:** `Code12Game/Utiliteraire.cs` (ligne 10)  
**Description:**  
La méthode `ForcePleinEcran(bool debug)` accepte un paramètre `debug` qui n'est jamais utilisé dans le corps de la méthode.

**Impact:** Code mort, confusion possible.

**Solution suggérée:**  
Soit utiliser le paramètre `debug` pour activer/désactiver des fonctionnalités de debug, soit supprimer le paramètre.

---

## Résumé des erreurs de compilation

```
Build FAILED.

/home/runner/work/Code12/Code12/Code12Game/Affichange.cs(55,26): error CS0103: The name 'DeckFactory' does not exist in the current context
/home/runner/work/Code12/Code12/Code12Game/Affichange.cs(76,35): error CS0103: The name 'CardFactory' does not exist in the current context

2 Error(s)
0 Warning(s)
```

---

## Recommandations

1. **Priorité immédiate:** Corriger les issues #1 et #2 pour permettre la compilation du projet
2. **Court terme:** Corriger l'issue #3 pour améliorer la qualité du code
3. **Moyen terme:** Considérer les issues #4, #5, et #6 pour améliorer la maintenabilité

---

**Date de vérification:** 2025-12-29  
**Version analysée:** Branche actuelle
