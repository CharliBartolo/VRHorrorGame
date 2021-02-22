using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = Vector3.Lerp(rigTarget.position, vrTarget.TransformPoint(trackingPositionOffset), Time.deltaTime * 20f);
        rigTarget.rotation = Quaternion.Lerp(rigTarget.rotation, vrTarget.rotation * Quaternion.Euler(trackingRotationOffset), Time.deltaTime * 15f);
        //rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }

    /*
    public void Map(Vector3 forearmPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(forearmPos, vrTarget.TransformPoint(trackingPositionOffset), out hit, 1f, LayerMask.GetMask("Interactables", "Default")))
        {
            rigTarget.position = Vector3.Lerp(rigTarget.position, hit.point, Time.deltaTime * 20f);
            rigTarget.rotation = Quaternion.Lerp(rigTarget.rotation, vrTarget.rotation * Quaternion.Euler(trackingRotationOffset), Time.deltaTime * 15f);
        }
        else
        {
            Map();
        }
    }
    */
}

public class VRRig : MonoBehaviour
{
    // Start is called before the first frame update
    public float turnSmoothness = 1f;
    public VRMap head;
    public VRMap leftHand;
    public Transform leftForearmPos;
    public VRMap rightHand;
    public Transform rightForearmPos;

    public Transform headConstraint;
    private Vector3 headBodyOffset;
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, 
            Vector3.ProjectOnPlane(headConstraint.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        head.Map();
        //leftHand.Map(leftForearmPos.position);
        //rightHand.Map(rightForearmPos.position);
        leftHand.Map();
        rightHand.Map();
    }
}
