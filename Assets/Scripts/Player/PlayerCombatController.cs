using System;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public enum WeapState
    {
        FISTS,
        SWORD
    }

    PlayerController pc;
    [SerializeField] private float lightAttackCoolDown, heavyAttackCoolDown;
    private float timeUntilLightAttack,timeUntlHeavyAttack;
    public Transform firingPoint;
    public GameObject lightAttackPrefab, HeavyAttackPrefab,projectilePrefab;
    [SerializeField] private float inputTimer, lightAttackRadius, lightAttackDamage, fireRate;
    [SerializeField] Animator combatAnim;
    [SerializeField] private Transform lightAttackHitBoxPos;
    [SerializeField] LayerMask whatIsDamageable;
    private float timeUntilFire;
    public bool isAttacking;
    public WeapState currentState;
    public String stateStr; 

    void Start()
    {
        currentState = WeapState.FISTS;
        stateStr = currentState.ToString(); 
        combatAnim = GetComponent<Animator>();
        pc = gameObject.GetComponent<PlayerController>();
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Z) && timeUntilFire < Time.time)
        {
            isAttacking = true; 
            Shoot();
            combatAnim.Play(stateStr + " Attack 2" );
            timeUntilFire = Time.time + fireRate;
        }
        if (Input.GetKeyDown(KeyCode.X) && timeUntilLightAttack < Time.time)
        {
            isAttacking = true; 
            SpawnLightAttack();
            combatAnim.Play(stateStr + " Attack 1");
            timeUntilLightAttack = Time.time + lightAttackCoolDown;
        }
        if (Input.GetKeyDown(KeyCode.C) && timeUntlHeavyAttack < Time.time)
        {
            isAttacking = true;
            SpawnHeavyAttack();
            combatAnim.Play(stateStr + " Attack 3");
            timeUntlHeavyAttack = Time.time + heavyAttackCoolDown;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SwitchWeapons(currentState);
            stateStr = currentState.ToString();
        }
    }
    private void Shoot()
    {
        float angle = pc.isFacingRight ? 0f : 180f;
        Instantiate(projectilePrefab, firingPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }

    private void SpawnLightAttack()
    {
        float angle = pc.isFacingRight ? 0f : 180f;
        Instantiate(lightAttackPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
    private void SpawnHeavyAttack()
    {
        float angle = pc.isFacingRight ? 0f : 180f;
        Instantiate(HeavyAttackPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
    public void ResetisAttacking() => isAttacking = false;
    private WeapState SwitchWeapons(WeapState state) => state switch
    {
        WeapState.FISTS => currentState = WeapState.SWORD,
        WeapState.SWORD => currentState = WeapState.FISTS,
        _               => throw new ArgumentException(message: "invalid enum value"),
    };
}
