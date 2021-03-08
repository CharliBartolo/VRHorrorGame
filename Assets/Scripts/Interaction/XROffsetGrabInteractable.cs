using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{    
    //bool attachHandToObject;    
    bool disableCollisionWhileGrabbing = true;

    private int storedLayer;
    private Vector3 initialAttachLocalPos;
    private Quaternion initialAttachLocalRot;    

    void Start() 
    {
        if (!attachTransform)
        {
            GameObject grab = new GameObject("Grab Pivot");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
        }      

        initialAttachLocalPos = attachTransform.localPosition;
        initialAttachLocalRot = attachTransform.localRotation;
        storedLayer = gameObject.layer;
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (disableCollisionWhileGrabbing)
        {
            gameObject.layer = LayerMask.NameToLayer("InteractablesNoCollide");
            Debug.Log("Disabled grabbed object collider");
        }

        if (args.interactor is XRDirectInteractor)
        {
            attachTransform.position = args.interactor.transform.position;
            attachTransform.rotation = args.interactor.transform.rotation;
        }
        else
        {
            attachTransform.position = initialAttachLocalPos;
            attachTransform.rotation = initialAttachLocalRot;
        }
        base.OnSelectEntering(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        if (disableCollisionWhileGrabbing)
        {
            gameObject.layer = storedLayer;
        }

        base.OnSelectExiting(args);
    }
}
