using Extensions;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float horizontalVelocity;
        [SerializeField] private float jumpForce;
        [SerializeField] private Transform groundedCollisionPoint;
        [SerializeField] private LayerMask groundCheckMask;

        private Rigidbody2D rb;

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
        }

        private void Update()
        {
            Jump();
            MoveHorizontal();
        }

        private void Jump()
        {
            if (Input.GetButtonDown("Jump") && Grounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        private void MoveHorizontal()
        {
            float inputDirection = Input.GetAxis("Horizontal");
            Vector2 newVelocity = rb.velocity;
            newVelocity.x = inputDirection * horizontalVelocity;
            rb.velocity = newVelocity;
        }
    }
}
