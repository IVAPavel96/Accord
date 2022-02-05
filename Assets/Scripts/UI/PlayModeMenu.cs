using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PlayModeMenu : UIWindow
    {
        [SerializeField] private string introSceneName;
        [SerializeField] private MainMenu mainMenu;
        [SerializeField] private ChooseLevelMenu chooseLevelMenu;

        [SerializeField] private Button newGameButton;
        [SerializeField] private Button selectLevelButton;
        [SerializeField] private Button goBackButton;
        
        // Start is called before the first frame update
        void Start()
        {
            newGameButton.onClick.AddListener(StartNewGame);
            selectLevelButton.onClick.AddListener(() => OpenMenu(chooseLevelMenu));
            goBackButton.onClick.AddListener(() => OpenMenu(mainMenu));
        }
        
        void StartNewGame()
        {
            SceneManager.LoadScene(introSceneName);
        }
        
    }
}