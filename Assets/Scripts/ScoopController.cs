using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScoopController : XRGrabInteractable
{
    [SerializeField] Collider targetIce;
    [SerializeField] Collider ScoopHeadCollider;
    void IgnoreCollisionWithTarget()
    {
        Physics.IgnoreCollision(ScoopHeadCollider, targetIce, true);
    }

    void RestoreCollisionWithTarget()
    {
        Physics.IgnoreCollision(ScoopHeadCollider, targetIce, false);

    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);

        // 1. 아이스크림 통 안에 있는지 체크
        IgnoreCollisionWithTarget();
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);
        RestoreCollisionWithTarget();
    }
}
