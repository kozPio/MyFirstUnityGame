using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public float walkspeed = 2.5f;
    public float jumpheight;

    [SerializeField] private Transform groundCheckTransform = null;
    public float groundCheckRadius = 0.2f;

    public Transform targetTransform;
    public LayerMask mouseAimMask;

    public Transform muzzleTransform;
    public LayerMask groundMask;
    public GameObject bulletPreFab;

    public AnimationCurve recoilCurv;
    public float recoilDuration = 0.25f;
    public float recoilMaxRotation = 45f;
    public Transform rightLowerArm;
    public Transform rightHand;

    private float inputMovment;
    private Animator animator;
    private Rigidbody rigidBodyComponent;
    private bool isGrounded;
    private Camera mainCamera;
    private float recoilTimer;

    private int FacingSign
    {
        get
        {
            Vector3 perp = Vector3.Cross(transform.forward, Vector3.forward);
            float dir = Vector3.Dot(perp, transform.up);
            return dir > 0f ? -1 : dir < 0f ? 1 : 0;
        }
    }



    //[SerializeField] private GameObject other;
    //private bool jumpKeyWasPressed = false;
    
    

    //private int superJumpsRemaning = 0;
    

    
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBodyComponent = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
       

    

        inputMovment = Input.GetAxis("Horizontal");
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
        {
            targetTransform.position = hit.point;
            
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidBodyComponent.velocity = new Vector3(rigidBodyComponent.velocity.x, 0, 0);
            rigidBodyComponent.AddForce(Vector3.up * Mathf.Sqrt(jumpheight * -1 * Physics.gravity.y), ForceMode.VelocityChange);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            //add switching gun
        }

        
    }

    private void Fire()
    {
        recoilTimer = Time.time;

        var go = Instantiate(bulletPreFab);
        go.transform.position = muzzleTransform.position;
        var bullet = go.GetComponent<Bullet>();
        bullet.Fire(go.transform.position, muzzleTransform.eulerAngles, gameObject.layer);

        
    }

    private void LateUpdate()
    {
        if( recoilTimer< 0)
        {
            return;
        }

        float curveTime = (Time.time - recoilTimer) / recoilDuration;

        if(curveTime > 1f)
        {
            recoilTimer = -1f;

        }
        else
        {
            rightLowerArm.Rotate(Vector3.forward, recoilCurv.Evaluate(curveTime) * recoilMaxRotation, Space.Self);
        }
    }

    // FixedUpdate is caled while every phisics updates
    void FixedUpdate()
    {
        rigidBodyComponent.velocity = new Vector3(inputMovment* walkspeed, rigidBodyComponent.velocity.y, 0);
        animator.SetFloat("speed", FacingSign * rigidBodyComponent.velocity.x / walkspeed);
        rigidBodyComponent.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(targetTransform.position.x - transform.position.x), 0)));


        isGrounded = Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundMask, QueryTriggerInteraction.Ignore);
        animator.SetBool("isGrounded", isGrounded);
    }


    private void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, targetTransform.position);

        animator.SetLookAtWeight(1);
        animator.SetLookAtPosition(targetTransform.position);
    }

}
