using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEarthMonk : Enemy
{
    [SerializeField] Rigidbody2D monkRb;
    [SerializeField] Animator monkAnim;
    [SerializeField] RaycastHit2D edgeCheck;
    [SerializeField] Transform groundCheck, firingPoint;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float wait = 0.5f, agroRange, attackRange, timeUntilFire, fireRate;
    [SerializeField] GameObject attackPrefab;
    void Start()
    {
        fireRate = timeUntilFire; 
        currentHealth = maxHealth;
    }
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if (!isHurt && !isDead)
        {
            if (distToPlayer > agroRange)
            {
                if (edgecheck(edgeCheck, groundCheck, groundLayer))
                {
                    Patrol(monkAnim, "isRunning", monkRb, moveSpeed, isFacingRight);
                }
                else
                {
                    StopPatrol(monkAnim, "isRunning", monkRb, ref wait, ref isFacingRight);
                }
            }
            else if (distToPlayer <= agroRange)
            {

                if (wait > 0)
                {
                    wait -= Time.deltaTime;
                }
                else
                {
                    monkAnim.Play("Run");
                    if (transform.position.x < Mathf.Floor(GameObject.FindGameObjectWithTag("Player").transform.position.x))
                    {
                        monkRb.velocity = new Vector2(moveSpeed, 0);
                        transform.localScale = new Vector2(-1, 1);
                    }
                    else if (transform.position.x > Mathf.Floor(GameObject.FindGameObjectWithTag("Player").transform.position.x))
                    {
                        monkRb.velocity = new Vector2(-moveSpeed, 0);
                        transform.localScale = new Vector2(1, 1);
                    }
                }


                if (distToPlayer <= attackRange)
                {
                    wait = 0.5f;
                    monkRb.velocity = Vector2.zero;
                    if (timeUntilFire > 0 && !isHurt)
                    {
                        monkAnim.Play("Idle");
                        timeUntilFire -= Time.deltaTime;
                    }
                    else if (timeUntilFire <= 0 && !isHurt)
                    {
                        monkAnim.Play("Attack");
                    }
                }
            }
        }
        else
        {
            monkRb.velocity = Vector2.zero;
        }
        HealthCheck(monkRb, monkAnim, "Death");
    }
    public void notHurt() { isHurt = false; }
    public void Dead() { Destroy(gameObject); }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !isDead)
        {
            isHurt = true; 
            TakeDamage(monkAnim, "Hurt", monkRb, 10);
        }
    }
    public void Shoot()
    {
        float angle = isFacingRight ? 0f : 180f;
        Instantiate(attackPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        timeUntilFire = fireRate;
    }
}
