using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.TextCore.Text;

public class ZombieEvent : MonoBehaviour
{
    public BaseZombieEnemyStats BaseZombieEnemyStats;
    public ParticleSystem hitEffect;
    public Transform hitEffectSpawn;
    [SerializeField] protected Transform characterPosition;
    private float rotationVelocity = 3f;
    protected bool attackDistance = false;
    public Animator zombyEnemyAnimationController;
    public bool isDefeated = false;
    protected bool hasInvoked = false;
    public event Action OnAprroach;
    public event Action OnEnemyDeath;
    protected float damage;
    [SerializeField] protected float timer;
    [SerializeField] protected float actionTime;
    [SerializeField] protected float e_raycastDistance;
    [SerializeField] private LayerMask m_layerToCollideWith;
    [SerializeField] protected RobotCharacterController robotCharacterControllerScript;
    [SerializeField] private Volume proximityPostProc;
    private ChromaticAberration newIntensity;

    protected virtual void Awake()
    {
        damage = BaseZombieEnemyStats.regularDamage;
        timer = BaseZombieEnemyStats.regularTimer;
        actionTime = BaseZombieEnemyStats.regularActionTime;
    }
    public void Start()
    {
        GameObject character = GameObject.Find("RobotCharacter");
        robotCharacterControllerScript = character.GetComponent<RobotCharacterController>();
        ResetTimer();
        e_raycastDistance = 3f;

    }
    protected virtual void Update()
    {
        if (!isDefeated)
        {
            HitDistance();
            EnemyAttackRange();
            LookAtCharacter();

        }
        else if (isDefeated && !hasInvoked)
        {
            EventHandler();
            ProximityPostProcHandler();
        }
    }
    protected void OnTriggerStay(Collider other)
    {
        RobotCharacterController character;
        if (other.TryGetComponent(out character))
        {
            attackDistance = true;
            OnAprroach?.Invoke();
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        RobotCharacterController character;
        if (other.TryGetComponent(out character))
        {
            attackDistance = false;
        }
    }
    protected virtual void HitDistance()
    {
        if (attackDistance && Input.GetKey(KeyCode.K) && robotCharacterControllerScript.stamina >= robotCharacterControllerScript.attackStaminaCosts[1] && robotCharacterControllerScript.health > 0)
        {
            isDefeated = true;
            zombyEnemyAnimationController.SetTrigger("Death");  
        }
    }

    protected void AttackTimer()
    {
        if (!isDefeated)
        {
            actionTime -= Time.deltaTime;
            if (actionTime <= 0 && robotCharacterControllerScript.health>0)
            {
                zombyEnemyAnimationController.SetTrigger("Attack");
                ResetTimer();
                ZombieAttack(robotCharacterControllerScript);
            }
        }
    }
    protected  void ResetTimer()
    {
        actionTime = timer;
    }
    protected void ZombieAttack(RobotCharacterController character)
    {
        if (attackDistance) 
        { 
        character.health -= damage;
        character.UpdateHealthBar();
        Instantiate(hitEffect, hitEffectSpawn.position, hitEffectSpawn.rotation);
        }
    }
    protected void LookAtCharacter()
    {
        var vectorToCharacter = characterPosition.position - transform.position;
        vectorToCharacter.y = 0f;
        var newRotation = Quaternion.FromToRotation(Vector3.forward, vectorToCharacter);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationVelocity);
    }
    protected void EnemyAttackRange()
    {
        var l_hasCollided =
            Physics.Raycast(transform.position, transform.forward, out RaycastHit p_raycastHitInfo, e_raycastDistance,
                m_layerToCollideWith);
        Debug.DrawRay(transform.position, transform.forward * e_raycastDistance, Color.green);

        if (l_hasCollided)
        {
            if (p_raycastHitInfo.collider.TryGetComponent(out RobotCharacterController character))
            {
                AttackTimer();
            }
        }
    }

    protected void EventHandler()
    {
        OnEnemyDeath?.Invoke();
        hasInvoked = true;       
    }
    protected void ProximityPostProcHandler()
    {
        var l_profile = proximityPostProc.profile;
        l_profile.TryGet<ChromaticAberration>(out newIntensity);
        newIntensity.intensity.Override(0f);  
    }


}
