using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor
{
    private const float HandleLabelOffset = 0.35f;
    private const float HandleScreenSize = 0.6f;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var levelData = (LevelData)target;
        var itemNames = CollectItemNames(levelData);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Items Used In Level", EditorStyles.boldLabel);

        if (itemNames.Count == 0)
        {
            EditorGUILayout.HelpBox("No items referenced by zone placements.", MessageType.Info);
            return;
        }

        foreach (var name in itemNames.OrderBy(n => n))
        {
            EditorGUILayout.LabelField($"- {name}");
        }
    }

    private void OnSceneGUI()
    {
        var levelData = (LevelData)target;
        if (levelData == null) return;

        DrawZoneHandles(levelData);
        DrawDispenserHandles(levelData);
    }

    private static HashSet<string> CollectItemNames(LevelData levelData)
    {
        var itemNames = new HashSet<string>();
        if (levelData == null || levelData.zones == null) return itemNames;

        foreach (var placement in levelData.zones)
        {
            if (placement.zoneData == null || placement.zoneData.npcGroups == null) continue;

            foreach (var group in placement.zoneData.npcGroups)
            {
                if (group == null || group.items == null) continue;

                foreach (var item in group.items)
                {
                    if (item == null) continue;
                    itemNames.Add(item.name);
                }
            }
        }

        return itemNames;
    }

    private void DrawZoneHandles(LevelData levelData)
    {
        if (levelData.zones == null) return;

        for (int i = 0; i < levelData.zones.Count; i++)
        {
            var placement = levelData.zones[i];
            var worldPos = new Vector3(placement.position.x, placement.position.y, 0f);

            Handles.color = Color.green;
            var size = HandleUtility.GetHandleSize(worldPos) * HandleScreenSize;
            var fmh_80_17_639054799958320922 = Quaternion.identity; var newPos = Handles.FreeMoveHandle(
                worldPos,
                size,
                Vector3.zero,
                Handles.SphereHandleCap);

            if (newPos != worldPos)
            {
                Undo.RecordObject(levelData, "Move Zone");
                placement.position = new Vector2(newPos.x, newPos.y);
                levelData.zones[i] = placement;
                EditorUtility.SetDirty(levelData);
            }

            Handles.Label(newPos + Vector3.up * HandleLabelOffset, $"Zone {i}", EditorStyles.boldLabel);
        }
    }

    private void DrawDispenserHandles(LevelData levelData)
    {
        if (levelData.dispenserData == null) return;

        for (int i = 0; i < levelData.dispenserData.Count; i++)
        {
            var dispenser = levelData.dispenserData[i];
            if (dispenser == null) continue;

            var worldPos = new Vector3(dispenser.position.x, dispenser.position.y, 0f);

            Handles.color = Color.red;
            var size = HandleUtility.GetHandleSize(worldPos) * HandleScreenSize;
            var fmh_112_17_639054799958348444 = Quaternion.identity; var newPos = Handles.FreeMoveHandle(
                worldPos,
                size,
                Vector3.zero,
                Handles.SphereHandleCap);

            if (newPos != worldPos)
            {
                Undo.RecordObject(dispenser, "Move Dispenser");
                dispenser.position = new Vector2(newPos.x, newPos.y);
                EditorUtility.SetDirty(dispenser);
            }

            Handles.Label(newPos + Vector3.up * HandleLabelOffset, $"Dispenser {i}", EditorStyles.boldLabel);
        }
    }
}
