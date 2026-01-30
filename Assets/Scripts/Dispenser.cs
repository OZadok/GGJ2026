using System;
using UnityEngine;

public class Dispenser : MonoBehaviour, IInteractable
{
    private Item item;
    [SerializeField] private GameObject interactButtonView;

    public static event Action<Item> OnItemCollected;

    public void Init(DispenserData dispenserData)
    {
        item = dispenserData.item;
    }

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