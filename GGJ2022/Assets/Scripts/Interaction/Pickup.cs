using UnityEngine;

public class Pickup : Interactable
{
    public string key;

    public override bool Available(uPlayer player)
    {
        return string.IsNullOrWhiteSpace(player.inventory.heldItemKey);
    }

    public override void DoInteract(uPlayer player)
    {
        if (!player.inventory.CanPickup())
            return; //TODO: Warn the player that they cannot pick it up

        player.inventory.SetItem(this.key);

        Debug.Log($"{player.name} picked up {this.name} on frame {Time.frameCount}");

        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 2f);

        base.DoInteract(player);
    }
}

