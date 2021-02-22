using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HandPresence : MonoBehaviour
{    
    //public ActionBasedController leftController;
    //public ActionBasedController rightController;
    public InputActionReference leftHand;  
    public InputActionReference rightHand;   

    private Animator playerAnimator;


    // Start is called before the first frame update
    void Start()
    {                
        playerAnimator = GetComponent<Animator>();

        leftHand.action.performed += UpdateHandAnimation;
        rightHand.action.performed += UpdateHandAnimation;

        /*
        // For list order, head = 0, left hand = 1, right hand = 2
        List<InputDevice> inputDevices = new List<InputDevice>();
        //InputDeviceCharacteristics headDeviceCharacteristics = InputDeviceCharacteristics.Camera         
        
        InputDevices.GetDevices(inputDevices);

        foreach(InputDevice device in inputDevices)
        {
            Debug.Log(device.name + device.characteristics);
        }

        foreach (InputDevice device in inputDevices)
        {
            switch (device.characteristics)
            {
                case (InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller):
                    if (leftHandDevice == null)
                    {
                        leftHandDevice = device;
                    }
                    else
                    {
                        Debug.Log("Second left hand device found, sticking with first device.");
                    }
                    break;

                case (InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller):
                    if (rightHandDevice == null)
                    {
                        rightHandDevice = device;
                    }
                    else
                    {
                        Debug.Log("Second right hand device found, sticking with first device.");
                    }
                    break;
            }
        }
        */
    }

    void UpdateHandAnimation(InputAction.CallbackContext context)
    {   
        float leftHandGripValue = leftHand.action.ReadValue<float>();
        float rightHandGripValue = rightHand.action.ReadValue<float>();
        //float leftHandGripValue = leftController.selectAction.action.ReadValue<float>();
        //float rightHandGripValue = rightController.selectAction.action.ReadValue<float>();

        //if (leftController.selectAction.action.ReadValue<float>() > 0)
        //{ 
            playerAnimator.SetFloat("LeftGrip", leftHandGripValue);
            playerAnimator.SetFloat("RightGrip", rightHandGripValue);
        //}
    }

    void HandIK()
    {
        //playerAnimator.GetIKPosition(AvatarIKGoal.LeftHand)
    }
}
