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
        Debug.Log(currentState.ToString()); 
        combatAnim = GetComponent<Animator>();
        pc = gameObject.GetComponent<PlayerController>();
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Z) && timeUntilFire < Time.time)
        {
            isAttacking = true; 
            Shoot();
            //its not reading it as a string. need to create a string and update it for each function. 
            combatAnim.Play(stateStr + "Attack 2");
            timeUntilFire = Time.time + fireRate;
        }
        if (Input.GetKeyDown(KeyCode.X) && timeUntilLightAttack < Time.time)
        {
            isAttacking = true; 
            SpawnLightAttack();
            combatAnim.Play(stateStr + "Attack 1");
            timeUntilLightAttack = Time.time + lightAttackCoolDown;
            //make a combat active bool so the idle animation does not try to fight it 
            //make a way for the attacks to link together
             
        }
        if (Input.GetKeyDown(KeyCode.C) && timeUntlHeavyAttack < Time.time)
        {
            SpawnHeavyAttack();
            timeUntlHeavyAttack = Time.time + heavyAttackCoolDown;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log(stateStr + "before switch state"); 
            SwitchWeapons(currentState);
            stateStr = currentState.ToString();
            Debug.Log(stateStr + " after switch state");
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
