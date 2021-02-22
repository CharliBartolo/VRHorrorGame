using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFootIK : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
[Range(0,1)]
    public float footPosWeight = 1f;
    public Transform footIKHintPos;
[Range(0,1)]
    public float footRotWeight = 1f;
    public Vector3 footOffset;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex) 
    {
        Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);
        //Vector3 rightKneePos = animator.GetIKHintPosition(AvatarIKHint.RightKnee) + Vector3.forward;

        Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);
        //Vector3 leftKneePos = animator.GetIKHintPosition(AvatarIKHint.LeftKnee) + Vector3.forward;        

        FootIK(leftFootPos, AvatarIKGoal.LeftFoot, AvatarIKHint.LeftKnee);
        FootIK(rightFootPos, AvatarIKGoal.RightFoot, AvatarIKHint.RightKnee);
        
        /*
        if (Physics.Raycast(rightFootPos + Vector3.up, Vector3.down, out hit))
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, footPosWeight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + footOffset);

            Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, footRotWeight);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, footRotation);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        }        
        
        
        if (Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out hit))
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, footPosWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + footOffset);

            Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, footRotWeight);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, footRotation);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        }       
        */      
    }

    private void FootIK(Vector3 footPos, AvatarIKGoal footIKGoal, AvatarIKHint footIKHint)
    {
        RaycastHit hit;

        if (Physics.Raycast(footPos + Vector3.up, Vector3.down, out hit, 2f, LayerMask.GetMask("Default", "Interactables")))
        {            
            animator.SetIKHintPosition(footIKHint, footPos + Vector3.up + transform.forward);
            //animator.SetIKHintPosition(footIKHint, hintPos);
            animator.SetIKHintPositionWeight(footIKHint, 1f);

            animator.SetIKPositionWeight(footIKGoal, footPosWeight);
            animator.SetIKPosition(footIKGoal, hit.point + footOffset);
            

            Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(footIKGoal, footRotWeight);
            animator.SetIKRotation(footIKGoal, footRotation);
        }
        else
        {
            animator.SetIKPositionWeight(footIKGoal, 0);
            animator.SetIKHintPositionWeight(footIKHint, 0f);
        }        
    }
}
