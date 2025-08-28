using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostPad : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody)
        {
            print("entered!");
            other.attachedRigidbody.AddForce(Vector3.up * 1000);
        }
    }
}
