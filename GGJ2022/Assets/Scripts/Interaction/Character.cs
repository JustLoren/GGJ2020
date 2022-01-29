using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Interactable
{
    private string activePrompt;
    public string firstPrompt = "I want a boombox.";
    public string secondPrompt = "Thanks for the boombox";

    public override bool Available(uPlayer player)
    {
        return base.Available(player);
    }

    public override void DoInteract(uPlayer player)
    {
        base.DoInteract(player);
    }

}
