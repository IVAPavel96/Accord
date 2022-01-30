using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCanvas : MonoBehaviour
{
    [SerializeField] private UIWindow startWindow;
    [SerializeField] private string nextLevel;

    private void Awake()
    {
        startWindow.Show(1f);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
