using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyZombieEvent : ZombieEvent
{
    protected override void ZombieAttack(RobotCharacterController character)
    {
        if (attackDistance)
        {
            character.health -= BaseZombieEnemyStats.regularDamage;
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
            if (BaseZombieEnemyStats.heavyActionTime <= 0 && robotCharacterControllerScript.health > 0)
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
}