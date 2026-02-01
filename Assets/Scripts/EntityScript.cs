using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    private static readonly int Action1 = Animator.StringToHash("Action1");
    private static readonly int Action2 = Animator.StringToHash("Action2");
    private static readonly int Action3 = Animator.StringToHash("Action3");
    private static readonly int Velocity = Animator.StringToHash("velocity");
    private static readonly int GrabTrigger = Animator.StringToHash("grabTrigger");
    
    private EquipmentAnchors _equipmentAnchors;
    public Dictionary<ItemType, GameObject> items =  new Dictionary<ItemType, GameObject>();
    
    private Animator _animator;

    private void Awake()
    {
        _equipmentAnchors = GetComponent<EquipmentAnchors>();
        _animator = GetComponent<Animator>();
    }

    public void SetItem(ItemData item)
    {
        //set item sprite at the position.
        Transform position = _equipmentAnchors.GetItemPosition(item.itemType);
        var go = Instantiate(item.baseItemPrefab ,position,false);
        go.GetComponent<SpriteRenderer>().sprite = item.itemSprites[Random.Range(0,item.itemSprites.Count)];
        items[item.itemType] = go;
    }

    public int GetItemsCount()
    {
        return items.Count;
    }
    
    public void DoAction1()
    {
        _animator.SetTrigger(Action1);
    }

    public void DoAction2()
    {
        _animator.SetTrigger(Action2);
    }
    
    public void DoAction3()
    {
        _animator.SetTrigger(Action3);
    }

    public void SetWalking(float velocity)
    {
        _animator.SetFloat(Velocity, velocity);
    }  
    public void TriggerGrab()
    {
        _animator.SetTrigger(GrabTrigger);
    }
}
