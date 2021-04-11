using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEarthMonk : Enemy
{
    [SerializeField] float agroRange, attackRange, coolDown, timeUntilFire;
    [SerializeField] Rigidbody2D enemyRb;
    [SerializeField] Animator guardEMonkAnim;
    [SerializeField] GameObject projectilePrefab;
    public Transform firingPoint;
    [SerializeField] Vector2 initPos;
    [SerializeField] bool attacked; 
    void Start()
    {
        initPos = transform.position; 
        currentHealth = maxHealth;
        attacked = false;
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if ((distToPlayer < agroRange) && !(distToPlayer < attackRange))
        {
            ChasePlayer(guardEMonkAnim, "isRunning", transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, enemyRb);
        }
        else if (distToPlayer < attackRange)
        {
            if (!isDead)
            {
                enemyRb.velocity = Vector2.zero;
                if (!attacked)
                {
                    guardEMonkAnim.SetBool("Attack1", true);
                    guardEMonkAnim.SetBool("Attack2", false);
                }
                else if (attacked)
                {
                    guardEMonkAnim.SetBool("Attack1", false);
                    guardEMonkAnim.SetBool("Attack2", true);
                }
            }
            else
            {
                guardEMonkAnim.SetBool("Attack1", false);
                guardEMonkAnim.SetBool("Attack2", false);
            }
        }
        else if (distToPlayer > agroRange)
        {
            StopChasePlayer(guardEMonkAnim, "isRunning", transform.position, initPos, enemyRb);
        }
        HealthCheck(enemyRb, guardEMonkAnim, "Monk-Death");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !isDead)
        {
            TakeDamage(guardEMonkAnim, "isHurt", enemyRb, collision.gameObject.GetComponent<Projectile>().bulletDamage);
        }
    }
    void Shoot()
    {
        //currently isFacingRight is not being set to true
        float angle = isFacingRight ? 0f : 180f;
        Instantiate(projectilePrefab, firingPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
    void switchAttack()
    {
        attacked = !attacked; 
    }
}
