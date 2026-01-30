using System.Collections.Generic;
using Core;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] List<SpawnPoint> spawnPoints;

    //List<NPCScript> npcs;
    public void Init(ZoneData zoneData)
    {
        if (spawnPoints.Count > zoneData.npcGroups.Count)
        {
            Debug.LogError($"not enough spawnpoints for groups for zone data {zoneData.name}");
            return;
        }
        int i = 0;
        transform.position = zoneData.zonePostion;
        foreach (Group group in zoneData.npcGroups)
        {
            // int spawnPointIndex = Random.Range(0, spawnPoints.Count);
            var point = spawnPoints[i];
            EntityManager.Instance.SpawnNpc(point, group);
            i++;
        }
    }
}
[System.Serializable]
public struct SpawnPoint
{
    public Transform spawnPoint;
    public FacingDirction dirction;

}
public enum FacingDirction
{
    Front, Back, Left, Right
}