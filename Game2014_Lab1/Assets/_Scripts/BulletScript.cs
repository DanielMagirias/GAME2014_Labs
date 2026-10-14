using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Boundary screenBounds;

    private BulletManager bulletManager;

    void Start()
    {
        bulletManager = FindAnyObjectByType<BulletManager>();
    }

    void Update()
    {
        Move();
        CheckBounds();
    }

    private void Move()
    {
        transform.position -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
    }

    private void CheckBounds()
    {
        if (transform.position.y < screenBounds.min)
        {
            //Destroy(gameObject);
            bulletManager.ReturnBullet(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Destroy(gameObject);
            bulletManager.ReturnBullet(gameObject);
        }
    }
}
