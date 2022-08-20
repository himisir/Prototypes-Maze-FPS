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

    //sprint Cool down stuff
    public float sprintDurationLimiter = 5f;
    public float sprintCoolDown = 10f;
    [SerializeField] private float sprintDuration = 0f;
    [SerializeField] private float sprintCoolDownCounter = 0f;
    [SerializeField] private bool isCanSprint;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        povRoot = transform.GetChild(0);
        playerFootSteps.stepDistance = walkStepDistance;
        playerFootSteps.volumeMin = walkVolumeMin;
        playerFootSteps.volumeMax = walkVolumeMax;
        isCanSprint = true;

    }

    void Update()
    {
        //SprintCoolDown();
        Crouch();
        Sprint();


    }




    void Sprint()
    {

        if (isCrouching)
        {
            sprintDuration = 0;
            if (sprintCoolDownCounter > 0)
            {
                sprintCoolDownCounter -= Time.deltaTime;
            }
            else sprintCoolDownCounter = 0;
            return;
        }
        if (isCanSprint)
        {
            if (Input.GetKey(KeyCode.E) && (this.GetComponent<CharacterController>().velocity.sqrMagnitude > 0)) //Having issue with Shift key, will fix it later. 
            {

                sprintDuration += Time.deltaTime;
                if (sprintDuration >= sprintDurationLimiter)
                {
                    sprintDuration = sprintDurationLimiter;
                    isCanSprint = false;
                    sprintCoolDownCounter = sprintCoolDown;
                    return;
                }
                isSprinting = true; playerMovement.speed = sprintSpeed;
                SetSound(sprintStepDistance, sprintVolumeMin, sprintVolumeMax);


            }
            else
            {
                isSprinting = false;
                if (sprintCoolDownCounter > 0)
                {
                    sprintCoolDownCounter -= Time.deltaTime;
                }
                else
                {
                    sprintCoolDownCounter = 0;
                    isCanSprint = true;
                    sprintDuration = 0f;
                }

                //

                playerMovement.speed = walkSpeed;
                SetSound(walkStepDistance, walkVolumeMin, walkVolumeMax);
            }
        }
        else
        {
            isSprinting = false;
            if (sprintCoolDownCounter > 0)
            {
                sprintCoolDownCounter -= Time.deltaTime;
            }
            else
            {
                sprintCoolDownCounter = 0;
                isCanSprint = true;
                sprintDuration = 0f;
            }
            //
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


    void SprintCoolDown() //Asses sprint cool down feature; 
    {
        if (isSprinting)
        {
            if (sprintDuration < sprintDurationLimiter)
            {
                sprintDuration += Time.deltaTime;
            }
            else
            {
                sprintCoolDownCounter = sprintCoolDown;
                isSprinting = false;
            }
        }
        else
        {
            sprintDuration = 0f;
            if (sprintCoolDownCounter > 0)
                sprintCoolDownCounter -= Time.deltaTime;
            else
            {
                sprintCoolDownCounter = 0;
            }
        }
        isCanSprint = (sprintCoolDownCounter <= 0) ? true : false;
    }
}
