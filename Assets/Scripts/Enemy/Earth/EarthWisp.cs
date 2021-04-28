using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWisp : Enemy
{
    [SerializeField] Rigidbody2D wispRB;
    [SerializeField] Animator wispAnim;
    [SerializeField] RaycastHit2D edgeCheck;
    [SerializeField] Transform groundCheck, firingPoint;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float wait = 1, agroRange, timeUntilFire;
    public float fireRate; 
    [SerializeField] GameObject projectilePrefab;
    void Start()
    {
        timeUntilFire = fireRate;
        currentHealth = maxHealth;
    }

    void Update()
    {
        HealthCheck(wispRB, wispAnim, "Death");
        float distToPlayer = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if (distToPlayer > agroRange)
        {
            if (edgecheck(edgeCheck, groundCheck, groundLayer))
            {
                Patrol(wispAnim, "isRunning", wispRB, moveSpeed, isFacingRight);
            }
            else
            {
                StopPatrol(wispAnim, "isRunning", wispRB, ref wait, ref isFacingRight);
            }
        }
        else if (distToPlayer < agroRange)
        {
            if(Mathf.Floor(GameObject.FindGameObjectWithTag("Player").transform.position.x) < transform.position.x)
            {
                isFacingRight = false;
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                isFacingRight = true;
                transform.localScale = new Vector2(-1, 1);
            }
            wispRB.velocity = Vector2.zero;
            wispAnim.SetBool("isRunning", false);
            if(timeUntilFire > 0 && !isHurt)
            {
                wispAnim.Play("Idle");
                timeUntilFire -= Time.deltaTime; 
            }
            else if (timeUntilFire <= 0 && !isHurt)
            {
                wispAnim.Play("Attack");
            }
        }
    }
    public void notHurt() {isHurt = false; }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !isDead)
        {
            isHurt = true; 
            TakeDamage(wispAnim, "Hurt", wispRB, 10);
        }
    }
    public void Shoot()
    {
        float angle = isFacingRight ? 0f : 180f;
        Instantiate(projectilePrefab, firingPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        timeUntilFire = fireRate;
    }
    public void Dead() { Destroy(gameObject); }
}
