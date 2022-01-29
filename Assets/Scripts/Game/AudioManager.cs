using UnityEngine;

namespace Game
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource deathSound;
        private void Start()
        {
            GameStateManager.Instance.onDeath.AddListener(OnDeath);
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.onDeath.RemoveListener(OnDeath);
        }

        private void OnDeath()
        {
            deathSound.Play();
        }
    }
}