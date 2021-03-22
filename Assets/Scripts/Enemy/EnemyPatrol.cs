using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] Rigidbody2D enemyrb2d;
    RaycastHit2D edgeCheck;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    private bool isFacingRight;
    [SerializeField] private float speed;
    float wait = 2f;
    public float
        currentHealth,
        maxHealth; 
    [SerializeField] Animator enemAnim;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        edgeCheck = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayer);
        if (edgeCheck.collider)
        {
            enemAnim.SetBool("isRunning", true);
            if (isFacingRight)
            {
                enemyrb2d.velocity = new Vector2(speed, enemyrb2d.velocity.y);
            }
            else
            {
                enemyrb2d.velocity = new Vector2(-speed, enemyrb2d.velocity.y);
            }
        }
        else
        {
            enemAnim.SetBool("isRunning", false);
            enemAnim.SetBool("isFloating", true);
            enemyrb2d.velocity = Vector2.zero;
            if (wait > 0)
            {
                wait -= Time.deltaTime;
            }
            else if (wait <= 0)
            {
                isFacingRight = !isFacingRight;
                transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
                wait = 2f;
                enemAnim.SetBool("isFloating", false); 
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            print("got damaged");
            TakeDamage(collision.gameObject.GetComponent<Projectile>().bulletDamage);
        }
    }
    public void TakeDamage(float damageEnem)
    {
        enemyrb2d.velocity = Vector2.zero;
        enemAnim.SetTrigger("isHurt");
        currentHealth -= damageEnem;
    }
    public void Death()
    {
        Destroy(gameObject);
    }
}
