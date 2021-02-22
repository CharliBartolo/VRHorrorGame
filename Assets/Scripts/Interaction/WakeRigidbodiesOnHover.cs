using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WakeRigidbodiesOnHover : MonoBehaviour
{
    void OnTriggerStay(Collider other) 
    {
        //Debug.Log("Wake Rigidbody script is running!");

        if (other.GetComponent<Rigidbody>() != null)
        {
            other.GetComponent<Rigidbody>().WakeUp();
        }
    }  
}
