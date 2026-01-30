using UnityEngine;

public class LevelVisualizer : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    [SerializeField] bool enable;

    void OnDrawGizmos()
    {
        if (!enable) return;
        Gizmos.color = Color.green;
        foreach (var zone in levelData.zoneData)
        {
            Gizmos.DrawWireSphere(zone.zonePostion, 0.5f);
        }
        Gizmos.color = Color.red;
        foreach (var dispenser in levelData.dispenserData)
        {
            Gizmos.DrawWireSphere(dispenser.position, 0.5f);
        }
    }
}
