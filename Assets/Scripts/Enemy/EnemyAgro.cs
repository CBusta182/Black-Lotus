using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgro : MonoBehaviour
{
    [SerializeField]
    float
        agroRange,
        attackRange,
        moveSpeed;
    Rigidbody2D enemyrb2d;
    private Vector2 initPos;
    [SerializeField] Animator enemAgroAnim;
    public float
        currentHealth,
        maxHealth; 
    void Start()
    {
        currentHealth = maxHealth;
        enemAgroAnim.SetBool("isFloating", true);
        initPos = transform.position; 
        enemyrb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if ((distToPlayer < agroRange))
        {
            ChasePlayer();
        }
        else if (distToPlayer < attackRange)
        {
            //attack player
        }
        else if (distToPlayer > agroRange)
        {
            StopChasePlayer();
        }
        if(currentHealth <= 0)
        {
            enemyrb2d.velocity = Vector2.zero;
            enemAgroAnim.Play("Monk-Death");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(collision.gameObject.GetComponent<Projectile>().bulletDamage);
        }
    }
    public void TakeDamage(float damageEnem)
    {
        enemyrb2d.velocity = Vector2.zero;
        enemAgroAnim.SetTrigger("isHurt");
        currentHealth -= damageEnem;
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    private void ChasePlayer()
    {
        enemAgroAnim.SetBool("isFloating", false);
        enemAgroAnim.SetBool("isRunning", true);
        //if the enemy is on the left of the player
        if (transform.position.x < GameObject.FindGameObjectWithTag("Player").transform.position.x)
        {
            enemyrb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1); 
        }
        //if the enemy is on the right of the player
        else
        {
            enemyrb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
    }
    private void StopChasePlayer()
    {
        if (Mathf.Floor(transform.position.x) < initPos.x)
        {
            enemyrb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        //if the enemy is on the right of the player
        else if (Mathf.Floor(transform.position.x) > initPos.x)
        {
            enemyrb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (Mathf.Floor(transform.position.x) == initPos.x)
        {
            enemyrb2d.velocity = Vector2.zero;
            transform.localScale = new Vector2(1, 1);
            enemAgroAnim.SetBool("isRunning", false);
            enemAgroAnim.SetBool("isFloating", true);
        }
    }
}