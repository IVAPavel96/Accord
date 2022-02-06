using System.Collections.Generic;
using System.Linq;
using Game;
using Player;
using UnityEngine;
using Zenject;

namespace UI
{
    public class InputScreen : MonoBehaviour
    {
        [SerializeField] private GameObject joystick;
        [SerializeField] private GameObject buttons;
        [SerializeField] private GameObject useButton;

        [Inject] private GameStateManager gameStateManager;

        private IEnumerable<PlayerTriggers> playerTriggersCached;

        void Awake()
        {
            int preferredValue = PlayerPrefs.GetInt("AndroidHorizontalInput", 0);
            SetHorizontalInputDevice(preferredValue);
        }

        private void Start()
        {
            playerTriggersCached = gameStateManager.Players.Select(player => player.GetComponent<PlayerTriggers>());
        }

        private void Update()
        {
            bool useButtonActive = playerTriggersCached.Any(player => player.IsInteractable);
            useButton.SetActive(useButtonActive);
        }

        private void SetHorizontalInputDevice(int value)
        {
            joystick.SetActive(false);
            buttons.SetActive(false);
            switch (value)
            {
                case 0:
                    buttons.SetActive(true);
                    break;
                case 1:
                    joystick.SetActive(true);
                    break;
                default:
                    Debug.LogError("Unknown input device!");
                    break;
            }
        }
    }
}