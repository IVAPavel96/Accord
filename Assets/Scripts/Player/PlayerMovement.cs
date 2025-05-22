using System;
using System.Linq;
using Extensions;
using Game;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using AudioType = Game.AudioType;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float horizontalVelocity;
        [SerializeField] private float jumpForce;
        [SerializeField] private Transform groundedCollisionPoint;
        [SerializeField] private LayerMask groundMask;

        [Inject] private GameStateManager gameStateManager;
        [Inject] private AudioManager audioManager;

        public float HorizontalMove { get; set; }

        private Rigidbody2D rb;
        private bool wasGrounded; //нужна для анимации завершения прыжка
        private bool isHittingLeft;
        private bool isHittingRight;
        private bool isHittingWalls;
        private float playerRadius;

        #region отладка

        [ShowInInspector] private bool IsHittingLeft => isHittingLeft;
        [ShowInInspector] private bool IsHittingRight => isHittingRight;
        [ShowInInspector] private bool IsHittingWalls => isHittingWalls;

        [ShowInInspector] private float distance;

        #endregion

        #region ивенты для аниматора

        //todo возможно, аналогично стоит сделать PlayerSound!!!
        public event Action<float> OnHorizontalMoving;
        public event Action OnJumping;
        public event Action OnLanding;
        public event Action<bool> OnHittingWalls;

        #endregion


        private readonly float groundCheckRadius = 0.1f;
        private readonly Collider2D[] collisionBuffer = new Collider2D[10];
        private bool Grounded
        {
            get
            {
                Vector2 position = groundedCollisionPoint.position.xy();
                int collisionCount = Physics2D.OverlapCircleNonAlloc(position, groundCheckRadius, collisionBuffer, groundMask);
                return collisionBuffer.Take(collisionCount).Any(collider => !collider.isTrigger);
            }
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            wasGrounded = Grounded;
            var circleR = GetComponent<CircleCollider2D>().radius;
            playerRadius = circleR * circleR;
            Physics2D.queriesStartInColliders = false;
            gameStateManager.RegisterPlayer(gameObject); // TODO убрать отсюда эту хуиту
        }

        private void Start()
        {
            gameStateManager.onDeath.AddListener(OnDeath);
            gameStateManager.onPlayerFinished.AddListener(OnPlayerFinished);
        }

        private void OnDestroy()
        {
            gameStateManager.onDeath.RemoveListener(OnDeath);
            gameStateManager.onPlayerFinished.RemoveListener(OnPlayerFinished);
        }

        private void OnDeath()
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector2.zero;
            enabled = false;
        }

        public void OnPlayerFinished(GameObject player)
        {
            if (player != gameObject)
                return;

            rb.linearVelocity = Vector2.zero;
            enabled = false;
        }


        private void Update()
        {
            MoveHorizontal();
            CheckGrounded();
            CheckWallsCollision();
        }

        public void Jump()
        {
            if (Grounded && enabled)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                OnJumping?.Invoke();
                audioManager.PlaySound(AudioType.Jump);
            }
        }


        private void MoveHorizontal()
        {
            //float inputDirection = Input.GetAxis("Horizontal");
            float inputDirection = HorizontalMove;
            SetHorizontalVelocity(inputDirection * horizontalVelocity);

            OnHorizontalMoving?.Invoke(inputDirection);
        }

        private void SetHorizontalVelocity(float velocity)
        {
            Vector2 newVelocity = rb.linearVelocity;
            newVelocity.x = velocity;
            rb.linearVelocity = newVelocity;
        }

        private void CheckWallsCollision()
        {
            Vector3 right = transform.right;
            isHittingRight = CheckHitFromDir(right);
            isHittingLeft = CheckHitFromDir(-right);
            isHittingWalls = isHittingRight || isHittingLeft;
            OnHittingWalls?.Invoke(isHittingWalls);
        }

        private void CheckGrounded()
        {
            if (!wasGrounded && Grounded)
            {
                OnLanding?.Invoke();
                audioManager.PlaySound(AudioType.Fall);
            }

            wasGrounded = Grounded;
        }

        private bool CheckHitFromDir(Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, playerRadius + 0.25f);
            return hit.collider != null && !hit.collider.isTrigger;

            //RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, );
            //if (hit.collider == null) return false;

            //distance = Vector2.SqrMagnitude(hit.point - (Vector2)transform.position);
            //return distance <= playerRadiusSqr + 0.05f;
        }
    }
}