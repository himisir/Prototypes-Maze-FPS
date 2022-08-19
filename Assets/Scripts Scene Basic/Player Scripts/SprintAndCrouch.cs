using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAndCrouch : MonoBehaviour
{

    private PlayerMovement playerMovement;
    private float sprintSpeed = 10f;
    private float walkSpeed = 5f;
    private float crouchSpeed = 2f;

    private Transform povRoot;
    private float standHeight = 1.6f;
    private float crouchHeight = 1f;

    [SerializeField] private bool isCrouching, isSprinting;
    //Sound Properties

    [SerializeField] private PlayerFootSteps playerFootSteps; //Use event instead of hard referencing next time
    private float sprintVolumeMin = 9f, sprintVolumeMax = 11f;
    private float crouchVolume = 0.1f;
    private float walkVolumeMin = 0.2f, walkVolumeMax = .6f;
    //Step Distance Properties
    private float walkStepDistance = 0.4f;
    private float sprintStepDistance = 0.2f;
    private float crouchStepDistance = 0.5f;


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        povRoot = transform.GetChild(0);
        playerFootSteps.stepDistance = walkStepDistance;
        playerFootSteps.volumeMin = walkVolumeMin;
        playerFootSteps.volumeMax = walkVolumeMax;

    }

    void Update()
    {
        //ShiftIssuePatch();
        Crouch();
        //Sprint();

    }

    public bool isShiftDown;
    void ShiftIssuePatch()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Shift Down");
            isShiftDown = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("Shift Up");
            isShiftDown = false;
        }
    }


    void Sprint()
    {
        if (isCrouching) return;
        // if (Input.GetKey(KeyCode.LeftShift))
        if (isShiftDown)
        {

            isSprinting = true;
            playerMovement.speed = sprintSpeed;
            SetSound(sprintStepDistance, sprintVolumeMin, sprintVolumeMax);
        }
        //if (Input.GetKeyUp(KeyCode.LeftShift))
        else
        {
            isSprinting = false;
            playerMovement.speed = walkSpeed;
            SetSound(walkStepDistance, walkVolumeMin, walkVolumeMax);
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
                SetSound(walkStepDistance, walkVolumeMin, walkVolumeMax);
            }
            else
            {
                povRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.speed = crouchSpeed;
                SetSound(crouchStepDistance, crouchVolume, crouchVolume);
                isCrouching = true;
            }
    }


    //Sets volume based on current state;  //use event Action during polishing process; 
    void SetSound(float _stepDistance, float _volumeMin, float _volumeMax)
    {
        playerFootSteps.stepDistance = _stepDistance;
        playerFootSteps.volumeMin = _volumeMin;
        playerFootSteps.volumeMax = _volumeMax;
    }

}
