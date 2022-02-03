using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button play;
        [SerializeField] private Button settings;
        [SerializeField] private Button exit;
        [SerializeField] private string level1Name;
        [SerializeField] private string settingsSceneName;
        [SerializeField] private GameObject backgroundMusic;

        private void Start()
        {
            play.onClick.AddListener(Play);
            settings.onClick.AddListener(OpenSettings);
            exit.onClick.AddListener(Exit);
            DontDestroyOnLoad(backgroundMusic);
            GameObject[] musics = GameObject.FindGameObjectsWithTag("BackgroundMusic");
            if (musics.Length > 1)
            {
                foreach (var music in musics)
                {
                    if (music == backgroundMusic)
                        Destroy(music);
                }
            }
        }

        private void Play()
        {
            SceneManager.LoadScene(level1Name);
        }

        private void OpenSettings()
        {
            SceneManager.LoadScene(settingsSceneName);
        }

        private void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}