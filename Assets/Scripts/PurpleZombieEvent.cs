using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Events;
public enum PurpleZombieEventStates
{
    StationaryBeahaviour,
    ChaseBehaviour
}

public class PurpleZombieEvent : ZombieEvent
{
    [SerializeField] float persuitDistance = 15f;
    [SerializeField] float speed = 2f;
    [SerializeField] private PurpleZombieEventStates currentState;
    bool enemyMovement;

    protected override void Update()
    {
        base.Update();
        if (!isDefeated)
        {
            SetCurrentState();
        }
    }

    private void ChaseBehaviour()
    {
        if (!isDefeated)
        {
            var vectorToCharacter = characterPosition.position - transform.position;
            var distance = vectorToCharacter.magnitude;
            if (distance > persuitDistance)
            {
                transform.position += vectorToCharacter.normalized * (speed * Time.deltaTime);
                enemyMovement = true;
            }
            else
            {
                enemyMovement = false;
            }
            EnemyMovement();
            HitDistance();
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
    public void SetCurrentState()
    {
        switch (currentState)
        {
            case PurpleZombieEventStates.StationaryBeahaviour:
                break;
            case PurpleZombieEventStates.ChaseBehaviour:
                ChaseBehaviour();
                break;
            default:
                break;
        }
    }
}

