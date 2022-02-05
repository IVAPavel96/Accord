using UI;
using UnityEngine;
using Zenject;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIWindow deathScreen;
        [SerializeField] private float deathScreenFadeTime;
        [SerializeField] private UIWindow winScreen;
        [SerializeField] private float winScreenFadeTime;

        [Inject] private GameStateManager gameStateManager;

        private void Start()
        {
            gameStateManager.onDeath.AddListener(OnDeath);
            gameStateManager.onWin.AddListener(OnWin);
        }

        private void OnDestroy()
        {
            gameStateManager.onDeath.RemoveListener(OnDeath);
            gameStateManager.onWin.RemoveListener(OnWin);
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