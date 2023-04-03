using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpWall : MonoBehaviour
{
    [SerializeField] private Transform[] newPositions;
    private float transformPositionTimer = 2f;
    private float timer = 2f;
    private void OnTriggerStay(Collider other)
    {
       
    HarryController character = other.GetComponent<HarryController>();
        if (character != null)
        {
            TransformPosition();           
        }
    }
    public Transform GetPosition()
    {
        return newPositions[Random.Range(0, newPositions.Length)];
    }
    private void TransformPosition()
    {
        var newPosition = GetPosition().position - transform.position;
        var newRotation = Quaternion.LookRotation(newPosition);
        transformPositionTimer -= Time.deltaTime;
        if (transformPositionTimer <= 0)
        {
            transform.position = newPosition;
            transform.rotation = newRotation;
            ResetTimer();
        }
    }
    public void ResetTimer()
    {
        transformPositionTimer = timer;
    }
}
