using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : Interactable
{
    private Rigidbody rb;
    private InteractionType type = InteractionType.Grab;
    private Transform grabAnchor = null;
    private KeyCode rotateKey = KeyCode.R;
    private float maxDistance = 1f;
    private bool grabbed = false;
    private Vector3 rot;
    private float rotationSens = 10f;
    private bool isRotating = false;
    private Vector3 originalPos;
    private Quaternion originalRot;
    void Awake()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        //if (GetComponent<Key>() == null)
        //    rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        cross = "grab";

    }

    public override Interactable Interact(Transform grabAnchor)
    {
        
        this.grabAnchor = grabAnchor;

        if (grabAnchor == null)
            return null;

        EventSystem.instance.SetCrossActive(false);
        EventSystem.instance.GrabUI(false);
        EventSystem.instance.ReleaseUI(true);
        EventSystem.instance.RotateUI(true);

        if (rb.isKinematic)
            rb.isKinematic = false;

        rot = new Vector3(transform.rotation.eulerAngles.x, 
                          grabAnchor.rotation.eulerAngles.y - transform.rotation.eulerAngles.y, 
                          transform.rotation.eulerAngles.z);

        grabbed = true;
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        return this;
    }

    public override Interactable Uninteract()
    {
        EventSystem.instance.SetCrossActive(true);
        EventSystem.instance.GrabUI(false);
        EventSystem.instance.ReleaseUI(false);
        EventSystem.instance.RotateUI(false);
        EventSystem.instance.RotateObject(true);

        isRotating = false;
        grabbed = false;
        rb.useGravity = true;
        grabAnchor = null;
        rb.velocity = Vector3.zero; 
        rb.angularVelocity = Vector3.zero;
        //if (GetComponent<Key>() != null)
        //    rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        return null;
    }

    private void Update()
    {
        RotateObject();
        CheckDistance();
    }

    private void CheckDistance()
    {
        if(grabbed && Vector3.Distance(transform.position, grabAnchor.position) > maxDistance)
        {
            Uninteract();
        }
    }

    private void FixedUpdate()
    {
        MoveToAnchor();
    }

    void RotateObject() 
    {
        if (grabbed)
        {
            if (Input.GetKeyDown(rotateKey))
            {
                isRotating = true;
                EventSystem.instance.RotateObject(false);
                EventSystem.instance.RotateUI(false);
            }
            if (Input.GetKey(rotateKey))
            {
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");

                Vector3 localX = transform.InverseTransformDirection(grabAnchor.right);
                Vector3 localY = -transform.InverseTransformDirection(grabAnchor.up);

                transform.Rotate(localY, x * rotationSens);
                transform.Rotate(localX, y * rotationSens);

                

            }
            if (Input.GetKeyUp(rotateKey))
            {
                rot = new Vector3(transform.rotation.eulerAngles.x,
                          grabAnchor.rotation.eulerAngles.y - transform.rotation.eulerAngles.y,
                          transform.rotation.eulerAngles.z);

                isRotating = false;
                EventSystem.instance.RotateObject(true);
                EventSystem.instance.RotateUI(true);
            }

        }
    }

    private void MoveToAnchor()
    {
        if (grabbed)
        {

            Debug.Log(Vector3.Distance(transform.position, grabAnchor.position));
            Vector3 DirectionToPoint = grabAnchor.position - transform.position;

            float DistanceToPoint = DirectionToPoint.magnitude;

            rb.velocity = DirectionToPoint * 50f * DistanceToPoint;

            if(!isRotating)
                transform.rotation = Quaternion.Euler(rot.x , grabAnchor.rotation.eulerAngles.y - rot.y, rot.z);

            rb.angularVelocity = Vector3.zero;
        }
    }

    





}