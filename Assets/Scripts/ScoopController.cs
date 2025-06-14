using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScoopController : XRGrabInteractable
{
    [SerializeField] IceCreamCan touchedCan;
    [SerializeField] Collider ScoopHeadCollider;

    [SerializeField] GameObject iceCreamPrefab;
    [SerializeField] Transform iceSpawnPoint;

    GameObject nowOnScoopIce;

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);

        // 1. 아이스크림 통 안에 있는지 체크
        if (touchedCan != null)
        {
            nowOnScoopIce = Instantiate(iceCreamPrefab, iceSpawnPoint);
            nowOnScoopIce.GetComponent<XRGrabInteractable>().enabled = false;
            nowOnScoopIce.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);

        // 1. 스쿱에 아이스크림이 장착되어 있을 때
        if (nowOnScoopIce != null)
        {
            nowOnScoopIce.transform.SetParent(null, true);
            nowOnScoopIce.GetComponent<XRGrabInteractable>().enabled = true;
            nowOnScoopIce.GetComponent<Rigidbody>().isKinematic = false;
            nowOnScoopIce = null;
        }
    }

    public void SetIceCreamCan(IceCreamCan can)
    {
        touchedCan = can;
    }

}
