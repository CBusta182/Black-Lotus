using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertElevator : MonoBehaviour
{
    [SerializeField] Rigidbody2D islandRb;
    [SerializeField] Vector2 initalPos, finalPos;
    public float moveSpeed; 

    void Start()
    {
        initalPos = transform.position; 
    }

    void Update()
    {

        if (Mathf.Floor(transform.position.y) == initalPos.y)
        {
            islandRb.velocity = new Vector2(0, moveSpeed); 
        }
        else if (Mathf.Floor(transform.position.y) == finalPos.y)
        {
            islandRb.velocity = new Vector2(0, -moveSpeed);
        }
    }
}
