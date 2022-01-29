using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance;
        private bool isDead;
        public bool IsDead => isDead;

        public UnityEvent onDeath = new UnityEvent();

        private void Awake()
        {
            Instance = this;
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void TriggerDeath()
        {
            if (!isDead)
            {
                onDeath.Invoke();
            }
            isDead = true;
        }
    }
}