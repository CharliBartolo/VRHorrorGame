using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatorController : MonoBehaviour
{
    public float speedThreshold = 0.1f;

    [Range(0,1)]
    public float smoothingVal = 0.5f;

    // Start is called before the first frame update
    private Animator animator;
    private Vector3 previousPos;
    private VRRig vrRig;

    void Start()
    {
        animator = GetComponent<Animator>();
        vrRig = GetComponent<VRRig>();
        previousPos = vrRig.head.vrTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 headsetSpeed = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headsetSpeed.y = 0f;

        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        previousPos = vrRig.head.vrTarget.position;

        // Set Animator Values
        float prevDirX = animator.GetFloat("xDir");
        float prevDirY = animator.GetFloat("yDir");

        animator.SetBool("isMoving", headsetLocalSpeed.magnitude > speedThreshold);
        animator.SetFloat("xDir", Mathf.Lerp(prevDirX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothingVal));
        animator.SetFloat("yDir", Mathf.Lerp(prevDirY, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothingVal));

    }
}
