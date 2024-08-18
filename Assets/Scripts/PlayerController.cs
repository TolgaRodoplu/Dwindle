using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Interactable interactedObject = null;
    public Transform playerCamera = null;
    private CharacterController controller = null;
    public Transform grabAnchor = null;

    [Header("Movement Parameters")]
    private KeyCode runKey = KeyCode.LeftShift;
    private KeyCode jumpKey = KeyCode.Space;
    private bool canMove = true;
    private bool isRunning = false;
    private float currentSpeed;
    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float runSpeed = 5.0f;
    [SerializeField][Range(0.0f, 0.5f)] private float moveSmoothTime = 0.3f;
    private Vector2 currentDir = Vector2.zero;
    private Vector2 currentDirVelocity = Vector2.zero;
    [SerializeField] private float gravity = -100f;
    private float velocityY = 0.0f;

    [Header("Looking Parameters")]
    private bool canLook = true;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField][Range(0.0f, 0.5f)] private float mouseSmoothTime = 0.03f;
    [SerializeField][Range(0f, 5f)] private float interractDistance = 1000f;
    private float xRotation = 0.0f;
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;

    [Header("Interaction Parameters")]
    private KeyCode interactionKey = KeyCode.E;




    private void Start()
    {
        this.transform.parent = null;
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        EventSystem.instance.playStarted += Activate;
        EventSystem.instance.rotateObj += SetCanLook;
    }

    void Update()
    {
        
        InterractWithObject();
        MouseLook();
        Move();

        //if (Input.GetKeyUp(KeyCode.Mouse0) && active)
        //    UI.instance.ToggleCanvas(true);

        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    UI.instance.ReturnToMenu();
        //}
    }

    private void InterractWithObject()
    {
        if (interactedObject != null)
        {
            if(Input.GetKeyDown(interactionKey))
            {
                interactedObject = interactedObject.Uninteract();
            }
           
        }

        
        else
        {
            var ray = new Ray(playerCamera.position, playerCamera.forward);
            RaycastHit hit;

            int layerMask = ~LayerMask.GetMask("Player");
            

            string crossName = "default";
            //string subtitleText = null;
            //bool subtitleStatus = false;

            if (Physics.Raycast(ray, out hit, interractDistance, layerMask))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red);
                //Debug.Log(hit.transform.name);
                Interactable interactable = hit.transform.GetComponent<Interactable>();

                //Subtitle explenation = hit.transform.transform.GetComponent<Subtitle>();


                //if (explenation != null)
                //{
                //    subtitleStatus = true;
                //    subtitleText = explenation.text;
                //}


                if (interactable != null)
                {
                    EventSystem.instance.GrabUI(true);

                    if (Input.GetKeyDown(interactionKey))
                    {
                        interactedObject = interactable.Interact(grabAnchor);
                        //UI.instance.ToggleCanvas(false);
                    }
                }
                else
                {
                    EventSystem.instance.GrabUI(false);
                }
                    
            }
            else
            {
                EventSystem.instance.GrabUI(false);
            }
                

        }
        
        //UI.instance.SubtitleToggle(subtitleStatus, subtitleText);
        //UI.instance.changeCrosshair(crossName);
    }
    private void MouseLook()
    {
        if (!canLook) return;


        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        xRotation -= currentMouseDelta.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -50f, 60f);

        playerCamera.localEulerAngles = Vector3.right * xRotation;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void SetCanLook(object sender, bool canLook)
    {
        this.canLook = canLook;
    }

    private void Move()
    {
        if (!canMove) return;

        if (Input.GetKeyDown(runKey))
        {
            isRunning = true;
            currentSpeed = runSpeed;
        }
        if (Input.GetKeyUp(runKey))
        {
            isRunning = false;
            currentSpeed = walkSpeed;
        }

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
        {
            velocityY = 0.0f;
        }


        velocityY += gravity * Time.deltaTime;

        Vector3 dir = (transform.forward * currentDir.y + transform.right * currentDir.x) * currentSpeed + Vector3.up * velocityY;

        controller.Move(dir * Time.deltaTime);
    }

    

    public void Activate()
    {

        //UI.instance.ToggleCanvas(true);
        Cursor.lockState = CursorLockMode.Locked;

    }
    public void Deactivate()
    {
        //UI.instance.ToggleCanvas(false);
        Cursor.lockState = CursorLockMode.Confined;
    }
}