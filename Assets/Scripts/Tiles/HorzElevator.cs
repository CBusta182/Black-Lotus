using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorzElevator : MonoBehaviour
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

        if (Mathf.Floor(transform.position.x) == initalPos.x)
        {
            islandRb.velocity = new Vector2(moveSpeed, 0);
        }
        else if (Mathf.Floor(transform.position.x) == finalPos.x)
        {
            islandRb.velocity = new Vector2(-moveSpeed, 0);
        }
    }
}
