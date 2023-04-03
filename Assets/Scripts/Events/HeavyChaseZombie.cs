using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyChaseZombie : ZombieEvent
{    
    [SerializeField] float persuitDistance = 25f;
    [SerializeField] float offsetDistance = 0.5f;
    [SerializeField] float speed = 0.5f;
    private bool enemyMovement;
    protected override void Update()
    {
        if (!isDefeated)
        {
            HitDistance();
            LookAtCharacter();
            EnemyAttackRange();
            ChaseEnemyBehaviour();
            EnemyMovement();
        }
        else if (isDefeated && !hasInvoked)
        {
            EventHandler();
        }
    }
    protected override void ZombieAttack(RobotCharacterController character)
    {
        if (attackDistance)
        {
            character.health -= BaseZombieEnemyStats.heavyDamage;
            character.UpdateHealthBar();
            Instantiate(hitEffect, hitEffectSpawn.position, hitEffectSpawn.rotation);
        }
    }
    protected virtual void ResetTimer()
    {
        BaseZombieEnemyStats.heavyActionTime = BaseZombieEnemyStats.heavyTimer;
    }
    protected override void AttackTimer()
    {
        if (!isDefeated)
        {
            GameObject character = GameObject.Find("RobotCharacter");
            RobotCharacterController robotCharacterControllerScript = character.GetComponent<RobotCharacterController>();
            BaseZombieEnemyStats.heavyActionTime -= Time.deltaTime;
            if (BaseZombieEnemyStats.heavyActionTime <= 0 && robotCharacterControllerScript.health>0)
            {   
                ZombieAttack(robotCharacterControllerScript);
                zombyEnemyAnimationController.SetTrigger("Attack");
                ResetTimer();
            }
        }
    }
    protected override void HitDistance()
    {
        GameObject character = GameObject.Find("RobotCharacter");
        RobotCharacterController robotCharacterControllerScript = character.GetComponent<RobotCharacterController>();
        if (attackDistance && Input.GetKey(KeyCode.L) && robotCharacterControllerScript.stamina >= robotCharacterControllerScript.attackStaminaCosts[2])
        {
            isDefeated = true;
            zombyEnemyAnimationController.SetTrigger("Death");
        }
    }
    private void ChaseEnemyBehaviour()
    {
        var vectorToCharacter = characterPosition.position - transform.position;
        var distance = vectorToCharacter.magnitude;
        if (distance < persuitDistance && distance >= offsetDistance)
        {
            transform.position += vectorToCharacter.normalized * (speed * Time.deltaTime);
            enemyMovement = true;
        }
        else
        {
            enemyMovement = false;
        }

    }
    private void EnemyMovement()
    {
        if (enemyMovement == true && !isDefeated)
        {
            zombyEnemyAnimationController.SetFloat("Speed", 0.15f);
        }
        else if (enemyMovement == false || isDefeated)
        {
            zombyEnemyAnimationController.SetFloat("Speed", 0f);
        }
    }

}

