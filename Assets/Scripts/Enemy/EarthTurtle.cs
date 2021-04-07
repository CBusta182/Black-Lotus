using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTurtle : Enemy
{
    [SerializeField] Rigidbody2D turtleRB;
    [SerializeField] Animator turtleAnim;
    [SerializeField] RaycastHit2D edgeCheck;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float wait = 3f;
    void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        if (edgecheck(edgeCheck, groundCheck, groundLayer))
        {
            Patrol(turtleAnim, "isWalking", turtleRB, moveSpeed, isFacingRight);
        }
    }

}
