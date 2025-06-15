using System.Collections.Generic;
using UnityEngine;

public class IceCream : MonoBehaviour, IStackable
{
    public GameObject upperSocket;
    public IceCreamTasteType taste;
    public List<Material> materials;
    public Corn baseCorn;

    public void SetTaste(IceCreamTasteType taste)
    {
        this.taste = taste;
        GetComponent<MeshRenderer>().material = materials[(int)taste];
    }
}
