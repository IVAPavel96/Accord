using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using Player;
using UnityEngine;
using Zenject;

namespace UI
{
    public class HintPanel : MonoBehaviour
    {
        [SerializeField] private Animator jumpButtonAnimator;
        [SerializeField] private Animator leftButtonAnimator;
        [SerializeField] private Animator rightButtonAnimator;
        [SerializeField] private Animator useButtonAnimator;
        [SerializeField] private Animator dPadAnimator;

        [SerializeField] private UIWindow hintMove;
        [SerializeField] private UIWindow hintJump;
        [SerializeField] private UIWindow hintUse;

        [Inject] private GameStateManager gameStateManager;

        private IEnumerable<PlayerTriggers> playerTriggersCached;
        private bool useHintShown;

        // Start is called before the first frame update
        void Start()
        {
            DisableBlinkingAnimations();
            hintMove.Show(0.2f);
            hintMove.onShow.AddListener(() => MoveControllersHighlight(true));
            hintMove.onHide.AddListener(() =>
            {
                DisableBlinkingAnimations();
                hintJump.Show(0.2f);
            });
            hintJump.onShow.AddListener(() => jumpButtonAnimator.enabled = true);
            hintJump.onHide.AddListener(DisableBlinkingAnimations);
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
            useButtonAnimator.enabled = false;
            jumpButtonAnimator.enabled = false;
        }

        private void MoveControllersHighlight(bool state)
        {
            dPadAnimator.enabled = state;
            leftButtonAnimator.enabled = state;
            rightButtonAnimator.enabled = state;
        }
    }
}