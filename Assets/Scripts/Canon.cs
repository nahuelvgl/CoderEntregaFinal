using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{

    public GameObject cannonBall;
    public Transform cannonTip;
    [SerializeField] private KeyCode shootKeyCode;
    [SerializeField] private KeyCode shootKeyCode1;
    [SerializeField] private KeyCode shootKeyCode2;






    private void Start()
    {
        shootKeyCode = KeyCode.J;
        shootKeyCode1 = KeyCode.K;
        shootKeyCode2 = KeyCode.L;




    }

    private void Update()
    {
        if (Input.GetKeyDown(shootKeyCode) || Input.GetKeyDown(shootKeyCode1) || Input.GetKeyDown(shootKeyCode2))
        {
            Shoot();

        }
    }

    private void Shoot()
    {
        Instantiate(cannonBall, cannonTip);

        Debug.Log("Shoot");
    }



}
