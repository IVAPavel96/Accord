using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class IntroCanvas : MonoBehaviour
{
    [SerializeField] private UIWindow startWindow;

    void Awake()
    {
        startWindow.Show(1f);
    }
}
