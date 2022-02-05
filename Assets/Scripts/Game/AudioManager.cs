using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game
{
    public enum AudioType
    {
        Jump,
        Fall,
    }

    public class AudioManager : SerializedMonoBehaviour
    {
        [SerializeField] private GameObject audioSourcePrefab;
        [SerializeField] private Dictionary<AudioType, AudioClipInfo> audioClipInfos;
        [SerializeField] private AudioSource deathSound;
        [SerializeField] private AudioSource winSound;

        [Serializable, InlineProperty]
        private class AudioClipInfo
        {
            [HideLabel] public AudioClip clip;
            [LabelWidth(50), Range(0f, 1f)] public float volume = 1f;
            [LabelWidth(50), Range(-3f, 3f)] public float pitch = 1f;
        }

        [Inject] private GameStateManager gameStateManager;

        private GameObjectPool audioPool;

        private void Start()
        {
            gameStateManager.onDeath.AddListener(OnDeath);
            gameStateManager.onWin.AddListener(OnWin);

            audioPool = gameObject.AddComponent<GameObjectPool>();
            audioPool.prefab = audioSourcePrefab;
            audioPool.Initialize();
        }

        private void OnDestroy()
        {
            gameStateManager.onDeath.RemoveListener(OnDeath);
            gameStateManager.onWin.RemoveListener(OnWin);
        }

        private void OnDeath()
        {
            deathSound.Play();
        }

        private void OnWin()
        {
            winSound.Play();
        }

        public void PlaySound(AudioType type)
        {
            AudioSource audioSource = audioPool.Pull<AudioSource>();
            AudioClipInfo clipInfo = audioClipInfos[type];
            SetupSound(audioSource, clipInfo);
            audioSource.Play();
            StartCoroutine(KillSound(audioSource));
        }

        private void SetupSound(AudioSource audioSource, AudioClipInfo clipInfo)
        {
            audioSource.clip = clipInfo.clip;
            audioSource.volume = clipInfo.volume;
            audioSource.pitch = clipInfo.pitch;
        }

        private IEnumerator KillSound(AudioSource source)
        {
            if (source.pitch == 0)
                Debug.LogError("Сука у тебя звук бесконечный штоле? pitch точно нулём должен быть?", source.gameObject);
            else
                yield return new WaitForSeconds(Mathf.Abs(source.time / source.pitch));

            while (source.isPlaying)
            {
                yield return null;
            }

            audioPool.Push(source.gameObject);
        }
    }
}