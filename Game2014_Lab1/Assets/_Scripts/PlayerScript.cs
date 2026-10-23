using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    private GameController gc;


    [SerializeField] public int lives;
    [SerializeField] private GameObject forcefield;
    [SerializeField] private int forcefieldDuration;
    private bool bIsForcefieldActive = false;

    [SerializeField] private float attackSpeedBuffDuration;
    private bool bIsAttackSpeedBuffActive = false;
    private float attackSpeedTimeRemaining = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        bulletManager = FindAnyObjectByType<BulletManager>();
        gc = FindAnyObjectByType<GameController>();

        nextFireTime = Time.time + fireRate;

        forcefield.SetActive(false);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            Destroy(collision.gameObject);
            TakeDamage();
        }
    }

    public void TakeDamage()
    {

        if (bIsForcefieldActive)
        {

            return;
        }

        lives--;
        gc.UpdateLivesText();
        StartCoroutine(ActivateForcefield());

        if (lives == 0)
        {

            SceneManager.LoadScene(2);
        }
    }

    IEnumerator ActivateForcefield()
    {
        forcefield.SetActive(true);
        bIsForcefieldActive = true;

        yield return new WaitForSeconds(forcefieldDuration);

        forcefield.SetActive(false);
        bIsForcefieldActive = false;

        StopCoroutine(ActivateForcefield());
    }

    public void PickupBuff(PickupType type)
    {
        if (type == PickupType.Heal)
        {
            if (lives == 3)
            {

                return;
            }

            lives++;
            gc.UpdateLivesText();
        }

        else if (type == PickupType.AttackSpeed)
        {

            if (bIsAttackSpeedBuffActive)
            {

                attackSpeedTimeRemaining += attackSpeedBuffDuration;
            }

            else
            {

                StartCoroutine(AttackSpeedBuff());
            }
        }
    }

    IEnumerator AttackSpeedBuff()
    {
        bIsAttackSpeedBuffActive = true;

        fireRate /= 2.0f;
        nextFireTime = Time.time + fireRate;

        attackSpeedTimeRemaining = attackSpeedBuffDuration;

        while (attackSpeedTimeRemaining > 0.0f)
        {

            attackSpeedTimeRemaining -= Time.deltaTime;
            yield return null;
        }

        fireRate *= 2.0f;

        bIsAttackSpeedBuffActive = false;
    }

}
