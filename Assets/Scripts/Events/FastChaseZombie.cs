using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastChaseZombie : ZombieEvent
{
    [SerializeField] float persuitDistance = 50f;
    [SerializeField] float offsetDistance = 0.5f;
    [SerializeField] float speed = 2f;
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
            character.health -= BaseZombieEnemyStats.fastDamage;
            character.UpdateHealthBar();
            Instantiate(hitEffect, hitEffectSpawn.position, hitEffectSpawn.rotation);

        }
    }
    protected virtual void ResetTimer()
    {
        BaseZombieEnemyStats.fastActionTime = BaseZombieEnemyStats.fastTimer;
    }
    protected override void AttackTimer()
    {
        if (!isDefeated)
        {
            GameObject character = GameObject.Find("RobotCharacter");
            RobotCharacterController robotCharacterControllerScript = character.GetComponent<RobotCharacterController>();
            BaseZombieEnemyStats.fastActionTime -= Time.deltaTime;
            if (BaseZombieEnemyStats.fastActionTime <= 0 && robotCharacterControllerScript.health > 0)
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
        if (attackDistance && Input.GetKey(KeyCode.J) && robotCharacterControllerScript.stamina >= robotCharacterControllerScript.attackStaminaCosts[0])
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
            enemyMovement= true;
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


