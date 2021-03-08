using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public GameObject[] hearts;
    public int life;
    PlayerController pc;
    [SerializeField] float 
        lightAttackCoolDown, 
        heavyAttackCoolDown;
    float 
        timeUntilLightAttack,
        timeUntlHeavyAttack;
    public Transform firingPoint;
    public GameObject 
        lightAttackPrefab,
        HeavyAttackPrefab,
        projectilePrefab;
    [SerializeField] private bool combatEnabled;
    private bool
        gotInput,
        isAttacking;
    private float lastInputime = Mathf.NegativeInfinity;
    [SerializeField] 
    private float 
        inputTimer, 
        lightAttackRadius, 
        lightAttackDamage,
        fireRate;
    [SerializeField] Animator combatAnim;
    [SerializeField] private Transform lightAttackHitBoxPos;
    [SerializeField] LayerMask whatIsDamageable;
    float timeUntilFire;
    void Start()
    {
        life = hearts.Length;
        combatAnim = GetComponent<Animator>();
        combatAnim.SetBool("canAttack", combatEnabled);
        pc = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            TakeDamage(); 
        }
        CheckCombatInput();
        CheckAttacks();
        if (Input.GetKey(KeyCode.Q) && timeUntilFire < Time.time)
        {
            Shoot();
            timeUntilFire = Time.time + fireRate;
        }
        if (Input.GetKey(KeyCode.W) && timeUntilLightAttack < Time.time)
        {
            SpawnLightAttack();
            timeUntilLightAttack = Time.time + lightAttackCoolDown;
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
    //TODO make it wait for the animation to finish and move this to player controller
    private void TakeDamage()
    {
        if (life >= 1)
        {
            life -= 1;
            Destroy(hearts[life].gameObject);
        }
        if (life < 1)
        {
            combatAnim.SetBool("dead", true);
            Destroy(gameObject);
            LevelManager.instance.Respawn();
            int i; 
            for(i=hearts.Length; i>=0; i--)
            {
                Instantiate(hearts[i].gameObject);
            }
        }
    }
    private void CheckCombatInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.E))
        {
            if (combatEnabled)
            {
                gotInput = true;
                lastInputime = Time.time;
            }
        }
    }
    private void CheckAttacks()
    {
        if (gotInput)
        {
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = false;
                combatAnim.SetBool("lightAttack", true);
                combatAnim.SetBool("isAttacking", isAttacking);
            }
        }
        if(Time.time >= lastInputime + inputTimer)
        {
            gotInput = false;
        }
    }
    private void CheckLightAttackHitbox()
    {
        Collider2D[] dectectedObjects = Physics2D.OverlapCircleAll(lightAttackHitBoxPos.position, lightAttackRadius, whatIsDamageable);
        foreach(Collider2D collider in dectectedObjects){
            collider.transform.parent.SendMessage("Damage", lightAttackDamage);
        }
    }
    private void FinishLightAttack()
    {
        isAttacking = false;
        combatAnim.SetBool("isAttacking", isAttacking);
        combatAnim.SetBool("lightAttack", false);
    }
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
