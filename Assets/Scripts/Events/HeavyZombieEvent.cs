using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyZombieEvent : ZombieEvent
{
    protected override void Awake()
    {
        damage = BaseZombieEnemyStats.heavyDamage;
        timer = BaseZombieEnemyStats.heavyTimer;
        actionTime = BaseZombieEnemyStats.heavyActionTime;
    }

    protected override void HitDistance()
    {
        if (attackDistance && Input.GetKey(KeyCode.L) && robotCharacterControllerScript.stamina >= robotCharacterControllerScript.attackStaminaCosts[2] && robotCharacterControllerScript.health > 0)
        {
            isDefeated = true;
            zombyEnemyAnimationController.SetTrigger("Death");
        }
    }
}