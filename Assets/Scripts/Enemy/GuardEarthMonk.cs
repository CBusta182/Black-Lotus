using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEarthMonk : Enemy
{
    [SerializeField] float agroRange;
    [SerializeField] Rigidbody2D enemyRb;
    [SerializeField] Animator guardEMonkAnim;
    [SerializeField] Vector2 initPos; 
    void Start()
    {
        initPos = transform.position; 
        currentHealth = maxHealth;
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if ((distToPlayer < agroRange))
        {
            ChasePlayer(guardEMonkAnim, "isFloating", "isRunning", transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, enemyRb);
        }
        else if (distToPlayer < attackRange)
        {
            //attack player
        }
        else if (distToPlayer > agroRange)
        {
            StopChasePlayer(guardEMonkAnim, "isFloating", "isRunning", transform.position, initPos, enemyRb);
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
}
