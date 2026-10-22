using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    private Queue<GameObject> pool;

    [SerializeField] private int bulletNumber = 50;

    [SerializeField] private GameObject bulletPrefab;


    void Start()
    {
        pool = new Queue<GameObject>();
        BuildBulletPool();
    }


    void Update()
    {
        
    }

    private void BuildBulletPool()
    {

        for (int i = 0; i < bulletNumber; i++)
        {

            var bullet = Instantiate(bulletPrefab);
            bullet.transform.SetParent(transform);
            bullet.SetActive(false);
            pool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet(Vector3 spawnPos, BulletOwner owner)
    {

        if (pool.Count < 1)
        {
            AddBullet();
        }

        var bullet = pool.Dequeue();
        bullet.transform.position = spawnPos;
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        bulletScript.SetOwner(owner);
        bullet.SetActive(true);
        return bullet;
    }

    private void AddBullet()
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.SetParent(transform);
        bullet.SetActive(false);
        pool.Enqueue(bullet);
        bulletNumber++; 
    }

    public void ReturnBullet(GameObject bullet)
    {

        bullet.SetActive(false);
        pool.Enqueue(bullet);
    }
}
