using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.U2D;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float horizontalVelocity;
        [SerializeField] private float jumpForce;
        [SerializeField] private Transform groundedCollisionPoint;
        [SerializeField] private LayerMask groundCheckMask;

        private Rigidbody2D rb;
        private Animator animator;
        private SpriteRenderer renderer;
        private bool wasGrounded; //нужна дл€ анимации завершени€ прыжка
        private bool isHittingLeft;
        private bool isHittingRight;
        private bool isHittingWalls;
        private float playerRadius;

        #region отладка
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

        [ShowInInspector] private bool IsHittingLeft => isHittingLeft;
        [ShowInInspector] private bool IsHittingRight => isHittingRight;
        [ShowInInspector] private bool IsHittingWalls => isHittingWalls;

        [ShowInInspector] private float distance;

        #endregion

        private readonly float groundCheckRadius = 0.1f;
        Collider2D[] temp = new Collider2D[1];
        private bool Grounded
        {
            get
            {
                Vector2 position = groundedCollisionPoint.position.xy();
                int collisionCount = Physics2D.OverlapCircleNonAlloc(position, groundCheckRadius, temp, groundCheckMask);
                return collisionCount > 0;
            }
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
            wasGrounded = Grounded;
            renderer = GetComponentInChildren<SpriteRenderer>();
            var circleR = GetComponent<CircleCollider2D>().radius;
            playerRadius = circleR * circleR;
            Physics2D.queriesStartInColliders = false;
        }

        private void Update()
        {
            Jump();
            MoveHorizontal();

            CheckWallsCollision();
            
        }

        private void Jump()
        {
            if (Input.GetButtonDown("Jump") && Grounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetBool("IsJumping", true);
            }
            if (!wasGrounded && Grounded)
            {
                Debug.Log("landing");
                OnLanding();
            }
            wasGrounded = Grounded;
        }

        public void OnLanding()
        {
            animator.SetBool("IsJumping", false); //вынес в отдельный метод - может еще что-нибудь понадобитс€
        }

        private void MoveHorizontal()
        {
            float inputDirection = Input.GetAxis("Horizontal");
            Vector2 newVelocity = rb.velocity;
            newVelocity.x = inputDirection * horizontalVelocity;
            rb.velocity = newVelocity;

            animator.SetFloat("Speed", Mathf.Abs(inputDirection));
            if (Mathf.Abs(inputDirection) > 0.01f)
                renderer.flipX = !(inputDirection > 0); //мен€ем ориентацию при смене направлени€
        }

        private void CheckWallsCollision()
        {
            isHittingRight = CheckHitFromDir(transform.right);
            isHittingLeft = CheckHitFromDir(-transform.right);
            isHittingWalls = isHittingRight || isHittingLeft;

            animator.SetBool("IsHittingWalls", isHittingWalls);

        }

        private bool CheckHitFromDir(Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, playerRadius + 0.25f);
            return hit.collider != null;

            //RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, );
            //if (hit.collider == null) return false;

            //distance = Vector2.SqrMagnitude(hit.point - (Vector2)transform.position);
            //return distance <= playerRadiusSqr + 0.05f;
        }
    }
}
