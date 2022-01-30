using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private string menuSceneName;

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(menuSceneName);
        }
    }
}