using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    private EquipmentAnchors _equipmentAnchors;
    public Dictionary<ItemType, Item> items =  new Dictionary<ItemType, Item>();

    private void Awake()
    {
        _equipmentAnchors = GetComponent<EquipmentAnchors>();
    }

    public void SetItem(Item item)
    {
        //set item sprite at the position.
        Transform position = _equipmentAnchors.GetItemPosition(item.itemType);
        var go = Instantiate(item.itemPrefab ,position,false);
        items[item.itemType] = item;
    }

    public int GetItemsCount()
    {
        return items.Count;
    }
}
