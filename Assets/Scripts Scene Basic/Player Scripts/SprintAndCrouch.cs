using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAndCrouch : MonoBehaviour
{

    private PlayerMovement playerMovement;
    public float sprintSpeed = 10f;
    public float walkSpeed = 5f;
    public float crouchSpeed = 2f;

    private Transform povRoot;
    private float standHeight = 1.6f;
    private float crouchHeight = 1f;

    public bool isCrouching;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        povRoot = transform.GetChild(0);
    }

    void Update()
    {
        Sprint();
        Crouch();
    }


    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = walkSpeed;
        }
    }
    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
            if (isCrouching)
            {
                povRoot.localPosition = new Vector3(0f, standHeight, 0f);
                playerMovement.speed = walkSpeed;
                isCrouching = false;
            }
            else
            {
                povRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.speed = crouchSpeed;
                isCrouching = true;
            }
    }

}
