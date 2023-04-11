using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastChaseZombie : ZombieEvent
{
    [SerializeField] float persuitDistance = 50f;
    [SerializeField] float offsetDistance = 0.5f;
    [SerializeField] float speed = 2f;
    private bool enemyMovement;
    protected override void Awake()
    {
        damage = BaseZombieEnemyStats.fastDamage;
        timer = BaseZombieEnemyStats.fastTimer;
        actionTime = BaseZombieEnemyStats.fastActionTime;
    }
    protected override void Update()
    {
        base.Update();
        if (!isDefeated)
        {
            ChaseEnemyBehaviour();
            EnemyMovement();
        }
    }
    protected override void HitDistance()
    {
        if (attackDistance && Input.GetKey(KeyCode.J) && robotCharacterControllerScript.stamina > robotCharacterControllerScript.attackStaminaCosts[0] && robotCharacterControllerScript.health > 0)
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


