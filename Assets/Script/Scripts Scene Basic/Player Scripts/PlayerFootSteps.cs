using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] footStepClips;

    [SerializeField] private CharacterController characterController;

    [HideInInspector] public float volumeMin, volumeMax;
    private float accumulatedDistance; //Distance traveled since last footstep sound
    [HideInInspector] public float stepDistance; //Distance of a footstep

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //characterController = GetComponentInParent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        SoundLogic();
    }

    //Will be used a bit later;
    void SoundReference(float _stepDistance, float _volumeMin, float _volumeMax)
    {
        stepDistance = _stepDistance;
        volumeMin = _volumeMin;
        volumeMax = _volumeMax;
    }

    void SoundLogic() //How the sound is triggered
    {
        if (!characterController.isGrounded)
            return; //If we are not on the ground, don't bother checking rest of the code

        if ((characterController.velocity.sqrMagnitude > 0))
        {
            accumulatedDistance += Time.deltaTime;
            if (accumulatedDistance > stepDistance)
            {
                PlaySound();
                accumulatedDistance = 0;

            }
        }
        else accumulatedDistance = 0;



    }
    void PlaySound() //Configure sound features and plays them
    {

        audioSource.volume = Random.Range(volumeMin, volumeMax);
        audioSource.clip = footStepClips[Random.Range(0, footStepClips.Length)];
        audioSource.Play();
    }
}
