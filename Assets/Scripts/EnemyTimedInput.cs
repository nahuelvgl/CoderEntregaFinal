using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyTimedInput : MonoBehaviour
{
    private float actionTime = 3f;
    private float timer = 3f;
    private bool attackdistance;
    private void OnTriggerStay(Collider other)
    {
        RobotCharacterController character = other.GetComponent<RobotCharacterController>();
      if (character != null)
        {
            timer -= Time.deltaTime;
            attackdistance = true;
            if (timer <= 0)
            {
                character.RecieveDamage();
                ResetTimer();
            }
        }
        else if (character = null)
        {
            attackdistance = false;
        }
    }
    private void HitDistance()
    {
        if (attackdistance == true && Input.GetKey(KeyCode.J))
        {
            Destroy(gameObject);
        }
    }
    private void AttackTime()

    {
        timer -= Time.deltaTime;
        RobotCharacterController character = GetComponent<RobotCharacterController>();
        if (attackdistance == true && timer <= 0)
        {
            timer -= Time.deltaTime;
            character.RecieveDamage();
            ResetTimer();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HitDistance();
        AttackTime();
    }
    private void ResetTimer()
    { timer = actionTime;
    }
}
