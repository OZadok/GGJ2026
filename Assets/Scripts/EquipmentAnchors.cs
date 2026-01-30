using Unity.VisualScripting;
using UnityEngine;

public class EquipmentAnchors : MonoBehaviour
{
    [SerializeField] Transform headPosition, bodyPosition, legPosition, handPosition;
    public Transform HeadPosition { get => headPosition; }
    public Transform BodyPostion { get => bodyPosition; }
    public Transform LegPostion { get => legPosition; }
    public Transform HandPostition { get => handPosition; }

    public Transform GetItemPosition(ItemType type)
    {
        if(type == ItemType.Head) return headPosition;
        else if (type == ItemType.Body) return bodyPosition;
        else if (type == ItemType.Leg )return legPosition;
        else if (type == ItemType.Hand )return handPosition;
        else throw new System.Exception($"how did we get here? item type position not found for type {type}");
    }
}