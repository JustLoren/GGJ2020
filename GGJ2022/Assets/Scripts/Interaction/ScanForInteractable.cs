using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanForInteractable : MonoBehaviour
{
    public Transform scanSource;
    public float scanDistance = 2f;
    public LayerMask targetLayer;
    public Interactable target = null;

    public void Scan(uPlayer player)
    {        
        var ray = new Ray(scanSource.position, scanSource.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if (Physics.Raycast(ray, out hit, scanDistance, targetLayer.value, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            var interactable = hit.transform.GetComponent<Interactable>();
            if (interactable != null)
            {
                if (interactable != target)
                    SwapTarget(player, interactable);
                return;
            }
        } else
        {
            Debug.DrawRay(ray.origin, ray.direction * scanDistance, Color.red);
        }
        SwapTarget(player, null);
    }

    private void SwapTarget(uPlayer player, Interactable newTarget)
    {
        if (target != null)        
            target.Deselect();
        
        target = newTarget;

        if (target != null && target.Available(player))
            target.Select(player);
    }
    
}
