using System.Linq;
using Game;
using Player;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Zenject;

namespace Input
{
    public class InputManager : MonoBehaviour
    {
        [Inject] private GameStateManager gameStateManager;

        private PlayerMovement[] playerMovements;
        private PlayerTriggers[] playerTriggers;
        private InputControl inputController;
        private void Start()
        {
            EnhancedTouchSupport.Enable();
            GameObject[] players = gameStateManager.Players.ToArray();
            playerMovements = players.Select(player => player.GetComponent<PlayerMovement>()).ToArray();
            playerTriggers = players.Select(player => player.GetComponent<PlayerTriggers>()).ToArray();

            inputController = new InputControl();
            inputController.Player.Enable();
            inputController.Player.Jump.started += JumpAll;
            inputController.Player.Use.started += UseAll;
        }

        private void OnDestroy()
        {
            inputController.Player.Jump.started -= JumpAll;
            inputController.Player.Use.started -= UseAll;

            inputController.Player.Disable();
            EnhancedTouchSupport.Disable();
        }

        private void Update()
        {
            playerMovements.ForEach(player => player.HorizontalMove = inputController.Player.Run.ReadValue<float>());
        }

        private void JumpAll(InputAction.CallbackContext context) => playerMovements.ForEach(player => player.Jump());

        private void UseAll(InputAction.CallbackContext context) => playerTriggers.ForEach(player => player.Use());
    }
}
