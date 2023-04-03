using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

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
    [SerializeField] private float e_raycastDistance = 3f;
    [SerializeField] private LayerMask m_layerToCollideWith;
    public void Start()
    {
        GameObject character = GameObject.Find("RobotCharacter");
        RobotCharacterController robotCharacterControllerScript = character.GetComponent<RobotCharacterController>();

    }
    protected virtual void Update()
    {
        if (!isDefeated)
        {
            HitDistance();
            LookAtCharacter();
            EnemyAttackRange();
        }
        else if (isDefeated && !hasInvoked)
        {
            EventHandler();
        }
    }
    protected void OnTriggerStay(Collider other)
    {
        RobotCharacterController character = other.GetComponent<RobotCharacterController>();
        if (character != null)
        {
            attackDistance = true;
            AttackTimer();
            OnAprroach?.Invoke();
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        RobotCharacterController character = other.GetComponent<RobotCharacterController>();
        if (character != null)
        {
            attackDistance = false;
        }
    }
    protected virtual void HitDistance()
    {
        GameObject character = GameObject.Find("RobotCharacter");
        RobotCharacterController robotCharacterControllerScript = character.GetComponent<RobotCharacterController>();
        if (attackDistance && Input.GetKey(KeyCode.K) && robotCharacterControllerScript.stamina >= robotCharacterControllerScript.attackStaminaCosts[1])
        {
            isDefeated = true;
            zombyEnemyAnimationController.SetTrigger("Death");
        }
    }

    protected virtual void AttackTimer()
    {
        if (!isDefeated)
        {
            BaseZombieEnemyStats.regularActionTime -= Time.deltaTime;
            GameObject character = GameObject.Find("RobotCharacter");
            RobotCharacterController robotCharacterControllerScript = character.GetComponent<RobotCharacterController>();
            if (BaseZombieEnemyStats.regularActionTime <= 0 && robotCharacterControllerScript.health>0)
            {
                zombyEnemyAnimationController.SetTrigger("Attack");
                ResetTimer();
                ZombieAttack(robotCharacterControllerScript);
            }
        }
    }
    private void ResetTimer()
    {
        BaseZombieEnemyStats.regularActionTime = BaseZombieEnemyStats.regularTimer;
    }
    protected virtual void ZombieAttack(RobotCharacterController character)
    {
        if (attackDistance) 
        { 
        character.health -= BaseZombieEnemyStats.regularDamage;
        character.UpdateHealthBar();
        Instantiate(hitEffect, hitEffectSpawn.position, hitEffectSpawn.rotation);
        }
    }
    protected void LookAtCharacter()
    {
        var vectorToCharacter = characterPosition.position - transform.position;
        var newRotation = Quaternion.LookRotation(vectorToCharacter);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationVelocity);
    }
    protected void EnemyAttackRange()
    {
        var l_hasCollided =
            Physics.Raycast(transform.position, transform.forward, out RaycastHit p_raycastHitInfo, e_raycastDistance,
                m_layerToCollideWith);

        if (l_hasCollided)
        {
            var character = p_raycastHitInfo.collider.GetComponent<RobotCharacterController>();
            if (character != null)
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

}
