using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Dropdown androidHorizontalInputDd;
    [SerializeField] private Button back;
    [SerializeField] private string mainMenuName;

    private void Start()
    {
        androidHorizontalInputDd.onValueChanged.AddListener((value) =>
        {
            Debug.Log("dropdown value: " + value);
            PlayerPrefs.SetInt("AndroidHorizontalInput", value);
        });
        back.onClick.AddListener(OpenMain);
    }
    private void OpenMain()
    {
        SceneManager.LoadScene(mainMenuName);
    }

}
