using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float maxHealth = 100, currentHealth, knockbackForce;
    public Slider slider;
    [SerializeField] Animator anim;
    public bool isDead, isHurt;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerController pc;
    [SerializeField] private PlayerCombatController pcc; 
    void Start()
    {
        isDead = false; 
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }
    void Update()
    {
        if (isHurt)
        {
            anim.Play(pcc.stateStr + "Hurt");
            knockBack();
        }
        if (isDead)
        {
            knockBack(); 
        }
        checkHealth();
    }
    public void checkHealth()
    {
        if (currentHealth < 1)
        {
            //knockBack();
            isDead = true;
            anim.Play(pcc.stateStr + "Death");
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
        SceneManager.LoadScene("Level 1");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Attack")) && !isDead)
        {
            if(currentHealth > 10) { isHurt = true; }
            currentHealth -= 10;
            SetHealth(currentHealth);
        }
         
    }
    public void endKnockBack(){isHurt = false; }
    public void knockBack()
    {
        if (pc.isFacingRight)
        {
            rb.AddForce(new Vector2(-knockbackForce, 0));
        }
        else
        {
            rb.AddForce(new Vector2(knockbackForce, 0));
        }
    }
}
