using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerFootsteps footsteps;
    private Vector3 move_Direction;
    public float speed;
    public float moveSpeed;
    public float sprint_Speed = 10f;
    public float crouch_Speed = 2f;
  
    private float gravity = 5f;
    public float jump_Forse = 1.33f;
    private float y_Energy;

    private Transform look_Root;
    private float stand_Height = 0.98f;
    private float crouch_Height = 0.44f;
    private bool is_Crouching = false;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        look_Root = transform.GetChild(0);
        footsteps = GetComponentInChildren<PlayerFootsteps>();
    }

    private void Start()
    {
        footsteps.VolumeMin = footsteps.WalkVolumeMin;
        footsteps.VolumeMax = footsteps.WalkVolumeMax;
        footsteps.StepDistance = footsteps.WalkStepDistance;
    }

    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
    }

    private void MoveThePlayer()
    {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));
        // convert move_direction from worldSpace to localSpace
        move_Direction = transform.TransformDirection(move_Direction);
        Crouch();
        if (Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching)
        {
            speed = sprint_Speed;
            footsteps.StepDistance = footsteps.SprintStepDistance;
            footsteps.VolumeMin = footsteps.SprintVolume;
            footsteps.VolumeMax = footsteps.SprintVolume;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching)
        {
            speed = moveSpeed;
            footsteps.VolumeMin = footsteps.WalkVolumeMin;
            footsteps.VolumeMax = footsteps.WalkVolumeMax;
            footsteps.StepDistance = footsteps.WalkStepDistance;
        }
        move_Direction *= speed * Time.deltaTime;
        ApplyGravity();
        characterController.Move(move_Direction);
    }

    private void Crouch()
    {
       if (Input.GetKeyDown(KeyCode.LeftControl) )
        {
            is_Crouching = true;
            footsteps.VolumeMin = footsteps.CrouchVolume;
            footsteps.VolumeMax = footsteps.CrouchVolume;
            footsteps.StepDistance = footsteps.CrouchStepDistance;
            look_Root.localPosition = new Vector3(0, crouch_Height, 0);
            speed = crouch_Speed;
        }

       if (Input.GetKeyUp(KeyCode.LeftControl)) {
            is_Crouching = false;
            footsteps.VolumeMin = footsteps.WalkVolumeMin;
            footsteps.VolumeMax = footsteps.WalkVolumeMax;
            footsteps.StepDistance = footsteps.WalkStepDistance;
            look_Root.localPosition = new Vector3(0, stand_Height, 0);
            speed = moveSpeed;
        }
    }

    private void ApplyGravity()
    {
        y_Energy -= gravity * Time.deltaTime;
        PlayerJump();
        move_Direction.y = y_Energy;
    }

    private void PlayerJump()
    {
       if(characterController.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
                 y_Energy = jump_Forse;
        }
    }
}
