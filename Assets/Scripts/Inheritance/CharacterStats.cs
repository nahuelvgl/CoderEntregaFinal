using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterStats", order = 1)]
public class CharacterStats : ScriptableObject
{
    [SerializeField] public float maxHealth = 100;
    [SerializeField] public float speed = 2f;
 
    }


