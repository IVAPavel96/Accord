using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public struct LevelSceneName
    {
        /// <summary>
        /// отображается в окне выбора уровня
        /// </summary>
        public string LevelName;
        /// <summary>
        /// соответствующее имя сцены
        /// </summary>
        public string SceneName;
    }

    public class ChooseLevelMenu : UIWindow
    {
        [SerializeField] private LevelSceneName[] levels;
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private RectTransform levelButtonsTransform;
        [SerializeField] private Button backButton;
        [SerializeField] private PlayModeMenu playModeMenu;

        void Start()
        {
            foreach (var levelSceneName in levels)
            {
                var button = Instantiate(buttonPrefab, levelButtonsTransform);
                var title = button.GetComponentInChildren<Text>();
                title.text = button.name = levelSceneName.LevelName;
                button.onClick.AddListener(() => SceneManager.LoadScene(levelSceneName.SceneName));
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(levelButtonsTransform);
            backButton.onClick.AddListener(() => OpenMenu(playModeMenu));
        }
    }
}