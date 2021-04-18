using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingIsland : MonoBehaviour
{
    [SerializeField] Rigidbody2D islandRb;
    [SerializeField] Vector2 initalPos, finalPos;
    public float moveSpeed;
    public string type; 

    void Start()
    {
        initalPos = transform.position;
        if ((Mathf.Floor(initalPos.x) < finalPos.x && Mathf.Floor(initalPos.y) < finalPos.y) || (Mathf.Floor(initalPos.x) < finalPos.x && Mathf.Floor(initalPos.y) < finalPos.y))
        {
            type = "Diagonal";
        }
        else if (Mathf.Floor(initalPos.x) < finalPos.x)
        {
            type = "Horizontal";
        }
        else if (Mathf.Floor(initalPos.y) < finalPos.y)
        {
            type = "Vertical";
        }
    }

    void Update()
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
        //if (type == "Diagonal")
        //{
        //    MoveDiagonally();
        //}
        //else if (type == "Horizontal")
        //{
        //    MoveHorizontally();
        //}
        //else if (type == "Vertical")
        //{
        //    MoveVertically(); 
        //}
        //else
        //{
        //    print("Not a valid movement");
        //}
    }
    public void MoveDiagonally()
    {
        if (Mathf.Floor(transform.position.x) <= initalPos.x && Mathf.Floor(transform.position.y) <= initalPos.y)
        {
            islandRb.velocity = new Vector2(moveSpeed, moveSpeed);
        }
        else if (Mathf.Floor(transform.position.x) >= finalPos.x && Mathf.Floor(transform.position.y) >= finalPos.y)
        {
            islandRb.velocity = new Vector2(-moveSpeed, -moveSpeed);
        }
    }
    public void MoveHorizontally()
    {
        if (Mathf.Floor(transform.position.x) <= initalPos.x)
        {
            islandRb.velocity = new Vector2(moveSpeed, 0);
        }
        else if (Mathf.Floor(transform.position.x) >= finalPos.x)
        {
            islandRb.velocity = new Vector2(-moveSpeed, 0);
        }
    }
    public void MoveVertically()
    {
        if (Mathf.Floor(transform.position.y) <= initalPos.y)
        {
            islandRb.velocity = new Vector2(0, moveSpeed);
        }
        else if (Mathf.Floor(transform.position.y)>= finalPos.y)
        {
            islandRb.velocity = new Vector2(0, -moveSpeed);
        }
    }
}
