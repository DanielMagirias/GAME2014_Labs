using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] float horizontalForce;
    [SerializeField] Boundary bounds;
    [SerializeField] [Range(0.0f, 0.1f)] float decay;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        CheckBounds();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity *= decay;

    }

    private void Move()
    {
        float x = GetTouchDirection();

        rb.AddForce(new Vector2(x * horizontalForce, 0.0f));
    }

    private float GetTouchDirection()
    {

        if (Touchscreen.current == null)
        {
            return 0.0f;
        }

        var touch = Touchscreen.current.primaryTouch;

        if (!touch.press.isPressed)
        {
            return 0.0f;
        }

        Vector2 pos = touch.position.ReadValue();

        if (pos.x < Screen.width / 2.0f)
        {
            return -1.0f;
        }

        return 1.0f;
    }

    private void CheckBounds()
    {
        if (transform.position.x < bounds.min)
        {
            transform.position = new Vector2(bounds.min, transform.position.y);
        }

        if (transform.position.x > bounds.max)
        {
            transform.position = new Vector2(bounds.max, transform.position.y);
        }
    }

}
