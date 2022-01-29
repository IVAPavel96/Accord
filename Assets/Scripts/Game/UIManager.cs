using UI;
using UnityEngine;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIWindow deathScreen;
        [SerializeField] private float deathScreenFadeTime;
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
            deathScreen.Show(deathScreenFadeTime);
        }
    }
}