using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Mirror;

public class Inventory : NetworkBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public string key;
        public GameObject obj;
    }

    public List<InventoryItem> knownItems;

    [SyncVar(hook = nameof(UpdateHeldItem))]
    public string heldItemKey;

    private void UpdateHeldItem(string _old, string _new)
    {
        foreach(var item in knownItems)
        {
            item.obj.SetActive(item.key == _new);
        }
    }

    public void GrabItem(string key)
    {
        CmdGrabItem(key);
    }

    [Command]
    public void CmdGrabItem(string key)
    {
        heldItemKey = key;
    }

    public bool CanPickup() { return string.IsNullOrWhiteSpace(heldItemKey); }
}

