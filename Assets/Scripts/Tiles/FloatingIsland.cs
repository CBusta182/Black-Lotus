using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingIsland : MonoBehaviour
{
    [SerializeField] Rigidbody2D islandRb;
    [SerializeField] Vector2 initalPos, finalPos;
    private GameObject target; 
    public float moveSpeed;
    //public Vector3 velocity, offset; 
    public string type; 

    void Start()
    {
        target = null; 
        initalPos = transform.position;
        if (initalPos.x < finalPos.x && initalPos.y < finalPos.y || (initalPos.x < finalPos.x && initalPos.y < finalPos.y))
        {
            type = "Diagonal";
        }
        else if (initalPos.x < finalPos.x)
        {
            type = "Horizontal";
        }
        else if (initalPos.y < finalPos.y)
        {
            type = "Vertical";
        }
    }

    void FixedUpdate()
    {
        switch (type)
        {
            case "Diagonal":
                MoveDiagonally();
                break;
            case "Horizontal":
                MoveHorizontally();
                break;
            case "Vertical":
                MoveVertically();
                break;
            default:
                print("Not a valid movement");
                break;
        }
    }
    //void OnTriggerStay2D(Collider2D col)
    //{
    //    target = col.gameObject;
    //    offset = target.transform.position - transform.position;
    //}
    //void OnTriggerExit2D(Collider2D col)
    //{
    //    target = null;
    //}
    //void LateUpdate()
    //{
    //    if (target != null)
    //    {
    //        target.transform.position = transform.position + offset;
    //    }
    //}
    public void MoveDiagonally()
    {
        if (transform.position.x <= initalPos.x && transform.position.y <= initalPos.y)
        {
            islandRb.velocity = new Vector2(moveSpeed, moveSpeed);
        }
        else if (transform.position.x >= finalPos.x && transform.position.y >= finalPos.y)
        {
            islandRb.velocity = new Vector2(-moveSpeed, -moveSpeed);
        }
    }
    public void MoveHorizontally()
    {
        if (transform.position.x <= initalPos.x)
        {
            islandRb.velocity = new Vector2(moveSpeed, 0);
        }
        else if (transform.position.x >= finalPos.x)
        {
            islandRb.velocity = new Vector2(-moveSpeed, 0);
        }
    }
    public void MoveVertically()
    {
        if (transform.position.y <= initalPos.y)
        {
            islandRb.velocity = new Vector2(0, moveSpeed);
        }
        else if (transform.position.y>= finalPos.y)
        {
            islandRb.velocity = new Vector2(0, -moveSpeed);
        }
    }
}
