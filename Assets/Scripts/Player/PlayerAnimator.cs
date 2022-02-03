using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerAnimator : MonoBehaviour
    {
        #region отладка

        [ShowInInspector]
        private bool IsJumpingAnimation => animator != null && animator.GetBool("IsJumping"); //debug
        [ShowInInspector]
        private float IsMovingAnimation //debug
        {
            get
            {
                if (animator != null) return animator.GetFloat("Speed");
                return 0f;
            }
        }

        #endregion



        private Animator animator;

        private PlayerMovement movement;

        void Start()
        {
            movement = GetComponent<PlayerMovement>();
            animator = GetComponentInChildren<Animator>();

            movement.OnJumping += () => animator.SetBool("IsJumping", true);
            movement.OnLanding += () => animator.SetBool("IsJumping", false);
            movement.OnHorizontalMoving += inputDirection => animator.SetFloat("Speed", Mathf.Abs(inputDirection));
            movement.OnHittingWalls += isHittingWalls => animator.SetBool("IsHittingWalls", isHittingWalls);

            movement.OnFinishing += ResetAnimationState;
        }
        
        private void ResetAnimationState()
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsHittingWalls", false);
            animator.SetFloat("Speed", 0);
        }

        void OnDestroy()
        {
            
        }

    }
}