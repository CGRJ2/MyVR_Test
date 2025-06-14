using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamCan : MonoBehaviour
{
    [SerializeField] IceCreamTasteType taste;
    int remainingAmount = 10;

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("out");
    }
}
