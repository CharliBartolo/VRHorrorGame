using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRAndVRRigDataClass
{
    public string Name {get; set; }
    public XRDirectInteractor XRDirInt {get; set;}
    public VRMap VRMapping {get; set;}

    public XRAndVRRigDataClass(string name, XRDirectInteractor xrDirInt, VRMap vrMapping)
    {
        Name = name;
        XRDirInt = xrDirInt;
        VRMapping = vrMapping;
    }

    public XRAndVRRigDataClass(string name)
    {
        Name = name;        
    }
}
