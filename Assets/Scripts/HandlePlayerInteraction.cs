using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandlePlayerInteraction : MonoBehaviour
{
    private EntityScript _entityScript;
    private System.Action _interaction;
    private PlayerScript _playerScript;

    private void Awake()
    {
        _entityScript = GetComponent<EntityScript>();
        _playerScript = GetComponent<PlayerScript>();
    }

    private void OnEnable()
    {
        Dispenser.OnItemCollected += DispenserOnOnItemCollected;
        Zone.OnJoinedToZone += ZoneOnOnJoinedToZone;
    }

    private void OnDisable()
    {
        Dispenser.OnItemCollected -= DispenserOnOnItemCollected;
        Zone.OnJoinedToZone -= ZoneOnOnJoinedToZone;
    }

    private void DispenserOnOnItemCollected(ItemData item)
    {
        if (_entityScript.items.ContainsKey(item.itemType))
        {
            Destroy(_entityScript.items[item.itemType].gameObject);
            _entityScript.items.Remove(item.itemType);
        }
        else
        {
            _entityScript.SetItem(item);
        }
    }
    
    private void ZoneOnOnJoinedToZone(Zone zone)
    {
        _playerScript.JoinZone(zone);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        interactable.ToggleInteractButton(true);
        _interaction = interactable.Interact;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<IInteractable>().ToggleInteractButton(false);
        Messenger.Default.Publish(new PlayerZoneChangeEvent(null));
        _interaction = null;
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.ReadValueAsButton()) return;

        _interaction?.Invoke();
        _interaction = null;
    }
}
