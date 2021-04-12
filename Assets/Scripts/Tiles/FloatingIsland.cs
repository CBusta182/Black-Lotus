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
    }

    void Update()
    {
        if((Mathf.Floor(initalPos.x) < finalPos.x && Mathf.Floor(initalPos.y) < finalPos.y) || (Mathf.Floor(initalPos.x) < finalPos.x && Mathf.Floor(initalPos.y) < finalPos.y))
        {
            type = "Diagonal"; 
        }
        else if (Mathf.Floor(initalPos.x) < finalPos.x)
        {
            type = "Horizontal"; 
        }
        else if (Mathf.Floor(initalPos.y) < finalPos.y)
        {
            if (transform.position.y == initalPos.y)
            {
                islandRb.velocity = new Vector2(0, moveSpeed);
            }
            else if (transform.position.y == finalPos.y)
            {
                islandRb.velocity = new Vector2(0, -moveSpeed);
            }
            type = "Vertical";
        }
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
                print("not valid positions");
                islandRb.velocity = Vector2.zero;
                break; 
        }
    }
    public void MoveDiagonally()
    {
        if (Mathf.Floor(transform.position.x) == initalPos.x && Mathf.Floor(transform.position.y) == initalPos.y)
        {
            islandRb.velocity = new Vector2(moveSpeed, moveSpeed);
        }
        else if (Mathf.Floor(transform.position.x) == finalPos.x && Mathf.Floor(transform.position.y) == finalPos.y)
        {
            islandRb.velocity = new Vector2(-moveSpeed, -moveSpeed);
        }
    }
    public void MoveHorizontally()
    {
        if (Mathf.Floor(transform.position.x) == initalPos.x)
        {
            islandRb.velocity = new Vector2(0, moveSpeed);
        }
        else if (Mathf.Floor(transform.position.x) == finalPos.x)
        {
            islandRb.velocity = new Vector2(0, -moveSpeed);
        }
    }
    public void MoveVertically()
    {
        if (transform.position.y == initalPos.y)
        {
            islandRb.velocity = new Vector2(0, moveSpeed);
        }
        else if (transform.position.y == finalPos.y)
        {
            islandRb.velocity = new Vector2(0, -moveSpeed);
        }
    }
}
