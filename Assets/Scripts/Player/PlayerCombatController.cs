﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCombatController : MonoBehaviour
{
    
    PlayerController pc;
    [SerializeField] float lightAttackCoolDown, heavyAttackCoolDown;
    float timeUntilLightAttack,timeUntlHeavyAttack;
    public Transform firingPoint;
    public GameObject lightAttackPrefab, HeavyAttackPrefab,projectilePrefab;
    [SerializeField] private float inputTimer, lightAttackRadius, lightAttackDamage, fireRate;
    [SerializeField] Animator combatAnim;
    [SerializeField] private Transform lightAttackHitBoxPos;
    [SerializeField] LayerMask whatIsDamageable;
    private float timeUntilFire;
    void Start()
    {
        combatAnim = GetComponent<Animator>();
        pc = gameObject.GetComponent<PlayerController>();
    }
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Z) && timeUntilFire < Time.time)
        {
            Shoot();
            combatAnim.Play("Fist Attack 2");
            timeUntilFire = Time.time + fireRate;
        }
        if (Input.GetKey(KeyCode.X) && timeUntilLightAttack < Time.time)
        {
            SpawnLightAttack();
            timeUntilLightAttack = Time.time + lightAttackCoolDown;
            //make a combat active bool so the idle animation does not try to fight it 
            //make a way for the attacks to link together
            combatAnim.Play("Fist Attack"); 
        }
        if (Input.GetKey(KeyCode.E) && timeUntlHeavyAttack < Time.time)
        {
            SpawnHeavyAttack();
            timeUntlHeavyAttack = Time.time + heavyAttackCoolDown;
        }
    }
    void Shoot()
    {
        float angle = pc.isFacingRight ? 0f : 180f;
        Instantiate(projectilePrefab, firingPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }

    void SpawnLightAttack()
    {
        float angle = pc.isFacingRight ? 0f : 180f;
        Instantiate(lightAttackPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
    void SpawnHeavyAttack()
    {
        float angle = pc.isFacingRight ? 0f : 180f;
        Instantiate(HeavyAttackPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
}
