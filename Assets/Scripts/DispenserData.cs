using UnityEngine;

[CreateAssetMenu(fileName = "DispenserData", menuName = "Scriptable Objects/DispenserData")]
public class DispenserData : ScriptableObject
{
    public Vector2 position;
    public GameObject prefab;
    public Item item;
}
