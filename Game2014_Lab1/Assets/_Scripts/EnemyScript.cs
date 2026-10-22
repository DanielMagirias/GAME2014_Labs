using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private Boundary movementBounds;
    [SerializeField] private Boundary startingXRange;
    [SerializeField] private Boundary startingYRange;

    private Vector2 startingPoint;
    [SerializeField] private float randomSpeed;

    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 3f;
    private float nextFireTime;

    private BulletManager bulletManager;


    void Start()
    {

        bulletManager = FindAnyObjectByType<BulletManager>();

        //randomSpeed = Random.Range(movementBounds.min, movementBounds.max);
        randomSpeed = 1.0f;

        startingPoint = new Vector2(Random.Range(startingXRange.min, startingXRange.max), Random.Range(startingYRange.min, startingYRange.max));

        transform.position = startingPoint;

        nextFireTime = Time.time + fireRate;

    }

    void Update()
    {
        Move();

        if (Time.time >= nextFireTime)
        {
            bulletManager.GetBullet(bulletSpawn.position, BulletOwner.Enemy);
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Move()
    {
        
        transform.position -= new Vector3(0f, randomSpeed * Time.deltaTime, 0f);

        if (transform.position.y <= movementBounds.min)
        {
            
            transform.position = new Vector2(Random.Range(startingXRange.min, startingXRange.max), movementBounds.max);
        }
    }
}
