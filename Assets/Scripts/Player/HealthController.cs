using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float maxHealth = 100, currentHealth;
    public Slider slider;
    [SerializeField] Animator anim;
    public bool isDead; 
    void Start()
    {
        isDead = false; 
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();
    }
    public void checkHealth()
    {
        if (slider.value < 1)
        {
            isDead = true;
            anim.Play("Fist Death");
            currentHealth = maxHealth;
            SetHealth(currentHealth);
        }
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(float health)
    {
        slider.value = health;
    }
    public void Death()
    {
        SceneManager.LoadScene("TestLevel");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !isDead)
        {
            currentHealth -= collision.gameObject.GetComponent<Projectile>().bulletDamage; 
            SetHealth(currentHealth);
        }
    }
    //public void TakeDamage(Animator enemyAnim, string hurtAnim, Rigidbody2D enemyRb, float damage)
    //{
    //    enemyRb.velocity = Vector2.zero;
    //    enemyAnim.SetTrigger(hurtAnim);
    //    currentHealth -= damage;
    //}
}
