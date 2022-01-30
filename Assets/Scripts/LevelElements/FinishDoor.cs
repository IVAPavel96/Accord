using DG.Tweening;
using Extensions;
using Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelElements
{
    [RequireComponent(typeof(LogicTriggerOutput))]
    public class FinishDoor : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer openState;
        [SerializeField] private SpriteRenderer closedState;
        [SerializeField, HideInInspector] private bool isOpened;
        [SerializeField] private float fadeDuration = 1f;

        private LogicTriggerOutput trigger;

        private void Start()
        {
            trigger = GetComponent<LogicTriggerOutput>();
            trigger.onTriggerEnable.AddListener(Open);
            trigger.onTriggerDisable.AddListener(Close);
            UpdateSprite();
        }

        private void OnDestroy()
        {
            trigger.onTriggerEnable.RemoveListener(Open);
            trigger.onTriggerDisable.RemoveListener(Close);
        }

        [Button]
        private void Open()
        {
            isOpened = true;
            UpdateSprite();
        }

        [Button]
        private void Close()
        {
            isOpened = false;
            UpdateSprite();
        }

        void UpdateSprite()
        {
            if (Application.isPlaying)
            {
                openState.DOFade(isOpened ? 1 : 0, fadeDuration);
                closedState.DOFade(!isOpened ? 1 : 0, fadeDuration);
            }
            else
            {
                openState.color = openState.color.WithAlpha(isOpened ? 1 : 0);
                closedState.color = openState.color.WithAlpha(!isOpened ? 1 : 0);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isOpened && other.CompareTag("Player"))
            {
                other.GetComponent<PlayerMovement>().OnFinish();
            }
        }
    }
}