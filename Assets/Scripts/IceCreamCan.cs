using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamCan : MonoBehaviour
{
    [SerializeField] IceCreamTasteType taste;
    int remainingAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScoopHead"))
        {
            ScoopController scoop = other.GetComponentInParent<ScoopController>();
            scoop.SetIceCreamCan(this);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ScoopHead"))
        {
            ScoopController scoop = other.GetComponentInParent<ScoopController>();
            scoop.SetIceCreamCan(null);
        }
            
    }


}
