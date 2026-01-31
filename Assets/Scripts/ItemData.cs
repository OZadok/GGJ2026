using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 0)]
public class ItemData : ScriptableObject
{
    public GameObject baseItemPrefab;
    public List<Sprite> itemSprites;
    public Vector3 spawnPointScale = Vector3.one;
    public Vector3 spawnPointOffset = Vector3.zero;
    public ItemType itemType;
}
public enum ItemType
{
    Head, Body, Leg, Hand,
}