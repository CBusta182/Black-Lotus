using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public PlayerCombatController playerCombatController; 
    public Slider slider;
    public GameObject healthBarPreFab;
    public Transform healthbarPos;
    public int maxHealth = 100, currentHealth;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            TakeDamage(); 
        }
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health; 
    }

    private void TakeDamage()
    {
        if (currentHealth >= 1)
        {
            currentHealth -= 10;
            SetHealth(currentHealth);
        }
        if (currentHealth < 1)
        {
            currentHealth = maxHealth;
            SetMaxHealth(currentHealth);
            playerCombatController.Death(); 
        }
    }
}
