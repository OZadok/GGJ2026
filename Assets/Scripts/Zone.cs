using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class Zone : MonoBehaviour, IInteractable
{
    [SerializeField] List<SpawnPoint> spawnPoints;

    [SerializeField] private GameObject interactButtonView;

    public static event Action<Zone> OnJoinedToZone;

    private List<NpcScript> _npcs = new List<NpcScript>();

    //List<NPCScript> npcs;
    public void Init(ZoneData zoneData)
    {
        if (spawnPoints.Count < zoneData.npcGroups.Count)
        {
            Debug.LogError($"not enough spawnpoints for groups for zone data {zoneData.name}");
            return;
        }
        int i = 0;
        transform.position = zoneData.zonePostion;
        foreach (Group group in zoneData.npcGroups)
        {
            // int spawnPointIndex = Random.Range(0, spawnPoints.Count); // need to add something to keep track of used sp's
            var point = spawnPoints[i];
            var npcScript = EntityManager.Instance.SpawnNpc(point, group);
            _npcs.Add(npcScript);
            i++;
        }
    }

    private void Start()
    {
        ToggleInteractButton(false);
    }

    public void AlertNpcs(bool isBlending)
    {
        foreach (var npc in _npcs)
        {
            npc.OnSuspicious(!isBlending);
        }
    }

    public void Interact()
    {
        OnJoinedToZone?.Invoke(this);
        ToggleInteractButton(false);
    }

    public void ToggleInteractButton(bool toShow)
    {
        interactButtonView.SetActive(toShow);
    }

    void OnDrawGizmos()
    {
        foreach (var item in spawnPoints)
        {
            Gizmos.DrawWireSphere(item.spawnPoint.position, 0.5f);
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