using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class uPlayer : NetworkBehaviour
{
    private ScanForInteractable scanner;
    private StarterAssets.StarterAssetsInputs input;    

    private void Start()
    {
        scanner = GetComponentInChildren<ScanForInteractable>();
        input = GetComponentInChildren<StarterAssets.StarterAssetsInputs>();
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            scanner.Scan(this);

            if (input.interact && scanner.target != null)
            {
                scanner.target.DoInteract(this);
            }
        }
    }
}
