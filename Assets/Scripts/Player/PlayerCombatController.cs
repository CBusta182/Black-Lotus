using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    //public int maxHealth = 100, currentHealth;
    public HealthBar healthBar; 
    PlayerController pc;
    [SerializeField] float lightAttackCoolDown, heavyAttackCoolDown;
    float timeUntilLightAttack,timeUntlHeavyAttack;
    public Transform firingPoint;
    public GameObject lightAttackPrefab, HeavyAttackPrefab,projectilePrefab;
    //private bool
    //    gotInput,
    //    isAttacking;
    //private float lastInputime = Mathf.NegativeInfinity;
    [SerializeField] 
    private float inputTimer, lightAttackRadius, lightAttackDamage, fireRate;
    [SerializeField] Animator combatAnim;
    [SerializeField] private Transform lightAttackHitBoxPos;
    [SerializeField] LayerMask whatIsDamageable;
    float timeUntilFire;
    void Start()
    {
        //currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
        combatAnim = GetComponent<Animator>();
        //combatAnim.SetBool("canAttack", combatEnabled);
        pc = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckCombatInput();
        //CheckAttacks();
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    TakeDamage(); 
        //}
        if (Input.GetKey(KeyCode.Q) && timeUntilFire < Time.time)
        {
            Shoot();
            timeUntilFire = Time.time + fireRate;
        }
        if (Input.GetKey(KeyCode.W) && timeUntilLightAttack < Time.time)
        {
            SpawnLightAttack();
            timeUntilLightAttack = Time.time + lightAttackCoolDown;
            //make a combat active bool so the idle animation does not try to fight it 
            //make a way for the attacks to link together
            combatAnim.Play("Fist Attack 1"); 
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
    public void Death()
    {
        Destroy(gameObject);
        LevelManager.instance.Respawn();
    }
    //private void TakeDamage()
    //{
    //    if (currentHealth >= 1)
    //    {
    //        currentHealth -= 10;
    //        healthBar.SetHealth(currentHealth);
    //    }
    //    if (currentHealth < 1)
    //    {
    //        Destroy(gameObject);
    //        LevelManager.instance.Respawn();
    //        //HealthBar.instance.resetHealth(); 
    //        currentHealth = maxHealth;
    //        healthBar.SetMaxHealth(currentHealth);
    //    }
    //}
    //TODO make it wait for the animation to finish and move this to player controller

    //private void CheckCombatInput()
    //{
    //    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.E))
    //    {
    //        if (combatEnabled)
    //        {
    //            gotInput = true;
    //            lastInputime = Time.time;
    //        }
    //    }
    //}
    //private void CheckAttacks()
    //{
    //    if (gotInput)
    //    {
    //        if (!isAttacking)
    //        {
    //            gotInput = false;
    //            isAttacking = false;
    //            combatAnim.SetBool("lightAttack", true);
    //            combatAnim.SetBool("isAttacking", isAttacking);
    //        }
    //    }
    //    if(Time.time >= lastInputime + inputTimer)
    //    {
    //        gotInput = false;
    //    }
    //}
    private void CheckLightAttackHitbox()
    {
        Collider2D[] dectectedObjects = Physics2D.OverlapCircleAll(lightAttackHitBoxPos.position, lightAttackRadius, whatIsDamageable);
        foreach(Collider2D collider in dectectedObjects){
            collider.transform.parent.SendMessage("Damage", lightAttackDamage);
        }
    }
    //private void FinishLightAttack()
    //{
    //    isAttacking = false;
    //    combatAnim.SetBool("isAttacking", isAttacking);
    //    combatAnim.SetBool("lightAttack", false);
    //}
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(lightAttackHitBoxPos.position, lightAttackRadius);
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
