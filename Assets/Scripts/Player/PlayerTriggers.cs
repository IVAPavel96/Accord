using Game;
using UnityEngine;

namespace Player
{
    public class PlayerTriggers : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Danger"))
            {
                GameStateManager.Instance.TriggerDeath();
            }
        }
    }
}
