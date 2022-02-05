using Game;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerAnimator : MonoBehaviour
    {
        [Inject] private GameStateManager gameStateManager;

        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private PlayerMovement movement;

        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsHittingWalls = Animator.StringToHash("IsHittingWalls");

        #region отладка

        [ShowInInspector]
        private bool IsJumpingAnimation => animator != null && animator.GetBool(IsJumping); //debug

        [ShowInInspector]
        private float IsMovingAnimation //debug
        {
            get
            {
                if (animator != null) return animator.GetFloat(Speed);
                return 0f;
            }
        }

        #endregion

        void Start()
        {
            movement = GetComponent<PlayerMovement>();
            animator = GetComponentInChildren<Animator>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            movement.OnJumping += () => animator.SetBool(IsJumping, true);
            movement.OnLanding += () => animator.SetBool(IsJumping, false);
            movement.OnHorizontalMoving += OnHorizontalMoving;
            movement.OnHittingWalls += isHittingWalls => animator.SetBool(IsHittingWalls, isHittingWalls);

            gameStateManager.onDeath.AddListener(ResetAnimationState);
            gameStateManager.onPlayerFinished.AddListener(OnPlayerFinished);
        }

        void OnDestroy()
        {
            gameStateManager.onDeath.RemoveListener(ResetAnimationState);
            gameStateManager.onPlayerFinished.RemoveListener(OnPlayerFinished);
        }

        private void OnHorizontalMoving(float inputDirection)
        {
            animator.SetFloat(Speed, Mathf.Abs(inputDirection));
            if (Mathf.Abs(inputDirection) > 0.01f)
                spriteRenderer.flipX = !(inputDirection > 0); //меняем ориентацию при смене направления
        }

        private void OnPlayerFinished(GameObject player)
        {
            if (player == gameObject)
            {
                ResetAnimationState();
            }
        }

        private void ResetAnimationState()
        {
            animator.SetBool(IsJumping, false);
            animator.SetBool(IsHittingWalls, false);
            animator.SetFloat(Speed, 0);
        }
    }
}