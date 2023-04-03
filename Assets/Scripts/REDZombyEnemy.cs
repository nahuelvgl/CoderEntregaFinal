using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class REDZombyEnemy : MonoBehaviour
{
    [SerializeField] private float actionTime = 1f;
    private float timer = 1.5f;
    private bool attackdistance;
    [SerializeField] private Animator zombyEnemyAnimationController;
    private bool isDefeated = false;
    private void Start()
    {
        actionTime = 1f;
    }
    void Update()
    {
        if (!isDefeated)
        {
            HitDistance();
        }
    }
    private void OnTriggerStay(Collider other)
    {
       RobotCharacterController character = other.GetComponent<RobotCharacterController>();
       if (character != null)
        {
            attackdistance = true;
            AttackTimer();
        }
        else if (character = null)
        {
            attackdistance = false;
        }
    }
    private void HitDistance()
    {
        if (attackdistance == true && Input.GetKey(KeyCode.K))
        {
            isDefeated = true;
            zombyEnemyAnimationController.SetTrigger ("Death");
        }
    }

    private void AttackTimer()
    {
        if (!isDefeated)
        { 
            actionTime -= Time.deltaTime;
            if (actionTime <= 0)
            {
            GameObject character = GameObject.Find("RobotCharacter");
            RobotCharacterController robotCharacterControllerScript = character.GetComponent<RobotCharacterController>();
            robotCharacterControllerScript.health -= 50;
            robotCharacterControllerScript.UpdateHealthBar();
            zombyEnemyAnimationController.SetTrigger("Attack");
            ResetTimer();
            }
        }
    }
    private void ResetTimer()
    {
        actionTime = timer;
    }
}
