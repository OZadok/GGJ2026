using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public float levelTime;
    public Sprite bgImage;
    public AudioClip backgroundMusic;
    public List<ZonePlacement> zones;
    public List<DispenserData> dispenserData;
}

[System.Serializable]
public struct ZonePlacement
{
    public ZoneData zoneData;
    public Vector2 position;
    public GameObject zonePrefab;
}