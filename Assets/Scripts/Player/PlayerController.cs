using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D player;

    private float xMove;
    public float jumpforce;
    public Transform feet;
    [SerializeField] Collider2D groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Animator playerAnimation;
    [HideInInspector] public bool isFacingRight = true;
    private void Update()
    {
        if (Math.Abs(xMove) > 0.05f) { playerAnimation.SetBool("isRunning", true); }
        else { playerAnimation.SetBool("isRunning", false); }
        HandleMovement();
        facingDirection(); 
    }
    private void HandleMovement()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.UpArrow) && isGrounded())
        {
            player.velocity = new Vector2(player.velocity.x, jumpforce);
        }
        player.velocity = new Vector2(xMove * moveSpeed, player.velocity.y);
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
