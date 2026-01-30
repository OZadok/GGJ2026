using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZoneData", menuName = "ZoneData", order = 0)]
public class ZoneData : ScriptableObject
{
    public Vector2 zonePostion;
    public List<Group> npcGroups; 
    
}
