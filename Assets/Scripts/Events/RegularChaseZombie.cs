using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularChaseZombie : ZombieEvent
{
    [SerializeField] float persuitDistance = 100f;
    [SerializeField] float offsetDistance = 0.5f;
    [SerializeField] float speed = 1.5f;
    private bool enemyMovement;
    protected override void Update()
    {
        base.Update();
        if (!isDefeated)
        {
            ChaseEnemyBehaviour();
            EnemyMovement();
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
