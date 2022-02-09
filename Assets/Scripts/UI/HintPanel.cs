using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Game;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class HintPanel : MonoBehaviour
    {
        // [SerializeField] private Animator jumpButtonAnimator;
        // [SerializeField] private Animator leftButtonAnimator;
        // [SerializeField] private Animator rightButtonAnimator;
        // [SerializeField] private Animator useButtonAnimator;
        // [SerializeField] private Animator dPadAnimator;
        [SerializeField] private Image[] moveHighlight;
        [SerializeField] private Image jumpHighlight;
        [SerializeField] private Image useHighlight;

        [SerializeField] private UIWindow hintMove;
        [SerializeField] private UIWindow hintJump;
        [SerializeField] private UIWindow hintUse;

        [SerializeField] private Color fadeColor = Color.gray;
        [SerializeField] private int fadeDuration = 1;

        [Inject] private GameStateManager gameStateManager;

        private IEnumerable<PlayerTriggers> playerTriggersCached;
        private bool useHintShown;
        private Sequence moveTweener;
        private Tweener jumpTweener;
        private Tweener useTweener;

        private void Start()
        {
            CreateTweeners();
            DisableBlinkingAnimations();
            hintMove.Show(0.2f);
            hintMove.onShow.AddListener(() => MoveControllersHighlight(true));
            hintMove.onHide.AddListener(() =>
            {
                DisableBlinkingAnimations();
                hintJump.Show(0.2f);
            });
            // hintJump.onShow.AddListener(() => jumpButtonAnimator.enabled = true);
            hintJump.onShow.AddListener(() => jumpTweener.Play());
            hintJump.onHide.AddListener(DisableBlinkingAnimations);
            hintUse.onShow.AddListener(() => useTweener.Play());
            hintUse.onHide.AddListener(DisableBlinkingAnimations);

            playerTriggersCached = gameStateManager.Players.Select(player => player.GetComponent<PlayerTriggers>());
            useHintShown = false;
        }

        private void Update()
        {
            bool useButtonActive = playerTriggersCached.Any(player => player.IsInteractable);
            if (useButtonActive && !useHintShown)
            {
                useHintShown = true;
                hintUse.Show(0.2f);
            }
        }

        private void DisableBlinkingAnimations()
        {
            MoveControllersHighlight(false);
            // useButtonAnimator.enabled = false;
            // jumpButtonAnimator.enabled = false;
            jumpTweener.Rewind();
            useTweener.Rewind();
        }

        private void MoveControllersHighlight(bool state)
        {
            // dPadAnimator.enabled = state;
            // leftButtonAnimator.enabled = state;
            // rightButtonAnimator.enabled = state;
            if (state)
                moveTweener.Play();
            else
                moveTweener.Rewind();
        }

        private void CreateTweeners()
        {
            moveTweener = DOTween.Sequence();
            foreach (var image in moveHighlight)
            {
                moveTweener.Join(image.DOColor(fadeColor, fadeDuration).From(Color.white));
            }
            moveTweener.SetLoops(-1, LoopType.Yoyo);
            jumpTweener = jumpHighlight.DOColor(fadeColor, fadeDuration).From(Color.white).SetLoops(-1, LoopType.Yoyo);
            useTweener = useHighlight.DOColor(fadeColor, fadeDuration).From(Color.white).SetLoops(-1, LoopType.Yoyo);
        }
    }
}