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

        randomSpeed = Random.Range(movementBounds.min, movementBounds.max);

        startingPoint = new Vector2(Random.Range(startingXRange.min, startingXRange.max), Random.Range(startingYRange.min, startingYRange.max));

        transform.position = startingPoint;

        nextFireTime = Time.time + fireRate;

    }

    void Update()
    {
        float x = Mathf.PingPong(Time.time * randomSpeed, movementBounds.max - movementBounds.min) + movementBounds.min;

        transform.position = new Vector2(x, transform.position.y);

        if (Time.time >= nextFireTime)
        {
            bulletManager.GetBullet(bulletSpawn.position, BulletOwner.Enemy);
            nextFireTime = Time.time + fireRate;
        }
    }
}
