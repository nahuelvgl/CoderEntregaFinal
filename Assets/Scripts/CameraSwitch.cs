using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;


public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera1;
    [SerializeField] private CinemachineVirtualCamera camera2;
    bool Switcher;

    void Update()
    {
        GameObject character = GameObject.Find("RobotCharacter");
        RobotCharacterController robotCharacterControllerScript = character.GetComponent<RobotCharacterController>();
        if (robotCharacterControllerScript.health<=0)
        {
            Toggle();
        }     
    }
    private void SwitchCamera(CinemachineVirtualCamera camOn, CinemachineVirtualCamera camOff)
    {
        camOn.gameObject.SetActive(true);
        camOff.gameObject.SetActive(false);
    }  
        public void Toggle()
        {
            if (Switcher)
            {
            SwitchCamera(camOn: camera2, camera1); 
            }
            else
        {
            SwitchCamera(camOn: camera1, camera2);
        }
        }
    }
   
