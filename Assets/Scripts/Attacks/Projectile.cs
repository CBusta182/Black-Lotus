using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed = 15f;
    public float bulletDamage = 10f;
    [SerializeField] Rigidbody2D projectile;

    private void FixedUpdate()
    {
        projectile.velocity = transform.right * bulletSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            Destroy(gameObject);
        }
    }
}
