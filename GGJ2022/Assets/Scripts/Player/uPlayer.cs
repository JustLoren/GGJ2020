using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class uPlayer : NetworkBehaviour
{
    public ScanForInteractable scanner;
    private void Update()
    {
        if (isLocalPlayer)
        {
            scanner.Scan(this);
        }
    }
}
