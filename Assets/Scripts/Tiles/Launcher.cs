using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{

    [SerializeField] private float jumpforce;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D gameObject = collision.gameObject.GetComponent<Rigidbody2D>(); 
        gameObject.velocity = new Vector2(gameObject.velocity.x, jumpforce);
    }
}
