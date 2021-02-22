using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabPosFixer : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    //public XRDirectInteractor directInteractor;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }
    

    public void FixHandPos()
    {                
        RaycastHit hit;
        if (Physics.Raycast(grabInteractable.selectingInteractor.transform.position, transform.position, out hit, 1f, LayerMask.GetMask("Default")))
        {
            grabInteractable.attachTransform.position = hit.transform.position;
            Debug.Log("Hand position fixed");
        }
        else
        {
            ResetHandPos();
            Debug.Log("Unable to fix hand position");
        }        
    }

    public void ResetHandPos()
    {
        grabInteractable.attachTransform.position = transform.position;
        Debug.Log("Hand position reset");
    }
}
