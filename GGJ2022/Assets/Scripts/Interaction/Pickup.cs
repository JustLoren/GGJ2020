using UnityEngine;

public class Pickup : MonoBehaviour
{
    public string key;
    private Interactable interactable;
    private void Start()
    {
        interactable = GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.Interact.AddListener(DoPickup);
        }
    }

    private void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.Interact.RemoveListener(DoPickup);
        }
    }

    public void DoPickup(uPlayer player)
    {
        if (!player.inventory.CanPickup())
            return; //TODO: Warn the player that they cannot pick it up

        player.inventory.GrabItem(this.key);

        Debug.Log($"{player.name} picked up {this.name} on frame {Time.frameCount}");
        
        Destroy(this.gameObject);
    }    
}

