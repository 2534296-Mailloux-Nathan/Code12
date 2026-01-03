# Système d'Input - Code12Game

## Vue d'ensemble

Le système d'input a été restructuré en deux classes distinctes dans le namespace `Code12Game.Input` :

- **`InputConfiguration.cs`** : Énumérations `GameAction` et `InputState`
- **`InputController.cs`** : Gestion de la configuration des touches et exécution des actions
- **`InputHandler.cs`** : Lecture hardware non-bloquante

## Architecture

### InputConfiguration

Contient deux énumérations :

```csharp
public enum GameAction
{
    Up, Down, Left, Right, Confirm, Back, Exit
}

public enum InputState
{
    View, Desk, Info
}
```

### InputController

Classe responsable de :

1. **Gestion des touches** : Dictionnaire `Dictionary<ConsoleKey, GameAction>`
2. **Configuration QWERTY par défaut** :
   - `Z` ? Up
   - `S` ? Down
   - `Q` ? Left
   - `D` ? Right
   - `Enter` ? Confirm
   - `Backspace` ? Back
   - `Escape` ? Exit

3. **Persistance** :
   - `LoadSettings(string filePath)` : Charge depuis JSON
   - `SaveSettings(string filePath)` : Sauvegarde en JSON
   - Crée automatiquement le fichier avec les defaults s'il n'existe pas

4. **Exécution d'actions** :
   - `ExecuteAction(GameAction action)` : Dispatche selon l'état actuel
   - Logique différente pour chaque état (Desk, View, Info)

### InputHandler

Classe responsable de :

1. **Lecture hardware non-bloquante**
2. **Méthode `Update()`** : À appeler dans la boucle de jeu
   - Utilise `Console.KeyAvailable` pour ne pas bloquer
   - Lit la touche avec `Console.ReadKey(true)`
   - Recherche l'action correspondante
   - Appelle `_controller.ExecuteAction(action)`

## Utilisation

### Initialisation

```csharp
// Créer le contrôleur
var inputController = new InputController();

// Charger les paramètres (crée le fichier s'il n'existe pas)
inputController.LoadSettings("config/input_settings.json");

// Créer le gestionnaire
var inputHandler = new InputHandler(inputController);
```

### Intégration dans la boucle de jeu

```csharp
while (true)
{
    inputHandler.Update(); // Lecture non-bloquante
    
    // Votre logique de jeu ici...
    
    await Task.Delay(16); // ~60 FPS
}
```

### Changement d'état

```csharp
inputController.CurrentState = InputState.View;
```

### Sauvegarde manuelle

```csharp
inputController.SaveSettings("config/input_settings.json");
```

## Fichier de configuration JSON

Le fichier généré automatiquement a cette structure :

```json
{
  "Z": "Up",
  "S": "Down",
  "Q": "Left",
  "D": "Right",
  "Enter": "Confirm",
  "Backspace": "Back",
  "Escape": "Exit"
}
```

## Encapsulation

- `KeyBindings` est une propriété `IReadOnlyDictionary` pour l'accès en lecture seule
- Le dictionnaire interne est protégé et ne peut être modifié que via `LoadSettings`

## Notes d'implémentation

- Utilise `System.Text.Json` pour la sérialisation
- Gestion d'erreurs robuste avec try-catch
- Les énums sont basées sur les états du layout `Affichange` (View, Desk, Info)
- La classe `InputControllerLegacy` conserve la logique métier originale

## Modifications existantes

La classe `InputController` originale a été renommée en `InputControllerLegacy` et reste inchangée fonctionnellement. Elle est appelée par la nouvelle classe `InputController` pour les actions de défilement des cartes.
