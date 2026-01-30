using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Dispenser : MonoBehaviour, IInteractable
{ 
    [SerializeField] private Item item;
    [SerializeField] private GameObject interactButtonView;

    public static event Action<Item> OnItemCollected;

    private void Start()
    {
        ToggleInteractButton(false);
    }

    public void Interact()
    {
        OnItemCollected?.Invoke(item);
        ToggleInteractButton(false);
    }

    public void ToggleInteractButton(bool toShow)
    {
        interactButtonView.SetActive(toShow);
    }
}
