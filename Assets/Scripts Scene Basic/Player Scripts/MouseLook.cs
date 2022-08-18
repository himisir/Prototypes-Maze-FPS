using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerRoot, povRoot; //the player root is the parent of the player and the pov root is the parent of the pov

    [SerializeField] private float mouseSensitivity = 5.0f;
    [SerializeField] private int smoothSteps = 10;
    [SerializeField] private float smoothWeight = 0.4f;
    [SerializeField] private float rollAngle = 10f;

    [SerializeField] private Vector2 defaultLookLimits = new Vector2(-70f, 80f);
    private Vector2 lookAngles;
    private Vector2 currentMouseLook;
    private Vector2 smoothMove;

    private float currentRollAngle;
    private int lastLookFrame;

    public bool invert;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Update()
    {
        LockCursor();
        LookAround();
    }

    void LockCursor() //Locks cursor before taking input from mouse;
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else Cursor.lockState = CursorLockMode.None;

        }
    }
    void LookAround()
    {
        currentMouseLook = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X)); //Make sure you are putting y axis before x axis
        lookAngles.x += currentMouseLook.x * mouseSensitivity * (invert ? 1f : -1f); //invert is a bool that is true if you want to invert the mouse look
        //invert for those who has their mouse set to inverted

        lookAngles.y += currentMouseLook.y * mouseSensitivity;

        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimits.x, defaultLookLimits.y); //clamp the look angle to the default look limits   

        currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw(MouseAxis.MOUSE_X), Time.deltaTime * rollAngle); //Rotation on the z axis for cool game effect, will implement later; 

        povRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, currentRollAngle); //rotate the pov root to the look angle
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0); //rotate the player root to the look angle
    }



}



