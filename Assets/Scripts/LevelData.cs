using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public Sprite bgImage;
    public AudioClip backgroundMusic;
    public Zone zonePrefab;
    public List<ZoneData> zoneData;
    public List<DispenserData> dispenserData;
    

}