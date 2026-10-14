using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private Boundary movementBounds;
    [SerializeField] private Boundary startingXRange;
    [SerializeField] private Boundary startingYRange;

    private Vector2 startingPoint;
    private float randomSpeed;

    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int fireRate = 10;

    private BulletManager bulletManager;


    void Start()
    {

        bulletManager = FindAnyObjectByType<BulletManager>();

        randomSpeed = Random.Range(movementBounds.min, movementBounds.max);

        startingPoint = new Vector2(Random.Range(startingXRange.min, startingXRange.max), Random.Range(startingYRange.min, startingYRange.max));

        transform.position = startingPoint;

    }

    void Update()
    {
        float x = Mathf.PingPong(Time.time * randomSpeed, movementBounds.max - movementBounds.min) + movementBounds.min;

        transform.position = new Vector2(x, transform.position.y);
    }

    private void FixedUpdate()
    {
        if (Time.frameCount % fireRate == 0)
        {
            //var bullet = Instantiate(bulletPrefab);
            //bullet.transform.position = bulletSpawn.position;

            bulletManager.GetBullet(bulletSpawn.position);
        }
    }
}
