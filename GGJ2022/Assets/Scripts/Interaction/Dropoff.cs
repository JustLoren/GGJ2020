using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropoff : Interactable
{
    public string keyNeeded;

    public override bool Available(uPlayer player)
    {
        return string.IsNullOrWhiteSpace(keyNeeded) || player.inventory.heldItemKey == keyNeeded;
    }

    public override void DoInteract(uPlayer player)
    {
        player.inventory.SetItem("");

        Debug.Log($"{player.name} dropped off at {this.name} at {Time.frameCount}");
        
        base.DoInteract(player);
    }

}
