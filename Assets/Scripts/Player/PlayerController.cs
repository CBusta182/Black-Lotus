using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D player;
    [SerializeField] private HealthController hc;
    [SerializeField] private PlayerCombatController pcc;
    private float xMove;
    public float jumpforce;
    public Transform feet;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public Animator playerAnimation;
    public bool isFacingRight = true;
    public bool inCombat;
    private void Update()
    {
        if (Math.Abs(xMove) > 0.05f && isGrounded() && !CombatCheck()) {
            playerAnimation.Play(pcc.stateStr + " Run"); 
        }
        else if (xMove == 0 && isGrounded() && !CombatCheck()) { 
            playerAnimation.Play(pcc.stateStr + " Idle"); 
        }
        if (!CombatCheck()) 
        {
            HandleMovement();
        }
        facingDirection(); 
    }
    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            xMove = 1; 
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            xMove = -1; 
        }
        else if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            xMove = 0; 
        }

        if (Input.GetKey(KeyCode.UpArrow) && isGrounded())
        {
            player.velocity = new Vector2(player.velocity.x, jumpforce);
            
        }
        if (!isGrounded())
        {
            playerAnimation.Play(pcc.stateStr + " Jump");
        }
        player.velocity = new Vector2(xMove * moveSpeed, player.velocity.y);
    }
    public bool CombatCheck()
    {
        if (hc.isDead || hc.isHurt || pcc.isAttacking)
        {
            inCombat = true; 
        }
        else
        {
            inCombat = false; 
        }
        return inCombat; 
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
    public bool isGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayer);
        if (groundCheck)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
