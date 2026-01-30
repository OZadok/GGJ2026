using System;
using System.Collections;
using UnityEngine;

public class NpcScript : MonoBehaviour
{
    private Group _group;

    public void Init(SpawnPoint spawnPoint, Group group)
    {
        _group = group;
        StartCoroutine(DoActions());
    }

    private IEnumerator DoActions()
    {
        while (true)
        {
            for (int i = 0; i < _group.actions.Count; i++)
            {
                var waitTime = UnityEngine.Random.Range(_group.actions[i].minTime, _group.actions[i].maxTime);
                yield return new WaitForSeconds(waitTime);
                //do action from type - _group.actions[i].type;
            }
            yield return null;
        }
        // ReSharper disable once IteratorNeverReturns
    }

    public void OnSuspicious(bool isSuspicious)
    {
        Debug.Log($"isSuspicious: {isSuspicious}");
    }
}
