using System.Dynamic;
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
    private Transform lockedTargetPos;
    private bool isLocked;
    private bool isLockedPosSet = false;

    public void Map()
    {
        if (!isLocked)
        {
            rigTarget.position = Vector3.Lerp(rigTarget.position, vrTarget.TransformPoint(trackingPositionOffset), Time.deltaTime * 30f);
            rigTarget.rotation = Quaternion.Lerp(rigTarget.rotation, vrTarget.rotation * Quaternion.Euler(trackingRotationOffset), Time.deltaTime * 20f);
        }
        else
        {
            rigTarget.position = Vector3.Lerp(rigTarget.position, lockedTargetPos.position, Time.deltaTime * 30f);
             rigTarget.rotation = Quaternion.Lerp(rigTarget.rotation, vrTarget.rotation * Quaternion.Euler(trackingRotationOffset), Time.deltaTime * 20f);
            //rigTarget.rotation = Quaternion.Lerp(rigTarget.rotation, lockedTargetPos.rotation * Quaternion.Euler(trackingRotationOffset), Time.deltaTime * 20f);             
        }
        
        //rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }

    private void InstantiateTargetPos()
    {
        lockedTargetPos = new GameObject("LockedTargetPos").transform;
        SetLimbLockPos();
    }

    public void LockLimb(bool statusToSet)
    {
        if (!isLockedPosSet)
        {
            InstantiateTargetPos();
            isLockedPosSet = true;
        }

        SetLimbLockPos();
        isLocked = statusToSet;                
    }

    private void SetLimbLockPos()
    {
        lockedTargetPos.position = rigTarget.position;
        lockedTargetPos.rotation = rigTarget.rotation;
        isLockedPosSet = true;
    }

    public bool IsLocked
    {
        get => isLocked;        
    }
}

public class VRRig : MonoBehaviour
{
    // Start is called before the first frame update
    public float turnSmoothness = 1f;
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Transform leftForearmPos;    
    public Transform rightForearmPos;

    public Transform headConstraint;
    private Vector3 headBodyOffset;

    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
        Climber.leftHandData.VRMapping = leftHand;
        Climber.rightHandData.VRMapping = rightHand;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, headConstraint.position + headBodyOffset, Time.deltaTime * 30f);
        transform.forward = Vector3.Lerp(transform.forward, 
            Vector3.ProjectOnPlane(headConstraint.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}
