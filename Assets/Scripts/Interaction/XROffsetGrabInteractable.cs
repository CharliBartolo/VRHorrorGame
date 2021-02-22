using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
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
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
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
}
