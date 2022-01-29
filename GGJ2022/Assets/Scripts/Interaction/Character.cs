using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Character : Interactable
{
    public string speechIconName;
    public string keyNeeded;
    public SpriteRenderer spriteRenderer;

    public string initialMessage = "I want an apple.";
    public string wantsYourItemMessage = "That's a nice apple you got there.";
    public string hasItemMessage = "Thanks for the apple";

    [SyncVar(hook = nameof(UpdateMissionFulfilled))]
    public bool missionFulfilled = false;

    private void UpdateMissionFulfilled(bool _old, bool _new)
    {
        if (_new && _lit)
        {
            Deselect();            
        }
    }

    public override void DoInteract(uPlayer player)
    {
        if (player.inventory.heldItemKey == keyNeeded)
        {
            player.RpcRescan(player.netIdentity.connectionToClient);
            player.inventory.SetItem("");
            missionFulfilled = true;
            base.DoInteract(player);
        }
    }
    
    public override void Deselect()
    {
        if (!_lit) return;
        
        _lit = false;

        Tooltipper.Instance.HideSpeech(this);
    }

    public override void Select(uPlayer player)
    {
        if (_lit) return;

        _lit = true;

        string text = "";
        if (missionFulfilled)
            text = hasItemMessage;
        else if (!string.IsNullOrWhiteSpace(keyNeeded) && player.inventory.heldItemKey == keyNeeded)
            text = wantsYourItemMessage;
        else
            text = initialMessage;

        Tooltipper.Instance.ShowSpeech(this, text);        
    }
}
