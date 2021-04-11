using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float currentHealth, maxHealth, moveSpeed, distToPlayer, xMove;
    public bool isFacingRight, isDead = false, isHurt = false;
    protected float knockBackSpeed;  
     
    public void ChasePlayer(Animator enemyAnim, string runningAnim, Vector2 enemyPos, Vector2 targetPos, Rigidbody2D enemyRb)
    {
        enemyAnim.SetBool(runningAnim, true);
        //if the enemy is on the left of the player
        if (enemyPos.x < targetPos.x)
        {
            enemyRb.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        //if the enemy is on the right of the player
        else
        {
            enemyRb.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
    }
    public void StopChasePlayer(Animator enemyAnim, string runningAnim, Vector2 enemyPos, Vector2 initPos, Rigidbody2D enemyRb)
    {
        if (Mathf.Floor(enemyPos.x) < Mathf.Floor(initPos.x))
        {
            enemyRb.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        //if the enemy is on the right of the player
        else if (Mathf.Floor(enemyPos.x) > Mathf.Floor(initPos.x))
        {
            enemyRb.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (Mathf.Floor(enemyPos.x) == Mathf.Floor(initPos.x))
        {
            enemyRb.velocity = Vector2.zero;
            transform.localScale = new Vector2(1, 1);
            enemyAnim.SetBool(runningAnim, false);
        }
    }
    public void facingDirection()
    {
        if (xMove > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            isFacingRight = true;
        }
        else if (xMove < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            isFacingRight = false;
        }
    }
    public void TakeDamage( Animator enemyAnim, string hurtAnim, Rigidbody2D enemyRb, float damage)
    {
        enemyRb.velocity = Vector2.zero;
        enemyAnim.SetTrigger(hurtAnim);
        currentHealth -= damage;
    }
    public virtual void HealthCheck(Rigidbody2D enemyRb, Animator enemyAnim, string deathAnim)
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            enemyRb.velocity = Vector2.zero;
            enemyAnim.Play(deathAnim);
        }
    }
    public bool edgecheck(RaycastHit2D edgeCheck, Transform groundCheck, LayerMask groundLayer)
    {
        edgeCheck = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayer);
        return edgeCheck.collider; 
    }
    public void Patrol(Animator enemyAnim, string runningAnim, Rigidbody2D enemyRb, float speed, bool isFacingRight)
    {
        enemyAnim.SetBool(runningAnim, true);
        if (isFacingRight)
        {
            enemyRb.velocity = new Vector2(speed, enemyRb.velocity.y);
        }
        else
        {
            enemyRb.velocity = new Vector2(-speed, enemyRb.velocity.y);
        }
    }
    public void StopPatrol(Animator enemyAnim, string runningAnim, Rigidbody2D enemyRb, ref float wait, ref bool isFacingRight)
    {
        enemyAnim.SetBool(runningAnim, false);
        enemyRb.velocity = Vector2.zero;
        if (wait > 0)
        {
            wait -= Time.deltaTime;
        }
        else if (wait <= 0)
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
            wait = 2f;
        }
    }
}
