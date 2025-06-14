using System.Collections.Generic;
using UnityEngine;

public class IceCream : MonoBehaviour
{
    public GameObject upperSocket;
    public IceCreamTasteType taste;
    public List<Material> materials;

    public void SetTaste(IceCreamTasteType taste)
    {
        this.taste = taste;
        GetComponent<MeshRenderer>().material = materials[(int)taste];
    }
}
