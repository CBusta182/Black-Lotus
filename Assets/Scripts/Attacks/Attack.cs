using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float horizontalSpeed;
    [SerializeField] float despawnTime; 
    [SerializeField] Rigidbody2D attack;
    private void Start()
    {
        StartCoroutine(DespawnObject());
    }
    private void FixedUpdate()
    {
        attack.velocity = transform.right * horizontalSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    IEnumerator DespawnObject()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
