using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using Mirror;

public class Interactable : NetworkBehaviour
{    
    private Renderer[] renderers = null;
    private Dictionary<Renderer, Material[]> defaultMats = new Dictionary<Renderer, Material[]>();
    private Dictionary<Renderer, Material[]> highlightMats = new Dictionary<Renderer, Material[]>();
    public Material highlight;
    public string tooltipMessage = "Press E to Pickup";

    public bool triggersGameEnd = false;
   
    public UnityEvent InteractTriggered;

    protected bool _lit = false;

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            defaultMats.Add(renderer, renderer.sharedMaterials);
            var mats = renderer.sharedMaterials.ToList();
            mats.Add(highlight);
            highlightMats.Add(renderer, mats.ToArray());
        }
    }

    public virtual bool Available(uPlayer player)
    {
        return true;
    }

    public virtual void Select(uPlayer player)
    {
        if (_lit) return;

        _lit = true;

        foreach(var renderer in renderers)
            renderer.materials = highlightMats[renderer];

        Tooltipper.Instance.ShowTip(TooltipType.Interactable, tooltipMessage);
    }

    public virtual void Deselect()
    {
        if (!_lit) return;
        _lit = false;

        foreach (var renderer in renderers)
            renderer.materials = defaultMats[renderer];
                
        Tooltipper.Instance?.HideTip(TooltipType.Interactable, tooltipMessage);
    }

    [Server]
    public virtual void DoInteract(uPlayer player)
    {
        RpcTriggerInteract();
    }

    [ClientRpc]
    private void RpcTriggerInteract()
    {
        InteractTriggered?.Invoke();
    }

    private void OnDestroy()
    {        
        Deselect();
    }
}
