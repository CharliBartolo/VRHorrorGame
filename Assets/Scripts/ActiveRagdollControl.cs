using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRagdollControl : MonoBehaviour
{
    //ConfigurableJointExtensions jointExtensions;
    public Transform[] animatedJoints;
    public Transform[] ragdollJoints;
    private Quaternion[] initialJointRotations;

    // Start is called before the first frame update
    void Start()
    {
        /*
        animatedJoints = new Transform[];
        ragdollJoints = new Transform[];

        for (int i = 0; i < animatedJoints.Length; i++)
        {
            initialJointRotations[i] = animatedJoints[i].localRotation;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
