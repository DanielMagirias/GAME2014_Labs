using UnityEngine;
using UnityEngine.SceneManagement;

public enum BulletOwner
{
    Player,
    Enemy
}

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Boundary screenBounds;

    private BulletManager bulletManager;
    private GameController gc;
    private PlayerScript ps;

    private BulletOwner owner;

    public void SetOwner(BulletOwner newOwner)
    {
        owner = newOwner;
    }

    void Start()
    {
        bulletManager = FindAnyObjectByType<BulletManager>();
        gc = FindAnyObjectByType<GameController>();
        ps = FindAnyObjectByType<PlayerScript>();
    }

    void Update()
    {
        Move();
        CheckBounds();
    }

    private void Move()
    {
        if (owner == BulletOwner.Enemy)
        {
            transform.position -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
        }

        if (owner == BulletOwner.Player)
        {
            transform.position -= new Vector3(0.0f, -speed * Time.deltaTime, 0.0f);
        }
    }

    private void CheckBounds()
    {
        if (transform.position.y < screenBounds.min || transform.position.y > screenBounds.max)
        {
            //Destroy(gameObject);
            bulletManager.ReturnBullet(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (owner == BulletOwner.Enemy && collision.CompareTag("Player"))
        {
            
            Debug.Log("Player Hit");
            bulletManager.ReturnBullet(gameObject);
            ps.TakeDamage();
        }

        
        else if (owner == BulletOwner.Player && collision.CompareTag("Enemy"))
        {
            
            Debug.Log("Enemy Hit");
            Destroy(collision.gameObject);
            bulletManager.ReturnBullet(gameObject);
        }
    }
}
