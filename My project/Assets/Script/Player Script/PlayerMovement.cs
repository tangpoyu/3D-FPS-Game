using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 move_Direction;
    public float speed = 5f;
    private float gravity = 5f;
    public float jump_Forse = 10f;
    private float y_Energy;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
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
        move_Direction *= speed * Time.deltaTime;
        ApplyGravity();
        characterController.Move(move_Direction);
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
            y_Energy = jump_Forse * Time.deltaTime;
        }
    }
}
