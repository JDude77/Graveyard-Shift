using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Attributes
    private CharacterController controller;
    private float playerSpeed = 3f;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private Transform groundCheck;
    private float groundDistance = 0.2f;
    private LayerMask groundMask;
    private bool isGrounded;
    private bool canMove;
    #endregion

    #region Getters & Setters
    #region Controller
    public CharacterController getController()
    {
        return controller;
    }//End controller getter
    public void setController(CharacterController newController)
    {
        controller = newController;
    }//End controller setter
    #endregion
    #region Player Speed
    //Getter
    public float getPlayerSpeed()
    {
        return playerSpeed;
    }//End player speed getter
    //Setter
    public void setPlayerSpeed(float newPlayerSpeed)
    {
        playerSpeed = newPlayerSpeed;
    }//End player speed setter
    #endregion
    #endregion
    //Start is called before the first frame update
    private void Start()
    {
        //Acquire all of the required components for movement from the player object
        controller = GetComponent<CharacterController>();
        groundCheck = transform.Find("GroundCheck").transform;
        groundMask = LayerMask.GetMask("Ground");
        //Enable player control
        canMove = true;
    }//End Start

    //Update is called once per frame
    private void Update()
    {
        //Run all movement code
        handleGravity();
        doMovement();
    }//End Update

    #region Behaviours
    //Make the player fall
    private void handleGravity()
    {
        //Check that the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //If the player is grounded and the gravity is still increasing
        if (isGrounded && velocity.y < 0)
        {
            //Stop the player falling so quickly
            velocity.y = -1f;
        }//End if
    }//End handleGravity

    private void doMovement()
    {
        //If the player character is allowed to move
        if (canMove)
        {
            //Get player movement input
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            //Calculate the direction of horizontal movement that the player should be doing
            Vector3 directionOfMovement = transform.right * x + transform.forward * z;

            //Apply horizontal movement according to player speed and framerate
            controller.Move(directionOfMovement * playerSpeed * Time.deltaTime);

            //Adjust velocity according to gravity and framerate
            velocity.y += gravity * Time.deltaTime;

            //Apply vertical movement according to framerate
            controller.Move(velocity * Time.deltaTime);
        }//End if
    }//End doMovement

    #endregion
}
