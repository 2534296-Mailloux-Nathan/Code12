using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Code12Data;

namespace Code12Game.Input
{
    /// <summary>
    /// Contrôleur d'entrée responsable de la gestion des touches et des configurations
    /// </summary>
    public partial class InputController
    {
        private Dictionary<ConsoleKey, GameAction> _keyBindings = new();

        /// <summary>
        /// Retourne une copie en lecture seule du dictionnaire de touches
        /// </summary>
        public IReadOnlyDictionary<ConsoleKey, GameAction> KeyBindings => _keyBindings.AsReadOnly();

        public InputController()
        {
            InitializeDefaultBindings();
        }

        /// <summary>
        /// Initialise les bindings par défaut (QWERTY)
        /// </summary>
        private void InitializeDefaultBindings()
        {
            _keyBindings = new Dictionary<ConsoleKey, GameAction>
            {
                { ConsoleKey.W, GameAction.Up },
                { ConsoleKey.S, GameAction.Down },
                { ConsoleKey.A, GameAction.Left },
                { ConsoleKey.D, GameAction.Right },
                { ConsoleKey.Enter, GameAction.Confirm },
                { ConsoleKey.Backspace, GameAction.Back },
                { ConsoleKey.Escape, GameAction.Exit },
                {ConsoleKey.P, GameAction.PiocherCarte}
            };
        }

        /// <summary>
        /// Charge les configurations de touches depuis un fichier JSON
        /// Si le fichier n'existe pas, initialise avec les valeurs par défaut
        /// </summary>
        public void LoadSettings(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    SaveSettings(filePath);
                    return;
                }

                var json = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                
                var bindings = JsonSerializer.Deserialize<Dictionary<string, string>>(json, options);
                
                if (bindings != null)
                {
                    _keyBindings.Clear();
                    foreach (var kvp in bindings)
                    {
                        if (Enum.TryParse<ConsoleKey>(kvp.Key, out var key) &&
                            Enum.TryParse<GameAction>(kvp.Value, out var action))
                        {
                            _keyBindings[key] = action;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement des paramètres: {ex.Message}");
                InitializeDefaultBindings();
            }
        }

        /// <summary>
        /// Sauvegarde les configurations de touches dans un fichier JSON
        /// </summary>
        public void SaveSettings(string filePath)
        {
            try
            {
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var bindingsForJson = new Dictionary<string, string>();
                foreach (var kvp in _keyBindings)
                {
                    bindingsForJson[kvp.Key.ToString()] = kvp.Value.ToString();
                }

                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(bindingsForJson, options);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la sauvegarde des paramètres: {ex.Message}");
            }
        }

        /// <summary>
        /// Exécute une action en fonction de l'état actuel du jeu
        /// </summary>
        public void ExecuteAction(GameAction action)
        {
            switch (GameData.ObtenirEtatActuel())
            {
                case InputState.Desk:
                    ExecuteDeskAction(action);
                    break;
                case InputState.View:
                    ExecuteViewAction(action);
                    break;
                case InputState.Info:
                    ExecuteInfoAction(action);
                    break;
                case InputState.MenuPrincipal:
                    ExecuteMenuPrincipal(action);
                    break;
            }
        }

        /// <summary>
        /// Traite les actions dans l'état Desk
        /// </summary>
        private void ExecuteDeskAction(GameAction action)
        {
            switch (action)
            {
                case GameAction.Left:
                    DefilerCartesVersLaGauche();
                    break;
                case GameAction.Right:
                    DefilerCartesVersLaDroite();
                    break;
                case GameAction.Up:
                    AllerAuDebutDesCartes();
                    break;
                case GameAction.Down:
                    AllerALaFinDesCartes();
                    break;
                case GameAction.Confirm:
                    HandleConfirmInDesk();
                    break;
                case GameAction.Back:
                    HandleBackInDesk();
                    break;
                case GameAction.Exit:
                    HandleExit();
                    break;
            }
        }

        /// <summary>
        /// Traite les actions dans l'état View
        /// </summary>
        private void ExecuteViewAction(GameAction action)
        {
            switch (action)
            {
                case GameAction.Back:
                    HandleBackInView();
                    break;
                case GameAction.Exit:
                    HandleExit();
                    break;
            }
        }

        /// <summary>
        /// Traite les actions dans l'état Info
        /// </summary>
        private void ExecuteInfoAction(GameAction action)
        {
            switch (action)
            {
                case GameAction.PiocherCarte:
                    PicherUneCarteScore();
                    break;
            }
        }

        /// <summary>
        /// Traite les actions dans l'état MenuPrincipal
        /// </summary>
        private void ExecuteMenuPrincipal(GameAction action)
        {
            switch (action)
            {
                //todo: implement menu principal state actions
            }
        }

        private void HandleExit()
        {
            Environment.Exit(0);
        }
    }
}
