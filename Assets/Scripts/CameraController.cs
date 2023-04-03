using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera1;
    [SerializeField] private CinemachineVirtualCamera camera2;



    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            TurnOnCamera(camera1,camera2) ;
        }

        if (Input.GetKey(KeyCode.K))
        {
            TurnOnCamera(camera2,camera1);
        }
    }

    private void TurnOnCamera(CinemachineVirtualCamera camToTurnOn,CinemachineVirtualCamera camToTurnOff) 
    {

        Debug.Log(message: "Turn on");
        //Opcion 1: Apagar y prender el GameObjetct
        camToTurnOn.gameObject.SetActive(true);
        camToTurnOff.gameObject.SetActive(false);


       
    
    }

}
