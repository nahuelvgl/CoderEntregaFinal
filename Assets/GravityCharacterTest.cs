using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCharacterTest : MonoBehaviour
{
    public Anillo attractorPlanet;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        playerTransform = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attractorPlanet)
        {
            attractorPlanet.Attract(playerTransform);
        }


    }
    
}
