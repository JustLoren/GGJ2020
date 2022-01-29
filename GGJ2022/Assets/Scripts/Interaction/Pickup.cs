using UnityEngine;

public class Pickup : MonoBehaviour
{
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
        Debug.Log($"{player.name} picked up {this.name} on frame {Time.frameCount}");
        Destroy(this.gameObject);
    }    
}

