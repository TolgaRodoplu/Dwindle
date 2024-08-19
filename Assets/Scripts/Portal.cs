using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform connectedPortal;
    public Transform player;
    private bool isOverLapping = false;
    public float scaleMult;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(isOverLapping)
        {
            Vector3 offset = player.position - transform.position;    
            float dotProduct = Vector3.Dot(transform.up, offset);

            if(dotProduct < 0) 
            {
                float scaleMult = connectedPortal.parent.localScale.x / player.localScale.x;
                
                Debug.Log(dotProduct);
                float rotDiff = -Quaternion.Angle(transform.rotation, connectedPortal.rotation);
                rotDiff += 180;
                player.Rotate(Vector3.up, rotDiff);
                //player.localScale *= scaleMult;
                Vector3 posOffset = Quaternion.Euler(0f, rotDiff, 0f) * offset;
                posOffset *= scaleMult;
                player.position = connectedPortal.position + posOffset;
                player.localScale *= scaleMult;
                player.GetComponent<PlayerController>().ScaleSpeed(scaleMult);
                isOverLapping = false;
            }
        }
    }

    
   

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isOverLapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isOverLapping = false;
        }
    }
}
