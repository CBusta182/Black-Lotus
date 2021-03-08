using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlam : MonoBehaviour
{
    PlayerController pm;
    [SerializeField] float slamForce;
    [SerializeField] float slamCd; 
    float timeUntilSlam; 
    void Start()
    {
        pm = gameObject.GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow) && !pm.isGrounded() && timeUntilSlam < Time.time)
        {
            pm.player.velocity = new Vector2(pm.player.velocity.x, -slamForce);
            timeUntilSlam = Time.time + slamCd; 
        }
    }
}
