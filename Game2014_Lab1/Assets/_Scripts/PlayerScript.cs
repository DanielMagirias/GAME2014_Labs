using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] float horizontalForce;
    [SerializeField] Boundary bounds;
    [SerializeField] [Range(0.0f, 0.1f)] float decay;

    private Rigidbody2D rb;

    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 3f;
    private float nextFireTime;
    private BulletManager bulletManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        bulletManager = FindAnyObjectByType<BulletManager>();

        nextFireTime = Time.time + fireRate;
    }

    void Update()
    {
        Move();
        CheckBounds();
        Fire();

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

    private void Fire()
    {

        if (Time.time >= nextFireTime)
        {
            bulletManager.GetBullet(bulletSpawn.position, BulletOwner.Player);
            nextFireTime = Time.time + fireRate;
        }
    }

}
