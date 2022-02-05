using Game;
using LevelElements;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerTriggers : MonoBehaviour
    {
        [Inject] private GameStateManager gameStateManager;

        private IInteractableObject interactable;

        public void Use()
        {
            interactable?.StartUse();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "Danger":
                    gameStateManager.TriggerDeath();
                    break;
                case "Logic":
                    interactable = (IInteractableObject) other.GetComponent(typeof(IInteractableObject));
                    break;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "Logic":
                    interactable = null;
                    break;
            }
        }
    }
}