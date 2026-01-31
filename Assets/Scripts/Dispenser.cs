using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class Dispenser : MonoBehaviour, IInteractable
{
    private ItemData item;
    [SerializeField] Transform itemSpawnPoint;
    [SerializeField] private GameObject interactButtonView;

    public static event Action<ItemData> OnItemCollected;

    public void Init(DispenserData dispenserData)
    {
        item = dispenserData.item;
        var go = Instantiate(item.baseItemPrefab, itemSpawnPoint);
        go.GetComponent<SpriteRenderer>().sprite = item.itemSprites[Random.Range(0,item.itemSprites.Count)];
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