using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private bool isGunButtonPressed = false;
    private float fireRate = 0.3f;
    private float nextFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.J))
        {
            isGunButtonPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            isGunButtonPressed = false;
        }

        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
            return;
            //can not fire
        }
       


        
    }


    private void FixedUpdate()
    {
        if (isGunButtonPressed && nextFire <= 0)
        {
            Shoot();
        }
    }

    
    private void Shoot()
    {
        Debug.Log("Boom");
        nextFire = fireRate;
        
    }
}
