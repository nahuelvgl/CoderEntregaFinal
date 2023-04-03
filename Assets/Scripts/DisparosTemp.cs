using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparosTemp : MonoBehaviour
{
    public GameObject bullet;
    public Transform cannonTip;
    [SerializeField] private float timer;
    private float shootTimer;

    void Start()
    {
        ResetTimer();
    }
    private void ResetTimer()
    {
        shootTimer = timer;
    }

    void Update()
    {
     ShootTime();  
    }

    private void ShootTime()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            Shoot();
            ResetTimer();
        }
    }    

    private void Shoot()
    {
        Instantiate(bullet, cannonTip);
    }   

}
