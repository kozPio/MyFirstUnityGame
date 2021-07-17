using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rigidBodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBodyComponent.velocity = new Vector3(Time.deltaTime * 2, rigidBodyComponent.velocity.y, 0); ;
    }

    private void FixedUpdate()
    {
        
    }
}
