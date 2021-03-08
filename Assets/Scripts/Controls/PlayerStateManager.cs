using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;    

public class PlayerStateManager : MonoBehaviour
{
    private ActionBasedContinuousMoveProvider continuousMovement;
    private CharacterController character;
    private Climber climber;
    private Vector3 savedCharVelocity;
    private enum movementStatus {Grounded, Falling, Climbing}
    private movementStatus currentStatus;
    private float initialTimeBeforeConsideredFalling = 0.5f;
    private float currentTimeBeforeConsideredFalling;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        continuousMovement = GetComponent<ActionBasedContinuousMoveProvider>();
        climber = GetComponent<Climber>();
        currentTimeBeforeConsideredFalling = initialTimeBeforeConsideredFalling;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentStatus = CheckStatus();

        switch (currentStatus)
        {
            case (movementStatus.Grounded):
                continuousMovement.enabled = true;                
                savedCharVelocity = character.velocity;
                ResetFallingTimer();
                Debug.Log("Grounded");
                break;
            case (movementStatus.Falling):
                continuousMovement.enabled = false;
                ApplyMomentum();
                Debug.Log("Falling");
                break;
            case (movementStatus.Climbing):
                continuousMovement.enabled = false;
                //savedCharVelocity = character.velocity;
                savedCharVelocity = Vector3.ClampMagnitude(character.velocity, 10f);
                currentTimeBeforeConsideredFalling = 0f;
                Debug.Log("Climbing");
                break;
        }           
    }

    movementStatus CheckStatus()
    {    
        RaycastHit hit;
        /*
        if (Physics.Raycast(footPos + Vector3.up, Vector3.down, out hit, 2f, LayerMask.GetMask("Default", "Interactables")))
        {

        }
        */
        if (climber.IsClimbing)
        {
            return movementStatus.Climbing;
        }
        else if (!character.isGrounded)
        //else if (Physics.Raycast(transform.position, Vector3.down, out hit, character.height + 0.1f, LayerMask.GetMask("Default", "Interactables")))
        {
            // Instead of returning falling, maybe increment a float by Time.deltaTime and if player is not grounded for x seconds, return falling
            currentTimeBeforeConsideredFalling -= Time.deltaTime;

            if (currentTimeBeforeConsideredFalling <= 0)
            {
                return movementStatus.Falling;
            }

            return currentStatus;            
        }
        else
        {
            return movementStatus.Grounded;
        }   
    }

    void ApplyMomentum()
    {          
        savedCharVelocity = savedCharVelocity + (Physics.gravity * Time.deltaTime);
        savedCharVelocity.y = Mathf.Clamp(savedCharVelocity.y, -10f, 1000f);
        character.Move(savedCharVelocity * Time.deltaTime);      
    }

    void ResetFallingTimer()
    {
        currentTimeBeforeConsideredFalling = initialTimeBeforeConsideredFalling;
    }
}
