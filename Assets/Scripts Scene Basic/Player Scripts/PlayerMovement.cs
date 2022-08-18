using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float gravity = -9.81f;
    private float yVelocity = 0f;
    private Vector3 moveDirection;


    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMethod(); //Takes input from the keyboard and mouse

    }

    void InputMethod()
    {
        moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0, Input.GetAxis(Axis.VERTICAL)); //Axis is a static class that holds the names of the axes(Horizontal and Vertical)
        
        moveDirection = transform.TransformDirection(moveDirection); // transform the direction to the direction of the player

        moveDirection *= speed*Time.deltaTime; // multiply the speed by the time between frames and the direction to get the velocity

        Gravity(); //apply gravity to the player
        characterController.Move(moveDirection);  // move the player
    }

    void Gravity(){
        yVelocity += gravity*Time.deltaTime; // gravity is applied to the y velocity (-9.81)
        Jump();
        moveDirection.y = yVelocity; // moveDirection is the velocity of the player in the y direction
    }
    void Jump(){
        if(characterController.isGrounded && Input.GetKeyDown(KeyCode.Space)){
            yVelocity = jumpForce;
        }
    }
}
