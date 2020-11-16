using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController charController;
    private CharacterAnimation playerAnimation;

    public float movementSpeed = 4f;
    public float gravity = 9.8f;
    public float rotationSpeed = 0.15f;
    public float rotateDegreesPerSecond = 180;

    void Awake()
    {
        charController = GetComponent<CharacterController>();
        playerAnimation = GetComponent<CharacterAnimation>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        AnimateWalk();
    }

    void Move()
    {
        if(Input.GetAxis(Axis.VERTICAL) > 0){
            Vector3 moveDirection = transform.forward;
            moveDirection.y -= gravity * Time.deltaTime; 

            charController.Move(moveDirection * movementSpeed * Time.deltaTime);
        } 
        else if(Input.GetAxis(Axis.VERTICAL) < 0){
            Vector3 moveDirection = -transform.forward;
            moveDirection.y -= gravity * Time.deltaTime; 
            charController.Move(moveDirection * movementSpeed * Time.deltaTime);
        }
        else {
            // charController = Vector3.zero;
            charController.Move(Vector3.zero);
        }
        // print(Input.GetAxis("Vertical"));
    }

    void Rotate()
    {
        Vector3 rotationDirection = Vector3.zero;

        if(Input.GetAxis(Axis.HORIZONTAL) < 0){
            rotationDirection = transform.TransformDirection(Vector3.left); 
        } 
        if(Input.GetAxis(Axis.HORIZONTAL) > 0){
            rotationDirection = transform.TransformDirection(Vector3.right); 
        } 
        
        if(rotationDirection != Vector3.zero){
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, Quaternion.LookRotation(rotationDirection),
                rotateDegreesPerSecond * Time.deltaTime
            );
        }

    }

    void AnimateWalk()
    {
        if(charController.velocity.sqrMagnitude != 0f){
            playerAnimation.Walk(true);
        } else {
            playerAnimation.Walk(false);
        }
    }

}
