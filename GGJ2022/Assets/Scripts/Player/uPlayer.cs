using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class uPlayer : NetworkBehaviour
{
    private ScanForInteractable scanner;
    private StarterAssets.StarterAssetsInputs input;
    public Inventory inventory;
    public Transform cameraObject;

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
                CmdDoInteract(scanner.target.gameObject);                
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
    private void CmdDoInteract(GameObject interactableObj)
    {
        var interactable = interactableObj.GetComponent<Interactable>();
        interactable.DoInteract(this);
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

    public string swapTooltip = "The other player wants to swap! Press Q to swap.";

    [TargetRpc]
    public void RpcMoveTo(NetworkConnection target, Vector3 position, Quaternion rotation, Quaternion lookRotation)
    {
        StartCoroutine(SwapRealities(position, rotation, lookRotation));
        Tooltipper.Instance.HideTip(TooltipType.DesireSwap, swapTooltip);
    }

    [TargetRpc]
    public void RpcNotifySwap(NetworkConnection target)
    {
        Tooltipper.Instance.ShowTip(TooltipType.DesireSwap, swapTooltip);
    }

    private IEnumerator SwapRealities(Vector3 position, Quaternion rotation, Quaternion lookRotation)
    {
        UIFader.Instance.ShowCurtain();
        while (UIFader.Instance.enabled)
            yield return null;

        Debug.Log($"Teleporting to {position}");
        var fps = this.GetComponent<StarterAssets.FirstPersonController>();
        fps.movementFrozen = true;
        var nt = this.GetComponent<NetworkTransform>();        
        nt.CmdTeleportAndRotate(position, rotation);
        cameraObject.localRotation = lookRotation;
        //this.transform.position = position;                
        
        yield return new WaitForSeconds(.25f);
        UIFader.Instance.HideCurtain();
        fps.movementFrozen = false;
    }
}
