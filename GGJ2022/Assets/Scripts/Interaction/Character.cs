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

    [SyncVar]
    public bool missionFulfilled = false;

    public override void DoInteract(uPlayer player)
    {
        if (player.inventory.heldItemKey == keyNeeded)
        {
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
        else if (player.inventory.heldItemKey == keyNeeded)
            text = wantsYourItemMessage;
        else
            text = initialMessage;

        Tooltipper.Instance.ShowSpeech(this, text);        
    }
}
