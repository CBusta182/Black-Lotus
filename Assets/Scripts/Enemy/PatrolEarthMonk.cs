using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEarthMonk : Enemy
{
    [SerializeField] Rigidbody2D enemyRb;
    [SerializeField] Animator PatrolEMonkAnim;
    [SerializeField] RaycastHit2D edgeCheck;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float wait = 2f;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(edgecheck(edgeCheck, groundCheck, groundLayer))
        {
            Patrol(PatrolEMonkAnim, "isRunning", enemyRb, moveSpeed, isFacingRight);
        }
        else
        {
            StopPatrol(PatrolEMonkAnim, "isRunning", "isFloating", enemyRb, ref wait, ref isFacingRight);
        }
        HealthCheck(enemyRb, PatrolEMonkAnim, "Monk-Death");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !isDead)
        {
            TakeDamage(PatrolEMonkAnim, "isHurt", enemyRb, collision.gameObject.GetComponent<Projectile>().bulletDamage);
        }
    }
}
