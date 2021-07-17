using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private  Transform groundCheckTransform = null;
    [SerializeField]  private LayerMask playerMask;
    [SerializeField] private GameObject other;
    private bool jumpKeyWasPressed = false;
    private float horizonatalInput;
    private Rigidbody rigidBodyComponent;
    private Transform positionComponent;
    private int superJumpsRemaning = 0;
    


    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
        positionComponent = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;  
        }


        horizonatalInput = Input.GetAxis("Horizontal");


        if(positionComponent.position.y < -3)
        {
            Debug.Log("Game Over");
            Destroy(other);
            
        }

        
    }

    // FixedUpdate is caled while every phisics updates
    void FixedUpdate()
    {
        rigidBodyComponent.velocity = new Vector3(horizonatalInput*2, rigidBodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyWasPressed)
        {
            float jumpPower = 5f;
            if(superJumpsRemaning > 0)
            {
                jumpPower *= 2;
                superJumpsRemaning--;
            }
            rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }

        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            superJumpsRemaning++;
        }
    }


}
