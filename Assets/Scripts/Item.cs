using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 0)]
public class Item : ScriptableObject
{
    public GameObject itemPrefab;
    public ItemType itemType;

}
public enum ItemType
{
    Head, Body, Leg, Hand,
}