
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake() {

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

    }

    private void FixedUpdate() {

        float horizontal = joystick.Horizontal;

        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);

        animator.SetBool("bIsMoving", Mathf.Abs(horizontal) > 0.1f);

        if (horizontal < -0.1f) {

            sr.flipX = true;
        }

        else if (horizontal > 0.1f) {

            sr.flipX = false;
        }
    }
}
