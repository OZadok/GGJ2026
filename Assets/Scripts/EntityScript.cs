using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    private static readonly int Action1 = Animator.StringToHash("Action1");
    private static readonly int Action2 = Animator.StringToHash("Action2");
    private static readonly int Action3 = Animator.StringToHash("Action3");
    private EquipmentAnchors _equipmentAnchors;
    public Dictionary<ItemType, GameObject> items =  new Dictionary<ItemType, GameObject>();
    
    private Animator _animator;

    private void Awake()
    {
        _equipmentAnchors = GetComponent<EquipmentAnchors>();
        _animator = GetComponent<Animator>();
    }

    public void SetItem(Item item)
    {
        //set item sprite at the position.
        Transform position = _equipmentAnchors.GetItemPosition(item.itemType);
        var go = Instantiate(item.itemPrefab ,position,false);
        items[item.itemType] = go;
    }

    public int GetItemsCount()
    {
        return items.Count;
    }
    
    public void DoAction1()
    {
        Debug.Log($"{name} did action 1");
        _animator.SetTrigger(Action1);
    }

    public void DoAction2()
    {
        Debug.Log($"{name} did action 2");
        _animator.SetTrigger(Action2);
    }
    
    public void DoAction3()
    {
        _animator.SetTrigger(Action3);
    }
}
