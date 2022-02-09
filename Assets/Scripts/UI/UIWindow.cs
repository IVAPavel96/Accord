using System;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class UIWindow : MonoBehaviour
    {
        [SerializeField] private CanvasGroup window;
        [SerializeField] private bool visibleOnAwake;
        [SerializeField] private bool callEventsOnAwake;
        public UnityEvent onShow = new UnityEvent();

        public UnityEvent onHide = new UnityEvent();
        private Tweener tweener;

        private void Awake()
        {
            if (visibleOnAwake)
                Show(0, callEventsOnAwake);
            else
                Hide(0, callEventsOnAwake);
        }

        private void OnDestroy()
        {
            tweener?.Kill();
        }

        public void Show(float fadeIn = 0)
        {
            Show(fadeIn, true);
        }

        public void Show(float fadeIn, bool useCallback)
        {
            if (useCallback)
                SetAlphaSmooth(1, fadeIn, () => onShow.Invoke());
            else
                SetAlphaSmooth(1, fadeIn, null);
            window.blocksRaycasts = true;
        }

        public void Hide(float fadeOut = 0)
        {
            Hide(fadeOut, true);
        }

        public void Hide(float fadeOut, bool useCallback)
        {
            if (useCallback)
                SetAlphaSmooth(0, fadeOut, () => onHide.Invoke());
            else
                SetAlphaSmooth(0, fadeOut, null);
            window.blocksRaycasts = false;
        }

        private void SetAlphaSmooth(float value, float duration, [CanBeNull] Action callback)
        {
            if (tweener != null && !tweener.IsComplete())
            {
                tweener.Complete();
                tweener.Kill();
                tweener = null;
            }

            if (duration > 0)
                tweener = window.DOFade(value, duration).SetEase(Ease.OutSine)
                    .OnComplete(() => callback?.Invoke()).SetAutoKill();
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