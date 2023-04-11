using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackText : MonoBehaviour
{
    private float destructionTimer = 3f;
    private void Update()
    {
        destructionTimer -= Time.deltaTime;
        SelfDestruct();
    }
    private void SelfDestruct()
    {
        if (destructionTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}