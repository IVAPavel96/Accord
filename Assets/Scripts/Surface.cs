using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Surface : MonoBehaviour
{
    private AudioSource fallSound;

    public void PlayFallSound()
    {
        fallSound.Play();
    } 
}
