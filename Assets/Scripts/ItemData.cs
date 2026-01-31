using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 0)]
public class ItemData : ScriptableObject
{
    public GameObject baseItemPrefab;
    public List<Sprite> itemSprites;
    public ItemType itemType;
}
public enum ItemType
{
    Head, Body, Leg, Hand,
}