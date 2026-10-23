using UnityEngine;

public enum PickupType
{
    Heal,
    Shield,
    AttackSpeed
}

public class PickupScript : MonoBehaviour
{
    private PickupType pickup;
    private int random;

    [SerializeField] private Sprite attackSpeed;
    [SerializeField] private Sprite heal;
    [SerializeField] private Sprite shield;

    private SpriteRenderer sr;
    private PlayerScript ps;

    [SerializeField] private float speed;

    private void Awake()
    {
        
        sr = GetComponent<SpriteRenderer>();
        ps = FindAnyObjectByType<PlayerScript>();

    }

    void Start()
    {
        random = Random.Range(1, 4);
        //random = 1;

        if (random == 1)
        {

            pickup = PickupType.Heal;
            sr.sprite = heal;
        }

        else if (random == 2)
        {

            pickup = PickupType.Shield;
            sr.sprite = shield;
        }

        else if (random == 3)
        {

            pickup = PickupType.AttackSpeed;
            sr.sprite = attackSpeed;
        }

        gameObject.AddComponent<BoxCollider2D>();
    }

    void Update()
    {
        transform.position -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);

        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {

            ps.PickupBuff(pickup);
            Destroy(gameObject);
        }
    }
}
