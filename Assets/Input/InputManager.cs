using System.Collections;
using System.Collections.Generic;
using Player;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerMovement[] playerMovements;
    private PlayerTriggers[] playerTriggers;
    private InputControl inputController;
    void OnEnable()
    {
        playerMovements = GetComponentsInChildren<PlayerMovement>();
        playerTriggers = GetComponentsInChildren<PlayerTriggers>();

        inputController = new InputControl();
        inputController.Player.Enable();
        inputController.Player.Jump.started += JumpAll;
        inputController.Player.Use.started += UseAll;
    }

    void Update()
    {
        playerMovements.ForEach(person => person.horizontalMove = inputController.Player.Run.ReadValue<float>());
    }

    void OnDestroy()
    {
        inputController.Player.Jump.started -= JumpAll;
        inputController.Player.Use.started -= UseAll;

        inputController.Player.Disable();
    }

    private void JumpAll(InputAction.CallbackContext context) => playerMovements.ForEach(person => person.Jump());

    private void UseAll(InputAction.CallbackContext context) => playerTriggers.ForEach(person => person.Use());
}
