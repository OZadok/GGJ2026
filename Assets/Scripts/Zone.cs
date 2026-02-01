using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class Zone : MonoBehaviour, IInteractable
{
    [SerializeField] List<SpawnPoint> spawnPoints;

    [SerializeField] private GameObject interactButtonView;

    public static event Action<Zone> OnJoinedToZone;

    private List<NpcScript> _npcs = new List<NpcScript>();

    HashSet<int> usedSpawnPoints = new HashSet<int>();
    public Action<bool> OnAlertChanged;

    public ZoneData ZoneData { get; private set; }

    //List<NPCScript> npcs;
    public void Init(ZoneData zoneData, Vector2 positon)
    {
        this.ZoneData = zoneData;
        if (spawnPoints.Count < zoneData.npcGroups.Count)
        {
            Debug.LogError($"not enough spawnpoints for groups for zone data {zoneData.name}");
            return;
        }

        transform.position = positon;

        usedSpawnPoints.Clear();
        foreach (Group group in zoneData.npcGroups)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Count); // need to add something to keep track of used sp's
            while (usedSpawnPoints.Contains(spawnPointIndex))
            {
                // Debug.Log($"retrying on used spawnpoint at index {spawnPointIndex}");
                spawnPointIndex = Random.Range(0, spawnPoints.Count);
            }

            usedSpawnPoints.Add(spawnPointIndex);
            var point = spawnPoints[spawnPointIndex];
            var npcScript = EntityManager.Instance.SpawnNpc(point, group);
            _npcs.Add(npcScript);
        }

        SortingLayerUtil.SetSortingLayer(GetComponent<SpriteRenderer>());
    }

    private void Start()
    {
        ToggleInteractButton(false);
    }

    public void AlertNpcs(bool isBlending)
    {
        OnAlertChanged?.Invoke(isBlending);
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
            if (item.spawnPoint == null) continue;
            Gizmos.DrawWireSphere(item.spawnPoint.position, 0.5f);
        }
    }
}

[System.Serializable]
public struct SpawnPoint
{
    public Transform spawnPoint;
}
