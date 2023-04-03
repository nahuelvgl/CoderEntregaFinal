using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public GameObject bullet;
    public Transform pointOfShoot;
    public int cargador ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cargador > 0)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3"))
            {
                Disparo();
                cargador--;
            }

        }
        else
        {
            Debug.Log(message: "NO HAY MAS BALAS");
        }

       
       
    }
  

    public void Disparo() 
    {
        Instantiate(bullet, pointOfShoot.position, pointOfShoot.rotation);

    }

}
