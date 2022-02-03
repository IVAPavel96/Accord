using System;
using Extensions;
using Game;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float horizontalVelocity;
        [SerializeField] private float jumpForce;
        [SerializeField] private Transform groundedCollisionPoint;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private AudioSource jumpSound;
        [SerializeField] private AudioSource fallSound;

        public float horizontalMove { get; set; }
    

        
        private Rigidbody2D rb;
        private SpriteRenderer renderer;
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
        public event Action OnFinishing;
        #endregion


        private readonly float groundCheckRadius = 0.1f;
        private readonly Collider2D[] collisionBuffer = new Collider2D[1];
        private bool Grounded
        {
            get
            {
                Vector2 position = groundedCollisionPoint.position.xy();
                int collisionCount = Physics2D.OverlapCircleNonAlloc(position, groundCheckRadius, collisionBuffer, groundMask);
                return collisionCount > 0;
            }
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            wasGrounded = Grounded;
            renderer = GetComponentInChildren<SpriteRenderer>();
            var circleR = GetComponent<CircleCollider2D>().radius;
            playerRadius = circleR * circleR;
            Physics2D.queriesStartInColliders = false;
            
        }

        private void Start()
        {
            GameStateManager.Instance.onDeath.AddListener(OnDeath);
            GameStateManager.Instance.RegisterPlayer(gameObject);
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.onDeath.AddListener(OnDeath);
        }

        private void OnDeath()
        {
            OnFinishing?.Invoke();
            
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            enabled = false;
        }

        public void OnFinish()
        {
            OnFinishing.Invoke();
            rb.velocity = Vector2.zero;
            enabled = false;
            GameStateManager.Instance.PlayerFinished(gameObject);
        }

        
        private void Update()
        {
            //Jump();
            MoveHorizontal();

            CheckWallsCollision();
            
        }

        public void Jump()
        {
            //if (Input.GetButtonDown("Jump") && Grounded)
            if (Grounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                OnJumping?.Invoke();
                jumpSound.Play();
            }
            if (!wasGrounded && Grounded)
            {
                OnLanding?.Invoke();
                fallSound.Play();
            }
            wasGrounded = Grounded;
        }
        

        private void MoveHorizontal()
        {
            //float inputDirection = Input.GetAxis("Horizontal");
            float inputDirection = horizontalMove;
            SetHorizontalVelocity(inputDirection * horizontalVelocity);

            OnHorizontalMoving?.Invoke(inputDirection);
            if (Mathf.Abs(inputDirection) > 0.01f)
                renderer.flipX = !(inputDirection > 0); //меняем ориентацию при смене направления
        }

        private void SetHorizontalVelocity(float velocity)
        {
            Vector2 newVelocity = rb.velocity;
            newVelocity.x = velocity;
            rb.velocity = newVelocity;
        }

        private void CheckWallsCollision()
        {
            isHittingRight = CheckHitFromDir(transform.right);
            isHittingLeft = CheckHitFromDir(-transform.right);
            isHittingWalls = isHittingRight || isHittingLeft;
            OnHittingWalls?.Invoke(isHittingWalls);
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
