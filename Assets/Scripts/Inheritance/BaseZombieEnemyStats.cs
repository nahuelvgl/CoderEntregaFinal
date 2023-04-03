using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BaseZombieEnemyStats", order = 1)]
public class BaseZombieEnemyStats : ScriptableObject
    {
    [SerializeField] public float fastActionTime = 0.5f;
    [SerializeField] public float fastTimer = 1f;
    [SerializeField] public float fastDamage = 25;
    [SerializeField] public float regularActionTime = 1f;
    [SerializeField] public float regularTimer = 1.5f;
    [SerializeField] public float regularDamage = 50;
    [SerializeField] public float heavyActionTime = 1.5f;
    [SerializeField] public float heavyTimer = 2f;
    [SerializeField] public float heavyDamage = 75;
}
