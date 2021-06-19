using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float maxHealth = 100, currentHealth, kbForce, kbTime, timeUntilEndKB;
    public Slider slider;
    [SerializeField] Animator anim;
    public bool isDead, isHurt;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerController pc;
    [SerializeField] private PlayerCombatController pcc; 
    void Start()
    {
        timeUntilEndKB = kbTime; 
        isDead = false; 
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }
    void Update()
    {
        if (isHurt){
            anim.Play(pcc.stateStr + " Hurt");
        }
        checkHealth();
    }
    public void checkHealth()
    {
        if (currentHealth < 1)
        {
            //knockBack();
            isDead = true;
            anim.Play(pcc.stateStr + " Death");
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
        SceneManager.LoadScene("Earth Kingdom");
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if ((collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Attack")) && !isDead)
    //    {
    //        if (isHurt = true && collision.rigidbody.position.x < rb.position.x)
    //        {
    //            if (currentHealth > 10) { isHurt = true; }
    //            currentHealth -= 10;
    //            SetHealth(currentHealth);
    //            timeUntilEndKB = kbTime; 
    //            while (timeUntilEndKB < Time.time && isHurt)
    //            {
    //                if (collision.gameObject.CompareTag("Spike"))
    //                {
    //                    Debug.Log("on the right");
    //                    rb.AddForce(new Vector2(kbForce * 100, 0));
    //                    Debug.Log(collision.rigidbody.position.x > rb.position.x);
    //                    timeUntilEndKB -= Time.time;
    //                }
    //                else
    //                {
    //                    rb.AddForce(new Vector2(kbForce, 0));
    //                    timeUntilEndKB -= Time.time;
    //                }
 
    //                Debug.Log("player pos: " + rb.position + "enem pos: " + collision.rigidbody.position);
    //            } 
    //        }
    //        else if (isHurt = true && collision.rigidbody.position.x > rb.position.x)
    //        {
    //            if (currentHealth > 10) { isHurt = true; }
    //            currentHealth -= 10;
    //            SetHealth(currentHealth);
    //            timeUntilEndKB = kbTime;
    //            while (timeUntilEndKB < Time.time && isHurt)
    //            {
    //                if (collision.gameObject.CompareTag("Spike"))
    //                {
    //                    Debug.Log("on the left");
    //                    rb.AddForce(new Vector2(-kbForce * 100, 0));
    //                    Debug.Log(collision.rigidbody.position.x > rb.position.x);
    //                    timeUntilEndKB -= Time.time;
    //                }
    //                else
    //                {
    //                    rb.AddForce(new Vector2(-kbForce, 0));
    //                    timeUntilEndKB -= Time.time;
    //                }
    //            }
    //            //rb.AddForce(new Vector2(-knockbackForce, 0));
    //        }
    //    }
         
    //}
    public void endKnockBack() => isHurt = false; 
    public void knockBack()
    {
        if (pc.isFacingRight)
        {
            rb.AddForce(new Vector2(-kbForce, 0));
        }
        else
        {
            rb.AddForce(new Vector2(kbForce, 0));
        }
    }
}
