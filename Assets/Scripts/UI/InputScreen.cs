using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScreen : MonoBehaviour
{
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject buttons;
    void Awake()
    {
        int preferredValue = PlayerPrefs.GetInt("AndroidHorizontalInput", 0);
        SetHorizontalInputDevice(preferredValue);
    }

    private void SetHorizontalInputDevice(int value)
    {
        joystick.SetActive(false);
        buttons.SetActive(false);
        Debug.Log("value: " + value);
        switch (value)
        {
            case 0:
                buttons.SetActive(true);
                break;
            case 1:
                joystick.SetActive(true);
                break;
            default:
                Debug.Log("неопознанное устройство ввода");
                break;
        }
    }
}
