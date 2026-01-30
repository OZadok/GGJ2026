using System;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    private EquipmentAnchors _equipmentAnchors;

    private void Awake()
    {
        _equipmentAnchors = GetComponent<EquipmentAnchors>();
    }

    public void SetItem(Item item)
    {
        //set item sprite at the position.
        Transform position = _equipmentAnchors.GetItemPosition(item.itemType);
        var go = Instantiate(item.itemPrefab ,position,false);
    }
}
