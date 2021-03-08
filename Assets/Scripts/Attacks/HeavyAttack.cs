using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : MonoBehaviour
{
    public float horizontalSpeed = 2f;
    public float heavyAttackDamage = 30f;
    [SerializeField] float despawnTime;
    [SerializeField] Rigidbody2D heavyAttack;
    private void Start()
    {
        StartCoroutine(DespawnObject());
    }
    private void FixedUpdate()
    {
        heavyAttack.velocity = transform.right * horizontalSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
    IEnumerator DespawnObject()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
