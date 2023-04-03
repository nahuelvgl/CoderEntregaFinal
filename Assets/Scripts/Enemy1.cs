using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField] Transform characterPosition;
    [SerializeField] Vector3 initialRotation;
    [SerializeField] float rotationVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAtCharacter();
    }
    private void LookAtCharacter()
    {
        var vectorToCharacter = characterPosition.position - transform.position;
        var newRotation = Quaternion.LookRotation(vectorToCharacter);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationVelocity);
    }
}
