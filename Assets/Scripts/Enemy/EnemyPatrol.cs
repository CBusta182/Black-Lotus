using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] Rigidbody2D enemy;
    RaycastHit2D edgeCheck;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    private bool isFacingRight;
    [SerializeField] private float speed;
    float wait = 2f;
    [SerializeField] Animator enemAnim; 
    void Update()
    {
        edgeCheck = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayer);
        if (edgeCheck.collider)
        {
            enemAnim.SetBool("isRunning", true);
            if (isFacingRight)
            {
                enemy.velocity = new Vector2(speed, enemy.velocity.y);
            }
            else
            {
                enemy.velocity = new Vector2(-speed, enemy.velocity.y);
            }
        }
        else
        {
            enemAnim.SetBool("isRunning", false);
            enemAnim.SetBool("isFloating", true);
            enemy.velocity = Vector2.zero;
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
}
