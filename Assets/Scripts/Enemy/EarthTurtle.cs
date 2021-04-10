﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTurtle : Enemy
{
    [SerializeField] Rigidbody2D turtleRB;
    [SerializeField] Animator turtleAnim;
    [SerializeField] RaycastHit2D edgeCheck;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float wait = 1, agroRange;
    public float spikeDamage = 5; 
    void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if (distToPlayer > agroRange)
        {
            turtleAnim.SetBool("tucked", false);
            if (edgecheck(edgeCheck, groundCheck, groundLayer))
            {
                Patrol(turtleAnim, "isWalking", turtleRB, moveSpeed, isFacingRight);
            }
            else
            {
                StopPatrol(turtleAnim, "isWalking", turtleRB, ref wait, ref isFacingRight); 
            }
        }
        else if (distToPlayer < agroRange)
        {
            turtleAnim.SetBool("isWalking", false);
            turtleAnim.SetBool("tuck", true);
            turtleRB.velocity = Vector2.zero; 
        }
    }
    public void tucked()
    {
        turtleAnim.SetBool("tuck", false);
        turtleAnim.SetBool("tucked", true); 
    }

}
