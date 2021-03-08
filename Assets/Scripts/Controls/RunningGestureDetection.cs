using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RunningGestureDetection : MonoBehaviour
{
    public InputActionReference leftHandVelRef;
    public InputActionReference rightHandVelRef;
    private ActionBasedContinuousMoveProvider continuousMoveProvider;
    private float sprintArmMovementValue = 1f;
    private float baseMovementSpeed;
    private float startingTimeBeforeWalkReset = 0.4f;
    private float timeBeforeWalkReset;
    // Start is called before the first frame update
    void Start()
    {
        continuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        baseMovementSpeed = continuousMoveProvider.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (leftHandVelRef.action.ReadValue<Vector3>().sqrMagnitude > sprintArmMovementValue ||rightHandVelRef.action.ReadValue<Vector3>().sqrMagnitude > sprintArmMovementValue)
        {
            timeBeforeWalkReset = startingTimeBeforeWalkReset; 
        }
        else
        {
            timeBeforeWalkReset -= Time.deltaTime;
        }

        if (timeBeforeWalkReset > 0f)
        {
            SetSprintSpeed();
        }
        else
        {
            ResetMovementSpeed();
        }        
    }

    void SetSprintSpeed()
    {
        continuousMoveProvider.moveSpeed = baseMovementSpeed * 2f;
    }

    void ResetMovementSpeed()
    {
        continuousMoveProvider.moveSpeed = baseMovementSpeed;
    }
}
