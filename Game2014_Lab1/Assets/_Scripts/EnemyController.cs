using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private GameObject enemy;
    [SerializeField] private float enemySpawnRate;

    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    
    void Update()
    {
        
    }

    IEnumerator EnemySpawn()
    {
        while (true)
        {

            Instantiate(enemy, transform.position, Quaternion.Euler(0.0f, 0.0f, 90.0f));
            yield return new WaitForSeconds(enemySpawnRate);
        }
    }
}
