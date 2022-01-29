using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class uPlayer : NetworkBehaviour
{
    private ScanForInteractable scanner;
    private StarterAssets.StarterAssetsInputs input;
    public Inventory inventory;

    private void Start()
    {
        scanner = GetComponentInChildren<ScanForInteractable>();
        input = GetComponentInChildren<StarterAssets.StarterAssetsInputs>();
    }

    public override void OnStartServer()
    {
        GameSystem.Instance.AddPlayer(this);
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

            input.interact = false;
            
            if (input.swapPlaces)
            {
                if (!wantsToSwap)
                {
                    CmdDoSwap();
                }
                input.swapPlaces = false;
            }
        }
    }

    [Command]
    private void CmdDoSwap()
    {
        wantsToSwap = true;
        GameSystem.Instance.Swap(this);
    }

    [SyncVar]
    public bool wantsToSwap;

    public bool isInUtopia;

    [TargetRpc]
    public void RpcMoveTo(NetworkConnection target, Vector3 position)
    {
        StartCoroutine(SwapRealities(position));
    }

    private IEnumerator SwapRealities(Vector3 position)
    {
        UIFader.Instance.ShowCurtain();
        while (UIFader.Instance.enabled)
            yield return null;

        Debug.Log($"Teleporting to {position}");
        var fps = this.GetComponent<StarterAssets.FirstPersonController>();
        fps.movementFrozen = true;
        var nt = this.GetComponent<NetworkTransform>();
        nt.CmdTeleport(position);
        //this.transform.position = position;                
        
        yield return new WaitForSeconds(.25f);
        UIFader.Instance.HideCurtain();
        fps.movementFrozen = false;
    }
}
