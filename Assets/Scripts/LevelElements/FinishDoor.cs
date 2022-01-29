using DG.Tweening;
using Player;
using UnityEngine;

namespace LevelElements
{
    [RequireComponent(typeof(LogicTriggerOutput))]
    public class FinishDoor : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer openState;
        [SerializeField] private SpriteRenderer closedState;
        [SerializeField] private bool isOpened;
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

        private void Open()
        {
            isOpened = true;
            UpdateSprite();
        }

        private void Close()
        {
            isOpened = false;
            UpdateSprite();
        }

        void UpdateSprite()
        {
            openState.DOFade(isOpened ? 1 : 0, fadeDuration);
            closedState.DOFade(!isOpened ? 1 : 0, fadeDuration);
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