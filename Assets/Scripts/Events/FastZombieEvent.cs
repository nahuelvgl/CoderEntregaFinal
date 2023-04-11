using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastZombieEvent : ZombieEvent
{
    protected override void Awake()
    {
        damage = BaseZombieEnemyStats.fastDamage;
        timer = BaseZombieEnemyStats.fastTimer;
        actionTime = BaseZombieEnemyStats.fastActionTime;
    }
    protected override void HitDistance()
    {
        if (attackDistance && Input.GetKey(KeyCode.J) && robotCharacterControllerScript.stamina > robotCharacterControllerScript.attackStaminaCosts[0] && robotCharacterControllerScript.health > 0)
        {
            isDefeated = true;
            zombyEnemyAnimationController.SetTrigger("Death");
        }
    }
}
