using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource footstepSound;

    [SerializeField]
    private AudioClip[] foostepClip;

    private CharacterController characterController;

    private float volumeMin, volumeMax;
    private float walkVolumeMin = 1f, walkVolumeMax = 1.6f;
    private float sprintVolume = 3f;
    private float crouchVolume = 0.1f;

    private float accumulateDistance = 0;
    private float stepDistance;
    private float walkStepDistance = 0.4f, sprintStepDistance = 0.1f, crouchStepDistance = 0.5f;

    public float VolumeMin { get => volumeMin; set => volumeMin = value; }
    public float VolumeMax { get => volumeMax; set => volumeMax = value; }
    public float WalkVolumeMin { get => walkVolumeMin; set => walkVolumeMin = value; }
    public float WalkVolumeMax { get => walkVolumeMax; set => walkVolumeMax = value; }
    public float SprintVolume { get => sprintVolume; set => sprintVolume = value; }
    public float CrouchVolume { get => crouchVolume; set => crouchVolume = value; }

    public float StepDistance { get => stepDistance; set => stepDistance = value; }
    public float WalkStepDistance { get => walkStepDistance; set => walkStepDistance = value; }
    public float SprintStepDistance { get => sprintStepDistance; set => sprintStepDistance = value; }
    public float CrouchStepDistance { get => crouchStepDistance; set => crouchStepDistance = value; }


    private void Awake()
    {
        footstepSound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootstepSound();
    }

    private void CheckToPlayFootstepSound()
    {
        if (!characterController.isGrounded) return;

        if(characterController.velocity.sqrMagnitude > 0)
        {
            accumulateDistance += Time.deltaTime;
            if(accumulateDistance > stepDistance)
            {
                footstepSound.volume = Random.Range(VolumeMin, VolumeMax);
                footstepSound.clip = foostepClip[Random.Range(0, foostepClip.Length)];
                footstepSound.Play();
                accumulateDistance = 0;
            }
        } else
        {
            accumulateDistance = 0;
        }
    }
}
