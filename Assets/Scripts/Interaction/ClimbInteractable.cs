using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class ClimbInteractable : XRBaseInteractable
{
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {        
        if (args.interactor is XRDirectInteractor)
        {        
            if (args.interactor.name == "LeftHand Controller")
            {
                Climber.leftHandData.XRDirInt = (XRDirectInteractor)args.interactor;
                Climber.climbingHands.Add(Climber.leftHandData);
                Climber.leftHandData.VRMapping.LockLimb(true);
            }
            else if (args.interactor.name == "RightHand Controller")
            {
                Climber.rightHandData.XRDirInt = (XRDirectInteractor)args.interactor;
                Climber.climbingHands.Add(Climber.rightHandData);
                Climber.rightHandData.VRMapping.LockLimb(true);
            }         
        }
        
        base.OnSelectEntering(args);                     
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {        
        if (args.interactor is XRDirectInteractor)
        {
            if (args.interactor.name == "LeftHand Controller")
            {                
                Climber.climbingHands.Remove(Climber.leftHandData);               
                Climber.leftHandData.VRMapping.LockLimb(false);
            }
            else if (args.interactor.name == "RightHand Controller")
            {                
                Climber.climbingHands.Remove(Climber.rightHandData);
                Climber.rightHandData.VRMapping.LockLimb(false);
            }        
            
        }   

        base.OnSelectExiting(args);        
    }
}
