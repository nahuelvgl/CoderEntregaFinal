using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] Transform characterPosition;
    [SerializeField] float persuitDistance = 2;
    [SerializeField] float speed = 0.05f;
    [SerializeField] Vector3 initialRotation;
    [SerializeField] float rotationVelocity;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        Persuit();
    }
    private void Persuit()
    {
        var vectorToCharacter = characterPosition.position - transform.position;
        var distance = vectorToCharacter.magnitude;
        if (distance > persuitDistance)
        {

            transform.position += vectorToCharacter.normalized * (speed * Time.deltaTime);

        }
    }
}
