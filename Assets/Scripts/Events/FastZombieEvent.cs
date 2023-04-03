using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastZombieEvent : ZombieEvent
{
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
        if (attackDistance && Input.GetKey(KeyCode.J) && robotCharacterControllerScript.stamina > robotCharacterControllerScript.attackStaminaCosts[0])
        {
            isDefeated = true;
            zombyEnemyAnimationController.SetTrigger("Death");
        }
    }
}
