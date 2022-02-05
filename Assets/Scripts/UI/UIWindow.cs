using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class UIWindow : MonoBehaviour
    {
        [SerializeField] private CanvasGroup window;
        public UnityEvent onShow = new UnityEvent();

        public UnityEvent onHide = new UnityEvent();

        private void Start()
        {
            SetAlpha(0, 0);
        }

        public void Show(float fadeIn = 0)
        {
            SetAlpha(1, fadeIn);
            Invoke(nameof(TriggerOnShow), fadeIn);
            window.blocksRaycasts = true;
        }

        private void TriggerOnShow()
        {
            onShow.Invoke();
        }

        public void Hide(float fadeOut = 0)
        {
            SetAlpha(0, fadeOut);
            Invoke(nameof(TriggerOnHide), fadeOut);
            window.blocksRaycasts = false;
        }

        private void TriggerOnHide()
        {
            onHide.Invoke();
        }

        private void SetAlpha(float value, float duration)
        {
            if (duration > 0)
                window.DOFade(value, duration).SetEase(Ease.OutSine);
            else
                window.alpha = value;
        }

        protected void OpenMenu(UIWindow menu, float fadeOut = 0)
        {
            Hide(fadeOut);
            menu.Show(fadeOut);
        }
    }
}