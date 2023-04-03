using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum Enemy3States
{
    BehaviourType1,
    BehaviourType2,
}

public class Enemy3 : MonoBehaviour
{
    [SerializeField] Transform characterPosition;
    [SerializeField] float persuitDistance = 2;
    [SerializeField] float speed = 0.05f;
    [SerializeField] Vector3 initialRotation;
    [SerializeField] float rotationVelocity;
    [SerializeField] private Enemy3States currentState;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetCurrentState();
    }
    private void BehaviourType1()
    {
        var vectorToCharacter = characterPosition.position - transform.position;
        var newRotation = Quaternion.LookRotation(vectorToCharacter);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationVelocity);
    }
    private void BehaviourType2()
    {
        var vectorToCharacter = characterPosition.position - transform.position;
        var distance = vectorToCharacter.magnitude;
        if (distance > persuitDistance)
        {

            transform.position += vectorToCharacter.normalized * (speed * Time.deltaTime);

        }
    }
     public void SetCurrentState()
    {
        switch (currentState)
        {
            case Enemy3States.BehaviourType1:
                BehaviourType1();
                break;
            case Enemy3States.BehaviourType2:
                BehaviourType2();
                break;
            default:
                break;
        }
    }
    

}
