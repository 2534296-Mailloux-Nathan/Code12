using System;

namespace Code12Game.Input
{
    /// <summary>
    /// Gestionnaire d'entrée responsable de la lecture hardware non-bloquante des touches
    /// </summary>
    public class InputHandler
    {
        private readonly InputController _controller;

        public InputHandler(InputController controller)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
        }

        /// <summary>
        /// Met à jour l'état des entrées de manière non-bloquante
        /// Utilise Console.KeyAvailable pour éviter de bloquer le thread
        /// </summary>
        public void Update()
        {
            if (!Console.KeyAvailable)
            {
                return;
            }

            var keyInfo = Console.ReadKey(intercept: true);
            
            if (_controller.KeyBindings.TryGetValue(keyInfo.Key, out var action))
            {
                _controller.ExecuteAction(action);
            }
        }
    }
}
