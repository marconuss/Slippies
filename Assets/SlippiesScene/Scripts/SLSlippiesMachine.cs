using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SLSlippiesMachine : MonoBehaviour
{
    private bool isReadyToInteract;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.name == "FakeHarold")
        {
            isReadyToInteract = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.name == "FakeHarold")
        {
            isReadyToInteract = false;
        }
    }

}
