using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interactable : MonoBehaviour
{    
    private Renderer[] renderers = null;
    private Dictionary<Renderer, Material[]> defaultMats = new Dictionary<Renderer, Material[]>();
    private Dictionary<Renderer, Material[]> highlightMats = new Dictionary<Renderer, Material[]>();
    public Material highlight;
    public string tooltipMessage = "Press E to Pickup";

    private bool _lit = false;

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

    public void Select(uPlayer player)
    {
        if (_lit) return;

        _lit = true;

        foreach(var renderer in renderers)
            renderer.materials = highlightMats[renderer];

        Tooltipper.Instance.ShowTip(TooltipType.Interactable, tooltipMessage);
    }

    public void Deselect(uPlayer player)
    {
        if (!_lit) return;
        _lit = false;

        foreach (var renderer in renderers)
            renderer.materials = defaultMats[renderer];

        Tooltipper.Instance.HideTip(TooltipType.Interactable, tooltipMessage);
    }
}