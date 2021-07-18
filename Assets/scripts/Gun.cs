using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private LayerMask notToHit;
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private float damage = 10;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private Transform _selection;
    private float fireRate = 0;
    private float timeToFire= 0;
    
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

       

        if(_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                var selectionRender = selection.GetComponent<Renderer>();
                if (selectionRender != null)
                {
                    selectionRender.material = highlightMaterial;

                }
                _selection = selection;
            }
            
        }

        if(fireRate == 0)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }
        } 
        else
        {
            if (Input.GetKey(KeyCode.Mouse0) && Time.time> timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
        
    }



    private void Shoot()
    {


        //var mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        //RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, notToHit);
     }

   

}





