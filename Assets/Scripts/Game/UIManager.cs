using UI;
using UnityEngine;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIWindow deathScreen;
        [SerializeField] private float deathScreenFadeTime;
        [SerializeField] private UIWindow winScreen;
        [SerializeField] private float winScreenFadeTime;

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
            deathScreen.Show(deathScreenFadeTime);
        }

        private void OnWin()
        {
            winScreen.Show(winScreenFadeTime);
        }
    }
}