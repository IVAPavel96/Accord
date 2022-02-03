using Game;
using LevelElements;
using UnityEngine;

namespace Player
{
    public class PlayerTriggers : MonoBehaviour
    {
        private LogicLever logicInput;
        public void Use()
        {
            if (logicInput == null) return;
            logicInput.ChangeLeverState();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Danger"))
            {
                GameStateManager.Instance.TriggerDeath();
            }

            if (other.CompareTag("Logic"))
                logicInput = other.GetComponent<LogicLever>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Logic"))
                logicInput = null;
        }
    }
}
