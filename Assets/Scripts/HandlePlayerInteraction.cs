using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandlePlayerInteraction : MonoBehaviour
{
    private EntityScript _entityScript;

    private System.Action _interaction;

    private void Awake()
    {
        _entityScript = GetComponent<EntityScript>();
    }

    private void OnEnable()
    {
        Dispenser.OnItemCollected += DispenserOnOnItemCollected;
    }

    private void OnDisable()
    {
        Dispenser.OnItemCollected -= DispenserOnOnItemCollected;
    }

    private void DispenserOnOnItemCollected(Item item)
    {
        _entityScript.SetItem(item);
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
        _interaction = null;
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.ReadValueAsButton()) return;

        _interaction?.Invoke();
        _interaction = null;
    }
}
