using UnityEngine;

namespace Game
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource deathSound;
        [SerializeField] private AudioSource winSound;
        private void Start()
        {
            GameStateManager.Instance.onDeath.AddListener(OnDeath);
            GameStateManager.Instance.onWin.AddListener(OnWin);
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.onDeath.RemoveListener(OnDeath);
            GameStateManager.Instance.onWin.RemoveListener(OnWin);
        }

        private void OnDeath()
        {
            deathSound.Play();
        }

        private void OnWin()
        {
            winSound.Play();
        }
    }
}