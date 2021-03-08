using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Climber : MonoBehaviour
{           
    public InputActionReference leftHandVelRef;
    public InputActionReference rightHandVelRef; 

    public static List<XRAndVRRigDataClass> climbingHands;
    public static XRAndVRRigDataClass leftHandData = new XRAndVRRigDataClass("leftHand");
    public static XRAndVRRigDataClass rightHandData = new XRAndVRRigDataClass("rightHand");  
    private bool isClimbing = false; 

    private ActionBasedContinuousMoveProvider continuousMovement;
    private CharacterController character;  

    // Start is called before the first frame update
    void Start()
    {         
        character = GetComponent<CharacterController>();
        continuousMovement = GetComponent<ActionBasedContinuousMoveProvider>();
        climbingHands = new List<XRAndVRRigDataClass>();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (climbingHands.Count > 0)
        {
            //continuousMovement.enabled = false;
            isClimbing = true;
            Climb();
            ShrinkPlayerHeight();
        }
        else 
        {
            isClimbing = false;
            //continuousMovement.enabled = true;            
        }
    }

    void Climb()
    {         
        //Debug.Log(climbingHands.Count);       

        switch (climbingHands.Count)
        {
            case 1:
                if (climbingHands[0].Name == "leftHand")
                {
                    MoveBody(leftHandVelRef.action.ReadValue<Vector3>());                    
                }
                else
                {
                    MoveBody(rightHandVelRef.action.ReadValue<Vector3>());
                }                
                break;
            case 2:
                MoveBody((leftHandVelRef.action.ReadValue<Vector3>() + rightHandVelRef.action.ReadValue<Vector3>()) * 0.75f);
                break;
            default:                
                break;
        }  
    }

    private void ShrinkPlayerHeight()
    {
        character.height = 0.1f;
    }

    void MoveBody(Vector3 movementVelocity)
    {
        character.Move(transform.rotation * -movementVelocity * Time.fixedDeltaTime);
    } 

    public bool IsClimbing
    {
        get => isClimbing;
    }
}
